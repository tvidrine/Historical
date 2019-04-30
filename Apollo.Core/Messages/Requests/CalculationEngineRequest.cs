// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/08/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Messages.Requests
{
    public class CalculationEngineRequest
    {
        public AuditTypeEnum AuditType { get; set; }
    }
}