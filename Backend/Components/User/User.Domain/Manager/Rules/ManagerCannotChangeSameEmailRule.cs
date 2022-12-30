using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotChangeSameEmailRule : IBusinessRule
    {
        private readonly Email _email;
        private readonly Email _newEmail;

        public string Message => "Manager cannot be modified with the same email";


        public ManagerCannotChangeSameEmailRule(Email email, Email newEmail)
        {
            _email = email;
            _newEmail = newEmail;
        }

        public bool IsBroken()
        {
            return _email == _newEmail;
        }
    }
}
