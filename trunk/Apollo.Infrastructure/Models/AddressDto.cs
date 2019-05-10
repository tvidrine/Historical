// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Common;

namespace Apollo.Infrastructure.Models
{
    public class AddressDto :DtoBase<IAddress, IAddress>
    {
        #region Public Properties
        public int EntityId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAddress model)
        {
            Id = model.Id;
            Line1 = model.Line1;
            Line2 = model.Line2;
            City = model.City;
            State = model.State;
            Zipcode = model.Zipcode;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAddress ToModel()
        {
            var model = new Address()
            {
                Line1 = Line1,
                Line2 = Line2,
                City = City,
                State = State,
                Zipcode = Zipcode,
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
