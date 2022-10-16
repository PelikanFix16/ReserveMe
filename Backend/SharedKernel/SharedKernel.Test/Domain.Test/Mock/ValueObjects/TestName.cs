using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.ValueObjects;

namespace Domain.Test.Mock.ValueObjects
{
    public sealed class TestName : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public TestName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
