// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Domain.Identity;
using Apollo.Core.Domain.Policies;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class AuditsDto : DtoBase<IAudit, IAudit>
    {

        #region Public Properties
        public int AuditId { get; set; }
        public int CarrierId { get; set; }
        public int RequestedByUser { get; set; }
        public int RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
        public string InsuredName { get; set; }
        public string CompanyName { get; set; }
        public string PolicyAddress { get; set; }
        public string PolicyAddress2 { get; set; }
        public string PolicyCity { get; set; }
        public string PolicyState { get; set; }
        public string PolicyZip { get; set; }
        public string PolicyPhone { get; set; }
        public string PolicyEmail { get; set; }
        public DateTime DueDate { get; set; }
        public byte AuditType { get; set; }
        public byte AuditFreq { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyStart { get; set; }
        public DateTime PolicyEnd { get; set; }
        public bool Split { get; set; }
        public string AgentName { get; set; }
        public string AgentPhone { get; set; }
        public string AgentCompany { get; set; }
        public string AgentEmail { get; set; }
        public string AgentAddress { get; set; }
        public string AgentAddress2 { get; set; }
        public string AgentCity { get; set; }
        public string AgentState { get; set; }
        public string AgentZip { get; set; }
        public int AuditStatus { get; set; }
        public decimal PercentComplete { get; set; }
        public int InvoiceID { get; set; }
        public DateTime CompleteDate { get; set; }
        public int MasterAuditID { get; set; }
        public string AuditPeriod { get; set; }
        public decimal PHPercentComplete { get; set; }
        public string PHCompletedBy { get; set; }
        public string PHCompletedByPhone { get; set; }
        public string PHComments { get; set; }
        public bool IsQcChecked { get; set; }
        public DateTime? QCTimeStamp { get; set; }
        public bool IsQCComplete { get; set; }
        public bool IsLastChild { get; set; }
        public bool IsRolledUp { get; set; }
        public DateTime PHStartDate { get; set; }
        public DateTime QCCompleteDate { get; set; }
        public decimal TotalHoursWorked { get; set; }
        public string OrderType { get; set; }
        public int AssignedToID { get; set; }
        public bool HasActivity { get; set; }
        public decimal TotalHoursPaid { get; set; }
        public bool IsBillable { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime RecordsReceived { get; set; }
        public bool RecordsComplete { get; set; }
        public string SpecialInstructions { get; set; }
        public bool IsReorder { get; set; }
        public bool IsReopen { get; set; }
        public bool IsDispute { get; set; }
        public int OriginalAuditID { get; set; }
        public int ReorderedToAuditID { get; set; }
        public string PHBestTimeToContact { get; set; }
        public DateTime PHDateOfBirth { get; set; }
        public string CarrierOrderNumber { get; set; }
        public int QCReviewerID { get; set; }
        public string ExposureBasis { get; set; }
        public bool ByLocation { get; set; }
        #endregion Public Properties

        #region FromModel

        public override IDto FromModel(IAudit model)
        {
            AuditId = model.Id;
            CarrierId = model.Policy.Client.Id;
            RequestedByUser = model.RequestedBy.Id;
            RequestedBy = model.RequestedBy.Id;
            RequestDate = model.RequestedOn.DateTime;
            InsuredName = model.Policy.InsuredName;
            CompanyName = model.Policy.CompanyName;
            PolicyAddress = model.Policy.Address.Line1;
            PolicyAddress2 = model.Policy.Address.Line2;
            PolicyCity = model.Policy.Address.City;
            PolicyState = model.Policy.Address.State;
            PolicyZip = model.Policy.Address.Zipcode;
            PolicyPhone = model.Policy.Phone;
            PolicyEmail = model.Policy.Email;
            DueDate = model.DueDate.DateTime;
            AuditType = (byte)model.AuditType;
            AuditPeriod = model.AuditPeriod;
            AuditFreq = (byte)model.AuditFrequency;
            PolicyNumber = model.Policy.PolicyNumber;
            PolicyStart = model.Policy.EffectiveStart.DateTime;
            PolicyEnd = model.Policy.EffectiveEnd.DateTime;
            //Split = model.Split;
            AgentName = model.Policy.Agent.Name;
            AgentPhone = model.Policy.Agent.Phone;
            AgentCompany = model.Policy.Agent.Company;
            AgentEmail = model.Policy.Agent.Email;
            AgentAddress = model.Policy.Agent.Address.Line1;
            AgentAddress2 = model.Policy.Agent.Address.Line2;
            AgentCity = model.Policy.Agent.Address.City;
            AgentState = model.Policy.Agent.Address.State;
            AgentZip = model.Policy.Agent.Address.Zipcode;
            AuditStatus = (int)model.AuditStatus;
            //PercentComplete = model.PercentComplete;
            //InvoiceID = model.InvoiceID;
            //CompleteDate = model.CompleteDate;
            //MasterAuditID = model.MasterAuditID;
            //AuditPeriod = model.AuditPeriod;
            //HasChild = model.HasChild;
            //PHPercentComplete = model.PHPercentComplete;
            //PHCompletedBy = model.PHCompletedBy;
            //PHCompletedByPhone = model.PHCompletedByPhone;
            //PHComments = model.PHComments;
            //WPDPickupDate = model.WPDPickupDate;
            IsQcChecked = model.CheckedForQualityControl;
            //QCTimeStamp = model.QCTimeStamp;
            //IsQCComplete = model.IsQCComplete;
            //IsLastChild = model.IsLastChild;
            //IsRolledUp = model.IsRolledUp;
            //PHStartDate = model.PHStartDate;
            //QCCompleteDate = model.QCCompleteDate;
            //TotalHoursWorked = model.TotalHoursWorked;
            //OrderType = model.OrderType;
            //AssignedToID = model.AssignedToID;
            //HasActivity = model.HasActivity;
            //TotalHoursPaid = model.TotalHoursPaid;
            //IsBillable = model.IsBillable;
            //AssignedDate = model.AssignedDate;
            //RecordsReceived = model.RecordsReceived;
            //RecordsComplete = model.RecordsComplete;
            //SpecialInstructions = model.SpecialInstructions;
            //IsReorder = model.IsReorder;
            //IsReopen = model.IsReopen;
            //IsDispute = model.IsDispute;
            //OriginalAuditID = model.OriginalAuditID;
            //ReorderedToAuditID = model.ReorderedToAuditID;
            //PHBestTimeToContact = model.PHBestTimeToContact;
            //PHDateOfBirth = model.PHDateOfBirth;
            ByLocation = model.ByLocation;
            //CarrierOrderNumber = model.CarrierOrderNumber;
            //QCReviewerID = model.QCReviewerID;

            return this;
        }


        #endregion FromModel

        #region ToModel
        public override IAudit ToModel()
        {
            var client = new Client
            {
                Id = CarrierId
            };

            var requestedByUser = new User
            {
                Id = RequestedBy
            };

            var agent = new Agent
            {
                Name = AgentName,
                Company = AgentCompany,
                Phone = AgentPhone,
                Email = AgentEmail,
                Address = new Address
                {
                    Line1 = AgentAddress,
                    Line2 = AgentAddress2,
                    City = AgentCity,
                    State = AgentState,
                    Zipcode = AgentZip
                }
            };

            var policy = new Policy
            {
                Agent = agent,
                Client = client,
                Address = new Address
                {
                    Line1 = PolicyAddress,
                    Line2 = PolicyAddress2,
                    City = PolicyCity,
                    State = PolicyState,
                    Zipcode = PolicyZip
                },
                EffectiveStart = PolicyStart,
                EffectiveEnd = PolicyEnd,
                PolicyNumber = PolicyNumber,
                Email = PolicyEmail,
                Phone = PolicyPhone,
                InsuredName = InsuredName,
                CompanyName = CompanyName
            };


            var model = new Audit
            {
                Id = AuditId,
                AssignmentNumber = AuditId,
                RequestedBy = requestedByUser,
                RequestedOn = RequestDate,
                Policy = policy,
                DueDate = DueDate,
                AuditType = (AuditTypeEnum)AuditType,
                AuditFrequency = (AuditFrequencyEnum)AuditFreq,
                AuditPeriod = AuditPeriod,
                ByLocation = ByLocation,
                //Split = Split,
                AuditStatus = (AuditStatuses)AuditStatus,
                //PercentComplete = PercentComplete,
                //InvoiceID = InvoiceID,
                //CompleteDate = CompleteDate,
                //MasterAuditID = MasterAuditID,
                //AuditPeriod = AuditPeriod,
                //HasChild = HasChild,
                //PHPercentComplete = PHPercentComplete,
                //PHCompletedBy = PHCompletedBy,
                //PHCompletedByPhone = PHCompletedByPhone,
                //PHComments = PHComments,
                CheckedForQualityControl = IsQcChecked,
                //QCTimeStamp = QCTimeStamp,
                //IsQCComplete = IsQCComplete,
                //IsLastChild = IsLastChild,
                //IsRolledUp = IsRolledUp,
                //PHStartDate = PHStartDate,
                //QCCompleteDate = QCCompleteDate,
                //TotalHoursWorked = TotalHoursWorked,
                AuditMethod = OrderType == "e-Audit" ? AuditMethods.eAudit : AuditMethods.ShareAudit,
                AuditorId = AssignedToID,
                //HasActivity = HasActivity,
                //TotalHoursPaid = TotalHoursPaid,
                //IsBillable = IsBillable,
                AssignedOn = AssignedDate,
                //RecordsReceived = RecordsReceived,
                //RecordsComplete = RecordsComplete,
                //SpecialInstructions = SpecialInstructions,
                //IsReorder = IsReorder,
                //IsReopen = IsReopen,
                //IsDispute = IsDispute,
                //OriginalAuditID = OriginalAuditID,
                //ReorderedToAuditID = ReorderedToAuditID,
                //PHBestTimeToContact = PHBestTimeToContact,
                //PHDateOfBirth = PHDateOfBirth,
                //CarrierOrderNumber = CarrierOrderNumber,
                //QCReviewerID = QCReviewerID
            };

            // Audit Frequency maybe in the Special Instructions due to incompetency 
            model.AuditFrequency = GetAuditFrequency(model);
            model.AuditExposureBasis = GetExposureBasis();
            return model;
        }

        private ExposureBasisEnum GetExposureBasis()
        {
            ExposureBasisEnum exposureBasis = ExposureBasisEnum.NotSet;

            if (string.IsNullOrEmpty(ExposureBasis))
                return exposureBasis;

            if (ExposureBasis.Contains("GrossSales"))
                exposureBasis = exposureBasis | ExposureBasisEnum.Sales;

            if (ExposureBasis.Contains("Payroll"))
                exposureBasis = exposureBasis | ExposureBasisEnum.Payroll;

            if (ExposureBasis.Contains("TotalCost"))
                exposureBasis = exposureBasis | ExposureBasisEnum.Costs;

            if (ExposureBasis.Contains("Units"))
                exposureBasis = exposureBasis | ExposureBasisEnum.Units;

            return exposureBasis;
        }

        private AuditFrequencyEnum GetAuditFrequency(IAudit audit)
        {
            var frequency = audit.AuditFrequency;

            if (string.IsNullOrEmpty(SpecialInstructions))
                frequency = AuditFrequencyEnum.Annual;
            else if (SpecialInstructions.Contains("Report Type: E-Audit Pre-Audit"))
                frequency = AuditFrequencyEnum.PreAudit;
            else if (SpecialInstructions.Contains("Report Type: E-Audit Mid-Term"))
                frequency = AuditFrequencyEnum.SemiAnnual;
            else if (SpecialInstructions.Contains("Report Type: E-Audit"))
                frequency = AuditFrequencyEnum.Annual;

            return frequency;
        }
        #endregion ToModel
    }
}
