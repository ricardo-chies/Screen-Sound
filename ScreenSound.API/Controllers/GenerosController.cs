using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Interfaces;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;

namespace ScreenSound.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class GenerosController(IGeneroService generoService) : ControllerBase
    {
        private readonly IGeneroService _generoService = generoService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroResponse>>> GetGeneros()
        {
            var generos = await _generoService.ListarGeneros();
            return Ok(generos);
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<GeneroResponse>> GetGenero(string nome)
        {
            var genero = await _generoService.RecuperarGeneroPorNome(nome);
            if (genero == null)
            {
                return NotFound("Gênero não encontrado.");
            }
            return Ok(genero);
        }

        [HttpPost]
        public async Task<ActionResult> PostGenero(GeneroRequest generoRequest)
        {
            await _generoService.AdicionarGenero(generoRequest);
            return CreatedAtAction(nameof(GetGenero), new { nome = generoRequest.Nome }, generoRequest);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenero(int id)
        {
            var result = await _generoService.DeletarGenero(id);
            if (!result)
            {
                return NotFound("Gênero para exclusão não encontrado.");
            }
            return NoContent();
        }
    }
}
