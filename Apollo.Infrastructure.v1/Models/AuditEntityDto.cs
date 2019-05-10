// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class AuditEntityDto : DtoBase<IAuditEntity, IAuditEntity>
    {
        #region Public Properties
        public int EntityID { get; set; }
        public int AuditID { get; set; }
        public string EntityName { get; set; }
        public string EntityAddress { get; set; }
        public string EntityAddress2 { get; set; }
        public string EntityCity { get; set; }
        public string EntityState { get; set; }
        public string EntityZip { get; set; }
        public string EntityPhone { get; set; }
        public string EntityTaxID { get; set; }
        public byte EntityType { get; set; }
        public bool HasPayroll { get; set; }
        public bool Has941s { get; set; }
        public string ExposureBasis { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditEntity model)
        {
            EntityID = model.Id;
            EntityName = model.Name;
            
            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAuditEntity ToModel()
        {
            var model = new AuditEntity
            {
                Id = EntityID,
                Name = EntityName,
                ExposureBasis = GetExposureBasis()
            };

            return model;
        }
        #endregion ToModel

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
    }
}