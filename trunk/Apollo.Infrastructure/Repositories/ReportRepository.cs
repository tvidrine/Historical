// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/06/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Reporting;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Reporting;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class ReportRepository : AbstractBaseRepository, IReportRepository
    {
        public ReportRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager)
        {
        }

        public async Task<GetResponse<IReadOnlyList<IAuditorActivityData>>> GetAuditorActivityReportDataAsync(int carrierId, int userId)
        {
            var response = new GetResponse<IReadOnlyList<IAuditorActivityData>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = "rpt_GetAuditorHistory";

                    var results = await connection
                        .QueryAsync<AuditorActivityData>(sql, new { carrierID = carrierId, AssignedToId = userId }, commandType: CommandType.StoredProcedure);

                    response.Content = results
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve auditor activity data .";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }
        public async Task<GetResponse<IReadOnlyList<ISupportActivityData>>> GetSupportActivityReportDataAsync(int carrierId, int userId)
        {
            var response = new GetResponse<IReadOnlyList<ISupportActivityData>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = "rpt_GetSupportHistory";

                    var results = await connection
                        .QueryAsync<SupportActivityData>(sql, new {carrierID = carrierId, AssignedToId = userId}, commandType: CommandType.StoredProcedure);

                    response.Content = results
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve support activity data .";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        } 
    }
}