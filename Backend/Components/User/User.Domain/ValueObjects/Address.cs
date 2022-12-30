using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.ValueObjects;
using User.Domain.ValueObjects.Rules;

namespace User.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Country { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public Address(
            string country,
            string street,
            string city,
            string state,
            string zipCode)
        {
            //Check rules for feature
            CheckRule(new StreetAddressRule(street));
            CheckRule(new CityAddressRule(city));
            CheckRule(new StateAddressRule(state));
            CheckRule(new ZipCodeAddressRule(zipCode));
            CheckRule(new CountryAddressRule(country));
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
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
