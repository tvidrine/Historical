// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class PayrollLimitApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
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
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimit = new Mock<IPayrollLimit>();

            // Setup mock methods/properties
            mockPayrollLimit.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockPayrollLimitRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimit = new Mock<IPayrollLimit>();

            // Setup mock methods/properties
            mockPayrollLimit.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockPayrollLimitRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.GetAllAsync(It.IsAny<PayrollLimitRequest>()))
                .Throws(new Exception());

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<PayrollLimitRequest>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.GetAllAsync(It.IsAny<PayrollLimitRequest>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.GetAllAsync(It.IsAny<PayrollLimitRequest>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IPayrollLimit>> { Message = "Successful." }));

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<PayrollLimitRequest>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.GetAllAsync(It.IsAny<PayrollLimitRequest>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimit = new Mock<IPayrollLimit>();

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockPayrollLimit.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimit = new Mock<IPayrollLimit>();

            // Setup mock methods/properties
            mockPayrollLimit.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockPayrollLimitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IPayrollLimit> { Message = "Successful." }));

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimits = new List<IPayrollLimit> { new Mock<IPayrollLimit>().Object };

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IPayrollLimit>>()))
                .Throws(new Exception());

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.SaveAllAsync(mockPayrollLimits);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IPayrollLimit>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimits = new List<IPayrollLimit> { new Mock<IPayrollLimit>().Object };

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IPayrollLimit>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IPayrollLimit>> { Message = "Successful." }));

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.SaveAllAsync(mockPayrollLimits);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IPayrollLimit>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimit = new Mock<IPayrollLimit>();

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.SaveAsync(It.IsNotNull<IPayrollLimit>()))
                .Throws(new Exception());

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.SaveAsync(mockPayrollLimit.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.SaveAsync(It.IsAny<IPayrollLimit>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockPayrollLimitValidator = new Mock<IPayrollLimitValidator>();
            var mockPayrollLimitRepository = new Mock<IPayrollLimitRepository>();
            var mockPayrollLimit = new Mock<IPayrollLimit>();

            // Setup mock methods/properties
            mockPayrollLimitRepository.Setup(x => x.SaveAsync(It.IsNotNull<IPayrollLimit>()))
                .Returns(Task.FromResult(new SaveResponse<IPayrollLimit> { Message = "Successful." }));

            // Act
            var sut = new PayrollLimitApplicationService(
                mockLogManager.Object, mockPayrollLimitRepository.Object, mockPayrollLimitValidator.Object);
            var response = await sut.SaveAsync(mockPayrollLimit.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockPayrollLimitRepository.Verify(x => x.SaveAsync(It.IsAny<IPayrollLimit>()));
        }
        #endregion Save Tests
    }
}
