// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Client
{
    public class ClientSettings : ModelBase, IClientSettings
    {
        private readonly IDictionary<ClientSettingsEnum, IClientSetting> _settings;
        private readonly IDictionary<Type, Func<IDictionary<ClientSettingsEnum, IClientSetting>, ClientSettingsEnum, object>> _getters 
            = new Dictionary<Type, Func<IDictionary<ClientSettingsEnum, IClientSetting>, ClientSettingsEnum, object>>
            {
                {typeof(bool), (settings, settingType)=>GetBoolean(settings, settingType) },
                {typeof(int), (settings, settingType)=>GetInteger(settings, settingType) },
                {typeof(string), (settings, settingType)=>GetString(settings, settingType) }
            };
        public ClientSettings()
        {
            _settings = new Dictionary<ClientSettingsEnum, IClientSetting>();   
        }

        #region Properties
        public int ClientId { get; set; }
       
        public string Format { get; set; }
        public string Frequency { get; set; }
        public string MonthlyDueDates { get; set; }
        public string QuarterlyDueDates { get; set; }
        public string SemiAnnualFullTerm { get; set; }
        public string SemiAnnualShortTerm { get; set; }
        public string SemiAnnualCancellation { get; set; }
        public string AnnualFullTerm { get; set; }
        public string AnnualShortTerm { get; set; }
        public string AnnualCancellation { get; set; }
        public int AuditDueDate { get; set; }
        public string LocationWarning { get; set; }
        public string AuditType { get; set; }
        public bool RequestAllSubs { get; set; }
        public bool ProcessClaims { get; set; }
        public string SubContractorLabel { get; set; }
        public bool UseCasualLabor { get; set; }
        public bool UseLocationEmployeeCount { get; set; }
        public bool UseShareAuditWelcomeLetter { get; set; }
        public string SubContractorHeaderContent { get; set; }
        public string AuditTypes { get; set; }
        public bool ShowClassAllocationSummary { get; set; }
        public string WpdEmailSubjectFormat { get; set; }
        public string WpdFilenameFormat { get; set; }
        public string BillingContact { get; set; }
        public string BillingContactEmail { get; set; }
        public string WpdEmail { get; set; }
        public string WpdOptions { get; set; }
        public int WelcomeLetterDays { get; set; }
        public byte[] Logo { get; set; }

        #endregion Properties

        #region Public Methods
        public void Set<T>(ClientSettingsEnum setting, T value)
        {
            if(!_settings.ContainsKey(setting))
                _settings.Add(
                    new KeyValuePair<ClientSettingsEnum, IClientSetting>(setting, 
                        new ClientSetting
                        {
                            SettingType = setting
                        }));

            _settings[setting].Value = value;
        }

        public T Get<T>(ClientSettingsEnum setting)
        {
            return (T) _getters[typeof(T)](_settings, setting);
        }
        #endregion Public Methods

        #region Getters
        private static bool GetBoolean(IDictionary<ClientSettingsEnum, IClientSetting> settings, ClientSettingsEnum setting)
        {
            return settings.ContainsKey(setting) && (bool) settings[setting].Value;
        }
        private static int GetInteger(IDictionary<ClientSettingsEnum, IClientSetting> settings, ClientSettingsEnum setting)
        {
            return settings.ContainsKey(setting) ? (int)settings[setting].Value : 0;
        }
        private static string GetString(IDictionary<ClientSettingsEnum, IClientSetting> settings, ClientSettingsEnum setting)
        {
            return settings.ContainsKey(setting) ? settings[setting].Value.ToString() : string.Empty;
        }
        #endregion
    }
}
