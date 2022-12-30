using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Aggregate;
using User.Domain.Manager.Events;
using User.Domain.Manager.Rules;
using User.Domain.ValueObjects;

namespace User.Domain.Manager
{
    public class ManagerAggregateRoot : AggregateRoot
    {
        public ManagerId? Id { get; private set; }
        public Address LocalAddress { get; private set; } = null!;
        public Email LocalEmail { get; private set; } = null!;
        public ManagerStatus Status { get; private set; } = ManagerStatus.DeActivated;
        public BlockedStatus BlockedStatus { get; private set; } = BlockedStatus.UnBlocked;
        public DateTimeOffset RegisteredDate { get; private set; }

        private void Apply(ManagerCreatedEvent e)
        {
            Id = e.Key as ManagerId;
            LocalAddress = e.Address;
            LocalEmail = e.Email;
            RegisteredDate = e.TimeStamp;
        }

        private void Apply(ManagerConfirmedEvent e)
        {
            Status = e.Status;
        }

        private void Apply(ManagerBlockedEvent e)
        {
            BlockedStatus = e.Status;
        }
        private void Apply(ManagerUnBlockedEvent e)
        {
            BlockedStatus = e.Status;
        }
        private void Apply(ManagerAddressChangedEvent e)
        {
            LocalAddress = e.Address;
        }

        public ManagerAggregateRoot(ManagerId id, Address localAddress, Email localEmail)
        {
            ApplyChange(new ManagerCreatedEvent(id, localAddress, localEmail, Version));
        }

        public ManagerAggregateRoot()
        {
        }

        public void Confirm()
        {
            CheckRule(new ManagerCannotBeConfirmedMoreThanOnceRule(Status));
            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerConfirmedEvent(Id, ManagerStatus.Activated, Version));
        }

        public void Block()
        {
            CheckRule(new ManagerCannotBeModifiedWithoutConfirmationRule(Status));
            CheckRule(new ManagerCannotBeBlockedMoreThanOnceRule(BlockedStatus));

            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerBlockedEvent(Id, BlockedStatus.Blocked, Version));
        }
        public void UnBlock()
        {
            CheckRule(new ManagerCannotBeModifiedWithoutConfirmationRule(Status));
            CheckRule(new ManagerCannotBeUnblockedMoreThanOnceRule(BlockedStatus));
            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerUnBlockedEvent(Id, BlockedStatus.UnBlocked, Version));
        }

        public void ChangeAddress(Address address)
        {
            CheckRule(new ManagerCannotBeModifiedWithoutConfirmationRule(Status));
            CheckRule(new ManagerCannotChangeWhenBlockedRule(BlockedStatus));
            CheckRule(new ManagerCannotChangeSameAddressRule(LocalAddress, address));
            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerAddressChangedEvent(Id, address, Version));
        }
    }
}
