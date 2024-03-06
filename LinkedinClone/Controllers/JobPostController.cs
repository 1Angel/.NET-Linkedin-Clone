using AutoMapper;
using LinkedinClone.Dtos;
using LinkedinClone.Dtos.JobPost;
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
        private readonly iUserProvider _userprovider;
        public JobPostController(IJobPostRepository jobPostRepository, IMapper mapper, iUserProvider userprovider)
        {
            _jobpostRepository = jobPostRepository;
            _mapper = mapper;
            _userprovider = userprovider;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateJobPostDto createJobPostDto)
        {
            try
            {
                var userId = _userprovider.UserId();
                var jobPost = _mapper.Map<JobPost>(createJobPostDto);

                var save = await _jobpostRepository.CreateJobPost(jobPost, userId);
                return Ok(save);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] FilterDto filterDto)
        {
            try
            {
                var post = await _jobpostRepository.Get(filterDto);
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
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateJobPostDto updateJobPostDto, [FromRoute] int id)
        {
            try
            {
                var UserId = _userprovider.UserId();
                var jobPost = _mapper.Map<JobPost>(updateJobPostDto);

                var update = await _jobpostRepository.UpdateJobPost(jobPost, id, UserId);
                return Ok(update);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return BadRequest();
            }
        }


        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var UserId = _userprovider.UserId();
                var jobpostId = await _jobpostRepository.GetById(id);
                if (jobpostId == null)
                {
                    return NotFound();
                }
                await _jobpostRepository.DeleteJobPost(id, UserId);
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


        [HttpGet("prueba")]
        [Authorize]
        public string probandoContext()
        {
            var userid = _userprovider.UserId();
            return userid;
        }
    }
}
