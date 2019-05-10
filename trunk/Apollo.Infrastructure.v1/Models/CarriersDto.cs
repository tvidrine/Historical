// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/16/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class CarriersDto : DtoBase<IClient, IClientInfo>
    {
        #region Public Properties
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierContact { get; set; }
        public string CarrierPhone { get; set; }
        public string CarrierFax { get; set; }
        public string CarrierAddress { get; set; }
        public string CarrierAddress2 { get; set; }
        public string CarrierCity { get; set; }
        public string CarrierState { get; set; }
        public string CarrierZip { get; set; }
        public string CarrierEmail { get; set; }
        public bool IsEnhancedCarrier { get; set; }
        public bool UseRates { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IClient model)
        {
            var contact = model.Contacts.FirstOrDefault();
            CarrierId = model.Id;
            CarrierName = model.Name;
            CarrierContact = contact == null ? string.Empty : contact.Name;
            CarrierPhone = contact == null ? string.Empty : contact.PhoneNumber;
            CarrierFax = contact == null ? string.Empty : contact.Name; ;
            CarrierAddress = model.Address.Line1;
            CarrierAddress2 = model.Address.Line2;
            CarrierCity = model.Address.City;
            CarrierState = model.Address.State;
            CarrierZip = model.Address.Zipcode;
            CarrierEmail = contact == null ? string.Empty : contact.Email;
            IsEnhancedCarrier = false;
            UseRates = false;
            
            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IClient ToModel()
        {
            var model = new Client
            {
                Id = CarrierId,
                Name = CarrierName,
                Address = new Address
                {
                    City = CarrierCity,
                    Line1 = CarrierAddress,
                    Line2 = CarrierAddress2,
                    State = CarrierState,
                    Zipcode = CarrierZip
                },
                Contacts = new List<IContact>
                {
                    new Contact
                    {
                        Name = CarrierContact,
                        PhoneNumber = CarrierPhone,
                        FaxNumber = CarrierFax,
                        Email = CarrierEmail
                    }
                }
            };

            return model;
        }
        #endregion ToModel

        public override IClientInfo ToInfo()
        {
            return new ClientInfo
            {
                Id = CarrierId,
                Name = CarrierName
            };
        }
    }
}
