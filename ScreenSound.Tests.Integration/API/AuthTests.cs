using Bogus;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using ScreenSound.Tests.Integration.API.Context;
using System.Net;
using System.Net.Http.Json;

namespace ScreenSound.Tests.Integration.API
{
    [Collection("ContextCollection")]
    public class AuthTests : IClassFixture<ScreenSoundWebApplicationFactory>
    {
        private readonly ScreenSoundWebApplicationFactory _factory;
        private readonly ILogger<AuthTests> _logger;

        public AuthTests(ScreenSoundWebApplicationFactory factory)
        {
            _factory = factory;

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<AuthTests>();
        }

        [Fact]
        public async Task PostRegister()
        {
            // arrange
            _logger.LogInformation("Iniciando o teste PostRegister.");
            using var client = _factory.CreateClient();
            var requestUri = "/auth/register";

            var faker = new Faker<RegisterRequest>("pt_BR")
                .StrictMode(true)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => "Senha@123");

            var user = faker.Generate();

            // act
            try
            {
                _logger.LogInformation("Enviando requisição POST para {RequestUri} com o usuário {Email}.", requestUri, user.Email);
                var result = await client.PostAsJsonAsync(requestUri, user);
                var content = await result.Content.ReadAsStringAsync();

                // assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                _logger.LogInformation("Teste PostRegister concluído com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro durante o teste PostLogin.");
                throw;
            }
        }

        [Fact]
        public async Task PostLogin()
        {
            // arrange
            _logger.LogInformation("Iniciando o teste PostLogin.");
            using var client = _factory.CreateClient();
            var requestUri = "/auth/login?useCookies=true";

            var user = new LoginRequest { Email = "teste@email.com", Password = "Senha@123" };

            // act
            try
            {
                _logger.LogInformation("Enviando requisição POST para {RequestUri} com o usuário {Email}.", requestUri, user.Email);
                var result = await client.PostAsJsonAsync(requestUri, user);
                // assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                _logger.LogInformation("Teste PostLogin concluído com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro durante o teste PostLogin.");
                throw;
            }
        }
    }
}