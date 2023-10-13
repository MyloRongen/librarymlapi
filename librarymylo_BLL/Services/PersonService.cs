using librarymylo_BLL.Interfaces.Repositories;
using librarymylo_BLL.Interfaces.Services;
using librarymylo_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task Login(Person person)
        {
            await _personRepository.Login(person);
        }

        public async Task<Person> GetPerson(Person person)
        {
            return await _personRepository.GetPerson(person);
        }
    }
}
