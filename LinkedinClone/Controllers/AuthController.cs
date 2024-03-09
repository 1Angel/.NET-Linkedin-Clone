using AutoMapper;
using LinkedinClone.Dtos.User;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly iUserProvider _userProvider;

        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, iUserProvider userProvider, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userProvider = userProvider;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var registerUser = await _userRepository.Register(registerUserDto);
            if(registerUser == null)
            {
                return BadRequest();
            }

            return Ok(registerUser);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var loggedIn =  await _userRepository.Login(loginUserDto);
            if(loggedIn == null)
            {
                return BadRequest();
            }
            return Ok(loggedIn);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult> UserProfile()
        {
            var userId = _userProvider.UserId();
            var user = await _userRepository.UserProfile(userId);

            var userua = _mapper.Map<UserProfileDto>(user);

            return Ok(userua);
        }
    }
}
