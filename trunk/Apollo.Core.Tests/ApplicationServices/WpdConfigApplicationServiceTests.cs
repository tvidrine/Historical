// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class WpdConfigApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var createResponse = await sut.CreateAsync(It.IsAny<int>());

            // Assert
            createResponse.IsSuccessful.Should().BeTrue();
            createResponse.Content.Should().NotBeNull();
            createResponse.Content.Id.Should().NotBe(0);
        }
        #endregion Create Tests

        #region Delete Tests
        [Fact]
        public async void DeleteErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();
            var mockWpdConfig = new Mock<IWpdConfig>();

            // Setup mock methods/properties
            mockWpdConfig.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockWpdConfigRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockWpdConfigRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();
            var mockWpdConfig = new Mock<IWpdConfig>();

            // Setup mock methods/properties
            mockWpdConfig.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockWpdConfigRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockWpdConfigRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();
            var mockWpdConfig = new Mock<IWpdConfig>();

            // Setup mock methods/properties
            mockWpdConfigRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockWpdConfig.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockWpdConfigRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();
            var mockWpdConfig = new Mock<IWpdConfig>();

            // Setup mock methods/properties
            mockWpdConfig.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockWpdConfigRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IWpdConfig> { Message = "Successful." }));

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockWpdConfigRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();
            var mockWpdConfig = new Mock<IWpdConfig>();

            // Setup mock methods/properties
            mockWpdConfigRepository.Setup(x => x.SaveAsync(It.IsNotNull<IWpdConfig>()))
                .Throws(new Exception());

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var response = await sut.SaveAsync(mockWpdConfig.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockWpdConfigRepository.Verify(x => x.SaveAsync(It.IsAny<IWpdConfig>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockWpdConfigRepository = new Mock<IWpdConfigRepository>();
            var mockWpdConfig = new Mock<IWpdConfig>();

            // Setup mock methods/properties
            mockWpdConfigRepository.Setup(x => x.SaveAsync(It.IsNotNull<IWpdConfig>()))
                .Returns(Task.FromResult(new SaveResponse<IWpdConfig> { Message = "Successful." }));

            // Act
            var sut = new WpdConfigApplicationService(
                mockLogManager.Object, mockWpdConfigRepository.Object);
            var response = await sut.SaveAsync(mockWpdConfig.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockWpdConfigRepository.Verify(x => x.SaveAsync(It.IsAny<IWpdConfig>()));
        }
        #endregion Save Tests
    }
}
