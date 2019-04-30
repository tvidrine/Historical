// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Apollo.Core.Base;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.DomainServices.Letters;
using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;

namespace Apollo.Core.Domain.Workflow.Steps
{
    [AuditActionKey("GenerateWelcomeLetter")]
    public class GenerateWelcomeLetterStep : IAuditAction
    {
        private readonly ILogManager _logManager;
        private readonly IDocumentApplicationService _documentApplicationService;
        private readonly IDictionary<AuditTypeEnum, Func<IToken, IToken>> _letterGenerators;
        private readonly IAuditApplicationService _auditApplicationService;
        private readonly IAuditConfiguration _auditConfiguration;
        private readonly IFileUploadApplicationService _auditUploadApplicationService;
        private readonly IWelcomeLetterGl _welcomeLetterGl;
        private readonly IWelcomeLetterWc _welcomeLetterWc;

        public GenerateWelcomeLetterStep(ILogManager logManager, 
            IAuditApplicationService auditApplicationService,
            IDocumentApplicationService documentApplicationService,
            IFileUploadApplicationService auditUploadApplicationService,
            IAuditConfiguration auditConfiguration,
            IWelcomeLetterGl wecomeLetterGl,
            IWelcomeLetterWc welcomeLetterWc)
        {
            _logManager = logManager;
            _auditApplicationService = auditApplicationService;
            _auditUploadApplicationService = auditUploadApplicationService;
            _documentApplicationService = documentApplicationService;
            _auditConfiguration = auditConfiguration;
            _welcomeLetterGl = wecomeLetterGl;
            _welcomeLetterWc = welcomeLetterWc;

            _letterGenerators = new Dictionary<AuditTypeEnum, Func<IToken, IToken>>
            {
                {AuditTypeEnum.NotSet, NoOpGenerator },
                {AuditTypeEnum.Combo, GenerateGlWelcomeletter },
                {AuditTypeEnum.GL, GenerateGlWelcomeletter },
                {AuditTypeEnum.WC, GenerateWcWelcomeLetter }
            };
        }

        public string RuleSetName { get; set; }

        public IToken Execute(IToken token)
        {
            try
            {
                if (token.Audit.AuditStatus == AuditStatuses.Submitted
                    && token.Audit.Policy.EffectiveEnd.AddDays(-token.Client.Settings.WelcomeLetterDays) <= DateTime.Now)
                {
                    // FOR NOW, USLI is the only client with GL welcome letters due to the incompetence of the inept developers, we have to deal with this bullshit for now
                    if (token.Audit.AuditMethod == AuditMethods.ShareAudit && token.Client.Id != 95)
                        return token;

                    token = _letterGenerators[token.Audit.AuditType](token);

                    if (token.IsSuccessful)
                    {
                        UpdateAuditStatus(token);
                        RecordNotification(token);
                        SaveBatchFileStep(token);
                        var fileInfo = SaveFile(token);
                        RecordFileCreation(token, fileInfo);
                        token.SetActivityNote($@"A letter was sent via US mail to the policy address listed above, attention {token.Audit.Policy.InsuredName}, requesting that the policyholder complete the audit by due date listed on letter using our online web portal.");
                    }
                }
                
                
            }
            catch (Exception e)
            {
                _logManager.LogError(e,"GenerateWelcomeLetter.Execute");
                token.SetException(e);
            }

            return token;
        }

        private void RecordFileCreation(IToken token, FileInfo fileInfo)
        {
            var creationRecord = new AuditUpload
            {
                AuditId = token.Audit.Id,
                EntityId = 0,
                Title = "Welcome Letter",
                Date = DateTime.Now,
                Size = $@"{(fileInfo.Length / 1024):N0} kb",
                AttachedBy = 7,
                DocumentType = token.Audit.AuditType == AuditTypeEnum.GL ? DocumentTypes.WelcomeLetterGl : DocumentTypes.WelcomeLetterWc,
                Description = $@"Welcome Letter - {DateTime.Now:d}",
                Directory = "~/WLArchive/",
                FilePath = fileInfo.Name,
                OriginalFileName = fileInfo.Name,
     
            };

            var response = _auditUploadApplicationService.SaveAsync(creationRecord).Result;

            if(!response.IsSuccessful)
                token.SetException(new InvalidOperationException(response.Message));
        }

        #region Private Methods
        
        private IToken GenerateGlWelcomeletter(IToken token)
        {
            try
            {
                // 1. Generate the document
                var request = new DocumentRequest
                {
                    Audit = token.Audit,
                    Client = token.Client,
                    ReportDate = DateTime.Now
                };

                var documentResponse =  _welcomeLetterGl.GenerateDocumentAsync(request).Result;

                // 2 return the results
                if (documentResponse.IsSuccessful)
                {
                    var documentStream = documentResponse.Content;
                    documentStream.Position = 0;
                    token.Document = new MemoryStream();
                    documentStream.CopyTo(token.Document);

                    token.Message = $@"Successfully generated a welcome letter for audit: {token.Audit.Id}";
                }
                else
                {
                    token.SetException(new InvalidOperationException(documentResponse.Message));

                    return token;
                }
               
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "GenerateWelcomeLetter.GenerateGlWelcomeLetter");
                token.SetException(e);
            }

            return token;
        }
        private IToken GenerateWcWelcomeLetter(IToken token)
        {
            try
            {
                // 1. Generate the document
                var request = new DocumentRequest
                {
                    Audit = token.Audit,
                    Client = token.Client,
                    ReportDate = DateTime.Now
                };

                var documentResponse = _welcomeLetterWc.GenerateDocumentAsync(request).Result;

                // 2 return the results
                if (documentResponse.IsSuccessful)
                {
                    var documentStream = documentResponse.Content;
                    documentStream.Position = 0;
                    token.Document = new MemoryStream();
                    documentStream.CopyTo(token.Document);

                    token.Message = $@"Successfully generated a welcome letter for audit: {token.Audit.Id}";
                }
                else
                {
                    token.SetException(new InvalidOperationException(documentResponse.Message));

                    return token;
                }
                
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "GenerateWelcomeLetter.GenerateWcWelcomeLetter");
                token.SetException(e);
            }
            return token;
        }
       
        private IToken NoOpGenerator(IToken token)
        {
            return token;
        }
        private void RecordNotification(IToken token)
        {
            var response = _auditApplicationService.NotifyPolicyHolder(token.Audit.Id, 1, DateTime.Now.Date).Result;
            if (!response.IsSuccessful)

            {
                token.SetException(new InvalidOperationException(response.Message));
            }
        }
        private void SaveBatchFileStep(IToken token)
        {
            var path = $@"{_auditConfiguration.LegacyConfiguration.WelcomeLetterFolder}\Batches\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = $@"WelcomeLetters_{DateTime.Today:yyyyMMdd}.pdf";
            _documentApplicationService.SaveBatch(Path.Combine(path, fileName), token.Document, FileTypes.Pdf);

            var saveDocumentHistoryResponse = _documentApplicationService.SaveDocumentHistoryAsync(
                new DocumentHistory
                {
                    DocumentTypeId = DocumentTypes.WelcomeLetterBatch,
                    Filename = fileName
                }).Result;

            if (!saveDocumentHistoryResponse.IsSuccessful)
                throw new InvalidOperationException(saveDocumentHistoryResponse.Message);
        }
        private FileInfo SaveFile(IToken token)
        {
            var fileName = $@"WelcomeLetter_{token.Audit.Id}.pdf";
            var fileInfo = new FileInfo($@"{_auditConfiguration.LegacyConfiguration.WelcomeLetterFolder}{fileName}");

            using (var fileStream = fileInfo.Create())
            {
                token.Document.Position = 0;
                token.Document.CopyTo(fileStream);
            }

            var saveDocumentHistoryResponse = _documentApplicationService.SaveDocumentHistoryAsync(
                new DocumentHistory
                {
                    AuditId = token.Audit.Id,
                    DocumentTypeId = token.Audit.AuditType == AuditTypeEnum.WC ?  DocumentTypes.WelcomeLetterWc : DocumentTypes.WelcomeLetterGl,
                    Filename = fileName
                }).Result;

            if (!saveDocumentHistoryResponse.IsSuccessful)
                throw new InvalidOperationException(saveDocumentHistoryResponse.Message);

            return fileInfo;
        }
        private void UpdateAuditStatus(IToken token)
        {
            var response = _auditApplicationService.UpdateAuditStatus(token.Audit.Id, AuditStatuses.NotifiedByLetter, 7).Result;

            if (!response.IsSuccessful)
            {
                token.SetException(new InvalidOperationException(response.Message));
            }
        }
        #endregion Private Methods
    }
}