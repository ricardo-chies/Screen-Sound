using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.MusicaServices
{
    public class RecuperarMusicaPorNomeTests
    {
        private readonly Mock<IScreenSoundDAL<Musica>> dalMusica;
        private readonly MusicaService service;

        public RecuperarMusicaPorNomeTests()
        {
            dalMusica = new Mock<IScreenSoundDAL<Musica>>();
            service = new MusicaService(dalMusica.Object, null);
        }

        [Fact]
        public async Task RecuperarMusicaPorNomeTest_ReturnMusica()
        {
            // Arrange
            string nome = "Beat it";
            var expectedMusica = new Musica
            {
                Nome = "Beat it",
                AnoLancamento = 1982,
                ArtistaId = 2
            };

            dalMusica.Setup(a => a.RecuperarMusicaComArtistaPor(a => a.Nome.ToUpper() == nome.ToUpper()))
                      .ReturnsAsync(expectedMusica);

            // Act
            var result = await service.RecuperarMusicaPorNome(nome);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedMusica.Nome, result.Nome);
        }

    }
}
