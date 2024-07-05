using ScreenSound.API.Requests;
using ScreenSound.API.Responses;

namespace ScreenSound.API.Interfaces
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroResponse>> ListarGeneros();
        Task<GeneroResponse?> RecuperarGeneroPorNome(string nome);
        Task AdicionarGenero(GeneroRequest generoRequest);
        Task<bool> DeletarGenero(int id);
    }
}
