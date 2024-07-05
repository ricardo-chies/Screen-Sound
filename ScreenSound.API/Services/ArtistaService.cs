using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Models;
using System.Security.Claims;
using ScreenSound.Shared.Data.Models;

namespace ScreenSound.API.Services
{
    public class ArtistaService(ScreenSoundDAL<Artista> dalArtista, ScreenSoundDAL<PessoaAcesso> dalPessoa) : IArtistaService
    {
        private readonly ScreenSoundDAL<Artista> _dalArtista = dalArtista;
        private readonly ScreenSoundDAL<PessoaAcesso> _dalPessoa = dalPessoa;

        public async Task<IEnumerable<ArtistaResponse>> RecuperarTodosArtistas()
        {
            var listArtistas = await _dalArtista.RecuperarTodosArtistasComAvaliacoesAsync();

            if (listArtistas == null || !listArtistas.Any())
            {
                return null;
            }

            var listArtistaResponse = listArtistas.Select(artista => new ArtistaResponse(
                artista.Id,
                artista.Nome,
                artista.Bio,
                artista.FotoPerfil)
            {
                Classificacao = artista.Avaliacoes.Any() ? artista.Avaliacoes.Average(a => a.Nota) : (double?)0
            }).ToList();

            return listArtistaResponse;
        }

        public async Task<ArtistaResponse> RecuperarArtistaPorNome(string nome)
        {
            var artista = await _dalArtista.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

            if (artista == null)
            {
                return null;
            }

            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil)
            {
                Classificacao = artista.Avaliacoes.Any() ? artista.Avaliacoes.Average(a => a.Nota) : (double?)0
            };
        }

        public async Task<bool> AdicionarArtista(IHostEnvironment env, ArtistaRequest artistaRequest)
        {
            var nome = artistaRequest.nome.Trim();
            var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpeg";

            var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil!));
            using FileStream fs = new(path, FileMode.Create);
            await ms.CopyToAsync(fs);

            var artista = new Artista(artistaRequest.nome, artistaRequest.bio)
            {
                FotoPerfil = $"/FotosPerfil/{imagemArtista}"
            };

            await _dalArtista.Adicionar(artista);
            return true;
        }

        public async Task<bool> DeletarArtista(int id)
        {
            var artista = await _dalArtista.RecuperarPor(a => a.Id == id);

            if (artista == null)
            {
                return false;
            }

            await _dalArtista.DeletarArtista(artista);
            return true;
        }

        public async Task<bool> AtualizarArtista(ArtistaRequestEdit artistaRequestEdit)
        {
            var artista = await _dalArtista.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
            if (artista == null)
            {
                return false;
            }
            artista.Nome = artistaRequestEdit.nome;
            artista.Bio = artistaRequestEdit.bio;
            await _dalArtista.Atualizar(artista);
            return true;
        }

        public async Task<AvaliacaoArtistaResponse> RecuperarAvaliacao(int id, HttpContext context)
        {
            var artista = await _dalArtista.RecuperarArtistaComAvaliacoesPorIdAsync(id);
            if (artista == null) return null;

            var email = context.User.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            var pessoa = await _dalPessoa.RecuperarPor(p => p.Email!.Equals(email))
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            var avaliacao = artista
                .Avaliacoes
                .FirstOrDefault(a => a.ArtistaId == id && a.PessoaId == pessoa.Id);

            return new AvaliacaoArtistaResponse(id, avaliacao?.Nota ?? 0);
        }

        public async Task<bool> AdicionarAvaliacao(AvaliacaoArtistaRequest request, HttpContext context)
        {
            var artista = await _dalArtista.RecuperarArtistaComAvaliacoesPorIdAsync(request.ArtistaId);
            if (artista == null) return false;

            var email = context.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                ?? throw new InvalidOperationException("Não conectado");

            var pessoa = await _dalPessoa.RecuperarPor(p => p.Email.Equals(email))
                ?? throw new InvalidOperationException("Não conectado");

            var avaliacao = artista.Avaliacoes
                .FirstOrDefault(a => a.ArtistaId == artista.Id && a.PessoaId == pessoa.Id);

            if (avaliacao == null)
            {
                artista.AdicionarNota(pessoa.Id, request.Nota);
            }
            else
            {
                avaliacao.Nota = request.Nota;
            }

            await _dalArtista.Atualizar(artista);
            return true;
        }
    }
}
