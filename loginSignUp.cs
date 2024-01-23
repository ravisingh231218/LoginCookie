using System.ComponentModel.DataAnnotations;

namespace UserLogin.Models
{
    public class loginSignUp
    {
        [Key]
        [Required(ErrorMessage ="UserName is required")]
        public string Username { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
