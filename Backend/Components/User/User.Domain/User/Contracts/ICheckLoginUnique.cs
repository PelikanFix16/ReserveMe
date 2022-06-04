using User.Domain.ValueObjects;

namespace User.Domain.User.Contracts
{
    public interface ICheckLoginUnique
    {
         Task<bool> CheckLoginExists(Login login);
    }
}