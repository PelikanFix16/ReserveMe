using User.Domain.ValueObjects;

namespace User.Domain.User.Factory
{
    public class UserAggregateRootFactory : IUserAggregateRootFactory
    {
        private BirthDate? _birthDate;
        private Login? _login;
        private Name? _name;
        private Password? _password;
        private UserId _userId;

        public UserAggregateRootFactory()
        {
            _userId = new UserId(Guid.NewGuid());
        }
        public IUserAggregateRootFactory AddBirthDate(DateTimeOffset birthDate)
        {
            BirthDate birthObject = BirthDate.Create(birthDate);
            _birthDate = birthObject;
            return this;
        }

        public IUserAggregateRootFactory AddLogin(string login)
        {
            Login loginObject = Login.Create(login);
            _login = loginObject;
            return this;
        }

        public IUserAggregateRootFactory AddName(string firstName, string lastName)
        {
            Name nameObject = Name.Create(firstName, lastName);
            _name = nameObject;
            return this;
        }

        public IUserAggregateRootFactory AddPassword(string password)
        {
            Password passwordObject = Password.Create(password);
            _password = passwordObject;
            return this;
        }

        public UserAggregateRoot Create()
        {   
            if(_birthDate is null)
                throw new NullReferenceException("Birth date cannot be null");
            if(_login is null)
                throw new NullReferenceException("Login cannot be null");
            if(_name is null)
                throw new NullReferenceException("Name cannot be null");
            if(_password is null)
                throw new NullReferenceException("Password cannot be null");
            
            return new UserAggregateRoot(_userId,_login,_password,_name,_birthDate);
        }
    }
}