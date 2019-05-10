// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/9/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts;
using Apollo.Core.Domain;

namespace Apollo.Infrastructure.Models
{
    public class SystemInfoDto : DtoBase<ISystemInfo, ISystemInfo>
    {
        #region Public Properties
        public string AppVersion { get; set; }
        public DateTimeOffset AppLastUpdated { get; set; }
        public string DbSchemaVersion { get; set; }
        public DateTimeOffset DbSchemaLastUpdated { get; set; }
        public bool ShowMaintenanceMessage { get; set; }
        public string MaintenanceMessage { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(ISystemInfo model)
        {
            Id = model.Id;
            AppVersion = model.AppVersion;
            AppLastUpdated = model.AppLastUpdated;
            DbSchemaVersion = model.DbSchemaVersion;
            DbSchemaLastUpdated = model.DbSchemaLastUpdated;
            ShowMaintenanceMessage = model.ShowMaintenanceMessage;
            MaintenanceMessage = model.MaintenanceMessage;
            LastUpdated = model.LastUpdated;
            LastUpdatedBy = model.LastUpdatedBy;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override ISystemInfo ToModel()
        {
            var model = new SystemInfo
            {
                Id = Id,
                AppVersion = AppVersion,
                AppLastUpdated = AppLastUpdated,
                DbSchemaVersion = DbSchemaVersion,
                DbSchemaLastUpdated = DbSchemaLastUpdated,
                ShowMaintenanceMessage = ShowMaintenanceMessage,
                MaintenanceMessage = MaintenanceMessage,
                LastUpdated = LastUpdated,
                LastUpdatedBy = LastUpdatedBy
            };

            return model;
        }
        #endregion ToModel
    }
}
