using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Common.Errors.RepositoriesErrors
{
    public class AggregateNotFoundError : BaseError
    {
        private const string ErrorMessage = "Aggregate with key {0} not found";
        private const int Code = 404;

        public AggregateNotFoundError(AggregateKey key)
            : base(string.Format(ErrorMessage, key), Code)
        {
        }
    }
}
