// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;

namespace Apollo.Infrastructure.Models
{
    public class WpdConfigDto : DtoBase<IWpdConfig, IWpdConfig>
    {
        #region Public Properties
        public int ClientId { get; set; }
        public byte DeliveryMethod { get; set; }
        public string DocumentTypes { get; set; }
        public byte Options { get; set; }
        public byte DeliverTo { get; set; }
        public string FileFormats { get; set; }
        
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IWpdConfig model)
        {
            Id = model.Id;
            ClientId = model.ClientId;
            DeliveryMethod = (byte)model.DeliveryMethod;
            //Options = model.Options;
            DeliverTo = (byte) model.DeliverTo;
            FileFormats = model.FileFormats.ToString();
            DocumentTypes = model.DocumentTypes.ToString();
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IWpdConfig ToModel()
        {
            var model = new WpdConfig(ClientId)
            {
                DeliveryMethod = (WpdDeliveryMethods)DeliveryMethod,
                //Options = Options,
                DeliverTo = (DeliverToOptions) DeliverTo,
                FileFormats = FileFormats.ToFileFormats(),
                DocumentTypes = DocumentTypes.ToDocumentTypes()
            };

            model.Id = Id;
            model.CreatedOn = CreatedOn;
            model.CreatedById = CreatedById;
            model.LastModifiedOn = LastModifiedOn;
            model.LastModifiedById = LastModifiedById;

            return model;
        }
        #endregion ToModel
    }
}
