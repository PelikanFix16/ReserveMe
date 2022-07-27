#pragma warning disable CA1822
using SharedKernel.Domain.BusinessRule;

namespace SharedKernel.Domain.EntityBase
{
    public abstract class Entity
    {
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}
#pragma warning restore CA1822
