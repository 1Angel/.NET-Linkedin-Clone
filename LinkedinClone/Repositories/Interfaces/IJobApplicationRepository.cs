using LinkedinClone.Dtos;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> Create(CreateJobApplicationDto createJobApplicationDto, int id, string userId);
        Task<ResponseDto> Delete(int id, string userId);

        Task<JobApplication> GetById(int id);
    }
}
