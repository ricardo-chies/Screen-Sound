using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using ScreenSound.Shared.Data.Models;

namespace ScreenSound.Tests.UnitTests.ArtistaServices
{
    public class RecuperarArtistaPorNomeTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly ArtistaService service;

        public RecuperarArtistaPorNomeTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            service = new ArtistaService(dalArtista.Object, null);
        }

        [Theory]
        [InlineData("Michael Jackson", "Michael Jackson", "Rei do Pop")]
        [InlineData("VCSNVS", null, null)]
        public async Task RecuperarArtistaPorNomeTest_ReturnArtista(string nome, string expectedNome, string expectedBio)
        {
            // Arrange
            Artista? expectedArtista = expectedNome == null ? null : new Artista(expectedNome, expectedBio);

            dalArtista.Setup(a => a.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper())))
                      .ReturnsAsync(expectedArtista);

            // Act
            var result = await service.RecuperarArtistaPorNome(nome);

            // Assert
            if (expectedArtista == null)
            {
                Assert.Null(result);
            }
            else
            {
                Assert.NotNull(result);
                Assert.Equal(expectedArtista.Nome, result.Nome);
                Assert.Equal(expectedArtista.Bio, result.Bio);
            }
        }

    }
}
