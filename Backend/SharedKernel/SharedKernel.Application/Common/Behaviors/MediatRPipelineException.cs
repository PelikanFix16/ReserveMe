using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using MediatR.Pipeline;
using SharedKernel.Application.Common.Errors.BehaviorsErrors;
using SharedKernel.Domain.BusinessRule;

namespace SharedKernel.Application.Common.Behaviors
{
    public class MediatRPipelineException<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResultBase<TResponse>, new()
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var res = await next();
                return res;
            }
            catch (Exception ex)
            {
                //More errors add we only use one error type like InnerDomainError for business rules
                // We also want catch undefined errors, not excepted errors and other errors
                var exMessage = ex switch
                {
                    BusinessRuleValidationException => "Inner data problem",
                    NullReferenceException => "Wrong operation",
                    _ => "Undefined error"
                };
                var response = new TResponse();
                response.WithError(new InnerDomainError(exMessage));
                return response;
            }
        }
    }
}
