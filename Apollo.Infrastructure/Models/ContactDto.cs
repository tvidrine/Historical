// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Common;

namespace Apollo.Infrastructure.Models
{
    public class ContactDto : DtoBase<IContact, IContact>
    {
        #region Public Properties
        public int EntityId { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string Email { get; set; }
        public string FaxNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IContact model)
        {
            Id = model.Id;
            EntityId = model.EntityId;
            ContactType = model.ContactType;
            Email = model.Email;
            FaxNumber = model.FaxNumber;
            Name = model.Name;
            PhoneNumber = model.PhoneNumber;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IContact ToModel()
        {
            var model = new Contact(EntityId)
            {
                ContactType = ContactType,
                Email = Email,
                FaxNumber = FaxNumber,
                Name = Name,
                PhoneNumber = PhoneNumber,
                Id = Id,
                CreatedOn = CreatedOn,
                CreatedById = CreatedById,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById,
            };


            return model;
        }
        #endregion ToModel
    }
}
