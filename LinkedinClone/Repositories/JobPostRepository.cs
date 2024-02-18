using LinkedinClone.Data;
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

        public async Task DeleteJobPost(int id)
        {
            var jobPostid = await _context.JobPosts.FirstOrDefaultAsync(x=>x.Id == id);
            if (jobPostid != null)
            {
                _context.JobPosts.Remove(jobPostid);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<List<JobPost>> Get()
        {
            var post = await _context.JobPosts.ToListAsync();
            return post;
        }

        public async Task<JobPost> GetById(int id)
        {
            var jobId = await _context.JobPosts.Include(x=>x.JobsApplication).Include(a=>a.User).FirstOrDefaultAsync(x => x.Id == id);
            return jobId;
        }

        public async Task<JobPost> UpdateJobPost(JobPost jobPost, int id)
        {
            var jobId = await _context.JobPosts.FirstOrDefaultAsync(x => x.Id == id);
            if(jobId != null)
            {
                jobId.Title = jobPost.Title;
                jobId.Description = jobPost.Description;

                await _context.SaveChangesAsync();
            }
            return jobId;
        }
    }
}
