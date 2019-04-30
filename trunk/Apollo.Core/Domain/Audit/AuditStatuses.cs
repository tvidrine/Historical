// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/10/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Domain.Audit
{
    public enum AuditStatuses
    {
        MissingData = 0,
        Submitted = 1,
        NotifiedByEmail = 2,
        InProgress = 3,
        Completed = 4,
        FollowUp = 5,
        Cancelled = 6,
        NonProductive = 7,
        PolicyHolderNotComplete = 8,
        InQualityControl = 9,
        QualityControlPending = 10,
        NotifiedByLetter = 11,
        Current = 12,
        NonProductivePending = 13,
        RecordsReceived = 14,
        PendingRecords = 15, 
        ReturnForCorrections = 16
    }
}