using SharedKernel.Domain.Aggregate;
using User.Domain.ValueObjects;

namespace User.Domain.UserRegistration
{
    public class UserRegistration : AggregateRoot
    {
        public UserRegistrationId Id { get; private set; }
        public Login Login { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public DateTimeOffset RegisteredDate { get; private set; }






    }
}