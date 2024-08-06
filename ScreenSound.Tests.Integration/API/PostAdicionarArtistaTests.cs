using System.Net.Http.Json;
using System.Net;
using ScreenSound.API.Requests;
using System.Text;

namespace ScreenSound.Tests.Integration.API
{
    public class PostAdicionarArtistaTests
    {
        [Fact]
        public async Task PostAdicionarArtista()
        {
            // arrange
            var app = new ScreenSoundWebApplicationFactory();

            using var client = await app.GetClientWithAccesTokenAsync();
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
    }
}