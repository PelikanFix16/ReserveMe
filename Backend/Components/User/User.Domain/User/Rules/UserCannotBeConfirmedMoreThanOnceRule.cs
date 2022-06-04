using SharedKernel.Domain.BusinessRule;

namespace User.Domain.User.Rules
{
    public class UserCannotBeConfirmedMoreThanOnceRule : IBusinessRule
    {
        public string Message => "User Registration cannot be confirmed more than once";

        private readonly UserStatus Status;

        internal UserCannotBeConfirmedMoreThanOnceRule(UserStatus status){
            Status = status;
        }

        public bool IsBroken() => Status == UserStatus.Activated;
    }
}