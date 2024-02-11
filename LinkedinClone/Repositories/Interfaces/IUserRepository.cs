using LinkedinClone.Dtos.User;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(RegisterUserDto registerUserDto);
        Task<User> Login(LoginUserDto loginUserDto);
    }
}
