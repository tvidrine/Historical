// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using FluentAssertions;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class ClassCodeApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();

            // Act
            var sut = new ClassCodeApplicationService(
                mockLogManager.Object, mockClassCodeValidator.Object, mockClassCodeRepository.Object);
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
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCode = new Mock<IClassCode>();

            // Setup mock methods/properties
            mockClassCode.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClassCodeRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCode = new Mock<IClassCode>();

            // Setup mock methods/properties
            mockClassCode.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClassCodeRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.GetAllAsync())
                .Throws(new Exception());

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.GetAllAsync());

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IClassCode>> { Message = "Successful." }));

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.GetAllAsync());
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCode = new Mock<IClassCode>();

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockClassCode.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCode = new Mock<IClassCode>();

            // Setup mock methods/properties
            mockClassCode.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClassCodeRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IClassCode> { Message = "Successful." }));

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCodes = new List<IClassCode> { new Mock<IClassCode>().Object };

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClassCode>>()))
                .Throws(new Exception());

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.SaveAllAsync(mockClassCodes);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClassCode>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCodes = new List<IClassCode> { new Mock<IClassCode>().Object };

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClassCode>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IClassCode>> { Message = "Successful." }));

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.SaveAllAsync(mockClassCodes);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IClassCode>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCode = new Mock<IClassCode>();

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.SaveAsync(It.IsNotNull<IClassCode>()))
                .Throws(new Exception());

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.SaveAsync(mockClassCode.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.SaveAsync(It.IsAny<IClassCode>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClassCodeValidator = new Mock<IClassCodeValidator>();
            var mockClassCodeRepository = new Mock<IClassCodeRepository>();
            var mockClassCode = new Mock<IClassCode>();

            // Setup mock methods/properties
            mockClassCodeRepository.Setup(x => x.SaveAsync(It.IsNotNull<IClassCode>()))
                .Returns(Task.FromResult(new SaveResponse<IClassCode> { Message = "Successful." }));

            // Act
            var sut = new ClassCodeApplicationService(mockLogManager.Object,mockClassCodeValidator.Object, mockClassCodeRepository.Object);
            var response = await sut.SaveAsync(mockClassCode.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClassCodeRepository.Verify(x => x.SaveAsync(It.IsAny<IClassCode>()));
        }
        #endregion Save Tests
    }
}
