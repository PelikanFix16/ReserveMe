using SharedKernel.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Employee.Events;
using User.Domain.Employee.Rules;
using User.Domain.ValueObjects;

namespace User.Domain.Employee
{
    public class EmployeeAggregateRoot : AggregateRoot
    {
        public EmployeeId? Id { get; private set; }
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public EmployeeStatus Status { get; private set; } = EmployeeStatus.DeActivated;
        public EmployeePrivileges Privileges { get; private set; } = EmployeePrivileges.None;
        public EmployeeBlockStatus BlockStatus { get; private set; } = EmployeeBlockStatus.UnBlocked;
        public DateTimeOffset CreatedAt { get; private set; }

        private void Apply(EmployeeCreatedEvent e)
        {
            Id = e.Key as EmployeeId;
            Email = e.Email;
            Privileges = e.Privileges;
            CreatedAt = e.TimeStamp;
        }

        private void Apply(EmployeeConfirmedEvent e) => Status = e.Status;
        private void Apply(EmployeeChangedPrivilegesEvent e) => Privileges = e.Privileges;
        private void Apply(EmployeeChangedPasswordEvent e) => Password = e.Password;
        private void Apply(EmployeeBlockedEvent e) => BlockStatus = e.Status;
        private void Apply(EmployeeUnBlockedEvent e) => BlockStatus = e.Status;

        public EmployeeAggregateRoot(
            EmployeeId id,
            Email email,
            EmployeePrivileges privileges) => ApplyChange(new EmployeeCreatedEvent(id,email,privileges,Version));

        public EmployeeAggregateRoot()
        {
        }

        public void Confirm()
        {
            CheckRule(new EmployeeCannotBeConfirmedMoreThanOnceRule(Status));
            CheckRule(new EmployeeCannotChangeWhenBlockedRule(BlockStatus));
            if (Id is null)
                throw new InvalidOperationException("Employee is not created yet");

            ApplyChange(new EmployeeConfirmedEvent(Id,EmployeeStatus.Activated,Version));
        }

        public void ChangePrivileges(EmployeePrivileges privileges)
        {
            CheckRule(new EmployeeCannotChangeWhenBlockedRule(BlockStatus));
            if (Id is null)
                throw new InvalidOperationException("Employee is not created yet");

            ApplyChange(new EmployeeChangedPrivilegesEvent(Id,privileges,Version));
        }

        public void ChangePassword(Password password)
        {
            CheckRule(new EmployeeCannotChangeWhenBlockedRule(BlockStatus));
            if (Id is null)
                throw new InvalidOperationException("Employee is not created yet");

            ApplyChange(new EmployeeChangedPasswordEvent(Id,password,Version));
        }

        public void Block()
        {
            CheckRule(new EmployeeCannotBeBlockedMoreThanOnceRule(BlockStatus));
            if (Id is null)
                throw new InvalidOperationException("Employee is not created yet");

            ApplyChange(new EmployeeBlockedEvent(Id,EmployeeBlockStatus.Blocked,Version));
        }

        public void UnBlock()
        {
            CheckRule(new EmployeeCannotBeUnblockedMoreThanOnceRule(BlockStatus));
            if (Id is null)
                throw new InvalidOperationException("Employee is not created yet");

            ApplyChange(new EmployeeUnBlockedEvent(Id,EmployeeBlockStatus.UnBlocked,Version));
        }
    }
}