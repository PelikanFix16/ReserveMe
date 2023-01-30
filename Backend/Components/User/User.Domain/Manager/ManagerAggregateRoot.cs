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
        public Email ManagerEmail { get; private set; } = null!;
        public ManagerStatus Status { get; private set; } = ManagerStatus.DeActivated;
        public ManagerBlockedStatus BlockedStatus { get; private set; } = ManagerBlockedStatus.UnBlocked;
        public DateTimeOffset RegisteredDate { get; private set; }

        private void Apply(ManagerCreatedEvent e)
        {
            Id = e.Key as ManagerId;
            ManagerEmail = e.Email;
            RegisteredDate = e.TimeStamp;
        }

        private void Apply(ManagerConfirmedEvent e) => Status = e.Status;

        private void Apply(ManagerBlockedEvent e) => BlockedStatus = e.Status;
        private void Apply(ManagerUnBlockedEvent e) => BlockedStatus = e.Status;
        private void Apply(ManagerEmailChangedEvent e) => ManagerEmail = e.Email;

        public ManagerAggregateRoot(ManagerId id,Email localEmail) => ApplyChange(new ManagerCreatedEvent(id,localEmail,Version));

        public ManagerAggregateRoot()
        {
        }

        public void Confirm()
        {
            CheckRule(new ManagerCannotBeConfirmedMoreThanOnceRule(Status));
            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerConfirmedEvent(Id,ManagerStatus.Activated,Version));
        }

        public void Block()
        {
            CheckRule(new ManagerCannotBeModifiedWithoutConfirmationRule(Status));
            CheckRule(new ManagerCannotBeBlockedMoreThanOnceRule(BlockedStatus));

            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerBlockedEvent(Id,ManagerBlockedStatus.Blocked,Version));
        }
        public void UnBlock()
        {
            CheckRule(new ManagerCannotBeModifiedWithoutConfirmationRule(Status));
            CheckRule(new ManagerCannotBeUnblockedMoreThanOnceRule(BlockedStatus));
            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerUnBlockedEvent(Id,ManagerBlockedStatus.UnBlocked,Version));
        }

        public void ChangeEmail(Email email)
        {
            CheckRule(new ManagerCannotBeModifiedWithoutConfirmationRule(Status));
            CheckRule(new ManagerCannotChangeWhenBlockedRule(BlockedStatus));
            CheckRule(new ManagerCannotChangeSameEmailRule(ManagerEmail,email));
            if (Id is null)
                throw new InvalidOperationException("Manager is not created yet");

            ApplyChange(new ManagerEmailChangedEvent(Id,email,Version));
        }
    }
}
