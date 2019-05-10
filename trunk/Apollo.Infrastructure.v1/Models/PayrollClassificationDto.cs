// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 03/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class PayrollClassificationDto : DtoBase<IPayrollClassification, IPayrollClassification>
    {
        #region Public Properties
        public int PayRollID { get; set; }
        public int AuditID { get; set; }
        public int EntityID { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string ClassCode { get; set; }
        public string ClassCodeDesc { get; set; }
        public string EmpState { get; set; }
        public int ClassCodeLookupID { get; set; }
        public string ClassCodeLookupCode { get; set; }
        public string ClassCodeComments { get; set; }
        #endregion Public Properties

        #region FromModel
        public override  IDto FromModel(IPayrollClassification model)
        {
            PayRollID = model.Id;
            AuditID = model.AuditId;
            EntityID = model.EntityId;
            EmpFirstName = model.FirstName;
            EmpLastName = model.LastName;
            EmpState = model.StateCode;
            ClassCodeLookupID = model.ClassCodeId;
            ClassCodeComments = model.ClassCodeComment;
            
            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IPayrollClassification ToModel()
        {
            var model = new PayrollClassification
            {
                Id = PayRollID,
                AuditId = AuditID,
                EntityId = EntityID,
                FirstName = EmpFirstName,
                LastName = EmpLastName,
                StateCode = EmpState,
                ClassCodeId = ClassCodeLookupID,
                ClassCodeComment = ClassCodeComments
                
            };

            return model;
        }
        #endregion ToModel
    }
}