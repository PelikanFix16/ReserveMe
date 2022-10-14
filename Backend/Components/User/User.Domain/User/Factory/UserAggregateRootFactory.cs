using User.Domain.ValueObjects;

namespace User.Domain.User.Factory
{
    public class UserAggregateRootFactory : IUserAggregateRootFactory
    {
        private BirthDate? _birthDate;
        private Login? _login;
        private Name? _name;
        private Password? _password;
        private readonly UserId _userId;

        public UserAggregateRootFactory()
        {
            _userId = new UserId(Guid.NewGuid());
        }

        public IUserAggregateRootFactory AddBirthDate(DateTimeOffset birthDate)
        {
            _birthDate = new BirthDate(birthDate);
            return this;
        }

        public IUserAggregateRootFactory AddLogin(string login)
        {
            _login = new Login(login);
            return this;
        }

        public IUserAggregateRootFactory AddName(string firstName, string lastName)
        {
            _name = new Name(firstName, lastName);
            return this;
        }

        public IUserAggregateRootFactory AddPassword(string password)
        {
            _password = new Password(password);
            return this;
        }

        public UserAggregateRoot Create()
        {
            if (_birthDate is null)
                throw new NullReferenceException("Birth date cannot be null");
            if (_login is null)
                throw new NullReferenceException("Login cannot be null");
            if (_name is null)
                throw new NullReferenceException("Name cannot be null");
            if (_password is null)
                throw new NullReferenceException("Password cannot be null");

            return new UserAggregateRoot(_userId, _login, _password, _name, _birthDate);
        }
    }
}
