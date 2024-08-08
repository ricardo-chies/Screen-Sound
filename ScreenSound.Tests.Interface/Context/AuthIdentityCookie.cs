using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace ScreenSound.Tests.Interface
{
    public static class AuthIdentityCookie
    {
        public static Cookie ObterCookie(IWebDriver driver)
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

            var confirmationMessage = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id=\"app\"]/div[3]/div/div/p"), "Você está conectado como teste@email.com"));

            // Assert
            Cookie authCookie = driver.Manage().Cookies.GetCookieNamed(".AspNetCore.Identity.Application");
            Assert.True(confirmationMessage);

            return authCookie;
        }
    }
}