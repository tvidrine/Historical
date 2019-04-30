// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/04/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;

namespace Apollo.Core.Messages.Requests
{
    public class DocumentHistoryRequest
    {
        public DocumentHistoryRequest()
        {
            PeriodEnd = DateTime.Today;
        }
        public int AuditId { get; set; }
        public int ClientId { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public override string ToString()
        {
            var state = new StringBuilder();

            if (AuditId > 0)
                state.AppendLine($@"Audit ID: {AuditId}");

            if (ClientId > 0)
                state.AppendLine($@"Client ID: {ClientId}");

            if (PeriodStart.HasValue)
                state.AppendLine($@"Period: {PeriodStart:d} - {PeriodEnd:d}");

            return state.ToString();
        }
    }
}