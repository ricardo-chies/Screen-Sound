using ScreenSound.API.Requests;
using ScreenSound.API.Responses;

namespace ScreenSound.API.Interfaces
{
    public interface IMusicaService
    {
        Task<IEnumerable<MusicaResponse>> ListarMusicas();
        Task<MusicaResponse> RecuperarMusicaPorNome(string nome);
        Task AdicionarMusica(MusicaRequest musicaRequest);
        Task<bool> DeletarMusica(int id);
        Task<bool> AtualizarMusica(MusicaRequestEdit musicaRequestEdit);
    }
}
