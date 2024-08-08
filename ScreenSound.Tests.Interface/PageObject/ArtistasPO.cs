using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace ScreenSound.Tests.Interface.PageObject
{
    public class ArtistasPO(IWebDriver _driver)
    {
        private readonly IWebDriver driver = _driver;
        private readonly WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(10));

        // Element Locators
        private static By TitleLocator => By.CssSelector(".mud-typography.mud-typography-h4.mb-4");

        public void GoToArtistasPage(string url, Cookie authCookie)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Cookies.AddCookie(authCookie);
            driver.Navigate().Refresh();
        }

        public bool IsTitleDisplayed(string expectedTitle)
        {
            return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(TitleLocator, expectedTitle));
        }
    }
}