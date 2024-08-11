using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Reflection;
using ScreenSound.Tests.Interface.PageObject;

namespace ScreenSound.Tests.Interface.Context
{
    public class WebDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }
        public WebDriverFixture()
        {
            Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        public Cookie ObterCookie()
        {
            // Arrange
            var loginPO = new LoginPO(Driver);
            loginPO.Go("https://localhost:7073/");
            loginPO.PerformLogin("teste@email.com", "Senha@123");

            // Act
            bool isUserLoggedIn = loginPO.IsLoginSuccessful("Você está conectado como teste@email.com");

            // Assert
            Assert.True(isUserLoggedIn);
            return Driver.Manage().Cookies.GetCookieNamed(".AspNetCore.Identity.Application");
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}