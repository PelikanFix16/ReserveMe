using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace Application.Test.Common.Behaviors.Commands
{
    public class TestCommand : IRequest<Result<TestDto>>
    {
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;

    }
}
