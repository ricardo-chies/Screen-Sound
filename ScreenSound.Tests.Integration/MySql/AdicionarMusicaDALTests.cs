using Bogus;
using ScreenSound.Data;
using ScreenSound.Models;
using ScreenSound.Tests.Integration.MySql.Context;

namespace ScreenSound.Tests.Integration.MySql
{
    [Collection("ContextCollection")]
    public class AdicionarMusicaDALTests
    {
        private readonly Data.Context context;
        private readonly ScreenSoundDAL<Musica> dalMusica;

        public AdicionarMusicaDALTests(ContextFixture fixture)
        {
            context = fixture.Context;
            dalMusica = new ScreenSoundDAL<Musica>(context);
        }

        [Fact]
        public async Task AdicionarMusica_Success()
        {
            // Arrange
            var generoFaker = new Faker<Genero>()
                .RuleFor(g => g.Nome, f => f.Music.Genre())
                .RuleFor(g => g.Descricao, f => f.Lorem.Sentence());

            var generos = generoFaker.Generate(1);

            var musicaFaker = new Faker<Musica>()
                .RuleFor(m => m.Nome, f => f.Music.Random.Word())
                .RuleFor(m => m.ArtistaId, 1)
                .RuleFor(m => m.AnoLancamento, f => f.Date.Past(30).Year)
                .RuleFor(m => m.Generos, generos);

            var musica = musicaFaker.Generate();

            // Act
            await dalMusica.Adicionar(musica);

            // Assert
            var musicaRecuperada = await dalMusica.RecuperarPor(m => m.Id == musica.Id);
            Assert.NotNull(musicaRecuperada);
            Assert.Equal(musica.Nome, musicaRecuperada.Nome);
            Assert.Equal(musica.ArtistaId, musicaRecuperada.ArtistaId);
            Assert.Equal(musica.AnoLancamento, musicaRecuperada.AnoLancamento);
            Assert.Equal(musica.Generos.First().Nome, musicaRecuperada.Generos.First().Nome);
            Assert.Equal(musica.Generos.First().Descricao, musicaRecuperada.Generos.First().Descricao);
        }
    }
}
