// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/06/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Reporting;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class ReportApplicationService : IReportApplicationService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILogManager _logManager;

        public ReportApplicationService(IReportRepository reportRepository, ILogManager logManager)
        {
            _reportRepository = reportRepository;
            _logManager = logManager;
        }
        public async Task<GetResponse<IReadOnlyList<IAuditorActivityData>>> GetAuditorActivityReportDataAsync(int carrierId, int userId)
        {
            var getResponse = new GetResponse<IReadOnlyList<IAuditorActivityData>>();

            try
            {
                getResponse = await _reportRepository.GetAuditorActivityReportDataAsync(carrierId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving data for the Support Activity Report");
            }

            return getResponse;
        }
        public async Task<GetResponse<IReadOnlyList<ISupportActivityData>>> GetSupportActivityReportDataAsync(int carrierId, int userId)
        {
            var getResponse = new GetResponse<IReadOnlyList<ISupportActivityData>>();

            try
            {
                getResponse = await _reportRepository.GetSupportActivityReportDataAsync(carrierId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving data for the Support Activity Report");
            }

            return getResponse;
        }
    }
}