using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCDb_Context _db_Context;

        public EmployeeRepository(MVCDb_Context db_Context) : base(db_Context)
        {
            _db_Context = db_Context;
        }
        public IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> SearchEmployeesByName(string employeeName)
        => _db_Context.Employees.Where(E => E.Name.Contains(employeeName));
        
    }
}
