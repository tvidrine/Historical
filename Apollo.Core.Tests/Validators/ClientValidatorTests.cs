using FluentAssertions;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.Core.Domain.Enums;
using Apollo.Core.DomainServices.Validators;
using Xunit;

namespace Apollo.Core.Tests.Validators
{
	public class ClientValidatorTests
	{
		[Theory]
		[ClassData(typeof(ClientValidatorTestData))]
		public async void ClientValidatorReturnsCorrectResult(IClient testClient, bool expectedResult)
		{
			var sut = new ClientValidator();
			var result = await sut.IsValidAsync(testClient);

			result.Should().Be(expectedResult);
		}
	}

	public class ClientValidatorTestData : TheoryData<IClient, bool>
	{
		public ClientValidatorTestData()
		{
			Add(new Client(), false);
			Add(
				new Client
				{
					Name = "Test Name",
					Address = new Address(),
					AuditType = AuditTypeEnum.Combo,
					ProcessType = AuditProcessTypeEnum.Physical
				}, true);
		}
	}
}