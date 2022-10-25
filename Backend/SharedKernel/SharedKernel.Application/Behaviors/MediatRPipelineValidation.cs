using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using SharedKernel.Application.Errors.BehaviorsErrors;

namespace SharedKernel.Application.Behaviors
{
    public class MediatRPipelineValidation<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
         where TResponse : ResultBase<TResponse>, new()
    {
        private readonly IValidator<TRequest>? _validator;

        public MediatRPipelineValidation(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.IsValid)
            {
                return await next();
            }

            var response = new TResponse();
            var errors = validatorResult.Errors.ConvertAll(x => new ValidationError(x.PropertyName, x.ErrorMessage));
            response.WithErrors(errors);
            return response;
        }
    }
}
