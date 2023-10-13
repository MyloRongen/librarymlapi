using librarymylo_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Interfaces.Services
{
    public interface IPersonService
    {
        Task Login(Person person);
        Task<Person> GetPerson(Person person);
    }
}
