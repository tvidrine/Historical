// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class SalesApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var createResponse = await sut.CreateAsync();

            // Assert
            createResponse.IsSuccessful.Should().BeTrue();
            createResponse.Content.Should().NotBeNull();
        }
        #endregion Create Tests

        #region Delete Tests
        [Fact]
        public async void DeleteErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSales = new Mock<ISales>();

            // Setup mock methods/properties
            mockSales.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockSalesRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSales = new Mock<ISales>();

            // Setup mock methods/properties
            mockSales.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockSalesRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllSalesErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>()))
                .Throws(new Exception());

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }
        [Fact]
        public async void GetAllSalesVerificationErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.GetAllSalesVerificationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<SalesPeriodType>()))
                .Throws(new Exception());

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllSalesTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<ISales>> { Message = "Successful." }));

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.GetAllSalesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SalesPeriodType>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSales = new Mock<ISales>();

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockSales.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSales = new Mock<ISales>();

            // Setup mock methods/properties
            mockSales.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockSalesRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<ISales> { Message = "Successful." }));

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSaless = new List<ISales> { new Mock<ISales>().Object };

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ISales>>()))
                .Throws(new Exception());

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.SaveAllSalesAsync(mockSaless);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ISales>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSaless = new List<ISales> { new Mock<ISales>().Object };

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ISales>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<ISales>> { Message = "Successful." }));

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.SaveAllSalesAsync(mockSaless);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ISales>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSales = new Mock<ISales>();

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.SaveAsync(It.IsNotNull<ISales>()))
                .Throws(new Exception());

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.SaveAsync(mockSales.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.SaveAsync(It.IsAny<ISales>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockSalesValidator = new Mock<ISalesValidator>();
            var mockSalesRepository = new Mock<ISalesRepository>();
            var mockFileUploader = new Mock<IFileUploadApplicationService>();
            var mockSales = new Mock<ISales>();

            // Setup mock methods/properties
            mockSalesRepository.Setup(x => x.SaveAsync(It.IsNotNull<ISales>()))
                .Returns(Task.FromResult(new SaveResponse<ISales> { Message = "Successful." }));

            // Act
            var sut = new SalesApplicationService(
                mockLogManager.Object, mockSalesRepository.Object, mockSalesValidator.Object, mockFileUploader.Object);
            var response = await sut.SaveAsync(mockSales.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockSalesRepository.Verify(x => x.SaveAsync(It.IsAny<ISales>()));
        }
        #endregion Save Tests
    }
}
