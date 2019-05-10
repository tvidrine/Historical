// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/4/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class AuditEntityRepositoryV1 : BaseRepository<AuditEntityDto, IAuditEntity, IAuditEntity>, IAuditEntityRepository
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IClassCodeRepository _classCodeRepository;
        private readonly IEntityPrincipalRepository _principalRepository;

        public AuditEntityRepositoryV1(IConnectionFactory connectionFactory, 
            ILogManager logManager, 
            ILocationRepository locationRepository,
            IClassCodeRepository classCodeRepository,
            IEntityPrincipalRepository principalRepository)
            : base(connectionFactory, logManager, new AuditEntityDto())
        {
            _locationRepository = locationRepository;
            _classCodeRepository = classCodeRepository;
            _principalRepository = principalRepository;
        }

        public override async Task<GetResponse<IReadOnlyList<IAuditEntity>>> GetAllAsync(int parentId)
        {
            var response = new GetResponse<IReadOnlyList<IAuditEntity>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetSelectByParentIdStatement();

                    var results = await connection.QueryAsync<AuditEntityDto>(sql, new { ParentId = parentId });

                    var entities = results
                        .Select(dto => dto.ToModel())
                        .AsList();

                    
                    foreach (var entity in entities)
                    {
                        // get the locations for each entity
                        var locationResponse = await _locationRepository.GetAllAsync(entity.Id);
                        
                        if (locationResponse.IsSuccessful && locationResponse.Content.Any())
                            entity.Locations = locationResponse
                                .Content
                                .AsList();

                        // get the principals for each entity
                        var principalResponse = await _principalRepository.GetAllAsync(entity.Id);
                        if (principalResponse.IsSuccessful && principalResponse.Content.Any())
                            entity.Principals = principalResponse
                                .Content
                                .AsList();

                    }
                    response.Content = entities;
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve entity records.";
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
                DELETE FROM [dbo].[Entities]
                WHERE [EntityID] = @entityid;";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[Entities] AS T
                USING (VALUES 
                        ( @entityid
                        , @auditid
                        , @entityname
                        , @entityaddress
                        , @entityaddress2
                        , @entitycity
                        , @entitystate
                        , @entityzip
                        , @entityphone
                        , @entitytaxid
                        , @entitytype
                        , @haspayroll
                        , @has941s
                        )
                       ) AS S
                       (
                          [EntityID]
                        , [AuditID]
                        , [EntityName]
                        , [EntityAddress]
                        , [EntityAddress2]
                        , [EntityCity]
                        , [EntityState]
                        , [EntityZip]
                        , [EntityPhone]
                        , [EntityTaxID]
                        , [EntityType]
                        , [HasPayroll]
                        , [Has941s]
                       ) ON T.[EntityID] = S.[EntityID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditID]
                        , [EntityName]
                        , [EntityAddress]
                        , [EntityAddress2]
                        , [EntityCity]
                        , [EntityState]
                        , [EntityZip]
                        , [EntityPhone]
                        , [EntityTaxID]
                        , [EntityType]
                        , [HasPayroll]
                        , [Has941s]
                           )
                    VALUES (
                          S.[AuditID]
                        , S.[EntityName]
                        , S.[EntityAddress]
                        , S.[EntityAddress2]
                        , S.[EntityCity]
                        , S.[EntityState]
                        , S.[EntityZip]
                        , S.[EntityPhone]
                        , S.[EntityTaxID]
                        , S.[EntityType]
                        , S.[HasPayroll]
                        , S.[Has941s]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AuditID] = S.[AuditID], T.[EntityName] = S.[EntityName], T.[EntityAddress] = S.[EntityAddress], T.[EntityAddress2] = S.[EntityAddress2], T.[EntityCity] = S.[EntityCity], 
                        T.[EntityState] = S.[EntityState], T.[EntityZip] = S.[EntityZip], T.[EntityPhone] = S.[EntityPhone], T.[EntityTaxID] = S.[EntityTaxID], T.[EntityType] = S.[EntityType], 
                        T.[HasPayroll] = S.[HasPayroll], T.[Has941s] = S.[Has941s] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectByParentIdStatement()
        {
            return $@"{GetSelectStatement()} AND AuditId = @parentId";
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [EntityID]
                        , [AuditID]
                        , [EntityName]
                        , [EntityAddress]
                        , [EntityAddress2]
                        , [EntityCity]
                        , [EntityState]
                        , [EntityZip]
                        , [EntityPhone]
                        , [EntityTaxID]
                        , [EntityType]
                        , [HasPayroll]
                        , [Has941s]
                        , [ExposureBasis] = 
                            (SELECT Stuff((SELECT N', ' + ClassCodeBasisID 
			                               FROM
				                                (SELECT DISTINCT EntityId, ClassCodeBasisID
				                                 FROM dbo.ClassCodes2Audit
				                                 WHERE EntityId = e.EntityId) C
			                               FOR XML PATH(''),TYPE)
	                                .value('text()[1]','nvarchar(max)'),1,2,N'')
                            )	
                    FROM [dbo].[Entities] e
                    WHERE 1 = 1 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



