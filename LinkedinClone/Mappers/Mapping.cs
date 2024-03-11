using AutoMapper;
using LinkedinClone.Dtos.Education;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Dtos.JobPost;
using LinkedinClone.Dtos.Skills;
using LinkedinClone.Dtos.User;
using LinkedinClone.Models;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<User, UserProfileDto>();



            //skills
            CreateMap<CreateSkillDto, Skills>();

            CreateMap<Skills, SkillsDto>();



            //education
            CreateMap<CreateEducationDto, Education>();

            CreateMap<Education,  EducationDto>();  

        }
    }
}
