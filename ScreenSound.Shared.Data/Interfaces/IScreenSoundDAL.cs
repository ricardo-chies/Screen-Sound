using ScreenSound.Models;
using System.Linq.Expressions;

namespace ScreenSound.Shared.Data.Interfaces
{
    public interface IScreenSoundDAL<T> where T : class
    {
        Task<IEnumerable<T>> Listar();
        Task Adicionar(T objeto);
        Task Atualizar(T objeto);
        Task Deletar(T objeto);
        Task<T?> RecuperarPor(Expression<Func<T, bool>> condicao);
        Task<IEnumerable<T>> ListarPor(Expression<Func<T, bool>> condicao);
        Task DeletarArtista(Artista artista);
        Task<IEnumerable<Musica>> ListarMusicasComArtistas();
        Task<Musica?> RecuperarMusicaComArtistaPor(Expression<Func<Musica, bool>> condicao);
        Task<Artista?> RecuperarArtistaComAvaliacoesPorIdAsync(int id);
        Task<IEnumerable<Artista>> RecuperarTodosArtistasComAvaliacoesAsync();
    }

}
