namespace SharedKernel.Application.Common.Errors.RepositoriesErrors
{
    public class SaveEventError : BaseError
    {
        private const string ErrorMessage = "Failed to save events to event repository";
        private const int Code = 503;

        public SaveEventError()
            : base(ErrorMessage, Code)
        {
        }
    }
}
