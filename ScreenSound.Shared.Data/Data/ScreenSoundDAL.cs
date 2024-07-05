using Microsoft.EntityFrameworkCore;
using ScreenSound.Models;
using System.Linq.Expressions;

namespace ScreenSound.Data
{
    public class ScreenSoundDAL<T> where T : class
    {
        private readonly Context context;

        public ScreenSoundDAL(Context context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<T>> Listar()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task Adicionar(T objeto)
        {
            await context.Set<T>().AddAsync(objeto);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            await context.SaveChangesAsync();
        }

        public async Task Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            await context.SaveChangesAsync();
        }

        public async Task<T?> RecuperarPor(Expression<Func<T, bool>> condicao)
        {
            return await context.Set<T>().FirstOrDefaultAsync(condicao);
        }

        public async Task<IEnumerable<T>> ListarPor(Expression<Func<T, bool>> condicao)
        {
            return await context.Set<T>().Where(condicao).ToListAsync();
        }

        public async Task DeletarArtista(Artista artista)
        {
            var artistaEntity = await context.Set<Artista>()
                .Include(a => a.Musicas)
                .FirstOrDefaultAsync(a => a.Id == artista.Id);

            if (artistaEntity != null)
            {
                context.Set<Musica>().RemoveRange(artistaEntity.Musicas);
                context.Set<Artista>().Remove(artistaEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Musica>> ListarMusicasComArtistas()
        {
            return await context.Set<Musica>()
                .Include(m => m.Artista)
                .ToListAsync();
        }

        public async Task<Musica?> RecuperarMusicaComArtistaPor(Expression<Func<Musica, bool>> condicao)
        {
            return await context.Set<Musica>()
                .Include(m => m.Artista)
                .FirstOrDefaultAsync(condicao);
        }

        public async Task<Artista?> RecuperarArtistaComAvaliacoesPorIdAsync(int id)
        {
            return await context.Artistas
                .Include(a => a.Avaliacoes)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Artista>> RecuperarTodosArtistasComAvaliacoesAsync()
        {
            return await context.Artistas
                .Include(a => a.Avaliacoes)
                .ToListAsync();
        }
    }
}
