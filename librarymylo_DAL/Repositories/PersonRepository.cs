using librarymylo_BLL.Interfaces.Repositories;
using librarymylo_BLL.Models;
using librarymylo_DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace librarymylo_DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Login(Person person)
        {
            try
            {
                _dbContext.Persons.Add(person);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
           
        }

        public async Task<Person?> GetPerson(Person person)
        {
            try
            {
                Person existinPerson = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == person.Id);

                return existinPerson;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
