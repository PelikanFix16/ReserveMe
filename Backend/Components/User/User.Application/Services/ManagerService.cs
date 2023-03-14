using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Repositories;
using User.Application.Interfaces.Services;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;

namespace User.Application.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerProjectionRepository _repository;
        private readonly IMapper _mapper;

        public ManagerService(IManagerProjectionRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> ManagerCreateAsync(ManagerProjection managerProjection)
        {
            EmailDto emailDto = _mapper.Map<EmailDto>(managerProjection.Email);
            Result<ManagerProjection> manager = await _repository.GetByEmailAsync(emailDto);
            if (manager.IsSuccess)
                return Result.Fail("Manager already exists");

            await _repository.SaveAsync(managerProjection);
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }
    }
}