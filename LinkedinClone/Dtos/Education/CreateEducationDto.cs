using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Dtos.Education
{
    public class CreateEducationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
