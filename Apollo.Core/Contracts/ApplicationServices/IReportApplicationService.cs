// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/06/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Reporting;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IReportApplicationService
    {
         Task<GetResponse<IReadOnlyList<IAuditorActivityData>>> GetAuditorActivityReportDataAsync(int carrierId, int userId);
         Task<GetResponse<IReadOnlyList<ISupportActivityData>>> GetSupportActivityReportDataAsync(int carrierId, int userId);
    }
}