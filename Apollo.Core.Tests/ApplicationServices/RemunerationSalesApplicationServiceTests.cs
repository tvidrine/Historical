// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class RemunerationSalesApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
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
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSales = new Mock<IRemunerationSales>();

            // Setup mock methods/properties
            mockRemunerationSales.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRemunerationSalesRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSales = new Mock<IRemunerationSales>();

            // Setup mock methods/properties
            mockRemunerationSales.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRemunerationSalesRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<RemunerationRequest>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IRemunerationSales>> { Message = "Successful." }));

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<RemunerationRequest>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSales = new Mock<IRemunerationSales>();

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockRemunerationSales.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSales = new Mock<IRemunerationSales>();

            // Setup mock methods/properties
            mockRemunerationSales.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRemunerationSalesRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IRemunerationSales> { Message = "Successful." }));

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSaless = new List<IRemunerationSales> { new Mock<IRemunerationSales>().Object };

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationSales>>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.SaveAllAsync(mockRemunerationSaless);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationSales>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSaless = new List<IRemunerationSales> { new Mock<IRemunerationSales>().Object };

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationSales>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IRemunerationSales>> { Message = "Successful." }));

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.SaveAllAsync(mockRemunerationSaless);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationSales>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSales = new Mock<IRemunerationSales>();

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.SaveAsync(It.IsNotNull<IRemunerationSales>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.SaveAsync(mockRemunerationSales.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.SaveAsync(It.IsAny<IRemunerationSales>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationSalesValidator = new Mock<IRemunerationSalesValidator>();
            var mockRemunerationSalesRepository = new Mock<IRemunerationSalesRepository>();
            var mockRemunerationSales = new Mock<IRemunerationSales>();

            // Setup mock methods/properties
            mockRemunerationSalesRepository.Setup(x => x.SaveAsync(It.IsNotNull<IRemunerationSales>()))
                .Returns(Task.FromResult(new SaveResponse<IRemunerationSales> { Message = "Successful." }));

            // Act
            var sut = new RemunerationSalesApplicationService(
                mockLogManager.Object, mockRemunerationSalesRepository.Object, mockRemunerationSalesValidator.Object);
            var response = await sut.SaveAsync(mockRemunerationSales.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationSalesRepository.Verify(x => x.SaveAsync(It.IsAny<IRemunerationSales>()));
        }
        #endregion Save Tests
    }
}
