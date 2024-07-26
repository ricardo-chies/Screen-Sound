using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.UnitTests.ArtistaServices
{
    public class RecuperarTodosArtistasTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly ArtistaService service;

        public RecuperarTodosArtistasTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            service = new ArtistaService(dalArtista.Object, null);
        }

        [Fact]
        public async Task RecuperarTodosArtistasTest_ReturnArtistas()
        {
            // Arrange
            IEnumerable<Artista> expectedArtista = [
                    new("Michael Jackson", "Rei do Pop"),
                    new("Queen", "Banda britânica de Rock")
                ];

            dalArtista.Setup(a => a.RecuperarTodosArtistasComAvaliacoesAsync())
                      .ReturnsAsync(expectedArtista);

            // Act
            var result = await service.RecuperarTodosArtistas();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                artista =>
                {
                    Assert.Equal("Michael Jackson", artista.Nome);
                    Assert.Equal("Rei do Pop", artista.Bio);
                },
                artista =>
                {
                    Assert.Equal("Queen", artista.Nome);
                    Assert.Equal("Banda britânica de Rock", artista.Bio);
                });
        }
    }
}
