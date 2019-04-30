// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Client
{
    public interface IClientSettings : IHaveAuditData, IHaveId
    {
        int ClientId { get; set; }
        string BillingContact { get; set; }
        string BillingContactEmail { get; set; }
        string WpdEmail { get; set; }
        string WpdOptions { get; set; }
        string Format { get; set; }
        string Frequency { get; set; }
        string MonthlyDueDates { get; set; }
        string QuarterlyDueDates { get; set; }
        string SemiAnnualFullTerm { get; set; }
        string SemiAnnualShortTerm { get; set; }
        string SemiAnnualCancellation { get; set; }
        string AnnualFullTerm { get; set; }
        string AnnualShortTerm { get; set; }
        string AnnualCancellation { get; set; }
        int AuditDueDate { get; set; }
        string LocationWarning { get; set; }
        string AuditType { get; set; }
        bool RequestAllSubs { get; set; }
        bool ProcessClaims { get; set; }
        string SubContractorLabel { get; set; }
        bool UseCasualLabor { get; set; }
        bool UseLocationEmployeeCount { get; set; }
        string SubContractorHeaderContent { get; set; }
        string AuditTypes { get; set; }
        bool ShowClassAllocationSummary { get; set; }
        string WpdEmailSubjectFormat { get; set; }
        string WpdFilenameFormat { get; set; }
        bool UseShareAuditWelcomeLetter { get; set; }
        int WelcomeLetterDays { get; set; }
        byte[] Logo { get; set; }

        T Get<T>(ClientSettingsEnum setting);
        void Set<T>(ClientSettingsEnum setting, T value);
        
    }

    
}
