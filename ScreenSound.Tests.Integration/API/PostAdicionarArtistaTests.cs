using System.Net.Http.Json;
using System.Net;
using ScreenSound.API.Requests;
using System.Text;
using ScreenSound.Tests.Integration.API.Context;

namespace ScreenSound.Tests.Integration.API
{
    [Collection("ContextCollection")]
    public class PostAdicionarArtistaTests(ScreenSoundWebApplicationFactory factory) : IClassFixture<ScreenSoundWebApplicationFactory>
    {
        private readonly ScreenSoundWebApplicationFactory _factory = factory;

        [Fact]
        public async Task PostAdicionarArtista()
        {
            // arrange

            using var client = await _factory.GetClientWithAccesTokenAsync();
            var requestUri = "/Artistas";

            var fakePath = Path.Combine(Path.GetTempPath(), "fake", "path");

            Directory.CreateDirectory(Path.Combine(fakePath, "wwwroot", "FotosPerfil"));

            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes("fake image data"));

            var artistaRequest = new ArtistaRequest(
                "Artista de teste",
                "Criando um artista para teste via teste de integração.",
                base64String
            );

            // act
            var result = await client.PostAsJsonAsync(requestUri, artistaRequest);

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task PostAdicionarArtistaUnauthorized()
        {
            // arrange
            using var client = _factory.CreateClient();
            var requestUri = "/Artistas";

            var fakePath = Path.Combine(Path.GetTempPath(), "fake", "path");

            Directory.CreateDirectory(Path.Combine(fakePath, "wwwroot", "FotosPerfil"));

            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes("fake image data"));

            var artistaRequest = new ArtistaRequest(
                "Artista de teste",
                "Criando um artista para teste via teste de integração.",
                base64String
            );

            // act
            var result = await client.PostAsJsonAsync(requestUri, artistaRequest);

            // assert
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}