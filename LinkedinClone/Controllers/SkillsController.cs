using AutoMapper;
using LinkedinClone.Dtos.Skills;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly IMapper _mapper;
        private readonly iUserProvider _userProvider;
        public SkillsController(ISkillsRepository skillsRepository, IMapper mapper, iUserProvider iUserProvider)
        {
            _userProvider = iUserProvider;
            _skillsRepository = skillsRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateSkillDto createSkillDto)
        {
            try
            {
                var userId = _userProvider.UserId();
                var skills = _mapper.Map<Skills>(createSkillDto);

                var create = _skillsRepository.Create(skills, userId);
                return Ok(create);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var userId = _userProvider.UserId();
                _skillsRepository.Delete(id, userId);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var skillId = await _skillsRepository.GetById(id);
            if(skillId == null)
            {
                return NotFound();
            }
            return Ok(skillId);
        }
    }
}
