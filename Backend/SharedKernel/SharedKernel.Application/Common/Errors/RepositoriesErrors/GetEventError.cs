namespace SharedKernel.Application.Common.Errors.RepositoriesErrors
{
    public class GetEventError : BaseError
    {
        private const string ErrorMessage = "Failed to get events to event repository";
        private const int Code = 503;

        public GetEventError()
            : base(ErrorMessage, Code)
        {
        }
    }
}
