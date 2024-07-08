using Moq;
using ScreenSound.API.Requests;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using System.Text;

namespace ScreenSound.Tests
{
    public class AtualizarArtistaTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly ArtistaService service;

        public AtualizarArtistaTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            service = new ArtistaService(dalArtista.Object, null);
        }

        [Fact]
        public async Task AtualizarArtistaTest_ReturnTrue()
        {
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes("fake image data"));

            var artistaRequestEdit = new ArtistaRequestEdit(
                1,
                "Michael Jackson",
                "Rei do Pop",
                base64String
            );

            var artista = new Artista("Michael Jackson", "Rei do Pop");

            dalArtista.Setup(a => a.RecuperarPor(a => a.Id == artistaRequestEdit.Id))
                      .ReturnsAsync(artista);
            dalArtista.Setup(a => a.Atualizar(artista)).Returns(Task.CompletedTask);

            // Act
            var result = await service.AtualizarArtista(artistaRequestEdit);

            // Assert
            Assert.True(result);
            dalArtista.Verify(a => a.Atualizar(It.IsAny<Artista>()), Times.Once);
        }
    }
}
