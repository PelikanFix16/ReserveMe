using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.ValueObjects;
using SharedKernel.Domain.Event;

namespace Domain.Test.Mock.Events
{
    public class TestCreatedEvent : DomainEvent
    {
        public TestName Name { get; private set; } = null!;
        public TestBirthDate BirthDate { get; private set; } = null!;

        public TestCreatedEvent(
            TestId key,
            TestName name,
            TestBirthDate birthDate,
            int version)
            : base(key, version)
        {
            Name = name;
            BirthDate = birthDate;
        }
    }
}