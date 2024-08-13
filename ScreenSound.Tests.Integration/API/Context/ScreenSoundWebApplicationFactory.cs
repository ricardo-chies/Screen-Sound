using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Testcontainers.MySql;

namespace ScreenSound.Tests.Integration.API.Context
{
    public class ScreenSoundWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        // Utilizado para testes de Integração na API, utilizando o MySql criado container com docker-compose.
        //public Data.Context Context { get; private set; }

        //private IServiceScope scope;
        //private readonly MySqlContainer mySqlContainer = new MySqlBuilder()
        //    .WithImage("mysql:8.0")
        //    .Build();

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices(services =>
        //    {
        //        services.RemoveAll(typeof(DbContextOptions<Data.Context>));
        //        services.AddDbContext<Data.Context>(options => options
        //        .UseLazyLoadingProxies()
        //        .UseMySql("server = localhost; port = 3307; database = ScreenSound; user = root; password = 123456; Persist Security Info = False", // MySql Container
        //        new MySqlServerVersion(new Version(7, 0, 0))));

        //        base.ConfigureWebHost(builder);
        //    });
        //}

        public async Task<HttpClient> GetClientWithAccesTokenAsync()
        {
            var client = CreateClient();
            var requestUri = "/auth/login";

            var user = new LoginRequest { Email = "teste@email.com", Password = "Senha@123" };
            var result = await client.PostAsJsonAsync(requestUri, user);

            var content = await result.Content.ReadAsStringAsync();
            var action = JsonSerializer.Deserialize<UserTokenDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", action.AccessToken);

            return client;
        }

        //public async Task InitializeAsync()
        //{
        //    await mySqlContainer.StartAsync();
        //    scope = Services.CreateScope();
        //    Context = scope.ServiceProvider.GetRequiredService<Data.Context>();
        //}

        //async Task IAsyncLifetime.DisposeAsync()
        //{
        //    await mySqlContainer.DisposeAsync();
        //}

        // Utilizado para testes de Integração no banco de dados MySql, criando novo banco para os testes.
        private readonly MySqlContainer _mySqlContainer;
        public Data.Context Context { get; private set; }

        public ScreenSoundWebApplicationFactory()
        {
            _mySqlContainer = new MySqlBuilder()
                .WithImage("mysql:8.0")
                .WithUsername("tester")
                .WithPassword("123456")
                .WithDatabase("ScreenSound_Test")
                .Build();
        }

        public async Task InitializeAsync()
        {
            // Inicia o container MySQL
            await _mySqlContainer.StartAsync();

            // Configura o contexto do Entity Framework para usar o banco de dados no container
            var options = new DbContextOptionsBuilder<Data.Context>()
                .UseMySql(_mySqlContainer.GetConnectionString(),
                new MySqlServerVersion(new Version(8, 0, 0)))
                .Options;

            Context = new Data.Context(options);

            // Aplica as migrations para criar o banco de dados
            await Context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            // Limpeza: Deleta o banco de dados de teste, se descomentar, irá gerar um novo banco a cada teste
            // await Context.Database.EnsureDeletedAsync();

            // Para o container e libera os recursos
            await _mySqlContainer.StopAsync();
            await _mySqlContainer.DisposeAsync();

            await Context.DisposeAsync();
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