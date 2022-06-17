using SharedKernel.Domain.Factories;

namespace User.Domain.User.Factory
{
    public interface IUserAggregateRootFactory : IAggregateFactory<UserAggregateRoot>
    {
        IUserAggregateRootFactory AddName(string firstName, string lastName);
        IUserAggregateRootFactory AddLogin(string login);
        IUserAggregateRootFactory AddPassword(string password);
        IUserAggregateRootFactory AddBirthDate(DateTimeOffset birthDate);


    }
}