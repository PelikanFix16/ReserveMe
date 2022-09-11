using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Test.Common.Behaviors.Commands;
using Application.Test.Common.Behaviors.Exceptions;
using FluentAssertions;
using FluentResults;
using MediatR;
using Moq;
using SharedKernel.Application.Common.Behaviors;
using SharedKernel.Domain.BusinessRule;
using Xunit;

namespace Application.Test.Common.Behaviors
{
    public class MediatRPipelineExceptionTest
    {
        [Fact]
        public async Task ShouldReturnResultErrorWithMessageWrongOperationWhenIsNullReferenceExceptionInHandlerAsync()
        {
            var pipeline = new MediatRPipelineException<TestCommand, Result<TestDto>>();
            var command = new TestCommand()
            {
                Name = "John",
                Login = "test@example.com"
            };
            var mockDelegate = new Mock<RequestHandlerDelegate<Result<TestDto>>>();
            mockDelegate.Setup(x => x()).Throws(new NullReferenceException());
            var res = await pipeline.Handle(command, new System.Threading.CancellationToken(), mockDelegate.Object);

            res.IsFailed.Should().BeTrue();
            res.Errors.Should().HaveCount(1);
            res.Errors[0].Message.Should().Be("Wrong operation");
        }

        [Fact]
        public async Task ShouldReturnResultErrorWithMessageInnerDataProblemWhenIsBusinessRuleValidationExceptionAsync()
        {
            var pipeline = new MediatRPipelineException<TestCommand, Result<TestDto>>();
            var command = new TestCommand()
            {
                Name = "John",
                Login = "test@example.com"
            };
            var mockDelegate = new Mock<RequestHandlerDelegate<Result<TestDto>>>();
            mockDelegate.Setup(x => x()).Throws(new BusinessRuleValidationException(new BusinessRuleTest()));
            var res = await pipeline.Handle(command, new System.Threading.CancellationToken(), mockDelegate.Object);

            res.IsFailed.Should().BeTrue();
            res.Errors.Should().HaveCount(1);
            res.Errors[0].Message.Should().Be("Inner data problem");
        }

        [Fact]
        public async Task ShouldReturnResultErrorWithMessageUndefinedErrorWhenIsBusinessRuleValidationExceptionAsync()
        {
            var pipeline = new MediatRPipelineException<TestCommand, Result<TestDto>>();
            var command = new TestCommand()
            {
                Name = "John",
                Login = "test@example.com"
            };
            var mockDelegate = new Mock<RequestHandlerDelegate<Result<TestDto>>>();
            mockDelegate.Setup(x => x()).Throws(new Exception());
            var res = await pipeline.Handle(command, new System.Threading.CancellationToken(), mockDelegate.Object);

            res.IsFailed.Should().BeTrue();
            res.Errors.Should().HaveCount(1);
            res.Errors[0].Message.Should().Be("Undefined error");
        }

        [Fact]
        public async Task ShouldReturnNoErrorResultWhenNoExceptionInHandlerAsync()
        {
            var pipeline = new MediatRPipelineException<TestCommand, Result<TestDto>>();
            var command = new TestCommand()
            {
                Name = "John",
                Login = "test@example.com"
            };
            var testDto = new TestDto()
            {
                Name = "John",
                Login = "test"
            };
            var mockDelegate = new Mock<RequestHandlerDelegate<Result<TestDto>>>();
            mockDelegate.Setup(x => x()).ReturnsAsync(Result.Ok(testDto));
            var res = await pipeline.Handle(command, new System.Threading.CancellationToken(), mockDelegate.Object);

            res.IsSuccess.Should().BeTrue();
            res.Value.Name.Should().Be("John");
            res.Value.Login.Should().Be("test");
        }
    }
}
