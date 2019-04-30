// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class AuditStepApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var createResponse = await sut.CreateAsync(It.IsAny<int>());

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
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditStep = new Mock<IAuditStep>();

            // Setup mock methods/properties
            mockAuditStep.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditStepRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditStep = new Mock<IAuditStep>();

            // Setup mock methods/properties
            mockAuditStep.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditStepRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IAuditStep>> { Message = "Successful." }));

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditStep = new Mock<IAuditStep>();

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockAuditStep.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditStep = new Mock<IAuditStep>();

            // Setup mock methods/properties
            mockAuditStep.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditStepRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IAuditStep> { Message = "Successful." }));

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditSteps = new List<IAuditStep> { new Mock<IAuditStep>().Object };

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStep>>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.SaveAllAsync(mockAuditSteps);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStep>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditSteps = new List<IAuditStep> { new Mock<IAuditStep>().Object };

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStep>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IAuditStep>> { Message = "Successful." }));

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.SaveAllAsync(mockAuditSteps);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStep>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditStep = new Mock<IAuditStep>();

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditStep>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.SaveAsync(mockAuditStep.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditStep>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStepValidator = new Mock<IAuditStepValidator>();
            var mockAuditStepRepository = new Mock<IAuditStepRepository>();
            var mockAuditStep = new Mock<IAuditStep>();

            // Setup mock methods/properties
            mockAuditStepRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditStep>()))
                .Returns(Task.FromResult(new SaveResponse<IAuditStep> { Message = "Successful." }));

            // Act
            var sut = new AuditStepApplicationService(
                mockLogManager.Object, mockAuditStepRepository.Object, mockAuditStepValidator.Object);
            var response = await sut.SaveAsync(mockAuditStep.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStepRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditStep>()));
        }
        #endregion Save Tests
    }
}
