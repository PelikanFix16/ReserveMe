using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public Address(
            string street,
            string city,
            string state,
            string zipCode)
        {
            //Check rules for feature
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return ZipCode;
        }
    }
}
