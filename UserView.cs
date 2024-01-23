using System.ComponentModel.DataAnnotations;

namespace UserLogin.Models
{
    public class UserView
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]

        public string Email { get; set; }
        [Required(ErrorMessage = "UserName is required")]

        public string Username { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
