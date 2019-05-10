// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class CertificateOfInsuranceRepository : BaseRepository<CertificateOfInsuranceDto, ICertificateOfInsurance, ICertificateOfInsurance>, ICertificateOfInsuranceRepository
    {
        private readonly IAuditUploadRepository _auditUploadRepository;

        public CertificateOfInsuranceRepository(IConnectionFactory connectionFactory, 
            ILogManager logManager,
            IAuditUploadRepository auditUploadRepository)
            : base(connectionFactory, logManager, new CertificateOfInsuranceDto())
        {
            _auditUploadRepository = auditUploadRepository;
        }

        public async Task<GetResponse<ICertificateOfInsurance>> GetForLaborAsync(int laborId)
        {
            var response = new GetResponse<ICertificateOfInsurance>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND LaborId = @laborId";

                    var results = await connection.QueryAsync<CertificateOfInsuranceDto>(sql, new {LaborId = laborId});
                    var dto = results.FirstOrDefault();

                    if (dto != null)
                    {
                        var certificate = dto.ToModel();
                        if (dto?.UploadedFileId != null)
                        {
                            var fileResult = await _auditUploadRepository.GetByIdAsync(dto.UploadedFileId.Value);

                            if (fileResult.IsSuccessful)
                            {
                                certificate.File = fileResult.Content;
                            }
                        }
                        
                        response.Content = certificate;
                    }
                    

                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to locate any merge documents.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[CertificateOfInsurance] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[CertificateOfInsurance] AS T
                USING (VALUES 
                        ( @id
                        , @laborId
                        , @carriername
                        , @policynumber
                        , @policystart
                        , @policyend
                        , @uploadedfileid
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [LaborId]
                        , [CarrierName]
                        , [PolicyNumber]
                        , [PolicyStart]
                        , [PolicyEnd]
                        , [UploadedFileId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[LaborId] = S.[LaborId]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [LaborId]
                        , [CarrierName]
                        , [PolicyNumber]
                        , [PolicyStart]
                        , [PolicyEnd]
                        , [UploadedFileId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[LaborId]
                        , S.[CarrierName]
                        , S.[PolicyNumber]
                        , S.[PolicyStart]
                        , S.[PolicyEnd]
                        , S.[UploadedFileId]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[CarrierName] = S.[CarrierName], T.[PolicyNumber] = S.[PolicyNumber], T.[PolicyStart] = S.[PolicyStart], 
                        T.[PolicyEnd] = S.[PolicyEnd], T.[UploadedFileId] = S.[UploadedFileId], T.[CreatedOn] = S.[CreatedOn], 
                        T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [LaborId]
                        , [CarrierName]
                        , [PolicyNumber]
                        , [PolicyStart]
                        , [PolicyEnd]
                        , [UploadedFileId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[CertificateOfInsurance]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



