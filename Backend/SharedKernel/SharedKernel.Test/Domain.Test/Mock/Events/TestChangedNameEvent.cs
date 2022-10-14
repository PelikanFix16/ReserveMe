using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.ValueObjects;
using SharedKernel.Domain.Event;

namespace Domain.Test.Mock.Events
{
    public class TestChangedNameEvent : DomainEvent
    {
        public TestName Name { get; private set; } = null!;

        public TestChangedNameEvent(
            TestId key,
            TestName name,
            int version)
            : base(key, version)
        {
            Name = name;
        }
    }
}
