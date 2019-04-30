// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class AddressApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.CreateAsync();

            // Assert
            response.Content.Should().NotBeNull();
            response.Content.Id.Should().NotBe(0);
        }
        #endregion Create Tests

        #region Delete Tests
        [Fact]
        public async void DeleteErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddress = new Mock<IAddress>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddress.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAddressRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddress = new Mock<IAddress>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddress.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAddressRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse {  Message = "Successful." }));

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IAddress>> { Message = "Successful." }));

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.GetAllAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));
        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddress = new Mock<IAddress>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockAddress.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddress = new Mock<IAddress>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddress.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockAddressRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IAddress> { Message = "Successful." }));

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddresses = new List<IAddress> { new Mock<IAddress>().Object };
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.SaveAllAsync(It.IsNotNull<IReadOnlyList<IAddress>>()))
                .Throws(new Exception());
	        mockAddressValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IAddress>(), CancellationToken.None))
		        .Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.SaveAllAsync(mockAddresses);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAddress>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
			var mockAddresses = new List<IAddress>{ new Mock<IAddress>().Object};
			var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAddress>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IAddress>> { Message = "Successful." }));
            mockAddressValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IAddress>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult()));

            // Act
            var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.SaveAllAsync(mockAddresses);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IAddress>>()));

            // Verify the application service is calling the validator
            mockAddressValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IAddress>(), CancellationToken.None));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddress = new Mock<IAddress>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAddress>()))
                .Throws(new Exception());
	        mockAddressValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IAddress>(), CancellationToken.None))
		        .Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.SaveAsync(mockAddress.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.SaveAsync(It.IsAny<IAddress>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockAddress = new Mock<IAddress>();
            var mockAddressValidator = new Mock<IAddressValidator>();

            // Setup mock methods/properties
            mockAddressRepository.Setup(x => x.SaveAsync(It.IsNotNull<IAddress>()))
                .Returns(Task.FromResult(new SaveResponse<IAddress> { Message = "Successful." }));
			mockAddressValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IAddress>(), CancellationToken.None))
				.Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new AddressApplicationService(
                mockLogManager.Object, mockAddressRepository.Object, mockAddressValidator.Object);
            var response = await sut.SaveAsync(mockAddress.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockAddressRepository.Verify(x => x.SaveAsync(It.IsAny<IAddress>()));

			// Verify the application service is calling the validator
			mockAddressValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IAddress>(), CancellationToken.None));
		}
        #endregion Save Tests
    }
}
