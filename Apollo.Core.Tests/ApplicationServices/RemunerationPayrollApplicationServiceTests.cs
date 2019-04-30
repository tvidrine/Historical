// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/21/2018
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
    public class RemunerationPayrollApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
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
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemuneration = new Mock<IRemunerationPayroll>();

            // Setup mock methods/properties
            mockRemuneration.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRemunerationRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemuneration = new Mock<IRemunerationPayroll>();

            // Setup mock methods/properties
            mockRemuneration.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRemunerationRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<RemunerationRequest>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IRemunerationPayroll>> { Message = "Successful." }));

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<RemunerationRequest>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.GetAllAsync(It.IsAny<RemunerationRequest>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemuneration = new Mock<IRemunerationPayroll>();

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockRemuneration.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemuneration = new Mock<IRemunerationPayroll>();

            // Setup mock methods/properties
            mockRemuneration.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRemunerationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IRemunerationPayroll> { Message = "Successful." }));

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemunerations = new List<IRemunerationPayroll> { new Mock<IRemunerationPayroll>().Object };

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationPayroll>>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.SaveAllAsync(mockRemunerations);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationPayroll>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemunerations = new List<IRemunerationPayroll> { new Mock<IRemunerationPayroll>().Object };

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationPayroll>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IRemunerationPayroll>> { Message = "Successful." }));

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.SaveAllAsync(mockRemunerations);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IRemunerationPayroll>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemuneration = new Mock<IRemunerationPayroll>();

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.SaveAsync(It.IsNotNull<IRemunerationPayroll>()))
                .Throws(new Exception());

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.SaveAsync(mockRemuneration.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.SaveAsync(It.IsAny<IRemunerationPayroll>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRemunerationValidator = new Mock<IRemunerationPayrollValidator>();
            var mockRemunerationRepository = new Mock<IRemunerationRepository>();
            var mockRemuneration = new Mock<IRemunerationPayroll>();

            // Setup mock methods/properties
            mockRemunerationRepository.Setup(x => x.SaveAsync(It.IsNotNull<IRemunerationPayroll>()))
                .Returns(Task.FromResult(new SaveResponse<IRemunerationPayroll> { Message = "Successful." }));

            // Act
            var sut = new RemunerationPayrollApplicationService(
                mockLogManager.Object, mockRemunerationRepository.Object, mockRemunerationValidator.Object);
            var response = await sut.SaveAsync(mockRemuneration.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRemunerationRepository.Verify(x => x.SaveAsync(It.IsAny<IRemunerationPayroll>()));
        }
        #endregion Save Tests
    }
}
