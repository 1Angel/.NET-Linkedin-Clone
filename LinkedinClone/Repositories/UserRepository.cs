using LinkedinClone.Dtos.User;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;

namespace LinkedinClone.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            
        }
        public Task<User> Login(LoginUserDto loginUserDto)
        {
            throw new NotImplementedException();
        }

        public Task<User> Register(RegisterUserDto registerUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
