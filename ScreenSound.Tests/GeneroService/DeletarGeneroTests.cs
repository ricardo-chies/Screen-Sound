using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.GeneroServices
{
    public class DeletarGeneroTests
    {
        private readonly Mock<IScreenSoundDAL<Genero>> dalGenero;
        private readonly GeneroService service;

        public DeletarGeneroTests()
        {
            dalGenero = new Mock<IScreenSoundDAL<Genero>>();
            service = new GeneroService(dalGenero.Object);
        }

        [Fact]
        public async Task DeletarGeneroTest_ReturnTrue()
        {
            // Arrange
            int generoId = 1;
            var genero = new Genero
            {
                Id = 1,
                Nome = "Rock",
                Descricao = "Um gênero musical popular que se originou como rock and roll nos Estados Unidos no final dos anos 1940 e início dos anos 1950."
            };

            dalGenero.Setup(a => a.RecuperarPor(a => a.Id == generoId))
                      .ReturnsAsync(genero);
            dalGenero.Setup(a => a.Deletar(genero)).Returns(Task.CompletedTask);

            // Act
            var result = await service.DeletarGenero(generoId);

            // Assert
            Assert.True(result);
            dalGenero.Verify(a => a.Deletar(It.IsAny<Genero>()), Times.Once);
        }
    }
}
