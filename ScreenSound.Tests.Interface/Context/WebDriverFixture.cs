using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Reflection;

namespace ScreenSound.Tests.Interface.Context
{
    public class WebDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }
        public WebDriverFixture()
        {
            Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}