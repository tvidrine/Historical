// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/12/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.WebApi.Contracts;

namespace Apollo.WebApi.Models
{
    /// <summary>
    ///     A DTO for a Client domain model
    /// </summary>
    public class ClientDto : IDto<ClientDto>
    {
        /// <summary>
        /// </summary>
        public ClientDto()
        {
            Contacts = new List<Contact>();
        }

        public Address Address { get; set; }
        public AuditTypeEnum AuditType { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public List<Contact> Contacts { get; }
        public string CreatedById { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string Id { get; set; }
        public string LastModifiedById { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public AuditProcessTypeEnum ProcessType { get; set; }

        /// <summary>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ClientDto FromModel<TModel>(TModel model)
        {
            if (model is IClient client)
            {
                Address = client.Address as Address;
                AuditType = client.AuditType;
                ClientType = client.ClientType;
                Contacts.AddRange(client.Contacts.Select(c => c as Contact));
                Id = client.Id.ToString();
                Name = client.Name;
                ProcessType = client.ProcessType;
                ParentId = client.ParentId.ToString();
                CreatedById = client.CreatedById.ToString();
                CreatedOn = client.CreatedOn;
                LastModifiedById = client.LastModifiedById.ToString();
                LastModifiedOn = client.LastModifiedOn;
            }

            return this;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IClient ToModel()
        {
            var client = new Client
            {
                Address = Address,
                AuditType = AuditType,
                ClientType = ClientType,
                Id = int.Parse(Id),
                Name = Name,
                ProcessType =ProcessType
            };

            ((List<IContact>) client.Contacts).AddRange(Contacts);

            return client;
        }
    }
}