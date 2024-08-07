using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ScreenSound.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ScreenSound.Tests.Integration
{
    public class ScreenSoundWebApplicationFactory : WebApplicationFactory<Program>
    {
        // Utilizado para testes de Integração na API, utilizando o MySql criado container com docker-compose.
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<Context>));
                services.AddDbContext<Context>(options => options
                .UseLazyLoadingProxies()
                .UseMySql("server = localhost; port = 3307; database = ScreenSound; user = root; password = 123456; Persist Security Info = False", // MySql Container
                new MySqlServerVersion(new Version(7, 0, 0))));

                base.ConfigureWebHost(builder);
            });
        }

        public async Task<HttpClient> GetClientWithAccesTokenAsync()
        {
            var client = this.CreateClient();
            var requestUri = "/auth/login";

            var user = new LoginRequest { Email = "teste@email.com", Password = "Senha@123" };
            var result = await client.PostAsJsonAsync(requestUri, user);

            var content = await result.Content.ReadAsStringAsync();
            var action = JsonSerializer.Deserialize<UserTokenDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", action.AccessToken);

            return client;
        }

    }

    internal class UserTokenDTO
    {
        public string? TokenType { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
    }
}