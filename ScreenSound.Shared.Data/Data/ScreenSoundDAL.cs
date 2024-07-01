using Microsoft.EntityFrameworkCore;
using ScreenSound.Models;

namespace ScreenSound.Data;
public class ScreenSoundDAL<T> where T : class
{
    private readonly Context context;

    public ScreenSoundDAL(Context context)
    {
        this.context = context;
    }

    public IEnumerable<T> Listar()
    {
        return context.Set<T>().ToList();
    }
    public void Adicionar(T objeto)
    {
        context.Set<T>().Add(objeto);
        context.SaveChanges();
    }
    public void Atualizar(T objeto)
    {
        context.Set<T>().Update(objeto);
        context.SaveChanges();
    }
    public void Deletar(T objeto)
    {
        context.Set<T>().Remove(objeto);
        context.SaveChanges();
    }

    public T? RecuperarPor(Func<T, bool> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }

    public IEnumerable<T> ListarPor(Func<T, bool> condicao)
    {
        return context.Set<T>().Where(condicao);
    }

    public void DeletarArtista(Artista artista)
    {
        // Inclui as músicas relacionadas ao artista
        var artistaEntity = context.Set<Artista>()
            .Include(a => a.Musicas)
            .FirstOrDefault(a => a.Id == artista.Id);

        if (artistaEntity != null)
        {
            // Remove todas as músicas relacionadas ao artista
            context.Set<Musica>().RemoveRange(artistaEntity.Musicas);

            // Remove o próprio artista
            context.Set<Artista>().Remove(artistaEntity);

            context.SaveChanges();
        }
    }

    public IEnumerable<Musica> ListarMusicasComArtistas()
    {
        return context.Set<Musica>()
            .Include(m => m.Artista)
            .ToList();
    }

    public Musica? RecuperarMusicaComArtistaPor(Func<Musica, bool> condicao)
    {
        return context.Set<Musica>()
            .Include(m => m.Artista)
            .FirstOrDefault(condicao);
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
