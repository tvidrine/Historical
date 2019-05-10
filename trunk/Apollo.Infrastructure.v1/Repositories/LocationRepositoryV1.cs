// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/7/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class LocationRepositoryV1 : BaseRepository<LocationsDto, ILocation, ILocationInfo>, ILocationRepository
    {
        private IPayrollRepository _payrollRepository
            ;

        public LocationRepositoryV1(IConnectionFactory connectionFactory, 
            ILogManager logManager,
            IPayrollRepository payrollRepository)
            : base(connectionFactory, logManager, new LocationsDto())
        {
            _payrollRepository = payrollRepository;
        }

        public override async Task<GetResponse<IReadOnlyList<ILocation>>> GetAllAsync(int entityId)
        {
            var response = new GetResponse<IReadOnlyList<ILocation>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectByParentIdStatement()};
                                 {GetLocationClassCodesSelectStatement(AuditTypeEnum.WC)} WHERE EntityId = @parentId";

                    var results = await connection.QueryMultipleAsync(sql, new { ParentId = entityId });

                    // Get locations
                    var locations = results
                        .Read<LocationsDto>()
                        .Select(dto => dto.ToModel())
                        .AsList();

                    var classCodeDtos = results
                        .Read<ClassCodeDto>()
                        .AsList();

                    foreach (var location in locations)
                    {
                        // Get class codes for the location
                        location.ClassCodes = classCodeDtos
                            .Where(dto => dto.LocationID == location.Id)
                            .Select(dto => dto.ToModel())
                            .AsList();

                        // get the payroll for this location
                        var payrollResponse = await _payrollRepository.GetAllPayrollAsync(location.Id);
                        if (payrollResponse.IsSuccessful && payrollResponse.Content.Any())
                            location.Payroll = payrollResponse
                                .Content
                                .AsList();
                    }

                    response.Content = locations;
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve location records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<ILocationInfo>>> GetInfoListAsync(int entityId)
        {
            var response = new GetResponse<IReadOnlyList<ILocationInfo>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectInfoStatement()} AND EntityID = @entityId";

                    var results = await connection.QueryAsync<LocationsDto>(sql, new { EntityId = entityId });

                    response.Content = results
                        .Select(dto => dto.ToInfo())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve location summary records.";
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
                UPDATE [dbo].[Locations] SET IsDeleted = 1
                WHERE [LocationID] = @locationid";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[Locations] AS T
                USING (VALUES 
                        ( @locationid
                        , @entityid
                        , @locationname
                        , @locationaddress
                        , @locationaddress2
                        , @locationcity
                        , @locationstate
                        , @locationzip
                        , @locationphone
                        , @estexposure
                        , @payroll
                        , @isverified
                        , @employeecount
                        , @displayindex
                        )
                       ) AS S
                       (
                          [LocationID]
                        , [EntityID]
                        , [LocationName]
                        , [LocationAddress]
                        , [LocationAddress2]
                        , [LocationCity]
                        , [LocationState]
                        , [LocationZip]
                        , [LocationPhone]
                        , [EstExposure]
                        , [PayRoll]
                        , [IsVerified]
                        , [EmployeeCount]
                        , [DisplayIndex]
                       ) ON T.[LocationID] = S.[LocationID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [EntityID]
                        , [LocationName]
                        , [LocationAddress]
                        , [LocationAddress2]
                        , [LocationCity]
                        , [LocationState]
                        , [LocationZip]
                        , [LocationPhone]
                        , [EstExposure]
                        , [PayRoll]
                        , [IsVerified]
                        , [EmployeeCount]
                        , [DisplayIndex]
                           )
                    VALUES (
                          S.[EntityID]
                        , S.[LocationName]
                        , S.[LocationAddress]
                        , S.[LocationAddress2]
                        , S.[LocationCity]
                        , S.[LocationState]
                        , S.[LocationZip]
                        , S.[LocationPhone]
                        , S.[EstExposure]
                        , S.[PayRoll]
                        , S.[IsVerified]
                        , S.[EmployeeCount]
                        , S.[DisplayIndex]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[EntityID] = S.[EntityID], T.[LocationName] = S.[LocationName], T.[LocationAddress] = S.[LocationAddress], T.[LocationAddress2] = S.[LocationAddress2], T.[LocationCity] = S.[LocationCity], 
                        T.[LocationState] = S.[LocationState], T.[LocationZip] = S.[LocationZip], T.[LocationPhone] = S.[LocationPhone], T.[EstExposure] = S.[EstExposure], T.[PayRoll] = S.[PayRoll], 
                        T.[IsVerified] = S.[IsVerified], T.[EmployeeCount] = S.[EmployeeCount], T.[DisplayIndex] = S.[DisplayIndex] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        private string GetLocationClassCodesSelectStatement(AuditTypeEnum auditType)
        {
            return $@"
                    SELECT 
	                    LocationID,
	                    ClassCodeState,
	                    ClassCode,
	                    ClassCodeDesc,
	                    ClassCodeId = CASE WHEN (ClassCodeLookUpId IS NULL OR ClassCodeLookupId = 0)
                            THEN dbo.fnGetClassCodeId({(int)auditType}, ClassCodeState, ClassCode, ClassCodeDesc) 
                            ELSE ClassCodeLookupId END
                    FROM dbo.ClassCodes2Audit
                    ";
        }
        protected override string GetSelectByParentIdStatement()
        {
            return $@"{GetSelectStatement()} AND EntityId = @parentId";
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [LocationID]
                        , [EntityID]
                        , [LocationName]
                        , [LocationAddress]
                        , [LocationAddress2]
                        , [LocationCity]
                        , [LocationState]
                        , [LocationZip]
                        , [LocationPhone]
                        , [EstExposure]
                        , [PayRoll]
                        , [IsVerified]
                        , [EmployeeCount]
                        , [DisplayIndex]
                    FROM [dbo].[Locations]
                    WHERE IsDeleted = 0 
                    ";
        }

        protected override string GetSelectInfoStatement()
        {
            return @"
                    SELECT 
                          [LocationID]
                        , [LocationName]
                    FROM [dbo].[Locations]
                    WHERE IsDeleted = 0";
        }

        #endregion Select Statement
        #endregion Sql Statements


    }
}



