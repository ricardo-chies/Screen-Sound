using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.GeneroServices
{
    public class RecuperarGeneroPorNomeTests
    {
        private readonly Mock<IScreenSoundDAL<Genero>> dalGenero;
        private readonly GeneroService service;

        public RecuperarGeneroPorNomeTests()
        {
            dalGenero = new Mock<IScreenSoundDAL<Genero>>();
            service = new GeneroService(dalGenero.Object);
        }

        [Fact]
        public async Task RecuperarGeneroPorNomeTest_ReturnGenero()
        {
            // Arrange
            string nome = "Rock";
            var expectedGenero = new Genero
            {
                Id = 1,
                Nome = "Rock",
                Descricao = "Um gênero musical popular que se originou como rock and roll nos Estados Unidos no final dos anos 1940 e início dos anos 1950."
            };

            dalGenero.Setup(a => a.RecuperarPor(a => a.Nome.ToUpper() == nome.ToUpper()))
                      .ReturnsAsync(expectedGenero);

            // Act
            var result = await service.RecuperarGeneroPorNome(nome);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGenero.Nome, result.Nome);
        }

    }
}
