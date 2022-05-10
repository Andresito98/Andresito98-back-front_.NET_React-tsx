using Back.NET.Data;
using Back.NET.Dtos;
using Back.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatoController : Controller
    {
        private readonly CandidatoContext _candidatoContext;
        private readonly IUpdateCandidato _updateCandidato;

        public CandidatoController(CandidatoContext candidatoContext, IUpdateCandidato updateCandidato)
        {
            _candidatoContext = candidatoContext;
            _updateCandidato = updateCandidato;

        }

        [HttpGet]
        //[Route("GetAllPersons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Candidato>))]
        public async Task<IActionResult> GetCandidatos()
        {
            var result = _candidatoContext.Candidatos.Select(c => c.ToDto()).ToList();

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        //[Route("GetPersonId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Candidato))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCandidatosId(int id)
        {
            CandidatoEntity result = await _candidatoContext.Get(id);

            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        //[Route("DelePersonId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var result = await _candidatoContext.Delete(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        //[Route("AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Candidato))]
        public async Task<IActionResult> CreateCandidato(CreateCandidato candidato)
        {
            CandidatoEntity result = await _candidatoContext.Add(candidato);

            return new CreatedResult($"https://localhost:7200/api/candidatos/{result.Id}", null);
        }

        [HttpPut]
        //[Route("UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Candidato))]
        public async Task<IActionResult> UpdateCandidato(Candidato candidato)
        {
            Candidato? result = await _updateCandidato.Execute(candidato);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }

    }
}
