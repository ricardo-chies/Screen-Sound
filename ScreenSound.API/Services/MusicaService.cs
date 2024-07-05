using ScreenSound.API.Interfaces;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.API.Services
{
    public class MusicaService(ScreenSoundDAL<Musica> dalMusica, ScreenSoundDAL<Genero> dalGenero) : IMusicaService
    {
        private readonly ScreenSoundDAL<Musica> _dalMusica = dalMusica;
        private readonly ScreenSoundDAL<Genero> _dalGenero = dalGenero;

        public async Task<IEnumerable<MusicaResponse>> ListarMusicas()
        {
            var listMusica = await _dalMusica.ListarMusicasComArtistas();
            return listMusica.Select(musica => new MusicaResponse(musica.Id, musica.Nome, musica.Artista?.Id, musica.Artista?.Nome));
        }

        public async Task<MusicaResponse?> RecuperarMusicaPorNome(string nome)
        {
            var musica = await _dalMusica.RecuperarMusicaComArtistaPor(a => a.Nome.ToUpper() == nome.ToUpper());
            return musica != null ? new MusicaResponse(musica.Id, musica.Nome, musica.Artista?.Id, musica.Artista?.Nome) : null;
        }

        public async Task AdicionarMusica(MusicaRequest musicaRequest)
        {
            var musica = new Musica(musicaRequest.Nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.AnoLancamento,
                Generos = musicaRequest.Generos?.Any() == true ?
                    await GeneroRequestConverter(musicaRequest.Generos) : new List<Genero>()
            };

            await _dalMusica.Adicionar(musica);
        }

        public async Task<bool> DeletarMusica(int id)
        {
            var musica = await _dalMusica.RecuperarPor(a => a.Id == id);
            if (musica == null)
                return false;

            await _dalMusica.Deletar(musica);
            return true;
        }

        public async Task<bool> AtualizarMusica(MusicaRequestEdit musicaRequestEdit)
        {
            var musica = await _dalMusica.RecuperarPor(a => a.Id == musicaRequestEdit.Id);
            if (musica == null)
                return false;

            musica.Nome = musicaRequestEdit.Nome;
            musica.AnoLancamento = musicaRequestEdit.AnoLancamento;

            await _dalMusica.Atualizar(musica);
            return true;
        }

        private async Task<List<Genero>> GeneroRequestConverter(IEnumerable<GeneroRequest> generos)
        {
            var listGeneros = new List<Genero>();
            foreach (var generoRequest in generos)
            {
                var genero = await _dalGenero.RecuperarPor(g => g.Nome.ToUpper() == generoRequest.Nome.ToUpper());
                listGeneros.Add(genero ?? new Genero { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao });
            }
            return listGeneros;
        }
    }
}
