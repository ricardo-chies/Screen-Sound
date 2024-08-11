using OpenQA.Selenium;
using ScreenSound.Tests.Interface.PageObject;
using ScreenSound.Tests.Interface.Context;

namespace ScreenSound.Tests.Interface.Pages
{
    [Collection("WebDriverFixture")]
    public class LoginPageTests
    {
        private readonly IWebDriver driver;
        private readonly LoginPO loginPO;

        public LoginPageTests(WebDriverFixture fixture)
        {
            driver = fixture.Driver;
            loginPO = new LoginPO(driver);
        }

        [Fact]
        public void LoginSucesso()
        {
            // Arrange
            loginPO.Go("https://localhost:7073/");

            // Act
            loginPO.ClickLogin();
            loginPO.EnterEmail("teste@email.com");
            loginPO.EnterPassword("Senha@123");
            loginPO.SubmitLogin();

            // Assert
            Assert.True(loginPO.IsLoginSuccessful("Você está conectado como teste@email.com"));

            // Logout
            loginPO.Logout();
        }

        [Theory]
        [InlineData("teste@email.com", "Senha@Errada")]
        [InlineData("emailErrado@email.com", "Senha@123")]
        [InlineData("", "Senha@123")]
        [InlineData("teste@email.com", "")]
        public void LoginError(string email, string password)
        {
            // Arrange
            loginPO.Go("https://localhost:7073/");

            // Act
            loginPO.ClickLogin();
            loginPO.EnterEmail(email);
            loginPO.EnterPassword(password);
            loginPO.SubmitLogin();

            // Assert
            bool messagePresent;
            try
            {
                messagePresent = loginPO.IsLoginSuccessful("Você está conectado como teste@email.com");
            }
            catch (WebDriverTimeoutException)
            {
                messagePresent = false;
            }

            Assert.False(messagePresent);
        }

        public void Dispose()
        {
            loginPO.Dispose();
        }
    }
}
