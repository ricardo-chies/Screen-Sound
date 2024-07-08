using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.MusicaServices
{
    public class DeletarMusicaTests
    {
        private readonly Mock<IScreenSoundDAL<Musica>> dalMusica;
        private readonly MusicaService service;

        public DeletarMusicaTests()
        {
            dalMusica = new Mock<IScreenSoundDAL<Musica>>();
            service = new MusicaService(dalMusica.Object, null);
        }

        [Fact]
        public async Task DeletarMusicaTest_ReturnTrue()
        {
            // Arrange
            int musicaId = 1;
            var musica = new Musica("Beat it");

            dalMusica.Setup(a => a.RecuperarPor(a => a.Id == musicaId))
                      .ReturnsAsync(musica);
            dalMusica.Setup(a => a.Deletar(musica)).Returns(Task.CompletedTask);

            // Act
            var result = await service.DeletarMusica(musicaId);

            // Assert
            Assert.True(result);
            dalMusica.Verify(a => a.Deletar(It.IsAny<Musica>()), Times.Once);
        }
    }
}
