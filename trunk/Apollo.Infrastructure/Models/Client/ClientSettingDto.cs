// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models.Client
{
    public class ClientSettingDto : DtoBase<IClientSetting, IClientSetting>
    {
        #region Public Properties
        public int ClientId { get; set; }
        public int SettingType { get; set; }
        public string SettingValue { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IClientSetting model)
        {
            Id = model.Id;
            ClientId = model.ClientId;
            SettingType = (int) model.SettingType;
            SettingValue = model.Value.ToString();
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IClientSetting ToModel()
        {
            var model = new ClientSetting
            {
                Id = Id,
                ClientId = ClientId,
                SettingType = (ClientSettingsEnum) SettingType,
                Value = SettingValue,
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
