using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Interfaces.Repositories;
using User.Application.Mapper.Dto;
using User.Application.Mapper.Projections;
using User.Infrastructure.Persistence.DataContext;

namespace User.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeProjectionRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext employeeContext) => _context = employeeContext;

        public async Task<Result<EmployeeProjection>> GetByEmailAsync(EmailDto email)
        {
            EmployeeProjection? employee = await _context.Employee.Where(x => x.Email == email.Email).FirstOrDefaultAsync();
            return employee ?? (Result<EmployeeProjection>)Result.Fail("Employee not found");
        }

        public async Task<Result<EmployeeProjection>> GetById(Guid id)
        {
            EmployeeProjection? employee = await _context.Employee.Where(x => x.Id == id).FirstOrDefaultAsync();
            return employee ?? (Result<EmployeeProjection>)Result.Fail("Employee not found");
        }

        public async Task<Result<EmployeeProjection>> GetByManagerId(Guid id)
        {
            EmployeeProjection? employee = await _context.Employee.Where(x => x.ManagerId == id).FirstOrDefaultAsync();
            return employee ?? (Result<EmployeeProjection>)Result.Fail("Employee not found");
        }
        public async Task SaveAsync(EmployeeProjection employeeProjection) => await _context.Employee.AddAsync(employeeProjection);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}