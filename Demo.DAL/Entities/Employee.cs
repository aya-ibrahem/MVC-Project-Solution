using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
       
        public string Name { get; set; }
       
        public int? Age { get; set; }
   
        public string Address { get; set; }
        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
     
        public string Email { get; set; }
  
        [Display(Name = "Phone Number")]
        public string PhoeNumber { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }= DateTime.Now;
        //public bool IsDeleted { get; set; }
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }
        public Department Departments { get; set; }
        public string ImageName { get; set; }
















    }
}
