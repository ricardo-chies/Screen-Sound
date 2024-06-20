using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] ScreenSoundDAL<Musica> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] ScreenSoundDAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

                if (musica is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(musica);

            });

            app.MapPost("/Musicas", ([FromServices] ScreenSoundDAL<Musica> dal, [FromBody] Musica musica) =>
            {
                dal.Adicionar(musica);
                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] ScreenSoundDAL<Musica> dal, int id) => {
                var musica = dal.RecuperarPor(a => a.Id == id);

                if (musica is null)
                {
                    return Results.NotFound();
                }

                dal.Deletar(musica);
                return Results.NoContent();

            });

            app.MapPut("/Musicas", ([FromServices] ScreenSoundDAL<Musica> dal, [FromBody] Musica musicaAtualizada) => {
                var musica = dal.RecuperarPor(a => a.Id == musicaAtualizada.Id);

                if (musica is null)
                {
                    return Results.NotFound();
                }

                musica.Nome = musicaAtualizada.Nome;
                musica.AnoLancamento = musicaAtualizada.AnoLancamento;

                dal.Atualizar(musica);
                return Results.Ok();
            });
        }
    }
}
