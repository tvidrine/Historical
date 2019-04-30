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
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Common;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
	public class ContactApplicationServiceTests
	{

		#region Create Tests
		[Fact]
		public async void CreateTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Act
            var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.CreateAsync(It.IsAny<int>(),It.IsNotNull<ContactTypeEnum>());

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
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContact = new Mock<IContact>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContact.Setup(c => c.Id).Returns(It.IsAny<int>());
			mockContactRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
				.Throws(new Exception());

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.DeleteAsync(It.IsAny<int>());

			// Assert
			response.IsSuccessful.Should().BeFalse();
		    response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

			// Verify the application service is logging the error.
			mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
		}

		[Fact]
		public async void DeleteTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContact = new Mock<IContact>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContact.Setup(c => c.Id).Returns(It.IsAny<int>());
			mockContactRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.DeleteAsync(It.IsAny<int>());

			// Assert
			response.IsSuccessful.Should().BeTrue();
		    response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
		}
		#endregion Delete Tests

		#region GetAll Tests
		[Fact]
		public async void GetAllErrorTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContactRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
				.Throws(new Exception());

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.GetAllAsync(It.IsAny<int>());

			// Assert
			response.IsSuccessful.Should().BeFalse();
		    response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));

			// Verify the application service is logging the error.
			mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
		}

		[Fact]
		public async void GetAllTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContactRepository.Setup(x => x.GetAllAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(new GetResponse<IReadOnlyList<IContact>> { Message = "Successful." }));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.GetAllAsync(It.IsAny<int>());

			// Assert
			response.IsSuccessful.Should().BeTrue();
		    response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.GetAllAsync(It.IsAny<int>()));
		}
		#endregion GetAll Tests

		#region Get Tests
		[Fact]
		public async void GetErrorTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContact = new Mock<IContact>();
			var mockContactValidator = new Mock<IContactValidator>();
            var mockAddressApplicationService = new Mock<IAddressApplicationService>();

			// Setup mock methods/properties
			mockContactRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
				.Throws(new Exception());
			mockContact.Setup(c => c.Id).Returns(It.IsAny<int>());

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.GetAsync(It.IsAny<int>());

			// Assert
			response.IsSuccessful.Should().BeFalse();
		    response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

			// Verify the application service is logging the error.
			mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
		}

		[Fact]
		public async void GetTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContact = new Mock<IContact>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContact.Setup(c => c.Id).Returns(It.IsAny<int>());
			mockContactRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(new GetResponse<IContact> { Message = "Successful." }));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.GetAsync(It.IsAny<int>());

			// Assert
			response.IsSuccessful.Should().BeTrue();
		    response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
		}
		#endregion Get Tests

		#region SaveAll Tests
		[Fact]
		public async void SaveAllErrorTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContacts = new List<IContact> { new Mock<IContact>().Object };
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContactRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()))
				.Throws(new Exception());
			mockContactValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IContact>(), CancellationToken.None))
				.Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.SaveAllAsync(mockContacts);

			// Assert
			response.IsSuccessful.Should().BeFalse();
		    response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()));

			// Verify the application service is logging the error.
			mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
		}

		[Fact]
		public async void SaveAllTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContacts = new List<IContact> { new Mock<IContact>().Object };
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContactRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()))
				.Returns(Task.FromResult(new SaveResponse<IReadOnlyList<IContact>> { Message = "Successful." }));
			mockContactValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IContact>(), CancellationToken.None))
				.Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.SaveAllAsync(mockContacts);

			// Assert
			response.IsSuccessful.Should().BeTrue();
		    response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<IContact>>()));

			// Verify the application service is calling the validator
			mockContactValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IContact>(), CancellationToken.None));
		}
		#endregion SaveAll Tests

		#region Save Tests
		[Fact]
		public async void SaveErrorTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContact = new Mock<IContact>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContactRepository.Setup(x => x.SaveAsync(It.IsNotNull<IContact>()))
				.Throws(new Exception());
			mockContactValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IContact>(), CancellationToken.None))
				.Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object, mockAddressApplicationService.Object);
			var response = await sut.SaveAsync(mockContact.Object);

			// Assert
			response.IsSuccessful.Should().BeFalse();
		    response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.SaveAsync(It.IsAny<IContact>()));

			// Verify the application service is logging the error.
			mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
		}

		[Fact]
		public async void SaveTest()
		{
			// Arrange
			var mockLogManager = new Mock<ILogManager>();
			var mockContactRepository = new Mock<IContactRepository>();
			var mockContact = new Mock<IContact>();
			var mockContactValidator = new Mock<IContactValidator>();
		    var mockAddressApplicationService = new Mock<IAddressApplicationService>();

            // Setup mock methods/properties
            mockContactRepository.Setup(x => x.SaveAsync(It.IsNotNull<IContact>()))
				.Returns(Task.FromResult(new SaveResponse<IContact> { Message = "Successful." }));
			mockContactValidator.Setup(x => x.ValidateAsync(It.IsNotNull<IContact>(), CancellationToken.None))
				.Returns(Task.FromResult(new ValidationResult()));

			// Act
			var sut = new ContactApplicationService(
				mockLogManager.Object, mockContactRepository.Object, mockContactValidator.Object,mockAddressApplicationService.Object);
			var response = await sut.SaveAsync(mockContact.Object);

			// Assert
			response.IsSuccessful.Should().BeTrue();
		    response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

			// Verify the application service is calling the correct repository method.
			mockContactRepository.Verify(x => x.SaveAsync(It.IsAny<IContact>()));

			// Verify the application service is calling the validator
			mockContactValidator.Verify(x => x.ValidateAsync(It.IsNotNull<IContact>(), CancellationToken.None));
		}
		#endregion Save Tests
	}
}
