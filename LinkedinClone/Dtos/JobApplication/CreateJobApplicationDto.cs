using LinkedinClone.Validations;
using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.JobApplication
{
    public class CreateJobApplicationDto
    {
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        [FileTypeValidation]
        public IFormFile CurriculumUrl { get; set; }
    }
}
