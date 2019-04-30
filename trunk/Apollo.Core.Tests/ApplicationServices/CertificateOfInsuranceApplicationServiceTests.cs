// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class CertificateOfInsuranceApplicationServiceTests
    {

        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object
                , mockFileUploadApplicationService.Object);
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
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurance = new Mock<ICertificateOfInsurance>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsurance.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockCertificateOfInsuranceRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurance = new Mock<ICertificateOfInsurance>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsurance.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockCertificateOfInsuranceRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region GetForLabor Tests
        [Fact]
        public async void GetForLaborErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.GetForLaborAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.GetForLaborAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.GetForLaborAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetForLaborTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.GetForLaborAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<ICertificateOfInsurance> { Message = "Successful." }));

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.GetForLaborAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.GetForLaborAsync(It.IsAny<int>()));
        }
        #endregion GetForLabor Tests

        #region Get Tests
        [Fact]
        public async void GetErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurance = new Mock<ICertificateOfInsurance>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockCertificateOfInsurance.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);

            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurance = new Mock<ICertificateOfInsurance>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsurance.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockCertificateOfInsuranceRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<ICertificateOfInsurance> { Message = "Successful." }));

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.GetAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }
        #endregion Get Tests

        #region SaveAll Tests
        [Fact]
        public async void SaveAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurances = new List<ICertificateOfInsurance> { new Mock<ICertificateOfInsurance>().Object };
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ICertificateOfInsurance>>()))
                .Throws(new Exception());

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.SaveAllAsync(mockCertificateOfInsurances);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ICertificateOfInsurance>>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurances = new List<ICertificateOfInsurance> { new Mock<ICertificateOfInsurance>().Object };
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();
            
            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ICertificateOfInsurance>>()))
                .Returns(Task.FromResult(new SaveResponse<IReadOnlyList<ICertificateOfInsurance>> { Message = "Successful." }));

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.SaveAllAsync(mockCertificateOfInsurances);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.SaveAllAsync(It.IsAny<IReadOnlyList<ICertificateOfInsurance>>()));
        }
        #endregion SaveAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurance = new Mock<ICertificateOfInsurance>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.SaveAsync(It.IsNotNull<ICertificateOfInsurance>()))
                .Throws(new Exception());

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.SaveAsync(mockCertificateOfInsurance.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.SaveAsync(It.IsAny<ICertificateOfInsurance>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockCertificateOfInsuranceValidator = new Mock<ICertificateOfInsuranceValidator>();
            var mockCertificateOfInsuranceRepository = new Mock<ICertificateOfInsuranceRepository>();
            var mockCertificateOfInsurance = new Mock<ICertificateOfInsurance>();
            var mockFileUploadApplicationService = new Mock<IFileUploadApplicationService>();

            // Setup mock methods/properties
            mockCertificateOfInsuranceRepository.Setup(x => x.SaveAsync(It.IsNotNull<ICertificateOfInsurance>()))
                .Returns(Task.FromResult(new SaveResponse<ICertificateOfInsurance> { Message = "Successful." }));

            // Act
            var sut = new CertificateOfInsuranceApplicationService(
                mockLogManager.Object, mockCertificateOfInsuranceRepository.Object, mockCertificateOfInsuranceValidator.Object,
                mockFileUploadApplicationService.Object);
            var response = await sut.SaveAsync(mockCertificateOfInsurance.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockCertificateOfInsuranceRepository.Verify(x => x.SaveAsync(It.IsAny<ICertificateOfInsurance>()));
        }
        #endregion Save Tests
    }
}
