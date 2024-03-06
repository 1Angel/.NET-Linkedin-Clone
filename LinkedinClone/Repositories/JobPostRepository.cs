using LinkedinClone.Data;
using LinkedinClone.Dtos;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkedinClone.Repositories
{
    public class JobPostRepository : IJobPostRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        public JobPostRepository(IWebHostEnvironment environment, AppDbContext context)
        {
            _environment = environment;
            _context = context;
        }
        public async Task<JobPost> CreateJobPost(JobPost jobPost, string UserId)
        {
            jobPost.UserId = UserId;
            var create = await _context.JobPosts.AddAsync(jobPost);
            _context.SaveChanges();
            return create.Entity;
        }

        public async Task<ResponseDto> DeleteJobPost(int id, string UserId)
        {
            var jobPostid = await _context.JobPosts.FirstOrDefaultAsync(x=>x.Id == id);
            if(jobPostid.UserId != UserId)
            {
                return new ResponseDto()
                {
                    Message = "You are not the creator of this job-post",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            if (jobPostid != null)
            {
                _context.JobPosts.Remove(jobPostid);
                await _context.SaveChangesAsync();
            }

            return new ResponseDto()
            {
                Message = "Job-Post Deleted",
                StatusCode = StatusCodes.Status200OK
            };
        }
        
        public async Task<List<JobPost>> Get(FilterDto filterDto)
        {
            var queryable = _context.JobPosts.AsQueryable();

            if (!string.IsNullOrEmpty(filterDto.title))
            {
                queryable = queryable.Where(x=>x.Title.Contains(filterDto.title));
            }

            queryable = queryable.Skip((filterDto.PageNumber - 1) * filterDto.PageSize).Take(filterDto.PageSize);

            var post = await queryable.ToListAsync();
            return post;
        }

        public async Task<JobPost> GetById(int id)
        {
            var jobId = await _context.JobPosts.Include(x=>x.JobsApplication).ThenInclude(x=>x.User).Include(a=>a.User).FirstOrDefaultAsync(x => x.Id == id);
            return jobId;
        }

        public async Task<ResponseDto> UpdateJobPost(JobPost jobPost, int id, string UserId)
        {
            var jobId = await _context.JobPosts.FirstOrDefaultAsync(x => x.Id == id);

            if(jobId.UserId != UserId)
            {
                return new ResponseDto()
                {
                    Message = "Yo are not the creator of this job-post",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }


            if(jobId != null)
            {
                jobId.Title = jobPost.Title;
                jobId.Description = jobPost.Description;

                await _context.SaveChangesAsync();
            }
            return new ResponseDto()
            {
                Message = "Job-Post Updated",
                StatusCode = StatusCodes.Status200OK,

            };
        }
    }
}
