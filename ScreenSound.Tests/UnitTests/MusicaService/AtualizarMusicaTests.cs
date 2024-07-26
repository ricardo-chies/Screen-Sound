using Moq;
using ScreenSound.API.Requests;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.UnitTests.MusicaServices
{
    public class AtualizarMusicaTests
    {
        private readonly Mock<IScreenSoundDAL<Musica>> dalMusica;
        private readonly MusicaService service;

        public AtualizarMusicaTests()
        {
            dalMusica = new Mock<IScreenSoundDAL<Musica>>();
            service = new MusicaService(dalMusica.Object, null);
        }

        [Fact]
        public async Task AtualizarMusicaTest_ReturnTrue()
        {
            var musicaRequestEdit = new MusicaRequestEdit(
                1,
                "Beat it",
                2,
                1982
            );

            var musica = new Musica("Beat it");

            dalMusica.Setup(a => a.RecuperarPor(a => a.Id == musicaRequestEdit.Id))
                      .ReturnsAsync(musica);
            dalMusica.Setup(a => a.Atualizar(musica)).Returns(Task.CompletedTask);

            // Act
            var result = await service.AtualizarMusica(musicaRequestEdit);

            // Assert
            Assert.True(result);
            dalMusica.Verify(a => a.Atualizar(It.IsAny<Musica>()), Times.Once);
        }
    }
}
