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
    public class MusicasController(IMusicaService musicaService) : ControllerBase
    {
        private readonly IMusicaService _musicaService = musicaService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicaResponse>>> GetMusicas()
        {
            var musicas = await _musicaService.ListarMusicas();
            return Ok(musicas);
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<MusicaResponse>> GetMusica(string nome)
        {
            var musica = await _musicaService.RecuperarMusicaPorNome(nome);
            if (musica == null)
                return NotFound();

            return Ok(musica);
        }

        [HttpPost]
        public async Task<IActionResult> PostMusica([FromBody] MusicaRequest musicaRequest)
        {
            await _musicaService.AdicionarMusica(musicaRequest);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusica(int id)
        {
            var result = await _musicaService.DeletarMusica(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutMusica([FromBody] MusicaRequestEdit musicaRequestEdit)
        {
            var result = await _musicaService.AtualizarMusica(musicaRequestEdit);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
