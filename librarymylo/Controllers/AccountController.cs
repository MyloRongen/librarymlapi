using librarymylo.WebApi.Models;
using librarymylo_BLL.Interfaces.Services;
using librarymylo_BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace librarymylo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IPersonService _personService;

        public AccountController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        [Route("AddPerson")]
        public async Task Post([FromBody] PersonViewModel personViewModel)
        {
            Person person = new()
            {
                Id = personViewModel.sub,
                Name = personViewModel.name,
                Email = personViewModel.email,
            };

            Person existingPerson = await _personService.GetPerson(person);

            if (existingPerson == null)
            {
                await _personService.Login(person);
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
