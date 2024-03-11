using Microsoft.AspNetCore.Identity;

namespace LinkedinClone.Models
{
    public class User: IdentityUser
    {
        public List<JobPost> JobPosts { get; set; }
        public List<JobApplication> JobApplications { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Education> Educations { get; set; }

    }
}
