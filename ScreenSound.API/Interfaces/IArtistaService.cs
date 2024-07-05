using ScreenSound.API.Requests;
using ScreenSound.API.Responses;

public interface IArtistaService
{
    Task<IEnumerable<ArtistaResponse>> RecuperarTodosArtistas();
    ArtistaResponse RecuperarArtistaPorNome(string nome);
    Task<bool> AdicionarArtista(IHostEnvironment env, ArtistaRequest artistaRequest);
    bool DeletarArtista(int id);
    bool AtualizarArtista(ArtistaRequestEdit artistaRequestEdit);
    Task<AvaliacaoArtistaResponse> RecuperarAvaliacao(int id, HttpContext context);
    Task<bool> AdicionarAvaliacao(AvaliacaoArtistaRequest request, HttpContext context);
}
