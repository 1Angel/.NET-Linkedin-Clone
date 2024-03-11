using LinkedinClone.Data;
using LinkedinClone.Dtos;
using LinkedinClone.Models;
using LinkedinClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkedinClone.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly AppDbContext _context;
        public EducationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Education> Create(Education education, string UserId)
        {
            education.UserId = UserId;
            var create = await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();
            return create.Entity;
        }

        public async Task<ResponseDto> Delete(int id, string UserId)
        {
            var educationId = await _context.Educations.FirstOrDefaultAsync(x => x.Id == id);

            if(educationId.UserId != UserId)
            {
                return new ResponseDto()
                {
                    Message = "you're not the creator of this",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            if (educationId != null)
            {
                 _context.Educations.Remove(educationId);
                await _context.SaveChangesAsync();
            }

            return new ResponseDto()
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public async Task<ResponseDto> Update(Education education, int id, string UserId)
        {
            var educationId = await _context.Educations.FirstOrDefaultAsync(x => x.Id == id);
            if(educationId.UserId != UserId)
            {
                return new ResponseDto()
                {
                    Message = "You're not the creator of this",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            if(educationId != null)
            {
                educationId.Name = education.Name;
                educationId.StartDate = education.StartDate;
                educationId.EndDate = education.EndDate;

                await _context.SaveChangesAsync();
            }

            return new ResponseDto()
            {
                Message = "Updated",
                StatusCode = StatusCodes.Status200OK
            };

        }
    }
}
