// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class ClientSettingApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
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
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSetting = new Mock<IClientSetting>();

            // Setup mock methods/properties
            mockClientSetting.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClientSettingRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSetting = new Mock<IClientSetting>();

            // Setup mock methods/properties
            mockClientSetting.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClientSettingRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IClientSetting>> { Message = "Successful." }));

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSetting = new Mock<IClientSetting>();

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockClientSetting.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSetting = new Mock<IClientSetting>();

            // Setup mock methods/properties
            mockClientSetting.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClientSettingRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IClientSetting> { Message = "Successful." }));

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSettings = new List<IClientSetting> { new Mock<IClientSetting>().Object };

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClientSetting>>()))
                .Throws(new Exception());

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.SaveAllAsync(mockClientSettings);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClientSetting>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSettings = new List<IClientSetting> { new Mock<IClientSetting>().Object };

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClientSetting>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IClientSetting>> { Message = "Successful." }));

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.SaveAllAsync(mockClientSettings);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClientSetting>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSetting = new Mock<IClientSetting>();

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.SaveAsync(It.IsNotNull<IClientSetting>()))
                .Throws(new Exception());

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.SaveAsync(mockClientSetting.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.SaveAsync(It.IsAny<IClientSetting>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientSettingValidator = new Mock<IClientSettingValidator>();
            var mockClientSettingRepository = new Mock<IClientSettingRepository>();
            var mockClientSetting = new Mock<IClientSetting>();

            // Setup mock methods/properties
            mockClientSettingRepository.Setup(x => x.SaveAsync(It.IsNotNull<IClientSetting>()))
                .Returns(Task.FromResult(new SaveResponse<IClientSetting> { Message = "Successful." }));

            // Act
            var sut = new ClientSettingApplicationService(
                mockLogManager.Object, mockClientSettingRepository.Object, mockClientSettingValidator.Object);
            var response = await sut.SaveAsync(mockClientSetting.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientSettingRepository.Verify(x => x.SaveAsync(It.IsAny<IClientSetting>()));
        }
        #endregion Save Tests
    }
}
