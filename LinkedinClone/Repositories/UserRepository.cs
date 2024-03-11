using LinkedinClone.Data;
using LinkedinClone.Dtos.User;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkedinClone.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;

        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> GeneratedToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Jwtkey").Value);

            var jwtHandler = new JwtSecurityTokenHandler();


            var claim = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim("Email", user.Email),
            };

            var roleUser = await  _userManager.GetRolesAsync(user);
            foreach(var role in roleUser)
            {
                claim.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var expire = DateTime.Now.AddMonths(1);
            var SignalCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claim, expires: expire, signingCredentials: SignalCredentials);

            var token = jwtHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<AuthResponseDto> Login(LoginUserDto loginUserDto)
        {
            var findEmail = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (findEmail == null)
            {
                return new AuthResponseDto()
                {
                    Message = $"User with the email {loginUserDto.Email} not exist",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            var comparePassword = await _userManager.CheckPasswordAsync(findEmail, loginUserDto.Password);
            if (!comparePassword)
            {
                return new AuthResponseDto()
                {
                    Message = "Password dont match",
                    Status = StatusCodes.Status400BadRequest
                };
            }

            var token = await GeneratedToken(findEmail);

            return new AuthResponseDto()
            {
                Message = "User Logged In",
                Status = StatusCodes.Status200OK,
                JwtToken = token
            };
        }

        //register
        public async Task<AuthResponseDto> Register(RegisterUserDto registerUserDto)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUserDto.Email);
            if(userExist != null)
            {
                return new AuthResponseDto()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = $"User with the email {registerUserDto.Email} already exists"
                };
            }

            var user = new User()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
            };

            var create = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (!create.Succeeded)
            {
                foreach (var error in create.Errors)
                {
                    return new AuthResponseDto()
                    {
                        Message = error.Description,
                        Status = StatusCodes.Status400BadRequest,
                    };
                }
            }
            if(await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            
            


            //jwttoken
            var token = await GeneratedToken(user);

            return new AuthResponseDto()
            {
                Status = StatusCodes.Status200OK,
                Message = "User Created sucessfully",
                JwtToken = token
            };


        }

        //user profile 
        public async Task<User> UserProfile(string userId)
        {
            var userinfo = await _context.users.Where(x=>x.Id == userId).Include(x => x.JobPosts).Include(x => x.JobApplications).Include(x=>x.Skills).FirstOrDefaultAsync();

            return userinfo;
        }
    }
}
