using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage = "Email is Required ")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required ")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required ")]
        [Compare("Password", ErrorMessage ="Confirm Password Password dos not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

        
    }
}
