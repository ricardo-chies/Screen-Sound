using ScreenSound.API.Requests;
using ScreenSound.API.Responses;

namespace ScreenSound.API.Interfaces;

public interface IArtistaService
{
    Task<IEnumerable<ArtistaResponse>> RecuperarTodosArtistas();
    Task<ArtistaResponse> RecuperarArtistaPorNome(string nome);
    Task<bool> AdicionarArtista(IHostEnvironment env, ArtistaRequest artistaRequest);
    Task<bool> DeletarArtista(int id);
    Task<bool> AtualizarArtista(ArtistaRequestEdit artistaRequestEdit);
    Task<AvaliacaoArtistaResponse> RecuperarAvaliacao(int id, HttpContext context);
    Task<bool> AdicionarAvaliacao(AvaliacaoArtistaRequest request, HttpContext context);
}