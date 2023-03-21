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
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeProjectionRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IMapper mapper,
            IEmployeeProjectionRepository employeeProjectionRepository)
        {
            _repository = employeeProjectionRepository;
            _mapper = mapper;
        }

        public async Task<Result> EmployeeCreateAsync(EmployeeProjection employeeProjection)
        {
            EmailDto emailDto = _mapper.Map<EmailDto>(employeeProjection.Email);
            Result<EmployeeProjection> employee = await _repository.GetByEmailAsync(emailDto);
            if (employee.IsSuccess)
                return Result.Fail("Employee already exists");

            await _repository.SaveAsync(employeeProjection);
            await _repository.SaveChangesAsync();
            return Result.Ok();

        }
    }
}