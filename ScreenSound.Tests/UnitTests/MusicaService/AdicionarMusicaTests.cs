using Moq;
using ScreenSound.API.Requests;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.UnitTests.MusicaServices
{
    public class AdicionarMusicaTests
    {
        private readonly Mock<IScreenSoundDAL<Musica>> dalMusica;
        private readonly Mock<IScreenSoundDAL<Genero>> dalGenero;
        private readonly MusicaService service;

        public AdicionarMusicaTests()
        {
            dalMusica = new Mock<IScreenSoundDAL<Musica>>();
            dalGenero = new Mock<IScreenSoundDAL<Genero>>();
            service = new MusicaService(dalMusica.Object, dalGenero.Object);
        }

        [Fact]
        public async Task AdicionarMusica_Should()
        {
            // Arrange
            var generoRequest = new List<GeneroRequest> { new("Pop", "Popular music")};

            var musicaRequest = new MusicaRequest
                (
                    "Billie Jean",
                    1,
                    1982,
                    generoRequest
                );

            var generoPop = new Genero { Nome = "Pop", Descricao = "Popular music" };

            dalGenero.Setup(g => g.RecuperarPor(g => g.Nome.ToUpper() == generoRequest[0].Nome.ToUpper()))
                     .ReturnsAsync(generoPop);

            dalGenero.Setup(g => g.Adicionar(It.IsAny<Genero>()))
                     .Returns(Task.CompletedTask);

            dalMusica.Setup(m => m.Adicionar(It.IsAny<Musica>())).Returns(Task.CompletedTask);

            // Act
            await service.AdicionarMusica(musicaRequest);

            // Assert
            dalMusica.Verify(m => m.Adicionar(It.Is<Musica>(musica =>
                musica.Nome == "Billie Jean" &&
                musica.ArtistaId == 1 &&
                musica.AnoLancamento == 1982 &&
                musica.Generos.Any(g => g.Nome == "Pop" && g.Descricao == "Popular music")
            )), Times.Once);
        }
    }
}
