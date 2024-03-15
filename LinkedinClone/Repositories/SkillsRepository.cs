using LinkedinClone.Data;
using LinkedinClone.Dtos;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkedinClone.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly AppDbContext _dbcontext;
        public SkillsRepository(AppDbContext context)
        {
            _dbcontext = context;
        }
        public async Task<Skills> Create(Skills skills, string userId)
        {
            skills.UserId = userId;
            var create = await _dbcontext.Skills.AddAsync(skills);
            await _dbcontext.SaveChangesAsync();
            return create.Entity;

        }

        public async Task<ResponseDto> Delete(int id, string userId)
        {
            var skillId = await _dbcontext.Skills.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(skillId.UserId !=  userId)
            {
                return new ResponseDto()
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            _dbcontext.Skills.Remove(skillId);
            await _dbcontext.SaveChangesAsync();


            return new ResponseDto()
            {
                Message = "Skill deleted",
                StatusCode = StatusCodes.Status200OK
            };

        }
        public async Task<Skills> GetById(int id)
        {
            var skillId = await _dbcontext.Skills.FirstOrDefaultAsync(x=>x.Id == id);
            return skillId;
        }
    }
}
