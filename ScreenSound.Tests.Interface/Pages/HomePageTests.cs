using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScreenSound.Tests.Interface.PageObject;
using System.Reflection;

namespace ScreenSound.Tests.Interface.Pages
{
    public class HomePageTests : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly HomePO homePO;

        public HomePageTests()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
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
