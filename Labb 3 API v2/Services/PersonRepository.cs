using Labb_3_API_v2.Data;
using Labb_3_API_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb_3_API_v2.Services
{
    public class PersonRepository : IRepository<Person>
    {
        private AppDbContext _appDbContext;
        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Person> Add(Person entity)
        {
            if (entity != null)
            {
                var result = await _appDbContext.AddAsync(entity);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Person> Delete(Person entity)
        {
            var result = await _appDbContext.Persons.FirstOrDefaultAsync(p => p.PersonId == entity.PersonId);
            if (result != null)
            {
                _appDbContext.Persons.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _appDbContext.Persons.ToListAsync();
        }

        public async Task<Person> GetById(int id)
        {
            return await _appDbContext.Persons.FirstOrDefaultAsync(p => p.PersonId == id);
        }

        public async Task<Person> Update(Person entity)
        {
            var person = await _appDbContext.Persons.FirstOrDefaultAsync(p => p.PersonId == entity.PersonId);
            if (person != null)
            {
                person.Name = entity.Name;
                person.Interests = entity.Interests;
                person.Phone = entity.Phone;
                person.Links = entity.Links;
                await _appDbContext.SaveChangesAsync();
                return person;
            }
            return null;
        }
    }
}
