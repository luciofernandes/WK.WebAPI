using Bogus;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.Catalog.Domain.Exceptions;
using WK.Catalog.Domain.Validation;
using Xunit;

namespace WK.Catalog.UnitTests.Domain.Validation
{
    
    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            var value = Faker.Commerce.ProductName();
            Action action =
                () => DomainValidation.NotNull(value, fieldName);
            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullThrowWhenNull()
        {
            string? value = null;
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNull(value, fieldName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} não pode ser nulo");
        }


        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} não pode ser vazio ou nulo");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOk()
        {
            var target = Faker.Commerce.ProductName();
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().NotThrow();
        }

    
    }
}
