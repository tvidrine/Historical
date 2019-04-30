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
    public class AuditStatusApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
           
            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
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
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatus = new Mock<IAuditStatus>();

            // Setup mock methods/properties
            mockAuditStatus.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditStatusRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatus = new Mock<IAuditStatus>();

            // Setup mock methods/properties
            mockAuditStatus.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditStatusRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.GetAllAsync())
                .Throws(new Exception());

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.GetAllAsync());

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IAuditStatus>> { Message = "Successful." }));

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.GetAllAsync());
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatus = new Mock<IAuditStatus>();

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockAuditStatus.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatus = new Mock<IAuditStatus>();

            // Setup mock methods/properties
            mockAuditStatus.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditStatusRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IAuditStatus> { Message = "Successful." }));

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatuss = new List<IAuditStatus> { new Mock<IAuditStatus>().Object };

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStatus>>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.SaveAllAsync(mockAuditStatuss);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStatus>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatuss = new List<IAuditStatus> { new Mock<IAuditStatus>().Object };

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStatus>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IAuditStatus>> { Message = "Successful." }));

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.SaveAllAsync(mockAuditStatuss);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAuditStatus>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatus = new Mock<IAuditStatus>();

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditStatus>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.SaveAsync(mockAuditStatus.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditStatus>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditStatusRepository = new Mock<IAuditStatusRepository>();
            var mockAuditStatus = new Mock<IAuditStatus>();

            // Setup mock methods/properties
            mockAuditStatusRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAuditStatus>()))
                .Returns(Task.FromResult(new SaveResponse<IAuditStatus> { Message = "Successful." }));

            // Act
            var sut = new AuditStatusApplicationService(
                mockLogManager.Object, mockAuditStatusRepository.Object);
            var response = await sut.SaveAsync(mockAuditStatus.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditStatusRepository.Verify(x => x.SaveAsync(It.IsAny<IAuditStatus>()));
        }
        #endregion Save Tests
    }
}
