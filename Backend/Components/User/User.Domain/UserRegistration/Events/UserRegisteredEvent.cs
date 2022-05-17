using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.UserRegistration.Events
{
    public class UserRegisteredEvent : DomainEvent
    {

        public UserRegistrationId Id { get;private set;}
        public Login Login {get; private set;}
        public Password Password {get;private set;}
        public Name Name {get; private set;}
        public DateTimeOffset BirthDate {get;private set;}
        


        public UserRegisteredEvent(UserRegistrationId key,Login login,Password password,Name name,DateTimeOffset birthdate, int version) : base(key, version)
        {
            Id = key;
            Login = login;
            Password = password;
            Name = name;
            BirthDate = birthdate; 

        }
    }
}