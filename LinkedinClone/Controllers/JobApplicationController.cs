using AutoMapper;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController: ControllerBase
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;



        public JobApplicationController(IJobApplicationRepository jobApplicationRepository, IMapper mapper)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
        }

        [HttpPost("create/{id}")]
        public async Task<ActionResult> CreateJobApp([FromForm] CreateJobApplicationDto createJobApplicationDto, [FromRoute] int id)
        {
            var jobApplication = await _jobApplicationRepository.Create(createJobApplicationDto, id);

            var joba = _mapper.Map<JobApplicationDto>(jobApplication);

            return Ok(joba);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationDto>> GetById([FromRoute] int id)
        {
            var jobAppid = await _jobApplicationRepository.GetById(id);
            if (jobAppid == null)
            {
                return NotFound();
            }
            return Ok(jobAppid);
        }
    }
}
