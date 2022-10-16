using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.ValueObjects;

namespace Domain.Test.Mock.ValueObjects
{
    public sealed class TestBirthDate : ValueObject
    {
        public DateTimeOffset Value { get; private set; }

        public TestBirthDate(DateTimeOffset birthDate)
        {
            Value = birthDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
