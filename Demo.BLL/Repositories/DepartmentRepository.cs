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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MVCDb_Context db_Context;

        public DepartmentRepository(MVCDb_Context db_Context):base(db_Context)
        {
            this.db_Context = db_Context;
        }
    }
}
