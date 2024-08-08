using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;

namespace ScreenSound.Tests.Interface.Pages
{
    public class HomePage : IDisposable
    {
        private IWebDriver driver;

        public HomePage()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void VerificarTitulo()
        {
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7073/");

            // Act
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("ScreenSound.Web"));

            // Assert
            Assert.Contains("ScreenSound.Web", driver.Title);
        }

        [Fact]
        public void VerificarBotaoLogin()
        {
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7073/");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);

            // Act
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); // Aumenta o tempo de espera para 20 segundos
            var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".mud-button-label")));

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
