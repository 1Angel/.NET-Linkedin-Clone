using AutoMapper;
using LinkedinClone.Dtos;
using LinkedinClone.Dtos.Education;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController: ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly iUserProvider _userProvider;
        public EducationController(IEducationRepository educationRepository, IMapper mapper, iUserProvider userProvider)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _userProvider = userProvider;
        }


        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] CreateEducationDto createEducationDto)
        {
            var education = _mapper.Map<Education>(createEducationDto);

            var UserId = _userProvider.UserId();

            var save = await _educationRepository.Create(education, UserId);
            return Ok(save);
        }


        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ResponseDto> Delete([FromRoute] int id)
        {
            var UserId = _userProvider.UserId();
            var education = await _educationRepository.Delete(id, UserId);

            return education;
        }
    }
}
