using OpenQA.Selenium;
using ScreenSound.Tests.Interface.Context;
using ScreenSound.Tests.Interface.PageObject;

namespace ScreenSound.Tests.Interface.Pages
{
    public class HomePageTests : IClassFixture<WebDriverFixture>
    {
        private readonly IWebDriver driver;
        private readonly HomePO homePO;

        public HomePageTests(WebDriverFixture fixture)
        {
            driver = fixture.Driver;
            homePO = new HomePO(driver);
        }

        [Fact]
        public void VerificarTitulo()
        {
            // Arrange
            homePO.GoToHomePage("https://localhost:7073/");

            // Act
            string pageTitle = homePO.GetPageTitle();

            // Assert
            Assert.Contains("ScreenSound.Web", pageTitle);
        }

        [Fact]
        public void VerificarBotaoLogin()
        {
            // Arrange
            homePO.GoToHomePage("https://localhost:7073/");

            // Act
            var loginButton = homePO.GetLoginButton();

            // Assert
            Assert.NotNull(loginButton);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
