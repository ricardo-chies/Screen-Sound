using System.Net;
using ScreenSound.Tests.Integration.API.Context;

namespace ScreenSound.Tests.Integration.API
{
    public class DeleteExcluirArtistaTest(ScreenSoundWebApplicationFactory factory) : IClassFixture<ScreenSoundWebApplicationFactory>
    {
        private readonly ScreenSoundWebApplicationFactory _factory = factory;

        [Fact]
        public async Task DeleteExcluirArtistaNoContent()
        {
            // Arrange
            var artistaExistente = _factory.Context.Artistas
                .OrderBy(a => a.Id)
                .LastOrDefault();
            using var client = await _factory.GetClientWithAccesTokenAsync();

            // Act
            var result = await client.DeleteAsync("/Artistas/" + artistaExistente.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
