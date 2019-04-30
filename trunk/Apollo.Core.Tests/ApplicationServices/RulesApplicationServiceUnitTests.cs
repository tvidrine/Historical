// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Base;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Contracts.DomainServices.Rules;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Rule;
using Apollo.Core.DomainServices.Rules;
using Apollo.Core.Messages.Responses;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class RulesApplicationServiceUnitTests 
    {
        #region Create Tests
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object,mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var createResponse = await sut.CreateAsync();

            // Assert
            createResponse.IsSuccessful.Should().BeTrue();
            createResponse.Content.Should().NotBeNull();
        }
        #endregion Create Tests

        #region CreateAssembly Tests
        [Fact]
        public void CreatePublishedAssemblyTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var testRuleSet = CreateTestRuleSet();

            mockRuleSetTranslator.Setup(x => x.Translate(It.IsAny<IRuleSet>(), true))
                .Returns(It.IsAny<string>());
            mockRuleAssemblyService.Setup(x => x.CreateAssembly(It.IsAny<IRuleSet>()))
                .Returns(It.IsAny<byte[]>());

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            
            var response = sut.CreateAssembly(testRuleSet, true);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);

            mockRuleSetTranslator.Verify(x =>x.Translate(It.IsAny<IRuleSet>(), true));
            mockRuleAssemblyService.Verify(x => x.CreateAssembly(It.IsAny<IRuleSet>()));

        }
        #endregion CreateAssembly Tests

        #region Delete Tests
        [Fact]
        public async void DeleteErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSet = new Mock<IRuleSet>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();

            // Setup mock methods/properties
            mockRuleSet.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRuleSetRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSet = new Mock<IRuleSet>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();

            // Setup mock methods/properties
            mockRuleSet.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRuleSetRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new DeleteResponse { Message = "Successful." }));

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.DeleteAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()));
        }
        #endregion Delete Tests

        #region Execute Tests
        [Fact]
        public void ExecuteRuleTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var ruleSetAssemblyService = new RuleSetAssemblyService(mockLogManager.Object);

            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, ruleSetAssemblyService);

            // Act
            var ruleSet = new RuleSet
            {
                Name = "TestRule",
                Code = $@"
                using System;

                namespace {Constants.RuleNamespace}
                {{
                    public class TestRule
                    {{
                        public int TestExecute()
                        {{
                            return 1;
                        }}
                    }}
                }}",
                Rules = new List<IRule>
                {
                    new Rule{ Name = "TestExecute"}
                }
            };
            var result = (int)sut.ExecuteRule(ruleSet, "TestExecute");

            // Assert
            result.Should().Be(1);

        }
        #endregion Execute Tests

        #region Get Tests
        [Fact]
        public async void GetByIdErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var mockRuleSet = new Mock<IRuleSet>();

            // Setup mock methods/properties
            mockRuleSetRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws(new Exception());
            mockRuleSet.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.GetByIdAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetByIdTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var mockRuleSet = new Mock<IRuleSet>();

            // Setup mock methods/properties
            mockRuleSet.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRuleSetRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new GetResponse<IRuleSet> { Message = "Successful." }));

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.GetByIdAsync(It.IsAny<int>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()));
        }

        [Fact]
        public async void GetByNameErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var mockRuleSet = new Mock<IRuleSet>();

            // Setup mock methods/properties
            mockRuleSetRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Throws(new Exception());
            mockRuleSet.Setup(c => c.Id).Returns(It.IsAny<int>());

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object,mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.GetByNameAsync(It.IsAny<string>());

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.GetByNameAsync(It.IsAny<string>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetByNameTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var mockRuleSet = new Mock<IRuleSet>();

            // Setup mock methods/properties
            mockRuleSet.Setup(c => c.Id).Returns(It.IsAny<int>());
            mockRuleSetRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new GetResponse<IRuleSet> { Message = "Successful." }));

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.GetByNameAsync(It.IsAny<string>());

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.GetByNameAsync(It.IsAny<string>()));
        }
        #endregion Get Tests

        #region GetAll Tests
        [Fact]
        public async void GetAllErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();

            // Setup mock methods/properties
            mockRuleSetRepository.Setup(x => x.GetAllAsync())
                .Throws(new Exception());

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.GetAllAsync());

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetAllTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();

            // Setup mock methods/properties
            mockRuleSetRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new GetResponse<IReadOnlyList<IRuleSet>> { Message = "Successful." }));

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.GetAllAsync();

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.GetAllAsync());
        }
        #endregion GetAll Tests

        #region Save Tests
        [Fact]
        public async void SaveErrorTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var mockRuleSet = new Mock<IRuleSet>();

            // Setup mock methods/properties
            mockRuleSetRepository.Setup(x => x.SaveAsync(It.IsNotNull<IRuleSet>()))
                .Throws(new Exception());

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object,  mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.SaveAsync(mockRuleSet.Object);

            // Assert
            response.IsSuccessful.Should().BeFalse();
            response.Errors.Count.Should().BeGreaterThan(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.SaveAsync(It.IsAny<IRuleSet>()));

            // Verify the application service is logging the error.
            mockLogManager.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        [Fact]
        public async void SaveTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleSetTranslator = new Mock<IRuleSetTranslator>();
            var mockRuleAssemblyService = new Mock<IRuleSetAssemblyService>();
            var mockRuleSet = new Mock<IRuleSet>();

            // Setup mock methods/properties
            mockRuleSetRepository.Setup(x => x.SaveAsync(It.IsNotNull<IRuleSet>()))
                .Returns(Task.FromResult(new SaveResponse<IRuleSet> { Message = "Successful." }));

            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, mockRuleSetTranslator.Object, mockRuleAssemblyService.Object);
            var response = await sut.SaveAsync(mockRuleSet.Object);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Message.Should().NotBeNullOrEmpty();

            // Verify the application service is calling the correct repository method.
            mockRuleSetRepository.Verify(x => x.SaveAsync(It.IsAny<IRuleSet>()));
        }
        #endregion Save Tests

        #region Private Methods
        private IRuleSet CreateTestRuleSet()
        {
            return new RuleSet
            {
                Name = "Test Rule Set",
                Rules = new List<IRule>
                {
                    new Rule { Body = "If an audit is ordered and the audit is workable, then generate the welcome letter.", IsPublished = true}
                }
            };
        }
        #endregion Private Methods
    }
}