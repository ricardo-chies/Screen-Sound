using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
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
                var listMusica = dal.Listar();

                if (listMusica is null)
                {
                    return Results.NotFound();
                }

                var listMusicaResponse = EntityListToResponseList(listMusica);
                return Results.Ok(listMusicaResponse);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] ScreenSoundDAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

                if (musica is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(EntityToResponse(musica));
            });

            app.MapPost("/Musicas", ([FromServices] ScreenSoundDAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.nome);
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

            app.MapPut("/Musicas", ([FromServices] ScreenSoundDAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) => {
                var musica = dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id);

                if (musica is null)
                {
                    return Results.NotFound();
                }

                musica.Nome = musicaRequestEdit.nome;
                musica.AnoLancamento = musicaRequestEdit.anoLancamento;

                dal.Atualizar(musica);
                return Results.Ok();
            });
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista?.Id, musica.Artista?.Nome);
        }

    }
}
