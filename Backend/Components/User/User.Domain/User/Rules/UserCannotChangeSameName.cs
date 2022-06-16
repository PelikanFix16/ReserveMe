using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;

namespace User.Domain.User.Rules
{
    public class UserCannotChangeSameName : IBusinessRule
    {
        public string Message => "User cannot change the same name";

        private readonly Name _newName;
        private readonly Name _oldName;
        public UserCannotChangeSameName(Name newName,Name oldName)
        {
            _newName = newName;
            _oldName = oldName;
        }
        public bool IsBroken() => _newName == _oldName;
    }
}