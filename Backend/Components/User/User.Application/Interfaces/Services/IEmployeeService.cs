using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Projections;

namespace User.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        public Task<Result> EmployeeCreateAsync(EmployeeProjection employeeProjection);
    }
}