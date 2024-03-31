using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository.IRepository
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
    }
}
