using LinkedinClone.Dtos;
using LinkedinClone.Models;

namespace LinkedinClone.Repositories.Interfaces
{
    public interface IEducationRepository
    {
        Task<Education> Create(Education education, string UserId);
        Task<ResponseDto> Update(Education education, int id, string UserId);
        Task<ResponseDto> Delete(int id, string UserId);
        
    }
}
