using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.UnitTests.ArtistaServices
{
    public class DeletarArtistaTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly ArtistaService service;

        public DeletarArtistaTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            service = new ArtistaService(dalArtista.Object, null);
        }

        [Fact]
        public async Task DeletarArtistaTest_ReturnTrue()
        {
            // Arrange
            int artistaId = 1;
            var artista = new Artista("Michael Jackson", "Rei do Pop");

            dalArtista.Setup(a => a.RecuperarPor(a => a.Id == artistaId))
                      .ReturnsAsync(artista);
            dalArtista.Setup(a => a.DeletarArtista(artista)).Returns(Task.CompletedTask);

            // Act
            var result = await service.DeletarArtista(artistaId);

            // Assert
            Assert.True(result);
            dalArtista.Verify(a => a.DeletarArtista(It.IsAny<Artista>()), Times.Once);
        }
    }
}
