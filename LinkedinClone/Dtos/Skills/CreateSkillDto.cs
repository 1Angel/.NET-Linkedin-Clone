using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.Skills
{
    public class CreateSkillDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(0)]
        public string Name { get; set; }
    }
}
