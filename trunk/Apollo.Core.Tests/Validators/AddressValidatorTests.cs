using FluentAssertions;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Common;
using Apollo.Core.DomainServices.Validators;
using Xunit;

namespace Apollo.Core.Tests.Validators
{
    public class AddressValidatorTests
    {
        [Theory]
        [ClassData(typeof(AddressValidatorTestData))]
        public async void AddressValidatorReturnsCorrectResult(IAddress model, bool expectedResult)
        {
            var sut = new AddressValidator();
            var result = await sut.IsValidAsync(model);

            result.Should().Be(expectedResult);
        }
    }

    public class AddressValidatorTestData : TheoryData<IAddress, bool>
    {
        public AddressValidatorTestData()
        {
            Add(new Address(), false);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "Some City",
                State = "La",
                Zipcode = "705011"
            }, false);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "Some City",
                State = "La",
                Zipcode = "705"
            }, false);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "",
                State = "La",
                Zipcode = "705011"
            }, false);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "Some City",
                State = "",
                Zipcode = "705011"
            }, false);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "Some City",
                State = "La",
                Zipcode = ""
            }, false);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "Some City",
                State = "La",
                Zipcode = "70501"
            }, true);
            Add(new Address()
            {
                Line1 = "100 some street",
                City = "Some City",
                State = "La",
                Zipcode = "70501-0001"
            }, true);
        }
    }
}