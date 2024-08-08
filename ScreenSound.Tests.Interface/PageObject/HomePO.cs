using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace ScreenSound.Tests.Interface.PageObject
{
    public class HomePO(IWebDriver _driver)
    {
        private readonly IWebDriver driver = _driver;
        private readonly WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(10));

        // Element Locators
        private static By LoginButton => By.CssSelector(".mud-button-label");

        // Actions
        public void GoToHomePage(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
        }

        public string GetPageTitle()
        {
            wait.Until(d => d.Title.Contains("ScreenSound.Web"));
            return driver.Title;
        }

        public IWebElement GetLoginButton()
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton));
        }
    }
}
