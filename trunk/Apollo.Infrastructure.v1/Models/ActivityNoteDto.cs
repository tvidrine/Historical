// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Activity;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class ActivityNoteDto : DtoBase<IActivityNote, IActivityNoteInfo>
    {
        #region Public Properties
        public int AlId { get; set; }
        public int AuditId { get; set; }
        public int? EntityId { get; set; }
        public int ActivityTypeId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime ActivityDate { get; set; }
        public int UserId { get; set; }
        public string ActivityDescription { get; set; }
        public int ToFromId { get; set; }
        public string EmailorPhone { get; set; }
        public int? CallResultId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IActivityNote model)
        {
            AlId = model.Id;
            AuditId = model.AuditId;
            EntityId = model.EntityId;
            ActivityTypeId = model.ActivityTypeId;
            ActionTypeId = model.ActionTypeId;
            ActivityDate = model.ActivityDate;
            UserId = model.UserId;
            ActivityDescription = model.ActivityDescription;
            ToFromId = 5;
            EmailorPhone = model.EmailorPhone;
            CallResultId = model.CallResultId;
            IsCompleted = model.IsCompleted;
            CreatedDate = model.CreatedOn.DateTime;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IActivityNote ToModel()
        {
            var model = new ActivityNote()
            {
                Id = AlId,
                AuditId = AuditId,
                EntityId = EntityId,
                ActivityTypeId = ActivityTypeId,
                ActionTypeId = ActionTypeId,
                ActivityDate = ActivityDate,
                UserId = UserId,
                ActivityDescription = ActivityDescription,
                ToFromId = ToFromId,
                EmailorPhone = EmailorPhone,
                CallResultId = CallResultId,
                IsCompleted = IsCompleted,
                CreatedOn = CreatedDate
            };

            return model;
        }
        #endregion ToModel
    }
}