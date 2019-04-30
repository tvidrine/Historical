// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class AuditApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var createResponse = await sut.CreateAsync(It.IsAny<AuditRequest>());

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
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();
            var mockAudit = new Mock<IAudit>();

            // Setup mock methods/properties
            mockAudit.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();
            var mockAudit = new Mock<IAudit>();

            // Setup mock methods/properties
            mockAudit.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();

            // Setup mock methods/properties
            mockAuditRepository.Setup(x => x.GetAllAsync(false))
                .Throws(new Exception());

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.GetAllAsync(false));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();

            // Setup mock methods/properties
            mockAuditRepository.Setup(x => x.GetAllAsync(false))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IAudit>> { Message = "Successful." }));

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.GetAllAsync(false));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();
            var mockAudit = new Mock<IAudit>();

            // Setup mock methods/properties
            mockAuditRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockAudit.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();
            var mockAudit = new Mock<IAudit>();

            // Setup mock methods/properties
            mockAudit.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAuditRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IAudit> { Message = "Successful." }));

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();
            var mockAudit = new Mock<IAudit>();

            // Setup mock methods/properties
            mockAuditRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAudit>()))
                .Throws(new Exception());

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.SaveAsync(mockAudit.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.SaveAsync(It.IsAny<IAudit>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAuditRepository = new Mock<IAuditRepository>();
            var mockPolicyApplicationService = new Mock<IPolicyApplicationService>();
            var mockClassCodeApplicationService = new Mock<IClassCodeApplicationService>();
            var mockAudit = new Mock<IAudit>();

            // Setup mock methods/properties
            mockAuditRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAudit>()))
                .Returns(Task.FromResult(new SaveResponse<IAudit> { Message = "Successful." }));

            // Act
            var sut = new AuditApplicationService(
                mockLogManager.Object, mockAuditRepository.Object, mockPolicyApplicationService.Object, mockClassCodeApplicationService.Object);
            var response = await sut.SaveAsync(mockAudit.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAuditRepository.Verify(x => x.SaveAsync(It.IsAny<IAudit>()));
        }
        #endregion Save Tests
    }
}
