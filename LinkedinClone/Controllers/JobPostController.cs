using AutoMapper;
using LinkedinClone.Dtos;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPostController: ControllerBase
    {
        private readonly IJobPostRepository _jobpostRepository;
        private readonly IMapper _mapper;
        public JobPostController(IJobPostRepository jobPostRepository, IMapper mapper)
        {
            _jobpostRepository = jobPostRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateJobPostDto createJobPostDto)
        {
            try
            {
                var jobPost = _mapper.Map<JobPost>(createJobPostDto);

                var save = await _jobpostRepository.CreateJobPost(jobPost);
                return Ok(save);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var post = await _jobpostRepository.Get();
                if (post == null)
                {
                    return BadRequest();
                }

                var jobpost = _mapper.Map<List<JobPostDto>>(post);
                return Ok(jobpost);
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync($"{ex}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPostDto>> GetById([FromRoute] int id)
        {
            try
            {
                var postid = await _jobpostRepository.GetById(id);
                if (postid == null)
                {
                    return NotFound();
                }

                var job = _mapper.Map<JobPostDto>(postid);
                return Ok(job);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return BadRequest();
            }
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateJobPostDto updateJobPostDto, [FromRoute] int id)
        {
            try
            {
                var jobPost = _mapper.Map<JobPost>(updateJobPostDto);

                var update = await _jobpostRepository.UpdateJobPost(jobPost, id);
                return Ok(update);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return BadRequest();
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var jobpostId = await _jobpostRepository.GetById(id);
                if (jobpostId == null)
                {
                    return NotFound();
                }
                await _jobpostRepository.DeleteJobPost(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("hola")]
        [Authorize(Roles ="admin")]
        public string Saludo()
        {
            return "hola que tal, solo los admin pueden ver esto";
        }
    }
}
