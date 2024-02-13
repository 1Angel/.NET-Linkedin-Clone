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
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
