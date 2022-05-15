using User.Domain.ValueObjects;

namespace User.Domain.UserRegistration.Contracts
{
    public interface ICheckLoginUnique
    {
         Task<bool> CheckLoginExists(Login login);
    }
}