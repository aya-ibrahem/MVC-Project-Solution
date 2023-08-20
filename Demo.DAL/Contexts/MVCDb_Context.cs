using Demo.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Demo.DAL.Contexts
{
    public class MVCDb_Context:IdentityDbContext<ApplicationUser>
    {
        public MVCDb_Context(DbContextOptions<MVCDb_Context> options):base(options)
        {
                
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=>
        //  optionsBuilder.UseSqlServer("server=.;database=MVCDb;trusted_connection=true;");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<ApplicationUser> ASpNetUsers { get; set; }

    }
}
