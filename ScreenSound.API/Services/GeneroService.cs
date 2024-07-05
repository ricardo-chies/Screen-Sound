using ScreenSound.API.Interfaces;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.API.Services
{
    public class GeneroService(ScreenSoundDAL<Genero> dalGenero) : IGeneroService
    {
        private readonly ScreenSoundDAL<Genero> _dalGenero = dalGenero;

        public async Task<IEnumerable<GeneroResponse>> ListarGeneros()
        {
            var generos = await _dalGenero.Listar();
            return generos.Select(genero => new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!));
        }

        public async Task<GeneroResponse?> RecuperarGeneroPorNome(string nome)
        {
            var genero = await _dalGenero.RecuperarPor(g => g.Nome.ToUpper() == nome.ToUpper());
            return genero != null ? new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!) : null;
        }

        public async Task AdicionarGenero(GeneroRequest generoRequest)
        {
            var genero = new Genero
            {
                Nome = generoRequest.Nome,
                Descricao = generoRequest.Descricao
            };
            await _dalGenero.Adicionar(genero);
        }

        public async Task<bool> DeletarGenero(int id)
        {
            var genero = await _dalGenero.RecuperarPor(g => g.Id == id);
            if (genero == null)
                return false;

            await _dalGenero.Deletar(genero);
            return true;
        }
    }
}
