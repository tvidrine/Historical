// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class QcApplicationService : IQcApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IQcRepository _qcDataRepository;
        private readonly IAuditApplicationService _auditApplicationService;

        public QcApplicationService(IAuditApplicationService auditApplicationService, ILogManager logManager, IQcRepository qcDataRepository)
        {
            _logManager = logManager;
            _qcDataRepository = qcDataRepository;
            _auditApplicationService = auditApplicationService;
        }

        public async Task<DeleteResponse> DeleteQcDataByAuditIdAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _qcDataRepository.DeleteQcDataByAuditIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete qcData");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<bool>> ReturnForCorrectionsAsync(int auditId, int userId)
        {
            var response = new GetResponse<bool>();
            // Get the requested audit
            // Set the status to return for corrections and turn off IsQcChecked
            var auditResponse = await _auditApplicationService.GetAsync(auditId);
            response.Join<GetResponse<bool>>(auditResponse);

            if (auditResponse.IsSuccessful)
            {
                var audit = auditResponse.Content;
                audit.AuditStatus = AuditStatuses.ReturnForCorrections;
                audit.CheckedForQualityControl = false;

                // Update the audit record
                var saveResponse = await _auditApplicationService.SaveAsync(audit);
                response.Join<GetResponse<bool>>(saveResponse);

                // Log the change in status
                var updateStatusResponse = await _auditApplicationService.UpdateAuditStatus(audit.Id, AuditStatuses.ReturnForCorrections, userId);
                response.Join<GetResponse<bool>>(updateStatusResponse);

                // Delete the Qc Data for this audit so it does not show up on the Qc Dashboard
                var deleteQcDataResponse = await DeleteQcDataByAuditIdAsync(audit.Id);
                response.Join<GetResponse<bool>>(deleteQcDataResponse);

            }

            return response;
        }
    }
}