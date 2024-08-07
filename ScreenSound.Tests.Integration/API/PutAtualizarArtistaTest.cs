using ScreenSound.API.Responses;
using ScreenSound.Tests.Integration.API.Context;
using System.Net;
using System.Net.Http.Json;

namespace ScreenSound.Tests.Integration.API
{
    public class PutAtualizarArtistaTest(ScreenSoundWebApplicationFactory factory) : IClassFixture<ScreenSoundWebApplicationFactory>
    {
        private readonly ScreenSoundWebApplicationFactory _factory = factory;

        [Fact]
        public async Task PutAtualizarArtistaOk()
        {
            // Arrange
            var artistaExistente = _factory.Context.Artistas
                .OrderBy(a => a.Id)
                .LastOrDefault();
            using var client = await _factory.GetClientWithAccesTokenAsync();

            artistaExistente.Nome = "Artista Atualizado";
            artistaExistente.Bio = "Bio Atualizada";

            // Act
            var result = await client.PutAsJsonAsync($"/Artistas/", artistaExistente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
