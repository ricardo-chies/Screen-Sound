using OpenQA.Selenium;
using ScreenSound.Tests.Interface.PageObject;

namespace ScreenSound.Tests.Interface
{
    public static class AuthIdentityCookie
    {
        public static Cookie ObterCookie(IWebDriver driver)
        {
            // Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Go("https://localhost:7073/");
            loginPO.PerformLogin("teste@email.com", "Senha@123");

            // Act
            bool isUserLoggedIn = loginPO.IsLoginSuccessful("Você está conectado como teste@email.com");

            // Assert
            Assert.True(isUserLoggedIn);
            return driver.Manage().Cookies.GetCookieNamed(".AspNetCore.Identity.Application");
        }
    }
}