using Labb_3_API_v2.Data;
using Labb_3_API_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb_3_API_v2.Services
{
    public class LinkRepository : IRepository<Link>
    {
        private AppDbContext _appDbContext;
        public LinkRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Link> Add(Link entity)
        {
            if(entity != null)
            {
                var result = await _appDbContext.AddAsync(entity);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public Task<Link> Delete(Link entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _appDbContext.Links.ToListAsync();
        }

        public async Task<Link> GetById(int id)
        {
            return await _appDbContext.Links.FirstOrDefaultAsync(li => li.LinkId == id);
        }

        public Task<Link> Update(Link entity)
        {
            throw new NotImplementedException();
        }
    }
}
