using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtensions
    {

        public static void AddEndpointsGeneros(this WebApplication app)
        {
            app.MapPost("/Generos", ([FromServices] ScreenSoundDAL<Genero> dal, [FromBody] GeneroRequest generoReq) =>
            {
                dal.Adicionar(RequestToEntity(generoReq));
            });


            app.MapGet("/Generos", ([FromServices] ScreenSoundDAL<Genero> dal) =>
            {
                return EntityListToResponseList(dal.Listar());
            });

            app.MapGet("/Generos/{nome}", ([FromServices] ScreenSoundDAL<Genero> dal, string nome) =>
            {
                var genero = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (genero is not null)
                {
                    var response = EntityToResponse(genero!);
                    return Results.Ok(response);
                }
                return Results.NotFound("Gênero não encontrado.");
            });

            app.MapDelete("/Generos/{id}", ([FromServices] ScreenSoundDAL<Genero> dal, int id) =>
            {
                var genero = dal.RecuperarPor(a => a.Id == id);
                if (genero is null)
                {
                    return Results.NotFound("Gênero para exclusão não encontrado.");
                }
                dal.Deletar(genero);
                return Results.NoContent();
            });
        }

        private static Genero RequestToEntity(GeneroRequest generoRequest)
        {
            return new Genero() { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao };
        }

        private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generos)
        {
            return generos.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
        }
    }
}
