using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.JobPost
{
    public class CreateJobPostDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
