using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.OpenApi.Expressions;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Models;
using System.Security.Claims;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndpointsArtistas(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("artistas")
                .RequireAuthorization()
                .WithTags("Artistas");

            groupBuilder.MapGet("", async ([FromServices] ScreenSoundDAL<Artista> dal) =>
            {
                var listArtistas = await dal.RecuperarTodosArtistasComAvaliacoesAsync();

                if (listArtistas is null || !listArtistas.Any())
                {
                    return Results.NotFound();
                }

                var listArtistaResponse = listArtistas.Select(artista => new ArtistaResponse(
                    artista.Id,
                    artista.Nome,
                    artista.Bio,
                    artista.FotoPerfil)
                {
                    Classificacao = artista.Avaliacoes.Any() ? artista.Avaliacoes.Average(a => a.Nota) : (double?)0
                }).ToList();

                return Results.Ok(listArtistaResponse);
            });

            groupBuilder.MapGet("{nome}", ([FromServices] ScreenSoundDAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(EntityToResponse(artista));
            });

            groupBuilder.MapPost("", async ([FromServices] IHostEnvironment env, [FromServices] ScreenSoundDAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var nome = artistaRequest.nome.Trim();
                var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpeg";

                var path = Path.Combine(env.ContentRootPath,"wwwroot", "FotosPerfil", imagemArtista);

                using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil!));
                using FileStream fs = new(path, FileMode.Create);
                await ms.CopyToAsync(fs);

                var artista = new Artista(artistaRequest.nome, artistaRequest.bio)
                {
                    FotoPerfil = $"/FotosPerfil/{imagemArtista}"
                };

                dal.Adicionar(artista);
                return Results.Ok();
            });

            groupBuilder.MapDelete("{id}", ([FromServices] ScreenSoundDAL<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Id == id);

                if (artista is null)
                {
                    Results.NotFound();
                }

                dal.DeletarArtista(artista);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] ScreenSoundDAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) => {

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

            groupBuilder.MapGet("{id}/avaliacao", async (
                        int id,
                        HttpContext context,
                        [FromServices] ScreenSoundDAL<Artista> dalArtista,
                        [FromServices] ScreenSoundDAL<PessoaAcesso> dalPessoa
                        ) =>
            {
                var artista = await dalArtista.RecuperarArtistaComAvaliacoesPorIdAsync(id);
                if (artista is null) return Results.NotFound();

                var email = context.User.Claims
                    .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value
                    ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

                var pessoa = dalPessoa.RecuperarPor(p => p.Email!.Equals(email))
                    ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

                var avaliacao = artista
                    .Avaliacoes
                    .FirstOrDefault(a => a.ArtistaId == id && a.PessoaId == pessoa.Id);

                if (avaliacao is null) return Results.Ok(new AvaliacaoArtistaResponse(id, 0));
                else return Results.Ok(new AvaliacaoArtistaResponse(id, avaliacao.Nota));
            });

            groupBuilder.MapPost("avaliacao", async (
                HttpContext context,
                [FromBody] AvaliacaoArtistaRequest request,
                [FromServices] ScreenSoundDAL<Artista> dalArtista,
                [FromServices] ScreenSoundDAL<PessoaAcesso> dalPessoa
                ) =>
            {
                var artista = await dalArtista.RecuperarArtistaComAvaliacoesPorIdAsync(request.ArtistaId);
                if (artista is null) return Results.NotFound();

                var email = context.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                    ?? throw new InvalidOperationException("Não conectado");

                var pessoa = dalPessoa.RecuperarPor(p => p.Email.Equals(email))
                    ?? throw new InvalidOperationException("Não conectado");

                var avaliacao = artista.Avaliacoes
                    .FirstOrDefault(a => a.ArtistaId == artista.Id && a.PessoaId == pessoa.Id);

                if (avaliacao is null)
                {
                    artista.AdicionarNota(pessoa.Id, request.Nota);
                }
                else
                {
                    avaliacao.Nota = request.Nota;
                }

                dalArtista.Atualizar(artista);

                return Results.Created();
            });
        }

        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
        {
            return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil)
            { 
                Classificacao = artista
                    .Avaliacoes
                    .Select(a => a.Nota)
                    .DefaultIfEmpty(0)
                    .Average()
            };
        }

    }
}