using SharedKernel.Application.Interfaces.BaseError;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Errors.RepositoriesErrors
{
    public class AggregateVersionError : BaseError
    {
        private const string ErrorMessage = "Aggregate with key {0} has invalid version";
        private const int Code = 500;

        public AggregateVersionError(AggregateKey key)
            : base(string.Format(ErrorMessage, key), Code)
        {
        }
    }
}
