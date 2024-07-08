using Microsoft.Extensions.Hosting;
using Moq;
using ScreenSound.API.Requests;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using System.Text;

namespace ScreenSound.Tests.ArtistaServices
{
    public class AdicionarArtistaTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly ArtistaService service;

        public AdicionarArtistaTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            service = new ArtistaService(dalArtista.Object, null);
        }

        [Fact]
        public async Task AdicionarArtistaTest_ReturnTrue()
        {
            // Arrange
            var mockEnvironment = new Mock<IHostEnvironment>();
            var fakePath = Path.Combine(Path.GetTempPath(), "fake", "path");
            mockEnvironment.Setup(env => env.ContentRootPath).Returns(fakePath);

            Directory.CreateDirectory(Path.Combine(fakePath, "wwwroot", "FotosPerfil"));

            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes("fake image data"));

            var artistaRequest = new ArtistaRequest(
                "Michael Jackson",
                "Rei do Pop",
                base64String
            );

            dalArtista.Setup(a => a.Adicionar(It.IsAny<Artista>())).Returns(Task.CompletedTask);

            // Act
            var result = await service.AdicionarArtista(mockEnvironment.Object, artistaRequest);

            // Assert
            Assert.True(result);
            dalArtista.Verify(a => a.Adicionar(It.IsAny<Artista>()), Times.Once);
        }
    }
}
