using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ScreenSound.Tests.Interface.Context;
using SeleniumExtras.WaitHelpers;

namespace ScreenSound.Tests.Interface.Pages
{
    public class ArtistasPageTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
    {
        private readonly IWebDriver driver = fixture.Driver;

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
    }
}
