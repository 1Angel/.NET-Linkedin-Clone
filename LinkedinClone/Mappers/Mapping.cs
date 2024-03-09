using AutoMapper;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Dtos.JobPost;
using LinkedinClone.Dtos.Skills;
using LinkedinClone.Dtos.User;
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


            //user
            CreateMap<User, UserDto>();


            //skills
            CreateMap<CreateSkillDto, Skills>();
        }
    }
}
