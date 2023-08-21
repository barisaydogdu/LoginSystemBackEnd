using System.ComponentModel.DataAnnotations;

namespace LoginSystemBackEnd.Models
{
    public class UserLoginModel
    {
        //Giriş yapmak için kullanılacak
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
