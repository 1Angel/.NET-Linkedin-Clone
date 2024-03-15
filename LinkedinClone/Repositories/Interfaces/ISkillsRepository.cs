using LinkedinClone.Dtos;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface ISkillsRepository
    {
        Task<Skills> Create(Skills skills, string userId);
        Task<Skills> GetById(int id);
        Task<ResponseDto> Delete(int id, string userId);

    }
}
