using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.Catalog.Domain.Exceptions;

namespace WK.Catalog.Domain.Validation
{
    public class DomainValidation
    {
        public static void NotNull(object? target, string fieldName)
        {
            if (target is null)
                throw new EntityValidationException(
                    $"{fieldName} não pode ser nulo");
        }

        public static void NotNullOrEmpty(string? target, string fieldName)
        {
            if (String.IsNullOrWhiteSpace(target))
                throw new EntityValidationException(
                    $"{fieldName} não pode ser vazio ou nulo");
        }

    }
}
