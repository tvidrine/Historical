using FluentAssertions;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Common;
using Apollo.Core.DomainServices.Validators;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.Validators
{
	public class ContactValidatorTests
	{
		[Theory]
		[ClassData(typeof(ContactValidatorTestData))]
		public async void ContactValidatorReturnsCorrectResult(IContact model, bool expectedResult)
		{
			var sut = new ContactValidator();
			var result = await sut.IsValidAsync(model);

			result.Should().Be(expectedResult);
		}
	}

	public class ContactValidatorTestData : TheoryData<IContact, bool>
	{
		public ContactValidatorTestData()
		{
			Add(new Contact(It.IsAny<int>()), false);
			Add(new Contact(It.IsAny<int>())
			{
				Name = "Contact Name",
				Email = "someone@zoomaudits.com",
				PhoneNumber = "3375551212",
				ContactType = ContactTypeEnum.Billing
			}, true);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone@zoomaudits.com",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Client
			}, true);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "",
				Email = "someone@zoomaudits.com",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = null,
				Email = "someone@zoomaudits.com",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone@zoomaudits",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "@zoomaudits.com",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone%zoomaudits.com",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone@zoomaudits.com",
				PhoneNumber = "555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone@zoomaudits.com",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.NotSet
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "",
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = null,
				PhoneNumber = "(337)555-1212",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone@zoomaudits.com",
				PhoneNumber = "",
				ContactType = ContactTypeEnum.Billing
			}, false);
			Add(new Contact(It.IsAny<int>())
            {
				Name = "Contact Name",
				Email = "someone@zoomaudits.com",
				PhoneNumber = null,
				ContactType = ContactTypeEnum.Billing
			}, false);
		}
	}
}