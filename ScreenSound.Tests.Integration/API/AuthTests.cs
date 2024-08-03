using Microsoft.AspNetCore.Identity.Data;
using System.Net;
using System.Net.Http.Json;

namespace ScreenSound.Tests.Integration.API
{
    public class AuthTests
    {
        [Fact]
        public async Task PostRegister()
        {
            // arrange
            var app = new ScreenSoundWebApplicationFactory();

            using var client = app.CreateClient();
            var requestUri = "/auth/register";

            var user = new RegisterRequest { Email = "teste@email.com", Password = "Senha@123" };

            // act
            var result = await client.PostAsJsonAsync(requestUri, user);

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task PostLogin()
        {
            // arrange
            var app = new ScreenSoundWebApplicationFactory();

            using var client = app.CreateClient();
            var requestUri = "/auth/login?useCookies=true";

            var user = new LoginRequest { Email = "teste@email.com", Password = "Senha@123" };

            // act
            var result = await client.PostAsJsonAsync(requestUri, user);
            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}