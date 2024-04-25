using Labb_3_API_v2.Data;
using Labb_3_API_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb_3_API_v2.Services
{
    public class InterestRepository : IRepository<Interest>
    {
        private AppDbContext _appDbContext;

        public InterestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Interest> Add(Interest entity)
        {
            if (entity != null)
            {
                var result = await _appDbContext.Interests.AddAsync(entity);
                _appDbContext.SaveChanges();
                return result.Entity;
            }
            return null;
        }

        public async Task<Interest> Delete(Interest entity)
        {
            var result = await GetById(entity.InterestId);
            if(result != null)
            {
                _appDbContext.Interests.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Interest>> GetAll()
        {
            return await _appDbContext.Interests.Include(i => i.Persons).ToListAsync();
        }

        public async Task<Interest> GetById(int id)
        {
            return await _appDbContext.Interests.FirstOrDefaultAsync(i => i.InterestId == id);
        }

        public async Task<Interest> Update(Interest entity)
        {
            var interest = await _appDbContext.Interests.FirstOrDefaultAsync(i => i.InterestId ==  entity.InterestId);
            if (interest != null)
            {
                interest.Title = entity.Title;
                interest.Description = entity.Description;
                interest.Persons = entity.Persons;
                interest.Links = entity.Links;
                await _appDbContext.SaveChangesAsync();
                return interest;
            }
            return null;
        }
    }
}
