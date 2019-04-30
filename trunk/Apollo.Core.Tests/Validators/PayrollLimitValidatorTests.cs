using System;
using Apollo.Core.Contracts.Domain.Payroll;
using FluentAssertions;
using Apollo.Core.Domain.Payroll;
using Apollo.Core.DomainServices.Validators;
using Xunit;

namespace Apollo.Core.Tests.Validators
{
    public class PayrollLimitValidatorTests
    {
        [Theory]
        [ClassData(typeof(PayrollLimitValidatorTestData))]
        public async void ValidatorReturnsCorrectResult(IPayrollLimit instance, bool expectedResult)
        {
            var sut = new PayrollLimitValidator();
            var result = await sut.IsValidAsync(instance);

            result.Should().Be(expectedResult);
        }
    }

    public class PayrollLimitValidatorTestData : TheoryData<IPayrollLimit, bool>
    {
        public PayrollLimitValidatorTestData()
        {
            Add(new PayrollLimit(), false);
            Add(new PayrollLimit
                {
                    EffectiveStart = DateTimeOffset.Now,
                    Max = 1m
                }
                , true);
            Add(new PayrollLimit
                {
                    EffectiveStart = DateTimeOffset.Now,
                    EffectiveEnd = DateTimeOffset.Now.AddDays(-1),
                    Max = 1m
                }
                , false);
            Add(new PayrollLimit
                {
                    Max = 2m,
                    Min = 1m
                }
                , true);
            Add(new PayrollLimit
                {
                    Max = 2m,
                    Min = 3m
                }
                , false);
        }
    }
}