// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Client;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class CarrierSettingsDto : DtoBase<IClientSettings, IClientSettings>
    {
        #region Public Properties
        public int CarrierSettingsID { get; set; }
        public int CarrierID { get; set; }
        public string BillingContact { get; set; }
        public string BillingContactEmail { get; set; }
        public string WPDEmail { get; set; }
        public string WPDOptions { get; set; }
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
        public int WLFormID { get; set; }
        public int WLDays { get; set; }
        public string LocationWarning { get; set; }
        public string AuditType { get; set; }
        public bool CCAgent { get; set; }
        public bool RequestAllSubs { get; set; }
        public bool ProcessClaims { get; set; }
        public string SubContractorLabel { get; set; }
        public bool UseCasualLabor { get; set; }
        public bool UseLocationEmployeeCount { get; set; }
        public string SubContractorHeaderContent { get; set; }
        public string AuditTypes { get; set; }
        public bool COIRequired { get; set; }
        public bool ByLocationAllowed { get; set; }
        public bool ByLocationDefault { get; set; }
        public bool ShowClassAllocationSummary { get; set; }
        public string WpdEmailSubjectFormat { get; set; }
        public string WpdFilenameFormat { get; set; }
        public bool UseShareAuditWelcomeLetter { get; set; }
        public byte[] Logo { get; set; }
        public string LogoFilename { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IClientSettings model)
        {
            CarrierSettingsID = model.Id;
            CarrierID = model.ClientId;
            BillingContact = model.BillingContact;
            BillingContactEmail = model.BillingContactEmail;
            WPDEmail = model.WpdEmail;
            WPDOptions = model.WpdOptions;
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
            WLDays = model.WelcomeLetterDays;
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
            Logo = model.Logo;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IClientSettings ToModel()
        {
            var model = new ClientSettings
            {
                Id = CarrierSettingsID,
                ClientId = CarrierID,
                BillingContact = BillingContact,
                BillingContactEmail = BillingContactEmail,
                WpdEmail = WPDEmail,
                WpdOptions = WPDOptions,
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
                WelcomeLetterDays = WLDays,
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

        public IClientSettings ToModel(IAuditConfiguration configuration)
        {
            var model = new ClientSettings
            {
                Id = CarrierSettingsID,
                BillingContact = BillingContact,
                BillingContactEmail = BillingContactEmail,
                WpdEmail = WPDEmail,
                WpdOptions = WPDOptions,
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
                WelcomeLetterDays = WLDays,
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
                Logo = GetLogoData(LogoFilename, configuration)
            };

            return model;
        }

        private byte[] GetLogoData(string filename, IAuditConfiguration configuration)
        {
            var fullfilename = $@"{configuration.LegacyConfiguration.ClientImageFolder}{filename}";

            //if(!File.Exists(fullfilename))
            //    fullfilename = $@"{configuration.LegacyConfiguration.ClientImageFolder}19thumb.png";

            //return File.ReadAllBytes(fullfilename);

            if(File.Exists(fullfilename))
                return File.ReadAllBytes(fullfilename);

            return null;
        }
        #endregion ToModel
    }
}
