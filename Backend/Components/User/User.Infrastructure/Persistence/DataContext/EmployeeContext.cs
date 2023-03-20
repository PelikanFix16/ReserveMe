using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Application.Mapper.Projections;

namespace User.Infrastructure.Persistence.DataContext
{
    public class EmployeeContext : DbContext
    {
        public DbSet<EmployeeProjection> Employee { get; set; } = null!;

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
    }
}