// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain;

namespace Apollo.Infrastructure.Models
{
    public class CertificateOfInsuranceDto : DtoBase<ICertificateOfInsurance, ICertificateOfInsurance>
    {
        #region Public Properties
        public int LaborId { get; set; }
        public string CarrierName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? PolicyStart { get; set; }
        public DateTime? PolicyEnd { get; set; }
        public int? UploadedFileId { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(ICertificateOfInsurance model)
        {
            Id = model.Id;
            LaborId = model.LaborId;
            CarrierName = model.CarrierName;
            PolicyNumber = model.PolicyNumber;
            PolicyStart = model.PolicyStart;
            PolicyEnd = model.PolicyEnd;
            UploadedFileId = model.File?.Id;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override ICertificateOfInsurance ToModel()
        {
            var model = new CertificateOfInsurance
            {
                Id = Id,
                LaborId = LaborId,
                CarrierName = CarrierName,
                PolicyNumber = PolicyNumber,
                PolicyStart = PolicyStart,
                PolicyEnd = PolicyEnd,
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
