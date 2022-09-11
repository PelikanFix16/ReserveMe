using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Test.Common.Behaviors.Commands;
using FluentAssertions;
using FluentResults;
using MediatR;
using Moq;
using SharedKernel.Application.Common.Behaviors;
using Xunit;

namespace Application.Test.Common.Behaviors
{
    public class MediatRPipelineValidationTest
    {
        [Fact]
        public async Task ShouldReturnResultOkWithTestDtoWhenValidationIsCorrectAsync()
        {
            var pipeline = new MediatRPipelineValidation<TestCommand, Result<TestDto>>(new TestValidator());
            var command = new TestCommand()
            {
                Name = "John",
                Login = "login"
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

        [Fact]
        public async Task ShouldReturnResultErrorWithMessageNameIsRequiredWhenNameIsEmptyAndNameMustBeAtLeast3CharactersAsync()
        {
            var pipeline = new MediatRPipelineValidation<TestCommand, Result<TestDto>>(new TestValidator());
            var command = new TestCommand()
            {
                Name = "",
                Login = "login"
            };
            var testDto = new TestDto()
            {
                Name = "John",
                Login = "test"
            };
            var mockDelegate = new Mock<RequestHandlerDelegate<Result<TestDto>>>();
            mockDelegate.Setup(x => x()).ReturnsAsync(Result.Ok(testDto));
            var res = await pipeline.Handle(command, new System.Threading.CancellationToken(), mockDelegate.Object);

            res.IsFailed.Should().BeTrue();
            res.Errors.Should().HaveCount(2);
            res.Errors[0].Message.Should().Be("Error while validating Name - Name is required");
        }

        [Fact]
        public async Task ShouldReturnResultErrorWithMessageLoginMustBeAtMost10charactersWhenLoginIsMoreThan10charactersAsync()
        {
            var pipeline = new MediatRPipelineValidation<TestCommand, Result<TestDto>>(new TestValidator());
            var command = new TestCommand()
            {
                Name = "John",
                Login = "login_login_login"
            };
            var testDto = new TestDto()
            {
                Name = "John",
                Login = "test"
            };
            var mockDelegate = new Mock<RequestHandlerDelegate<Result<TestDto>>>();
            mockDelegate.Setup(x => x()).ReturnsAsync(Result.Ok(testDto));
            var res = await pipeline.Handle(command, new System.Threading.CancellationToken(), mockDelegate.Object);

            res.IsFailed.Should().BeTrue();
            res.Errors.Should().HaveCount(1);
            res.Errors[0].Message.Should().Be("Error while validating Login - Login must be at most 10 characters");
        }
    }
}
