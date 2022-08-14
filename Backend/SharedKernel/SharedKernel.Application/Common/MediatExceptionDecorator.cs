using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using SharedKernel.Application.Common.Errors;
using SharedKernel.Domain.BusinessRule;

namespace SharedKernel.Application.Common
{
    public class MediatExceptionDecorator<TCommand, TResult> :
        IRequestHandler<TCommand, TResult>
            where TCommand : IRequest<TResult>
            where TResult : Result<TResult>
    {
        private readonly IRequestHandler<TCommand, TResult> _decorated;

        public MediatExceptionDecorator(IRequestHandler<TCommand, TResult> decorated)
        {
            _decorated = decorated;
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _decorated.Handle(request, cancellationToken);
            }
            catch (Exception ex)
            {
                var exMessage = ex switch
                {
                    BusinessRuleValidationException => "Inner data problem",
                    NullReferenceException => "Wrong operation",
                    _ => "Undefined error"
                };
                return (TResult)Result.Fail(new InnerDomainError(exMessage));
            }
        }
    }
}
