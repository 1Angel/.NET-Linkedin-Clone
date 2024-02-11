using AutoMapper;
using LinkedinClone.Dtos;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Models;

namespace LinkedinClone.Mappers
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            //jobpost mapping
            CreateMap<CreateJobPostDto, JobPost>();

            CreateMap<JobPost, JobPostDto>();


            CreateMap<UpdateJobPostDto, JobPost>();

            //job-application mapping
            CreateMap<JobApplication, JobApplicationDto>();
        }
    }
}
