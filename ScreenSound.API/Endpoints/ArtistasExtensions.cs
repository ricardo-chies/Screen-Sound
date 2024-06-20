using Microsoft.AspNetCore.Mvc;
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
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] ScreenSoundDAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal, [FromBody] Artista artista) =>
            {
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

            app.MapPut("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal, [FromBody] Artista artistaAtualizado) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Id == artistaAtualizado.Id);

                if (artista is null)
                {
                    Results.NotFound();
                }

                artista.Nome = artistaAtualizado.Nome;
                artista.Bio = artistaAtualizado.Bio;
                artista.FotoPerfil = artistaAtualizado.FotoPerfil;

                dal.Atualizar(artista);
                return Results.Ok();
            });
        }
    }
}
