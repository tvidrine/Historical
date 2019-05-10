// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain;
using Apollo.Core.Domain.Enums;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class LaborDto : DtoBase<ILabor, ILabor>
    {
        #region Public Properties
        public int LaborID { get; set; }
        public int EntityID { get; set; }
        public int AuditID { get; set; }
        public int LaborType { get; set; }
        public int LaborInvoiceType { get; set; }
        public string LaborName { get; set; }
        public string LaborDescription { get; set; }
        public string LaborClassCode { get; set; }
        public string LaborClassCodeDesc { get; set; }
        public decimal LaborAmount { get; set; }
        public bool IsLaborOnly { get; set; }
        public string LaborState { get; set; }
        public string LaborInsured { get; set; }
        public int ClassCodeLookupID { get; set; }
        public string ClassCodeLookupCode { get; set; }
        public decimal LaborInsuredLimit { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(ILabor model)
        {
            LaborID = model.Id;
            EntityID = model.EntityId;
            AuditID = model.AuditId;
            LaborType = (int) model.LaborType;
            LaborInvoiceType = 1;
            LaborName = model.LaborerName;
            LaborDescription = model.LaborDescription;
            LaborAmount = model.LaborAmount;
            LaborState = model.LaborState;
            
            IsLaborOnly = model.IsLaborOnly;
            LaborInsured = model.IsLaborInsured ? "Yes" : "No";
            LaborInsuredLimit = model.AggregateLimit;
            ClassCodeLookupID = model.ClassCodeId;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override ILabor ToModel()
        {
            var model = new Labor
            {
                Id = LaborID,
                EntityId = EntityID,
                AuditId = AuditID,
                LaborType = (LaborTypes) LaborType,
                LaborerName = LaborName,
                LaborDescription = LaborDescription,
                LaborAmount = LaborAmount,
                LaborState = LaborState,
                IsLaborOnly =  IsLaborOnly,
                IsLaborInsured = LaborInsured.Equals("Yes"),
                ClassCodeId = ClassCodeLookupID,
                AggregateLimit = LaborInsuredLimit
            };

            return model;
        }
        #endregion ToModel
    }
}
