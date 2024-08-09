using OpenQA.Selenium;
using ScreenSound.Tests.Interface.PageObject;
using ScreenSound.Tests.Interface.Context;

namespace ScreenSound.Tests.Interface.Pages
{
    public class LoginPageTests : IClassFixture<WebDriverFixture>
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

        [Fact]
        public void LoginError()
        {
            // Arrange
            loginPO.Go("https://localhost:7073/");

            // Act
            loginPO.ClickLogin();
            loginPO.EnterEmail("teste@email.com");
            loginPO.EnterPassword("Senha@Errada");
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
