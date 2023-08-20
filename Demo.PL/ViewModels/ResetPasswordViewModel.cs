using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
        [Required(ErrorMessage = "Password is Required ")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required ")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Password dos not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
