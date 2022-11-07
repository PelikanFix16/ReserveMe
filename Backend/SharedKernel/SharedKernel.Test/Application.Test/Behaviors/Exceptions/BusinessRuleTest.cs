using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace Application.Test.Behaviors.Exceptions
{
    public class BusinessRuleTest : IBusinessRule
    {
        public string Message => "test";

        public bool IsBroken() => true;
    }
}
