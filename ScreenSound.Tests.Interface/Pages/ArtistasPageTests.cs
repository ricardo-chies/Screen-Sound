using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;

namespace ScreenSound.Tests.Interface.Pages
{
    public class ArtistasPageTests : IDisposable
    {
        private IWebDriver driver;

        public ArtistasPageTests()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void VerificarTitulo()
        {
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7073/");
            Cookie authCookie = AuthIdentityCookie.ObterCookie(driver);

            driver.Manage().Cookies.AddCookie(authCookie);
            driver.Navigate().Refresh();
            driver.Navigate().GoToUrl("https://localhost:7073/Artistas");

            // Act
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var confirmationTitle = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".mud-typography.mud-typography-h4.mb-4"), "Artistas cadastrados"));

            // Assert
            Assert.True(confirmationTitle);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
