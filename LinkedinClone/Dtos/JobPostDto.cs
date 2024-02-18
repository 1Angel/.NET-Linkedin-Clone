using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Dtos.User;
using LinkedinClone.Models;

namespace LinkedinClone.Dtos
{
    public class JobPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDto User { get; set; }
        public List<JobApplicationDto> JobsApplication { get; set; }    
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
