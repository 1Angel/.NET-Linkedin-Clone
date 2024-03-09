using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Dtos.JobPost;
using LinkedinClone.Dtos.Skills;

namespace LinkedinClone.Dtos.User
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }   
        public List<JobPostDto> JobPosts { get; set; }
        public List<JobApplicationDto> JobApplications { get; set; }

        public List<SkillsDto> Skills { get; set; }
    }
}
