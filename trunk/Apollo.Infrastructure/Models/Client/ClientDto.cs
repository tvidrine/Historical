// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/06/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models.Client
{
    public class ClientDto : DtoBase<IClient, IClientInfo>
    {
        #region Public Properties
        public string Name { get; set; }
        public AuditTypeEnum AuditType { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public AuditProcessTypeEnum ProcessType { get; set; }
        public int ParentClientId { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IClient model)
        {
            Id = model.Id;
            Name = model.Name;
            AuditType = model.AuditType;
            ClientType = model.ClientType;
            ProcessType = model.ProcessType;
            ParentClientId = model.ParentId;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }

        #endregion FromModel

        #region ToModel
        public override IClient ToModel()
        {
            var model = new Core.Domain.Client.Client
            {
                Name = Name,
                AuditType = AuditType,
                ClientType = ClientType,
                ProcessType = ProcessType,
                ParentId = ParentClientId,
                Id = Id,
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
