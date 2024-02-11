using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos
{
    public class CreateJobPostDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
