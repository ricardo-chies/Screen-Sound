using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ScreenSound.Tests.Interface.PageObject
{
    public class LoginPO(IWebDriver _driver) : IDisposable
    {
        private readonly IWebDriver driver = _driver;
        private readonly WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(10));

        // Element Locators
        private static By LoginButtonLocator => By.CssSelector(".mud-button-label");
        private static By EmailFieldLocator => By.CssSelector(".mud-input-control:nth-child(2) .mud-input-slot:nth-child(1)");
        private static By PasswordFieldLocator => By.CssSelector(".mud-input-control:nth-child(3) .mud-input-slot:nth-child(1)");
        private static By SubmitButtonLocator => By.CssSelector(".mud-button-filled > .mud-button-label");
        private static By ConfirmationMessageLocator => By.XPath("//*[@id=\"app\"]/div[3]/div/div/p");

        public void Go(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
        }

        public void ClickLogin()
        {
            var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(LoginButtonLocator));
            loginButton.Click();
        }

        public void EnterEmail(string email)
        {
            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(EmailFieldLocator));
            emailField.Click();
            emailField.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(PasswordFieldLocator));
            passwordField.Click();
            passwordField.SendKeys(password);
        }

        public void SubmitLogin()
        {
            var submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(SubmitButtonLocator));
            submitButton.Click();
        }

        public bool IsLoginSuccessful(string expectedMessage)
        {
            return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(ConfirmationMessageLocator, expectedMessage));
        }

        public void Logout()
        {
            var logoutButton = wait.Until(ExpectedConditions.ElementToBeClickable(LoginButtonLocator));
            logoutButton.Click();
        }

        public void PerformLogin(string email, string password)
        {
            var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(LoginButtonLocator));
            loginButton.Click();

            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(EmailFieldLocator));
            emailField.Click();
            emailField.SendKeys(email);

            var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(PasswordFieldLocator));
            passwordField.Click();
            passwordField.SendKeys(password);

            var submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(SubmitButtonLocator));
            submitButton.Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}