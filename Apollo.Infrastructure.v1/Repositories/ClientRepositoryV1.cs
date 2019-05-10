// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/16/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Client;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class ClientRepositoryV1 : BaseRepository<CarriersDto, IClient, IClientInfo>, IClientRepository
    {
        private readonly IAuditConfiguration _auditConfiguration;

        public ClientRepositoryV1(IConnectionFactory connectionFactory, ILogManager logManager, IAuditConfiguration auditConfiguration)
            : base(connectionFactory, logManager, new CarriersDto())
        {
            _auditConfiguration = auditConfiguration;
        }
        public Task<GetResponse<IReadOnlyList<ClientConfiguration>>> GetConfigurationsAsync()
        {
            throw new System.NotImplementedException();
        }
        public override async Task<GetResponse<IClient>> GetByIdAsync(int id)
        {
            var response = new GetResponse<IClient>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()}
								 WHERE CarrierId = @id;
                                 {GetSettingsSelectStatement()}
                                 WHERE s.CarrierId = @id;";

                    var result = await connection.QueryMultipleAsync(sql, new { Id = id });
                    var client = result.Read<CarriersDto>()
                        .Select(dto => dto.ToModel())
                        .FirstOrDefault();

                    var clientSettings = result.Read<CarrierSettingsDto>()
                        .Select(dto => dto.ToModel(_auditConfiguration))
                        .FirstOrDefault();

                    if (client != null)
                        client.Settings = clientSettings;

                    response.Content = client;
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve the audit with Id:{id}.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }
        public override async Task<GetResponse<IReadOnlyList<IClient>>> GetAllAsync()
        {
            var response = new GetResponse<IReadOnlyList<IClient>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()};
                                 {GetSettingsSelectStatement()}";

                    var results = await connection.QueryMultipleAsync(sql);

                    var clients = results.Read<CarriersDto>()
                        .Select(dto => dto.ToModel())
                        .AsList();

                    var clientSettings = results.Read<CarrierSettingsDto>()
                        .GroupBy(g => g.CarrierID)
                        .ToDictionary(x => x.Key, x => x.First().ToModel(_auditConfiguration));

                    foreach (var client in clients)
                    {
                        if (clientSettings.ContainsKey(client.Id))
                            client.Settings = clientSettings[client.Id];
                    }
                    response.Content = clients;

                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve Client records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }
        public async Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync(ClientSettingsEnum setting, object value)
        {
            var response = new GetResponse<IReadOnlyList<IClientInfo>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectInfoStatement()}
                                    AND SettingType = @settingType
                                    AND SettingValue = @settingValue";

                    var results = await connection.QueryAsync<CarriersDto>(sql, new {SettingType = (int)setting, SettingValue = value.ToString()});

                    response.Content = results
                        
                        .Select(dto => dto.ToInfo())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve client records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [dbo].[Carriers] SET IsDeleted = 1
                WHERE [CarrierID] = @carrierid";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[Carriers] AS T
                USING (VALUES 
                        ( @carrierid
                        , @carriername
                        , @carriercontact
                        , @carrierphone
                        , @carrierfax
                        , @carrieraddress
                        , @carrieraddress2
                        , @carriercity
                        , @carrierstate
                        , @carrierzip
                        , @carrieremail
                        , @zoomauditsid
                        , @zoomauditspass
                        , @isenhancedcarrier
                        , @userates
                        , @chatlink
                        , @carrierurl
                        )
                       ) AS S
                       (
                          [CarrierID]
                        , [CarrierName]
                        , [CarrierContact]
                        , [CarrierPhone]
                        , [CarrierFax]
                        , [CarrierAddress]
                        , [CarrierAddress2]
                        , [CarrierCity]
                        , [CarrierState]
                        , [CarrierZip]
                        , [CarrierEmail]
                        , [ZoomAuditsID]
                        , [ZoomauditsPass]
                        , [IsEnhancedCarrier]
                        , [UseRates]
                        , [ChatLink]
                        , [CarrierURL]
                       ) ON T.[CarrierID] = S.[CarrierID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [CarrierName]
                        , [CarrierContact]
                        , [CarrierPhone]
                        , [CarrierFax]
                        , [CarrierAddress]
                        , [CarrierAddress2]
                        , [CarrierCity]
                        , [CarrierState]
                        , [CarrierZip]
                        , [CarrierEmail]
                        , [ZoomAuditsID]
                        , [ZoomauditsPass]
                        , [IsEnhancedCarrier]
                        , [UseRates]
                        , [ChatLink]
                        , [CarrierURL]
                           )
                    VALUES (
                          S.[CarrierName]
                        , S.[CarrierContact]
                        , S.[CarrierPhone]
                        , S.[CarrierFax]
                        , S.[CarrierAddress]
                        , S.[CarrierAddress2]
                        , S.[CarrierCity]
                        , S.[CarrierState]
                        , S.[CarrierZip]
                        , S.[CarrierEmail]
                        , S.[ZoomAuditsID]
                        , S.[ZoomauditsPass]
                        , S.[IsEnhancedCarrier]
                        , S.[UseRates]
                        , S.[ChatLink]
                        , S.[CarrierURL]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[CarrierName] = S.[CarrierName], T.[CarrierContact] = S.[CarrierContact], T.[CarrierPhone] = S.[CarrierPhone], T.[CarrierFax] = S.[CarrierFax], T.[CarrierAddress] = S.[CarrierAddress], 
                        T.[CarrierAddress2] = S.[CarrierAddress2], T.[CarrierCity] = S.[CarrierCity], T.[CarrierState] = S.[CarrierState], T.[CarrierZip] = S.[CarrierZip], T.[CarrierEmail] = S.[CarrierEmail], 
                        T.[ZoomAuditsID] = S.[ZoomAuditsID], T.[ZoomauditsPass] = S.[ZoomauditsPass], T.[IsEnhancedCarrier] = S.[IsEnhancedCarrier], T.[UseRates] = S.[UseRates], 
                        T.[ChatLink] = S.[ChatLink], T.[CarrierURL] = S.[CarrierURL] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectInfoStatement()
        {
            return @"
                    SELECT 
                          c.[CarrierID]
                        , c.[CarrierName]
                    FROM [dbo].[Carriers] c LEFT JOIN [v2].[ClientSetting] s ON c.[CarrierID] = s.ClientId AND s.IsDeleted = 0
                    WHERE c.IsDeleted = 0
                    ";
        }

        protected override string GetSummarySelectStatement()
        {
            return GetSelectStatement();
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [CarrierID]
                        , [CarrierName]
                        , [CarrierContact]
                        , [CarrierPhone]
                        , [CarrierFax]
                        , [CarrierAddress]
                        , [CarrierAddress2]
                        , [CarrierCity]
                        , [CarrierState]
                        , [CarrierZip]
                        , [CarrierEmail]
                        , [ZoomAuditsID]
                        , [ZoomauditsPass]
                        , [IsEnhancedCarrier]
                        , [UseRates]
                        , [ChatLink]
                        , [CarrierURL]
                    FROM [dbo].[Carriers]
                    ";
        }

        private string GetSettingsSelectStatement()
        {
            return @"
                    SELECT 
                          [CarrierSettingsID]
                        , s.[CarrierID]
                        , [BillingContact]
                        , [BillingContactEmail]
                        , [WPDEmail]
                        , [WPDOptions]
                        , [Format]
                        , [Frequency]
                        , [MonthlyDueDates]
                        , [QuarterlyDueDates]
                        , [SemiAnnualFullTerm]
                        , [SemiAnnualShortTerm]
                        , [SemiAnnualCancellation]
                        , [AnnualFullTerm]
                        , [AnnualShortTerm]
                        , [AnnualCancellation]
                        , [AuditDueDate]
                        , [WLFormID]
                        , [WLDays]
                        , [LocationWarning]
                        , [AuditType]
                        , [CCAgent]
                        , [RequestAllSubs]
                        , [ProcessClaims]
                        , [SubContractorLabel]
                        , [UseCasualLabor]
                        , [UseLocationEmployeeCount]
                        , [SubContractorHeaderContent]
                        , [AuditTypes]
                        , [COIRequired]
                        , [ByLocationAllowed]
                        , [ByLocationDefault]
                        , [ShowClassAllocationSummary]
                        , [WpdEmailSubjectFormat]
                        , [WpdFilenameFormat]
                        , [UseShareAuditWelcomeLetter]
                        , [Logo]
                        , [LogoFileName] = CASE WHEN i.CarrierImageId IS NULL THEN '0thumb.png' ELSE CONVERT(varchar(3),i.CarrierImageId) + 'thumb.' + i.Extension END
                    FROM [dbo].[CarrierSettings] s LEFT JOIN [dbo].[CarrierImages] i ON s.CarrierId = i.CarrierId AND i.IsDeleted = 0
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



