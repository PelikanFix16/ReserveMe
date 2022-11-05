using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentResults;
using Moq;
using SharedKernel.Application.Interfaces.Repositories;
using SharedKernel.Domain;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;
using User.Application.Commands.UserRegister;
using User.Application.Interfaces.Security;
using User.Application.Mapper;
using User.Application.Mapper.Dto;
using Xunit;

namespace Application.Test.Commands.UserRegister
{
    public class UserRegisterHandlerTest
    {
        private readonly UserRegisterCommand _command;

        public UserRegisterHandlerTest()
        {
            _command = new UserRegisterCommand()
            {
                Name = new NameDto()
                {
                    FirstName = "John",
                    LastName = "Doe"
                },
                Login = new LoginDto()
                {
                    Login = "example@mail.com"
                },
                Password = new PasswordDto()
                {
                    Password = "te@test212sS"
                },
                BirthDate = new BirthDateDto()
                {
                    BirthDate = AppTime.Now().AddYears(-20)
                }
            };
        }

        [Fact]
        public async Task UserRegisterHandlerShouldReturnUserRegisterDtoInResultObjectAsync()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UserAggregateProfile>());
            var mapper = configuration.CreateMapper();
            var mockHashPassword = new Mock<ISecurityHash>();
            mockHashPassword.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("$2a$11$9zqYOv34D7LwBi0f8nAUuuup1O0m7t.pmOPDbrd4Nwcd5Iq9PR2qq");
            var aggregateRepositoryMock = new Mock<IAggregateRepository>();
            aggregateRepositoryMock.Setup(x => x.Save(
                It.IsAny<AggregateRoot>(),
                It.IsAny<AggregateKey>()))
                .Returns(Result.Ok());

            var handler = new UserRegisterHandler(mapper, aggregateRepositoryMock.Object, mockHashPassword.Object);
            var x = await handler.Handle(_command, new System.Threading.CancellationToken());
            // check handler return result ok object
            x.IsSuccess.Should().BeTrue();
            // check handler return properly user register dto object
            x.Value.Login.Should().Be(_command.Login.Login);
            x.Value.Name.FirstName.Should().Be(_command.Name.FirstName);
            x.Value.Id.Should().NotBeEmpty();
            x.Value.Id.Should().Be(_command.Id.ToString());
        }

        [Fact]
        public async Task UserRegisterHandlerShouldCallSaveMethodInAggregateRepositoryAsync()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UserAggregateProfile>());
            var mapper = configuration.CreateMapper();
            var mockHashPassword = new Mock<ISecurityHash>();
            mockHashPassword.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("$2a$11$9zqYOv34D7LwBi0f8nAUuuup1O0m7t.pmOPDbrd4Nwcd5Iq9PR2qq");
            var aggregateRepositoryMock = new Mock<IAggregateRepository>();
            aggregateRepositoryMock.Setup(x => x.Save(
                It.IsAny<AggregateRoot>(),
                It.IsAny<AggregateKey>()))
                .Returns(Result.Ok());
            var handler = new UserRegisterHandler(mapper, aggregateRepositoryMock.Object, mockHashPassword.Object);
            var x = await handler.Handle(_command, new System.Threading.CancellationToken());

            aggregateRepositoryMock.Verify(
                x => x.Save(
                    It.IsAny<AggregateRoot>(),
                    It.IsAny<AggregateKey>()),
                Times.Once);
        }

        [Fact]
        public async Task UserRegisterHandlerShouldCallCommitAsyncMethodInAggregateRepositoryAsync()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UserAggregateProfile>());
            var mapper = configuration.CreateMapper();
            var aggregateRepositoryMock = new Mock<IAggregateRepository>();
            var mockHashPassword = new Mock<ISecurityHash>();
            mockHashPassword.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("$2a$11$9zqYOv34D7LwBi0f8nAUuuup1O0m7t.pmOPDbrd4Nwcd5Iq9PR2qq");
            aggregateRepositoryMock.Setup(x => x.Save(
                It.IsAny<AggregateRoot>(),
                It.IsAny<AggregateKey>()))
                .Returns(Result.Ok());
            var handler = new UserRegisterHandler(mapper, aggregateRepositoryMock.Object, mockHashPassword.Object);
            var x = await handler.Handle(_command, new System.Threading.CancellationToken());

            aggregateRepositoryMock.Verify(
                x => x.CommitAsync(),
                Times.Once);
        }
    }
}
