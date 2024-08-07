using ScreenSound.API.Responses;
using ScreenSound.Tests.Integration.API.Context;
using System.Net;
using System.Net.Http.Json;

namespace ScreenSound.Tests.Integration.API
{
    public class GetRecuperarArtistaPorNomeTests(ScreenSoundWebApplicationFactory factory) : IClassFixture<ScreenSoundWebApplicationFactory>
    {
        private readonly ScreenSoundWebApplicationFactory _factory = factory;

        [Fact]
        public async Task GetRecuperarArtistaPorNomeOK()
        {
            // Arrange
            var artistaExistente = _factory.Context.Artistas.FirstOrDefault();
            using var client = await _factory.GetClientWithAccesTokenAsync();

            // Act
            var result = await client.GetFromJsonAsync<ArtistaResponse>("/Artistas/" + artistaExistente.Nome);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Nome, artistaExistente.Nome);
            Assert.Equal(result.Bio, artistaExistente.Bio);
        }

        [Fact]
        public async Task GetRecuperarArtistaPorNomeNotFound()
        {
            // Arrange
            var artistaExistente = "Hahahah";
            using var client = await _factory.GetClientWithAccesTokenAsync();

            // Act
            var result = await client.GetAsync("/Artistas/" + artistaExistente);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
