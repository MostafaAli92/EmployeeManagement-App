using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeManagement.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
         IEmployeeRepository employee { get; }
        public Task Save();
    }
}
