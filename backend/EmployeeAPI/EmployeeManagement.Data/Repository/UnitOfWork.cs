using EmployeeManagement.Data.Context;
using EmployeeManagement.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IEmployeeRepository employee {  get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext,IEmployeeRepository employeeRepository)
        {
            _dbContext = dbContext;
            employee = employeeRepository;
        }

       

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
