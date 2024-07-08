using Moq;
using ScreenSound.API.Requests;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.GeneroServices
{
    public class AdicionarGeneroTests
    {
        private readonly Mock<IScreenSoundDAL<Genero>> dalGenero;
        private readonly GeneroService service;

        public AdicionarGeneroTests()
        {
            dalGenero = new Mock<IScreenSoundDAL<Genero>>();
            service = new GeneroService(dalGenero.Object);
        }

        [Fact]
        public async Task AdicionarGenero_Should()
        {
            // Arrange
            var generoRequest = new GeneroRequest
                (
                    "Rock",
                    "Um gênero musical popular que se originou como rock and roll nos Estados Unidos no final dos anos 1940 e início dos anos 1950."
                );

            dalGenero.Setup(g => g.Adicionar(It.IsAny<Genero>()))
                     .Returns(Task.CompletedTask);

            // Act
            await service.AdicionarGenero(generoRequest);

            // Assert
            dalGenero.Verify(m => m.Adicionar(It.Is<Genero>(Genero =>
                Genero.Nome == "Rock" &&
                Genero.Descricao == "Um gênero musical popular que se originou como rock and roll nos Estados Unidos no final dos anos 1940 e início dos anos 1950."
            )), Times.Once);
        }
    }
}
