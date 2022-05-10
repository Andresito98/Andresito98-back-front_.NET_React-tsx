using Back.NET.Data;
using Back.NET.Dtos;
using Back.NET.Helpers;
using Back.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonContext _personContext;
        private readonly IUpdatePerson _updatePerson;

        public PersonController(PersonContext personContext, IUpdatePerson updatePerson)
        {
            _personContext = personContext;
            _updatePerson = updatePerson;

        }

        [HttpGet]
        //[Route("GetAllPersons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Person>))]
        public async Task<IActionResult> GetPersons()
        {
            var result = _personContext.Persons.Select(c => c.ToDto()).ToList();

            return new OkObjectResult(result);
        }


        [HttpGet("{id}")]
        //[Route("GetPersonId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonId(int id)
        {
            PersonEntity result = await _personContext.Get(id);

            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        //[Route("DelePersonId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _personContext.Delete(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        //[Route("AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Person))]
        public async Task<IActionResult> CreatePerson(CreatePerson person)
        {
            PersonEntity result = await _personContext.Add(person);

            return new CreatedResult($"https://localhost:7200/api/persons/{result.Id}", null);
        }

        
        [HttpPut]
        //[Route("UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            Person? result = await _updatePerson.Execute(person);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}

