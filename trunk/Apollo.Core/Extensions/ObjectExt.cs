// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Extensions
{
    public static class ObjectExt
    {
        private static readonly Dictionary<Type, Func<object, object>> _converters =
            new Dictionary<Type, Func<object, object>>
            {
                {typeof(bool), (obj) => GetBooleanValue(obj) },
                {typeof(DateTime), (obj) => GetDateTimeValue(obj) },
                {typeof(DateTime?), (obj) => GetDateTimeNullableValue(obj) },
                {typeof(decimal), (obj) => GetDecimalValue(obj) },
                {typeof(double), (obj) => GetDoubleValue(obj) },
                {typeof(int), (obj) => GetIntValue(obj) },
                {typeof(int?), (obj) => GetIntNullableValue(obj) },
                {typeof(string), (obj) => GetStringValue(obj) },
                {typeof(ClientSettingsEnum), (obj) => GetClientSettingsEnumValue(obj) },
            };

        public static T To<T>(this object value)
        {
            return (T)_converters[typeof(T)](value);
        }

        private static bool GetBooleanValue(this object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            var lowerCaseValue = value.ToString().ToLower();

            return lowerCaseValue.Equals("true") || value.Equals("yes");
        }
        private static DateTime GetDateTimeValue(this object value)
        {
            return value == null || value == DBNull.Value ? DateTime.Now :
                DateTime.TryParse(value.ToString(), out var result) ? result : DateTime.Now;
        }
        private static DateTime? GetDateTimeNullableValue(this object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            return  DateTime.TryParse(value.ToString(), out var result) ? 
                result : 
                DateTime.Now;
        }
        private static decimal GetDecimalValue(this object value)
        {
            return value == null || value == DBNull.Value ?
                0 :
                decimal.TryParse(value.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out var result) ? result : 0;
        }
        private static double GetDoubleValue(this object value)
        {
            return value == null || value == DBNull.Value ?
                0 :
                double.TryParse(value.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out var result) ? result : 0;
        }
        private static int GetIntValue(this object value)
        {
            return value == null || value == DBNull.Value ?
                0 :
                int.TryParse(value.ToString(), out var result) ? result : 0;
        }
        private static int? GetIntNullableValue(this object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            return int.TryParse(value.ToString(), out var result) ? 
                result : 
                0;
        }
        private static string GetStringValue(this object value)
        {
            return value == null || value == DBNull.Value ? string.Empty : value.ToString();
        }
        private static ClientSettingsEnum GetClientSettingsEnumValue(this object value)
        {
            return value == null || value == DBNull.Value
                ? ClientSettingsEnum.NotSet
                : Enum.TryParse<ClientSettingsEnum>(value.ToString(), out var result) ? result : ClientSettingsEnum.NotSet;
        }
    }
}