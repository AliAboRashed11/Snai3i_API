using System.ComponentModel.DataAnnotations;

namespace Snai3i_MVC.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        public string first_name { get; set; }
        
        [Required]
        public string last_name { get; set; }

        [DataType(DataType.EmailAddress , ErrorMessage ="please enter a correct email ")]
        [Required]
        public string email { get; set; }

        public IFormFile? imageFile { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]

        [Compare("password" , ErrorMessage ="the password and confirmation " +
            "password do not match" )]
        public string ConfirmPassword { get; set; }
    }
}
