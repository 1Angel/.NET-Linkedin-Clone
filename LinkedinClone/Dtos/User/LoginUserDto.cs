using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.User
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Password { get; set; }
    }
}
