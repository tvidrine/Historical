// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/7/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Common;
using Apollo.Core.Domain.Policies;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class LocationsDto : DtoBase<ILocation, ILocationInfo>
    {
        #region Public Properties
        public int LocationID { get; set; }
        public int EntityID { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
        public string LocationPhone { get; set; }
        public decimal EstExposure { get; set; }
        public string PayRoll { get; set; }
        public bool IsVerified { get; set; }
        public int EmployeeCount { get; set; }
        public int DisplayIndex { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(ILocation model)
        {
            LocationID = model.Id;
            EntityID = model.EntityId;
            LocationName = model.Name;
            LocationAddress = model.MainAddress.Line1;
            LocationAddress2 = model.MainAddress.Line2;
            LocationCity = model.MainAddress.City;
            LocationState = model.MainAddress.State;
            LocationZip = model.MainAddress.Zipcode;
            LocationPhone = model.Phone;
            return this;
        }
        #endregion FromModel

        #region ToModel
        public override ILocation ToModel()
        {
            var model = new Location
            {
                Id = LocationID,
                EntityId = EntityID,
                MainAddress = new Address
                {
                    Line1 = LocationAddress,
                    Line2 = LocationAddress2,
                    City = LocationCity,
                    State = LocationState,
                    Zipcode = LocationZip,
                },
                Name = LocationName,
                Phone = LocationPhone
            };

            return model;
        }
        #endregion ToModel

        public override ILocationInfo ToInfo()
        {
            return new LocationInfo
            {
                Id = LocationID,
                Name = LocationName
            };
        }
    }
}
