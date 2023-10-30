using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Client.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone  required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

  

        [Required(ErrorMessage = "Password  required")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Password  required")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password not same")]
        public string ConfirmPassword { get; set; }

    
     
    }
}
