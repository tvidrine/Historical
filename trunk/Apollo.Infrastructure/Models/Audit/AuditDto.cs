// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Identity;

namespace Apollo.Infrastructure.Models.Audit
{
    public class AuditDto : DtoBase<IAudit, IAudit>
    {
        #region FromModel
        public override IDto FromModel(IAudit model)
        {
            Id = model.Id;
            AssignmentNumber = model.AssignmentNumber;
            AssignedOn = model.AssignedOn;
            AuditorId = model.AuditorId;
            CompletedDate = model.CompletedDate;
            InvoiceId = model.InvoiceId;
            PolicyId = model.Policy.Id;
            RequestedById = model.RequestedBy.Id;
            RequestedOn = model.RequestedOn;
            StartDate = model.StartDate;
            AuditStatusId = (int) model.AuditStatus;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAudit ToModel()
        {
            var requestedUser = new User{Id = RequestedById};
            var model = new Core.Domain.Audit.Audit
            {
                AssignedOn = AssignedOn,
                AssignmentNumber = AssignmentNumber,
                AuditorId = AuditorId,
                CompletedDate = CompletedDate,
                InvoiceId = InvoiceId,

                //Policy = PolicyId,
                RequestedBy = requestedUser,
                RequestedOn = RequestedOn,
                StartDate = StartDate,
                AuditStatus = (AuditStatuses) AuditStatusId,
                Id = Id,
                CreatedOn = CreatedOn,
                CreatedById = CreatedById,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById
            };

            return model;
        }
        #endregion ToModel

        #region Public Properties
        public DateTimeOffset AssignedOn { get; set; }
        public int AssignmentNumber { get; set; }
        public int AuditorId { get; set; }
        public DateTimeOffset CompletedDate { get; set; }
        public int InvoiceId { get; set; }
        public int PolicyId { get; set; }
        public int RequestedById { get; set; }
        public DateTimeOffset RequestedOn { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int AuditStatusId { get; set; }
        #endregion Public Properties
    }
}