// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Apollo.Core.Extensions
{
    public static class DateTimeExt
    {
        public static DateTimeOffset StartOfMonth(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, date.Offset);
        }
        public static DateTimeOffset EndOfMonth(this DateTimeOffset date)
        {
            return date.StartOfMonth().AddMonths(1).AddSeconds(-1);
        }
        public static DateTime EndOfQuarter(this DateTime date)
        {
            var quarter = date.GetQuarter();
            var month = quarter * 3;

            return new DateTime(date.Year, month, DateTime.DaysInMonth(date.Year, month));
        }

        public static DateTimeOffset EndOfQuarter(this DateTimeOffset date)
        {
            return date.DateTime.EndOfQuarter();
        }
        public static DateTime StartOfQuarter(this DateTime date)
        {
            var quarter = date.GetQuarter();
            var month = quarter * 3 - 2;
            return new DateTime(date.Year,month, 1);
        }

        public static DateTimeOffset StartOfQuarter(this DateTimeOffset date)
        {
            return date.DateTime.StartOfQuarter();
        }
        public static int GetQuarter(this DateTime date)
        {
            return (int) Math.Ceiling(date.Month / 3.0);
        }

        public static List<Tuple<DateTime, DateTime>> GetQuarters(DateTime startDate, DateTime endDate)
        {
            var list = new List<Tuple<DateTime, DateTime>>();

            var firstQuarterDate = startDate.StartOfQuarter();
            var lastQuarterDate = endDate.StartOfQuarter();

            for (var currentQuarterDate = firstQuarterDate;
                currentQuarterDate <= lastQuarterDate;
                currentQuarterDate = currentQuarterDate.AddMonths(3))
            {
                list.Add(new Tuple<DateTime, DateTime>(currentQuarterDate, currentQuarterDate.EndOfQuarter()));
            }
            return list;
        }
    }
}