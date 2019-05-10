// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Sales;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class SalesRepository : BaseRepository<SalesDto, ISales, ISales>, ISalesRepository
    {
        private readonly IAuditUploadRepository _auditUploadRepository;
        public SalesRepository(IConnectionFactory connectionFactory, 
            ILogManager logManager,
            IAuditUploadRepository auditUploadRepository)
            : base(connectionFactory, logManager, new SalesDto())
        {
            _auditUploadRepository = auditUploadRepository;
        }

        public async Task<DeleteResponse> DeleteAllAsync(int auditId, int entityId, int locationId)
        {
            var response = new DeleteResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetDeleteAllStatement();
                    var trans = connection.BeginTransaction();
                    var results = await connection.ExecuteAsync(sql, new { AuditId = auditId, EntityId = entityId, LocationId = locationId }, trans);

                    trans.Commit();
                    response.Message = $@"{results} {typeof(ISales).Name} records were deleted.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error deleting sales records";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<ISales>>> GetAllSalesAsync(int auditId, int entityId, SalesPeriodType periodType)
        {
            var response = new GetResponse<IReadOnlyList<ISales>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"spsAllSales";

                    var results = await connection.QueryAsync<SalesDto>(sql, new { AuditId = auditId, EntityId = entityId }, commandType:CommandType.StoredProcedure);

                    response.Content = results
                        .Where(dto =>  dto.PeriodType == (int)periodType)
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve sales records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }
        
        public async Task<GetResponse<IReadOnlyList<ISalesVerification>>> GetAllSalesVerificationAsync(int auditId, int entityId, string state, int locationId, SalesPeriodType periodType)
        {
            var response = new GetResponse<IReadOnlyList<ISalesVerification>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = "spsAllSalesVerification";

                    var results = await connection
                        .QueryAsync<SalesVerificationDto>(sql, new { AuditId = auditId, EntityId = entityId, LocationId = locationId }, commandType: CommandType.StoredProcedure);

                    var filteredResults = results
                        .Where(dto => dto.State == state && dto.PeriodType == (int) periodType)
                        .AsList();

                    var items = filteredResults 
                        .Select(dto => dto.ToModel())
                        .AsList();

                    foreach (var dto in filteredResults)
                    {
                        if (dto.VerificationFileId.HasValue)
                        {
                            var fileResult = await _auditUploadRepository.GetByIdAsync(dto.VerificationFileId.Value);
                            if (fileResult.IsSuccessful)
                                items.First(i => i.PeriodStart == dto.PeriodStart)
                                    .VerificationFile = fileResult.Content;
                        }
                    }

                    response.Content = items;
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve sales records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<ISalesVariance>>> GetSalesVarianceAsync(int auditId, int entityId, SalesPeriodType periodType)
        {
            var response = new GetResponse<IReadOnlyList<ISalesVariance>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = "spsSalesNeedsVerfication";

                    var results = await connection
                        .QueryAsync<SalesVarianceDto>(sql, new { AuditId = auditId, EntityId = entityId }, commandType: CommandType.StoredProcedure);

                    response.Content = results
                        .Where(dto => dto.PeriodType == (int)periodType)
                        .Select(dto => dto.ToModel())
                        .AsList();

                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve sales records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<SaveResponse<IReadOnlyList<ISales>>> SaveAllSalesAsync(IReadOnlyList<ISales> items)
        {
            var response = new SaveResponse<IReadOnlyList<ISales>>();

            try
            {
                if (items.Any())
                {
                    using (var connection = ConnectionFactory.GetConnection())
                    {
                        var sql = "spmSales";
                        var results = await connection.ExecuteAsync(sql, items.Select(m => Dto.FromModel(m)), commandType:CommandType.StoredProcedure);

                        response.Content = items;
                        response.Message = $@"Sales records were inserted/updated.";
                    }
                }
                
            }
            catch (Exception e)
            {
                var message = $@"Error saving Sales information.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<SaveResponse<IReadOnlyList<ISalesVerification>>> SaveAllSalesVerificationAsync(List<ISalesVerification> items)
        {
            var response = new SaveResponse<IReadOnlyList<ISalesVerification>>();

            try
            {
                if (items.Any())
                {
                    var dto = new SalesVerificationDto();
                    using (var connection = ConnectionFactory.GetConnection())
                    {
                        var sql = "spmSalesVerification";
                        var results = await connection.ExecuteAsync(sql, items.Select(m => dto.FromModel(m)), commandType: CommandType.StoredProcedure);

                        response.Content = items;
                        response.Message = $@"Sales verification records were inserted/updated.";
                    }
                }

            }
            catch (Exception e)
            {
                var message = $@"Error saving Sales information.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[Sales] SET IsDeleted = 1
                WHERE [Id] = @id";
        }

        private string GetDeleteAllStatement()
        {
            return @"
                UPDATE [v2].[Sales] SET IsDeleted = 1
                WHERE [AuditId] = @auditId AND [EntityId] = @entityId AND [LocationId] = @locationId";
        }
        #endregion Delete Statement

        #region Select Statement
        protected override string GetSelectByParentIdStatement()
        {
            return $@"{GetSelectStatement()}";
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [AuditId]
                        , [EntityId]
                        , [LocationId]
                        , [PeriodType]
                        , [PeriodStart]
                        , [PeriodEnd]
                        , [GrossSales]
                        , [AlcoholSales]
                        , [Freight]
                        , [IntercompanySales]
                        , [LotterSales]
                        , [Returns]
                        , [SalesTax]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[Sales]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



