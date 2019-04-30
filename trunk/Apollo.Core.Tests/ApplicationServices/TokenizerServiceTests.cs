// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/20/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Domain.Audit;
using Moq;
using FluentAssertions;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class TokenizerServiceTests
    {
        [Fact]
        public void GetValueReturnsCorrectValue()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var testObject = new Audit {InvoiceId = 5};

            // Act
            var sut = new TokenizerService(mockLogManager.Object);
            var response = sut.GetValue(testObject, "InvoiceId");

            // Assert
            response.Content.Should().NotBeNull();
            response.Content.Should().Be(5);
        }

        [Fact]
        public void GetGenericValueReturnsCorrectValue()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var testObject = new Audit { InvoiceId = 5 };

            // Act
            var sut = new TokenizerService(mockLogManager.Object);
            var response = sut.GetValue<int>(testObject, "InvoiceId");

            // Assert
            response.Content.Should().BeOfType(typeof(int));
            response.Content.Should().Be(5);
        }

        [Fact]
        public void GetValuesReturnsCorrectValues()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var testObject = new Audit { Id = 2, InvoiceId = 5 };
            var testFields = new List<string> { "InvoiceId", "Id" };

            // Act
            var sut = new TokenizerService(mockLogManager.Object);
            var response = sut.GetValues(testObject,testFields);

            // Assert
            response.Content["Id"].Should().Be(2);
            response.Content["InvoiceId"].Should().Be(5);

        }
    }
}