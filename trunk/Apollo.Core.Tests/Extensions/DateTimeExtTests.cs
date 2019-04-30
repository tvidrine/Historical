// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Apollo.Core.Extensions;
using Xunit;

namespace Apollo.Core.Tests.Extensions
{
    public class DateTimeExtTests
    {
        [Theory]
        [InlineData("9/01/2018", "7/01/2018")]
        [InlineData("3/01/2018", "1/01/2018")]
        [InlineData("10/01/2018", "10/01/2018")]
        [InlineData("5/01/2018", "4/01/2018")]
        [InlineData("6/01/2018", "4/01/2018")]
        [InlineData("1/01/2018", "1/01/2018")]
        [InlineData("12/01/2018", "10/01/2018")]
        public void StartOfQuarterMethodWorks(string date, string expected)
        {
            var testDate = DateTime.Parse(date);
            var expectedDate = DateTime.Parse(expected);

            testDate.StartOfQuarter().Should().Be(expectedDate);
        }

        [Theory]
        [InlineData("9/01/2018", "9/30/2018")]
        [InlineData("3/01/2018", "3/31/2018")]
        [InlineData("10/01/2018", "12/31/2018")]
        [InlineData("5/01/2018", "6/30/2018")]
        [InlineData("6/01/2018", "6/30/2018")]
        [InlineData("1/01/2018", "3/31/2018")]
        [InlineData("12/01/2018", "12/31/2018")]
        public void EndOfQuarterMethodWorks(string date, string expected)
        {
            var testDate = DateTime.Parse(date);
            var expectedDate = DateTime.Parse(expected);

            testDate.EndOfQuarter().Should().Be(expectedDate);
        }

        [Theory]
        [InlineData("9/1/2018", 3)]
        [InlineData("2/1/2018", 1)]
        [InlineData("1/11/2018", 1)]
        [InlineData("11/1/2018", 4)]
        public void GetQuarterWorks(string date, int expected)
        {
            var testDate = DateTime.Parse(date);
            testDate.GetQuarter().Should().Be(expected);
        }
    }
}