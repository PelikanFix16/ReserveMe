using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserRegisteredEvent : DomainEvent
    {

        public UserId Id { get;private set;}
        public Login Login {get; private set;}
        public Password Password {get;private set;}
        public Name Name {get; private set;}
        public BirthDate BirthDate {get;private set;}
        


        public UserRegisteredEvent(UserId key,Login login,Password password,Name name,BirthDate birthdate, int version) : base(key, version)
        {
            Id = key;
            Login = login;
            Password = password;
            Name = name;
            BirthDate = birthdate; 

        }
    }
}