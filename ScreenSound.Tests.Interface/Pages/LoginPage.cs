using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ScreenSound.Tests.Interface.Pages
{
    public class LoginPage : IDisposable
    {
        private IWebDriver driver;

        public LoginPage()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void LoginSucesso()
        {
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7073/");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);

            // Act
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".mud-button-label")));
            loginButton.Click();

            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".mud-input-control:nth-child(2) .mud-input-slot:nth-child(1)")));
            emailField.Click();
            emailField.SendKeys("teste@email.com");

            var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".mud-input-control:nth-child(3) .mud-input-slot:nth-child(1)")));
            passwordField.Click();
            passwordField.SendKeys("Senha@123");

            var submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".mud-button-filled > .mud-button-label")));
            submitButton.Click();

            // Assert                
            var confirmationMessage = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id=\"app\"]/div[3]/div/div/p"), "Você está conectado como teste@email.com"));
            Assert.True(confirmationMessage);

            var logoutButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".mud-button-label")));
            logoutButton.Click();
        }

        [Fact]
        public void LoginError()
        {
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7073/");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);

            // Act
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".mud-button-label")));
            loginButton.Click();

            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".mud-input-control:nth-child(2) .mud-input-slot:nth-child(1)")));
            emailField.Click();
            emailField.SendKeys("teste@email.com");

            var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".mud-input-control:nth-child(3) .mud-input-slot:nth-child(1)")));
            passwordField.Click();
            passwordField.SendKeys("Senha@Errada");

            var submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".mud-button-filled > .mud-button-label")));
            submitButton.Click();

            // Assert
            bool messagePresent = false;
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                messagePresent = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id=\"app\"]/div[3]/div/div/p"), "Você está conectado como teste@email.com"));
            }
            catch (WebDriverTimeoutException)
            {
                messagePresent = false;
            }

            Assert.False(messagePresent);
        }


        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
