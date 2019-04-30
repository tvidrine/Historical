// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class AuditEventApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
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
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvent = new Mock<IAuditEvent>();

            // Setup mock methods/properties
            mockAuditEvent.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditEventRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvent = new Mock<IAuditEvent>();

            // Setup mock methods/properties
            mockAuditEvent.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditEventRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.GetAllAsync())
                .Throws(new Exception());

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.GetAllAsync());

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IAuditEvent>> { Message = "Successful." }));

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.GetAllAsync());
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvent = new Mock<IAuditEvent>();

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockAuditEvent.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvent = new Mock<IAuditEvent>();

            // Setup mock methods/properties
            mockAuditEvent.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IAuditEvent> { Message = "Successful." }));

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvents = new List<IAuditEvent> { new Mock<IAuditEvent>().Object };

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditEvent>>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.SaveAllAsync(mockAuditEvents);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditEvent>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvents = new List<IAuditEvent> { new Mock<IAuditEvent>().Object };

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditEvent>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IAuditEvent>> { Message = "Successful." }));

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.SaveAllAsync(mockAuditEvents);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditEvent>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvent = new Mock<IAuditEvent>();

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditEvent>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.SaveAsync(mockAuditEvent.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditEvent>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditEventRepository = new Mock<IAuditEventRepository>();
            var mockAuditEvent = new Mock<IAuditEvent>();

            // Setup mock methods/properties
            mockAuditEventRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditEvent>()))
                .Returns(Task.FromResult(new SaveResponse<IAuditEvent> { Message = "Successful." }));

            // Act
            var sut = new AuditEventApplicationService(
                mockLogManager.Object, mockAuditEventRepository.Object);
            var response = await sut.SaveAsync(mockAuditEvent.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditEventRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditEvent>()));
        }
        #endregion Save Tests
    }
}
