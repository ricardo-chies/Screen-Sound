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

            app.MapPost("/Musicas", ([FromServices] ScreenSoundDAL<Musica> dalMusica, [FromServices] ScreenSoundDAL<Genero> dalGenero,
                [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.Nome)
                {
                    ArtistaId = musicaRequest.ArtistaId,
                    AnoLancamento = musicaRequest.AnoLancamento,
                    Generos = musicaRequest.Generos is not null ?
                        GeneroRequestConverter(musicaRequest.Generos, dalGenero) : new List<Genero>()
                };

                dalMusica.Adicionar(musica);
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

                musica.Nome = musicaRequestEdit.Nome;
                musica.AnoLancamento = musicaRequestEdit.AnoLancamento;

                dal.Atualizar(musica);
                return Results.Ok();
            });
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, ScreenSoundDAL<Genero> dalGenero)
        {
            var listGeneros = new List<Genero>();
            foreach (var i in generos)
            {
                var entity = RequestToEntity(i);
                var genero = dalGenero.RecuperarPor(g => g.Nome.ToUpper().Equals(i.Nome.ToUpper()));

                if(genero is not null)
                {
                    listGeneros.Add(genero);
                }
                else
                {
                    listGeneros.Add(entity);
                }
            }
            return listGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero()
            {
                Nome = genero.Nome,
                Descricao = genero.Descricao
            };
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
