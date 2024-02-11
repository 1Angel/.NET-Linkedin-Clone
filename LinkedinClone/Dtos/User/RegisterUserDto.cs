using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.User
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Password { get; set; }
    }
}
