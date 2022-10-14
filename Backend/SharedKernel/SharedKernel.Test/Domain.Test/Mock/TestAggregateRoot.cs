#pragma warning disable RCS1213, IDE0051
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Test.Mock.Events;
using Domain.Test.Mock.ValueObjects;
using SharedKernel.Domain.Aggregate;

namespace Domain.Test.Mock
{
    public class TestAggregateRoot : AggregateRoot
    {
        public TestId? Id { get; private set; }
        public TestName Name { get; private set; } = null!;
        public TestBirthDate BirthDate { get; private set; } = null!;
        public DateTimeOffset CreatedDate { get; private set; }

        private void Apply(TestCreatedEvent e)
        {
            Id = e.Key as TestId;
            Name = e.Name;
            BirthDate = e.BirthDate;
            CreatedDate = e.TimeStamp;
        }

        private void Apply(TestChangedNameEvent e)
        {
            Name = e.Name;
        }

        public TestAggregateRoot(TestId id, TestName name, TestBirthDate birthDate)
        {
            ApplyChange(new TestCreatedEvent(id, name, birthDate, Version));
        }

        public TestAggregateRoot()
        {
        }

        public void ChangeName(TestName newName)
        {
            if (Id is null)
                throw new InvalidOperationException("Aggregate root is not initialized");

            ApplyChange(new TestChangedNameEvent(Id, newName, Version));
        }
    }
}
