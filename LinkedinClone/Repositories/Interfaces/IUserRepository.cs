using LinkedinClone.Dtos.User;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<AuthResponseDto> Register(RegisterUserDto registerUserDto);
        Task<AuthResponseDto> Login(LoginUserDto loginUserDto);
        Task<User> UserProfile(string userId);
        Task<string> GeneratedToken(User user);
    }
}
