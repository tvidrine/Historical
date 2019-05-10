// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class ClientSettingsRepositoryV1 : BaseRepository<CarrierSettingsDto, IClientSettings, IClientSettings>, IClientSettingsRepository
    {
        private readonly IAuditConfiguration _auditConfiguration;

        public ClientSettingsRepositoryV1(IConnectionFactory connectionFactory, ILogManager logManager, IAuditConfiguration auditConfiguration)
            : base(connectionFactory, logManager, new CarrierSettingsDto())
        {
            _auditConfiguration = auditConfiguration;
        }

        public override Task<GetResponse<IReadOnlyList<IClientSettings>>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                DELETE FROM [dbo].[CarrierSettings]
                WHERE [CarrierSettingsID] = @carriersettingsid;";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[CarrierSettings] AS T
                USING (VALUES 
                        ( @carriersettingsid
                        , @carrierid
                        , @billingcontact
                        , @billingcontactemail
                        , @wpdemail
                        , @wpdoptions
                        , @format
                        , @frequency
                        , @monthlyduedates
                        , @quarterlyduedates
                        , @semiannualfullterm
                        , @semiannualshortterm
                        , @semiannualcancellation
                        , @annualfullterm
                        , @annualshortterm
                        , @annualcancellation
                        , @auditduedate
                        , @wlformid
                        , @wldays
                        , @locationwarning
                        , @audittype
                        , @ccagent
                        , @requestallsubs
                        , @processclaims
                        , @subcontractorlabel
                        , @usecasuallabor
                        , @uselocationemployeecount
                        , @subcontractorheadercontent
                        , @audittypes
                        , @coirequired
                        , @bylocationallowed
                        , @bylocationdefault
                        , @showclassallocationsummary
                        , @wpdemailsubjectformat
                        , @wpdfilenameformat
                        , @useshareauditwelcomeletter
                        , @logo
                        )
                       ) AS S
                       (
                          [CarrierSettingsID]
                        , [CarrierID]
                        , [BillingContact]
                        , [BillingContactEmail]
                        , [WPDEmail]
                        , [WPDOptions]
                        , [Format]
                        , [Frequency]
                        , [MonthlyDueDates]
                        , [QuarterlyDueDates]
                        , [SemiAnnualFullTerm]
                        , [SemiAnnualShortTerm]
                        , [SemiAnnualCancellation]
                        , [AnnualFullTerm]
                        , [AnnualShortTerm]
                        , [AnnualCancellation]
                        , [AuditDueDate]
                        , [WLFormID]
                        , [WLDays]
                        , [LocationWarning]
                        , [AuditType]
                        , [CCAgent]
                        , [RequestAllSubs]
                        , [ProcessClaims]
                        , [SubContractorLabel]
                        , [UseCasualLabor]
                        , [UseLocationEmployeeCount]
                        , [SubContractorHeaderContent]
                        , [AuditTypes]
                        , [COIRequired]
                        , [ByLocationAllowed]
                        , [ByLocationDefault]
                        , [ShowClassAllocationSummary]
                        , [WpdEmailSubjectFormat]
                        , [WpdFilenameFormat]
                        , [UseShareAuditWelcomeLetter]
                        , [Logo]
                       ) ON T.[CarrierSettingsID] = S.[CarrierSettingsID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [CarrierID]
                        , [BillingContact]
                        , [BillingContactEmail]
                        , [WPDEmail]
                        , [WPDOptions]
                        , [Format]
                        , [Frequency]
                        , [MonthlyDueDates]
                        , [QuarterlyDueDates]
                        , [SemiAnnualFullTerm]
                        , [SemiAnnualShortTerm]
                        , [SemiAnnualCancellation]
                        , [AnnualFullTerm]
                        , [AnnualShortTerm]
                        , [AnnualCancellation]
                        , [AuditDueDate]
                        , [WLFormID]
                        , [WLDays]
                        , [LocationWarning]
                        , [AuditType]
                        , [CCAgent]
                        , [RequestAllSubs]
                        , [ProcessClaims]
                        , [SubContractorLabel]
                        , [UseCasualLabor]
                        , [UseLocationEmployeeCount]
                        , [SubContractorHeaderContent]
                        , [AuditTypes]
                        , [COIRequired]
                        , [ByLocationAllowed]
                        , [ByLocationDefault]
                        , [ShowClassAllocationSummary]
                        , [WpdEmailSubjectFormat]
                        , [WpdFilenameFormat]
                        , [UseShareAuditWelcomeLetter]
                        , [Logo]
                           )
                    VALUES (
                          S.[CarrierID]
                        , S.[BillingContact]
                        , S.[BillingContactEmail]
                        , S.[WPDEmail]
                        , S.[WPDOptions]
                        , S.[Format]
                        , S.[Frequency]
                        , S.[MonthlyDueDates]
                        , S.[QuarterlyDueDates]
                        , S.[SemiAnnualFullTerm]
                        , S.[SemiAnnualShortTerm]
                        , S.[SemiAnnualCancellation]
                        , S.[AnnualFullTerm]
                        , S.[AnnualShortTerm]
                        , S.[AnnualCancellation]
                        , S.[AuditDueDate]
                        , S.[WLFormID]
                        , S.[WLDays]
                        , S.[LocationWarning]
                        , S.[AuditType]
                        , S.[CCAgent]
                        , S.[RequestAllSubs]
                        , S.[ProcessClaims]
                        , S.[SubContractorLabel]
                        , S.[UseCasualLabor]
                        , S.[UseLocationEmployeeCount]
                        , S.[SubContractorHeaderContent]
                        , S.[AuditTypes]
                        , S.[COIRequired]
                        , S.[ByLocationAllowed]
                        , S.[ByLocationDefault]
                        , S.[ShowClassAllocationSummary]
                        , S.[WpdEmailSubjectFormat]
                        , S.[WpdFilenameFormat]
                        , S.[UseShareAuditWelcomeLetter]
                        , S.[Logo]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[CarrierID] = S.[CarrierID], T.[BillingContact] = S.[BillingContact], T.[BillingContactEmail] = S.[BillingContactEmail], T.[WPDEmail] = S.[WPDEmail], T.[WPDOptions] = S.[WPDOptions], 
                        T.[Format] = S.[Format], T.[Frequency] = S.[Frequency], T.[MonthlyDueDates] = S.[MonthlyDueDates], T.[QuarterlyDueDates] = S.[QuarterlyDueDates], T.[SemiAnnualFullTerm] = S.[SemiAnnualFullTerm], 
                        T.[SemiAnnualShortTerm] = S.[SemiAnnualShortTerm], T.[SemiAnnualCancellation] = S.[SemiAnnualCancellation], T.[AnnualFullTerm] = S.[AnnualFullTerm], T.[AnnualShortTerm] = S.[AnnualShortTerm], T.[AnnualCancellation] = S.[AnnualCancellation], 
                        T.[AuditDueDate] = S.[AuditDueDate], T.[WLFormID] = S.[WLFormID], T.[WLDays] = S.[WLDays], T.[LocationWarning] = S.[LocationWarning], T.[AuditType] = S.[AuditType], 
                        T.[CCAgent] = S.[CCAgent], T.[RequestAllSubs] = S.[RequestAllSubs], T.[ProcessClaims] = S.[ProcessClaims], T.[SubContractorLabel] = S.[SubContractorLabel], T.[UseCasualLabor] = S.[UseCasualLabor], 
                        T.[UseLocationEmployeeCount] = S.[UseLocationEmployeeCount], T.[SubContractorHeaderContent] = S.[SubContractorHeaderContent], T.[AuditTypes] = S.[AuditTypes], T.[COIRequired] = S.[COIRequired], T.[ByLocationAllowed] = S.[ByLocationAllowed], 
                        T.[ByLocationDefault] = S.[ByLocationDefault], T.[ShowClassAllocationSummary] = S.[ShowClassAllocationSummary], T.[WpdEmailSubjectFormat] = S.[WpdEmailSubjectFormat], T.[WpdFilenameFormat] = S.[WpdFilenameFormat], T.[UseShareAuditWelcomeLetter] = S.[UseShareAuditWelcomeLetter], 
                        T.[Logo] = S.[Logo] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSummarySelectStatement()
        {
            return GetSelectStatement();
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [CarrierSettingsID]
                        , s.[CarrierID]
                        , [BillingContact]
                        , [BillingContactEmail]
                        , [WPDEmail]
                        , [WPDOptions]
                        , [Format]
                        , [Frequency]
                        , [MonthlyDueDates]
                        , [QuarterlyDueDates]
                        , [SemiAnnualFullTerm]
                        , [SemiAnnualShortTerm]
                        , [SemiAnnualCancellation]
                        , [AnnualFullTerm]
                        , [AnnualShortTerm]
                        , [AnnualCancellation]
                        , [AuditDueDate]
                        , [WLFormID]
                        , [WLDays]
                        , [LocationWarning]
                        , [AuditType]
                        , [CCAgent]
                        , [RequestAllSubs]
                        , [ProcessClaims]
                        , [SubContractorLabel]
                        , [UseCasualLabor]
                        , [UseLocationEmployeeCount]
                        , [SubContractorHeaderContent]
                        , [AuditTypes]
                        , [COIRequired]
                        , [ByLocationAllowed]
                        , [ByLocationDefault]
                        , [ShowClassAllocationSummary]
                        , [WpdEmailSubjectFormat]
                        , [WpdFilenameFormat]
                        , [UseShareAuditWelcomeLetter]
                        , [Logo]
                        , [LogoFilename] = CASE WHEN i.CarrierImageId IS NULL THEN '1thumb.png' ELSE CONVERT(varchar(3),i.CarrierImageId) + 'thump.' + i.Extension END
                    FROM [dbo].[CarrierSettings] s LEFT JOIN [dbo].[CarrierImages] i ON s.CarrierId = i.CarrierId
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



