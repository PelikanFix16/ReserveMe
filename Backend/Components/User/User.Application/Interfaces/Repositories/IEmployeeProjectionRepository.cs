using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;

namespace User.Application.Interfaces.Repositories
{
    public interface IEmployeeProjectionRepository
    {
        public Task<Result<EmployeeProjection>> GetByEmailAsync(EmailDto email);
        public Task<Result<EmployeeProjection>> GetByManagerId(Guid id);
        public Task<Result<EmployeeProjection>> GetById(Guid id);
        public Task SaveAsync(EmployeeProjection employeeProjection);
        public Task SaveChangesAsync();
    }
}