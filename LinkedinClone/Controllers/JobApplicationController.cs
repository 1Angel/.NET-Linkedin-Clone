using AutoMapper;
using LinkedinClone.Dtos.JobApplication;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController: ControllerBase
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;
        private readonly iUserProvider _userProvider;


        public JobApplicationController(IJobApplicationRepository jobApplicationRepository, IMapper mapper, iUserProvider  userProvider)
        {
            _userProvider = userProvider;
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create/{id}")]
        public async Task<ActionResult> CreateJobApp([FromForm] CreateJobApplicationDto createJobApplicationDto, [FromRoute] int id)
        {
            var userId = _userProvider.UserId();
            var jobApplication = await _jobApplicationRepository.Create(createJobApplicationDto, id, userId);

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


        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var userId = _userProvider.UserId();
                var jobAppId = await _jobApplicationRepository.GetById(id);
                if(jobAppId == null)
                {
                    return NotFound();
                }

                var jobApp = await _jobApplicationRepository.Delete(id, userId);
                return Ok(jobApp);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
