using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace Application.Test.Common.Behaviors.Commands
{
    public class TestHandler : IRequestHandler<TestCommand, Result<TestDto>>
    {
        public async Task<Result<TestDto>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
