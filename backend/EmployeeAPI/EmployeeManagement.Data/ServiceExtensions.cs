using EmployeeManagement.Data.Context;
using EmployeeManagement.Data.Repository;
using EmployeeManagement.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public static class ServiceExtensions
    {
        public static void ConfigureData(this IServiceCollection services, IConfiguration configuration)
        {
            var _GetConnectionString = configuration.GetConnectionString("EmployeeConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_GetConnectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
