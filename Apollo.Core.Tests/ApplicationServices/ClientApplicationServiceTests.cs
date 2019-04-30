// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class ClientApplicationServiceTests
    {
        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();
	        var mockAddressCreateResponse = new Mock<ICreateResponse<IAddress>>();
	        var mockContactCreateResponse = new Mock<ICreateResponse<IContact>>();

			// Setup mock methods/ properties
			mockAddressCreateResponse.Setup(x => x.IsSuccessful)
		        .Returns(true);
	        mockContactCreateResponse.Setup(x => x.IsSuccessful)
		        .Returns(true);
			mockAddressApplicationService.Setup(x => x.CreateAsync())
		        .Returns(Task.FromResult(mockAddressCreateResponse.Object));
	        mockContactApplicationService.Setup(x => x.CreateAsync(It.IsAny<int>(), It.IsNotNull<ContactTypeEnum>()))
		        .Returns(Task.FromResult(mockContactCreateResponse.Object));

			// Act
			var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var createResponse = await sut.CreateAsync();

            // Assert
	        createResponse.IsSuccessful.Should().BeTrue();
            createResponse.Content.Should().NotBeNull();
            createResponse.Content.Id.Should().NotBe(0);

	        // Verify the application service is calling the dependent services
			mockAddressApplicationService.Verify(x => x.CreateAsync());
			mockContactApplicationService.Verify(x => x.CreateAsync(It.IsAny<int>(), It.IsNotNull<ContactTypeEnum>()));

		}
		#endregion Create Test

		#region Delete Tests
		[Fact]
        public async void DeleteErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockClient = new Mock<IClient>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClient.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClientRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockClient = new Mock<IClient>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClient.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClientRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful."}));
            
            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());


            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.GetAllAsync())
                .Throws(new Exception());

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.GetAllAsync();


            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.GetAllAsync());

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IClient>> { Message = "Successful." }));

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.GetAllAsync());

        }
        #endregion GetAll Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockClient = new Mock<IClient>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockClient.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockClient = new Mock<IClient>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IClient> { Message = "Successful." }));
            mockClient.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.GetAsync(It.IsAny<int>());


            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.SaveAsync(It.IsAny<IClient>()))
                .Throws(new Exception());
            mockClientValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult()));

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.SaveAllAsync(new List<IClient> { new Client() });


            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling dependent methods
            mockClientRepository.Verify(x => x.SaveAsync(It.IsAny<IClient>()));
            mockClientValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None));
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.SaveAsync(It.IsAny<IClient>()))
                .Returns(Task.FromResult(new SaveResponse<IClient> { Message = "Successful." }));
            mockClientValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult()));
            mockAddressApplicationService.Setup(x => x.SaveAsync(It.IsAny<IAddress>()))
                .Returns(Task.FromResult(new SaveResponse<IAddress> { Message = "Successful." }));
            mockContactApplicationService.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IContact>> { Message = "Successful." }));

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.SaveAllAsync(new List<IClient> { new Client() });


            // Assert

            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling dependent methods
            mockClientRepository.Verify(x => x.SaveAsync(It.IsAny<IClient>()));
            mockClientValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None));
            mockAddressApplicationService.Verify(x => x.SaveAsync(It.IsAny<IAddress>()));
            mockContactApplicationService.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockClient = new Mock<IClient>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();


            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.SaveAsync(It.IsNotNull<IClient>()))
                .Throws(new Exception());
            mockClient.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockClientValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult()));

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.SaveAsync(mockClient.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockClientRepository.Verify(x => x.SaveAsync(It.IsAny<IClient>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockClientRepository = new Mock<IClientRepository>();
            var mockClientValidator = new Mock<IClientValidator>();
            var mockClient = new Mock<IClient>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();
            var mockContactApplicationService = new Mock<IContactApplicationService>();

            // Setup mock methods/ properties
            mockClientRepository.Setup(x => x.SaveAsync(It.IsAny<IClient>()))
                .Returns(Task.FromResult(new SaveResponse<IClient> { Message = "Successful." }));
            mockClientValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult()));
            mockAddressApplicationService.Setup(x => x.SaveAsync(It.IsAny<IAddress>()))
                .Returns(Task.FromResult(new SaveResponse<IAddress> { Message = "Successful." }));
            mockContactApplicationService.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IContact>> { Message = "Successful." }));

            // Act
            var sut = new ClientApplicationService(
                mockLogManager.Object, mockClientRepository.Object, mockClientValidator.Object, mockAddressApplicationService.Object, mockContactApplicationService.Object);
            var response = await sut.SaveAsync(mockClient.Object);

            // Verify correct response
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling dependent methods
            mockClientRepository.Verify(x => x.SaveAsync(It.IsAny<IClient>()));
			mockClientValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IClient>(), CancellationToken.None));
			mockAddressApplicationService.Verify(x => x.SaveAsync(It.IsAny<IAddress>()));
	        mockContactApplicationService.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()));
		}
        #endregion Save Tests
    }
}