using Bogus;
using Microsoft.AspNetCore.Identity.Data;
using ScreenSound.Tests.Integration.API.Context;
using System.Net;
using System.Net.Http.Json;

namespace ScreenSound.Tests.Integration.API
{
    [Collection("ContextCollection")]
    public class AuthTests(ScreenSoundWebApplicationFactory factory) : IClassFixture<ScreenSoundWebApplicationFactory>
    {
        private readonly ScreenSoundWebApplicationFactory _factory = factory;

        [Fact]
        public async Task PostRegister()
        {
            // arrange
            using var client = _factory.CreateClient();
            var requestUri = "/auth/register";

            var faker = new Faker<RegisterRequest>("pt_BR")
                .StrictMode(true)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => "Senha@123");

            var user = faker.Generate();

            // act
            var result = await client.PostAsJsonAsync(requestUri, user);
            var content = await result.Content.ReadAsStringAsync();

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task PostLogin()
        {
            // arrange
            using var client = _factory.CreateClient();
            var requestUri = "/auth/login?useCookies=true";

            var user = new LoginRequest { Email = "teste@email.com", Password = "Senha@123" };

            // act
            var result = await client.PostAsJsonAsync(requestUri, user);
            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}