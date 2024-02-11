using LinkedinClone.Models;

namespace LinkedinClone.Dtos.JobApplication
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CurriculumUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
