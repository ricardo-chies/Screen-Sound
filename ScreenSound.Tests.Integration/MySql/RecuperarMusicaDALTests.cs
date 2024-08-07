using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Data;
using ScreenSound.Models;
using ScreenSound.Tests.Integration.MySql.Context;
using Xunit.Abstractions;

namespace ScreenSound.Tests.Integration.MySql
{
    [Collection("ContextCollection")]
    public class RecuperarMusicaDALTests
    {
        private readonly Data.Context context;
        private readonly ScreenSoundDAL<Musica> dalMusica;
        public RecuperarMusicaDALTests(ContextFixture fixture, ITestOutputHelper output)
        {

            context = fixture.Context;
            dalMusica = new ScreenSoundDAL<Musica>(context);
            output.WriteLine(context.GetHashCode().ToString());
        }

        [Fact]
        public async Task RecuperarMusicaPor_ShouldReturnMusica()
        {
            // Arrange
            var idArtista = 1;
            var idMusica = 1;

            // Certifique-se de que o artista já existe ou crie um novo
            var expectedArtista = await context.Artistas.FirstOrDefaultAsync(a => a.Id == idArtista);
            if (expectedArtista == null)
            {
                expectedArtista = new Artista
                {
                    Id = idArtista,
                    Nome = "Bonnie Tyler",
                    Bio = "Bonnie Tyler é uma cantora galesa conhecida por sua voz rouca e poderosa.",
                    FotoPerfil = "/FotosPerfil/bonnie_tyler.jpeg"
                };
                context.Artistas.Add(expectedArtista);
            }

            var expectedMusica = new Musica
            {
                Id = idMusica,
                Nome = "Total Eclipse of the Heart",
                AnoLancamento = 1983,
                ArtistaId = idArtista
            };

            // Verifique se a música já existe para evitar duplicatas
            var musicaExistente = await context.Musicas.FirstOrDefaultAsync(m => m.Id == idMusica);
            if (musicaExistente == null)
            {
                context.Musicas.Add(expectedMusica);
                await context.SaveChangesAsync();
            }

            // Act
            var musicaRecuperada = await dalMusica.RecuperarPor(m => m.Id == idMusica);

            // Assert
            Assert.NotNull(musicaRecuperada);
            Assert.Equal(expectedMusica.Nome, musicaRecuperada.Nome);
            Assert.Equal(expectedMusica.AnoLancamento, musicaRecuperada.AnoLancamento);
            Assert.Equal(expectedMusica.ArtistaId, musicaRecuperada.ArtistaId);
        }

        [Fact]
        public void RecuperarMusicaPor_IdInexistente()
        {
            //arrange
            var idInexistente = -1;

            //act
            var musicaRecuperada = dalMusica.RecuperarPor(m => m.Id == idInexistente);

            //assert
            musicaRecuperada.Result.Should().BeNull();
        }
    }
}
