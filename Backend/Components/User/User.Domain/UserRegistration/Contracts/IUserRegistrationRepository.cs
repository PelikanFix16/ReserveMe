namespace User.Domain.UserRegistration.Contracts
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);

        Task<UserRegistration> GetById(UserRegistrationId userRegistrationId);

    }
}