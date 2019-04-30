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
    public class AuditTaskApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
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
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTask = new Mock<IAuditTask>();

            // Setup mock methods/properties
            mockAuditTask.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTask = new Mock<IAuditTask>();

            // Setup mock methods/properties
            mockAuditTask.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.GetAllAsync())
                .Throws(new Exception());

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.GetAllAsync());

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IAuditTask>> { Message = "Successful." }));

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.GetAllAsync());
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTask = new Mock<IAuditTask>();

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockAuditTask.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTask = new Mock<IAuditTask>();

            // Setup mock methods/properties
            mockAuditTask.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IAuditTask> { Message = "Successful." }));

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTasks = new List<IAuditTask> { new Mock<IAuditTask>().Object };

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditTask>>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.SaveAllAsync(mockAuditTasks);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditTask>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTasks = new List<IAuditTask> { new Mock<IAuditTask>().Object };

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditTask>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IAuditTask>> { Message = "Successful." }));

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.SaveAllAsync(mockAuditTasks);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditTask>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTask = new Mock<IAuditTask>();

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditTask>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.SaveAsync(mockAuditTask.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditTask>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditTaskRepository = new Mock<IAuditTaskRepository>();
            var mockAuditTask = new Mock<IAuditTask>();

            // Setup mock methods/properties
            mockAuditTaskRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditTask>()))
                .Returns(Task.FromResult(new SaveResponse<IAuditTask> { Message = "Successful." }));

            // Act
            var sut = new AuditTaskApplicationService(
                mockLogManager.Object, mockAuditTaskRepository.Object);
            var response = await sut.SaveAsync(mockAuditTask.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditTaskRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditTask>()));
        }
        #endregion Save Tests
    }
}
