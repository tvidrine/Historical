﻿// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Client;

namespace Apollo.Infrastructure.Models.Client
{
    public class ClientSettingsDto : DtoBase<IClientSettings, IClientSettings>
    {
        #region Public Properties
        public int CarrierSettingsId { get; set; }
        public int CarrierId { get; set; }
        public string WpdEmail { get; set; }
        public string WpdOptions { get; set; }
        public string Format { get; set; }
        public string Frequency { get; set; }
        public string MonthlyDueDates { get; set; }
        public string QuarterlyDueDates { get; set; }
        public string SemiAnnualFullTerm { get; set; }
        public string SemiAnnualShortTerm { get; set; }
        public string SemiAnnualCancellation { get; set; }
        public string AnnualFullTerm { get; set; }
        public string AnnualShortTerm { get; set; }
        public string AnnualCancellation { get; set; }
        public int AuditDueDate { get; set; }
        public int WlDays { get; set; }
        public string LocationWarning { get; set; }
        public string AuditType { get; set; }
        public bool CcAgent { get; set; }
        public bool RequestAllSubs { get; set; }
        public bool ProcessClaims { get; set; }
        public string SubContractorLabel { get; set; }
        public bool UseCasualLabor { get; set; }
        public bool UseLocationEmployeeCount { get; set; }
        public string SubContractorHeaderContent { get; set; }
        public string AuditTypes { get; set; }
        public bool ByLocationAllowed { get; set; }
        public bool ByLocationDefault { get; set; }
        public bool ShowClassAllocationSummary { get; set; }
        public string WpdEmailSubjectFormat { get; set; }
        public string WpdFilenameFormat { get; set; }
        public bool UseShareAuditWelcomeLetter { get; set; }
        public byte[] Logo { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IClientSettings model)
        {
            CarrierSettingsId = model.Id;
            CarrierId = model.ClientId;
            WpdEmail = model.WpdEmail;
            WpdOptions = model.WpdOptions;
            Format = model.Format;
            Frequency = model.Frequency;
            MonthlyDueDates = model.MonthlyDueDates;
            QuarterlyDueDates = model.QuarterlyDueDates;
            SemiAnnualFullTerm = model.SemiAnnualFullTerm;
            SemiAnnualShortTerm = model.SemiAnnualShortTerm;
            SemiAnnualCancellation = model.SemiAnnualCancellation;
            AnnualFullTerm = model.AnnualFullTerm;
            AnnualShortTerm = model.AnnualShortTerm;
            AnnualCancellation = model.AnnualCancellation;
            AuditDueDate = model.AuditDueDate;
            WlDays = model.WelcomeLetterDays;
            LocationWarning = model.LocationWarning;
            AuditType = model.AuditType;
            RequestAllSubs = model.RequestAllSubs;
            ProcessClaims = model.ProcessClaims;
            SubContractorLabel = model.SubContractorLabel;
            UseCasualLabor = model.UseCasualLabor;
            UseLocationEmployeeCount = model.UseLocationEmployeeCount;
            SubContractorHeaderContent = model.SubContractorHeaderContent;
            AuditTypes = model.AuditTypes;
            ShowClassAllocationSummary = model.ShowClassAllocationSummary;
            WpdEmailSubjectFormat = model.WpdEmailSubjectFormat;
            WpdFilenameFormat = model.WpdFilenameFormat;
            UseShareAuditWelcomeLetter = model.UseShareAuditWelcomeLetter;
            Logo = Logo;
            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IClientSettings ToModel()
        {
            var model = new ClientSettings
            {
                Id = CarrierSettingsId,
                ClientId = CarrierId,
                WpdEmail = WpdEmail,
                WpdOptions = WpdOptions,
                Format = Format,
                Frequency = Frequency,
                MonthlyDueDates = MonthlyDueDates,
                QuarterlyDueDates = QuarterlyDueDates,
                SemiAnnualFullTerm = SemiAnnualFullTerm,
                SemiAnnualShortTerm = SemiAnnualShortTerm,
                SemiAnnualCancellation = SemiAnnualCancellation,
                AnnualFullTerm = AnnualFullTerm,
                AnnualShortTerm = AnnualShortTerm,
                AnnualCancellation = AnnualCancellation,
                AuditDueDate = AuditDueDate,
                WelcomeLetterDays = WlDays,
                LocationWarning = LocationWarning,
                AuditType = AuditType,
                RequestAllSubs = RequestAllSubs,
                ProcessClaims = ProcessClaims,
                SubContractorLabel = SubContractorLabel,
                UseCasualLabor = UseCasualLabor,
                UseLocationEmployeeCount = UseLocationEmployeeCount,
                SubContractorHeaderContent = SubContractorHeaderContent,
                AuditTypes = AuditTypes,
                ShowClassAllocationSummary = ShowClassAllocationSummary,
                WpdEmailSubjectFormat = WpdEmailSubjectFormat,
                WpdFilenameFormat = WpdFilenameFormat,
                UseShareAuditWelcomeLetter = UseShareAuditWelcomeLetter,
                Logo = Logo
            };

            return model;
        }
        #endregion ToModel
    }
}
