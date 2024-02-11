using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> Create(CreateJobApplicationDto createJobApplicationDto, int id);
        Task<JobApplication> GetById(int id);
    }
}
