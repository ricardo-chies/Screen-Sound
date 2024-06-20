using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEnpointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal) =>
            {
                var listArtistas = dal.Listar();

                if (listArtistas is null)
                {
                    return Results.NotFound();
                }

                var listArtistaResponse = EntityListToResponseList(listArtistas);
                return Results.Ok(listArtistaResponse);
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] ScreenSoundDAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(EntityToResponse(artista));
            });

            app.MapPost("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.nome, artistaRequest.bio);
                dal.Adicionar(artista);
                return Results.Ok();
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] ScreenSoundDAL<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Id == id);

                if (artista is null)
                {
                    Results.NotFound();
                }

                dal.Deletar(artista);
                return Results.NoContent();
            });

            app.MapPut("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) => {

                var artista = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
                if (artista is null)
                {
                    return Results.NotFound();
                }
                artista.Nome = artistaRequestEdit.nome;
                artista.Bio = artistaRequestEdit.bio;
                dal.Atualizar(artista);
                return Results.Ok();
            });
        }

        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
        {
            return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
        }

    }
}