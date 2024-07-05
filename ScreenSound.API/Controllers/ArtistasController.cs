using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
namespace ScreenSound.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    //[ApiExplorerSettings(GroupName = "Artistas")]
    public class ArtistasController(IArtistaService artistaService, IHostEnvironment env) : ControllerBase
    {
        private readonly IArtistaService _artistaService = artistaService;
        private readonly IHostEnvironment _env = env;

        [HttpGet]
        public async Task<ActionResult<List<ArtistaResponse>>> GetArtistas()
        {
            var listArtistas = await _artistaService.RecuperarTodosArtistas();

            if (listArtistas == null || !listArtistas.Any())
            {
                return NotFound();
            }

            return Ok(listArtistas);
        }

        [HttpGet("{nome}")]
        public ActionResult<ArtistaResponse> GetArtistaPorNome(string nome)
        {
            var artista = _artistaService.RecuperarArtistaPorNome(nome);

            if (artista == null)
            {
                return NotFound();
            }

            return Ok(artista);
        }

        [HttpPost]
        public async Task<ActionResult> PostArtista([FromBody] ArtistaRequest artistaRequest)
        {
            var result = await _artistaService.AdicionarArtista(_env, artistaRequest);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArtista(int id)
        {
            var result = _artistaService.DeletarArtista(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut]
        public ActionResult PutArtista([FromBody] ArtistaRequestEdit artistaRequestEdit)
        {
            var result = _artistaService.AtualizarArtista(artistaRequestEdit);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("{id}/avaliacao")]
        public async Task<ActionResult<AvaliacaoArtistaResponse>> GetAvaliacao(int id)
        {
            var avaliacao = await _artistaService.RecuperarAvaliacao(id, HttpContext);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return Ok(avaliacao);
        }

        [HttpPost("avaliacao")]
        public async Task<ActionResult> PostAvaliacao([FromBody] AvaliacaoArtistaRequest request)
        {
            var result = await _artistaService.AdicionarAvaliacao(request, HttpContext);

            if (result)
            {
                return Created("", null);
            }

            return BadRequest();
        }
    }
}
