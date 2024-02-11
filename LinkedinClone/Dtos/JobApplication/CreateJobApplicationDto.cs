using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.JobApplication
{
    public class CreateJobApplicationDto
    {
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public IFormFile CurriculumUrl { get; set; }
    }
}
