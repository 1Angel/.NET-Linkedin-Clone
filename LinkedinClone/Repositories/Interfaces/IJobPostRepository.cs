using LinkedinClone.Dtos;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IJobPostRepository
    {
        Task<JobPost> CreateJobPost(JobPost jobPost, string UserId);
        Task<List<JobPost>> Get(FilterDto filterDto);
        Task<JobPost> GetById(int id);
        Task<ResponseDto> UpdateJobPost(JobPost jobPost, int id, string UserId);
        Task<ResponseDto> DeleteJobPost(int id, string UserId);
    }
}
