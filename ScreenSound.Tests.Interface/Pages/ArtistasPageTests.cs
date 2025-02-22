﻿using OpenQA.Selenium;
using ScreenSound.Tests.Interface.Context;
using ScreenSound.Tests.Interface.PageObject;

namespace ScreenSound.Tests.Interface.Pages
{
    [Collection("WebDriverFixture")]
    public class ArtistasPageTests
    {
        private readonly WebDriverFixture fixture;
        private readonly IWebDriver driver;
        private readonly ArtistasPO artistaPO;

        public ArtistasPageTests(WebDriverFixture fixture)
        {
            this.fixture = fixture;
            driver = fixture.Driver;
            artistaPO = new ArtistasPO(driver);
        }

        [Fact]
        public void VerificarTitulo()
        {
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7073/");
            Cookie authCookie = fixture.ObterCookie();

            artistaPO.GoToArtistasPage("https://localhost:7073/Artistas", authCookie);

            // Act
            var confirmationTitle = artistaPO.IsTitleDisplayed("Artistas cadastrados");

            // Assert
            Assert.True(confirmationTitle);
        }
    }
}
