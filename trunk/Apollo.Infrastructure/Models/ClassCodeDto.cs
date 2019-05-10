// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Domain.ClassCode;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models
{
    public class ClassCodeDto : DtoBase<IClassCode, IClassCode>
    {
        #region Public Properties
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsGeneralInclusion { get; set; }
        public int Clientid { get; set; }
        public string State { get; set; }
        public int AuditTypeId { get; set; }
        public int ExposureBasis { get; set; }
       
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IClassCode model)
        {
            Id = model.Id;
            Code = model.Code;
            Description = model.Description;
            IsGeneralInclusion = model.IsGeneralInclusion;
            Clientid = model.ClientId;
            State = model.State;
            AuditTypeId = (int) model.AuditType;
            ExposureBasis = (int) model.ExposureBasis;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IClassCode ToModel()
        {
            var model = new ClassCode
            {
                Id = Id,
                Code = Code,
                Description = Description,
                IsGeneralInclusion = IsGeneralInclusion,
                ClientId = Clientid,
                State = State == null ? State : State.TrimEnd(),
                ExposureBasis =(ExposureBasisEnum) ExposureBasis,
                AuditType = (AuditTypeEnum) AuditTypeId,
                CreatedOn = CreatedOn,
                CreatedById = CreatedById,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById
            };

            return model;
        }
        #endregion ToModel
    }
}
