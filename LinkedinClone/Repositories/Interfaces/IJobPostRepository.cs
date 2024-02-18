using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IJobPostRepository
    {
        Task<JobPost> CreateJobPost(JobPost jobPost, string UserId);
        Task<List<JobPost>> Get();
        Task<JobPost> GetById(int id);
        Task<JobPost> UpdateJobPost(JobPost jobPost, int id);
        Task DeleteJobPost(int id);
    }
}
