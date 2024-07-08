using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using ScreenSound.Shared.Data.Models;

namespace ScreenSound.Tests
{
    public class RecuperarArtistaPorNomeTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly ArtistaService service;

        public RecuperarArtistaPorNomeTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            service = new ArtistaService(dalArtista.Object, null);
        }

        [Fact]
        public async Task RecuperarArtistaPorNomeTest_ReturnArtista()
        {
            // Arrange
            string nome = "Michael Jackson";
            var expectedArtista = new Artista("Michael Jackson", "Rei do Pop");

            dalArtista.Setup(a => a.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper())))
                      .ReturnsAsync(expectedArtista);

            // Act
            var result = await service.RecuperarArtistaPorNome(nome);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedArtista.Nome, result.Nome);
            Assert.Equal(expectedArtista.Bio, result.Bio);
        }
    }
}
