﻿using LinkedinClone.Data;
using LinkedinClone.Dtos;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkedinClone.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IJobPostRepository _jobPostRepository;
        public JobApplicationRepository(AppDbContext context, IWebHostEnvironment webHostEnvironment, IJobPostRepository jobPostRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _jobPostRepository = jobPostRepository;
        }
        public async Task<JobApplication> Create(CreateJobApplicationDto createJobApplicationDto, int id, string userId)
        {

            //var findJobById = await _jobPostRepository.GetById(id);
 
            

            var folder = Path.Combine(_webHostEnvironment.ContentRootPath, "cvfiles");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = $"{Guid.NewGuid()}-{createJobApplicationDto.CurriculumUrl.FileName}";

            var cvFilePath = Path.Combine(folder, fileName);

            using(FileStream fileStream = new FileStream(cvFilePath, FileMode.Create))
            {
                fileStream.CopyToAsync(fileStream);   
            }

            var jobApplication = new JobApplication()
            {
                Description = createJobApplicationDto.Description,
                CurriculumUrl = cvFilePath,
                JobPostId = id,
                UserId = userId,
            };

            var save = await _context.JobApplications.AddAsync(jobApplication);
            await _context.SaveChangesAsync();
            return save.Entity;
        }

        public async Task<ResponseDto> Delete(int id, string userId)
        {
            var jobAppId = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);

             if(jobAppId.UserId != userId)
            {
                return new ResponseDto()
                {
                    Message = "You're not the creator of this Job-Application",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            _context.JobApplications.Remove(jobAppId);
            await _context.SaveChangesAsync();
            return new ResponseDto()
            {
                Message = "Job Application Deleted",
                StatusCode = StatusCodes.Status200OK
            };

        }

        public async Task<JobApplication> GetById(int id)
        {
            var jobAppid = await _context.JobApplications.FirstOrDefaultAsync(x=>x.Id == id);
            return jobAppid;
        }


        public async Task<List<JobApplication>> GetJobsAppByUser(string userId)
        {
            var jobsAppUser = await _context.JobApplications.Where(x=>x.UserId == userId).ToListAsync();
            return jobsAppUser;
        }
    }
}
