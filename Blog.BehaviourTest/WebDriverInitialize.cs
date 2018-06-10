using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace Blog.BehaviourTest
{

    [Binding]
    public class WebDriverInitialize
    {
        private readonly IObjectContainer _objectContainer;

        public WebDriverInitialize(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario("UsingFirefox")]
        public void InitializeFirefoxWebDriver()
        {
            var webDriver = new FirefoxDriver();
            Register(webDriver);
        }

        [BeforeScenario("UsingChrome")]
        public void InitializeChromeWebDriver()
        {
            var webDriver = new ChromeDriver();
            Register(webDriver);
        }

        [BeforeScenario("UsingIe")]
        public void InitializeInternetExplorerWebDriver()
        {
            var webDriver = new InternetExplorerDriver();
            Register(webDriver);
        }

        private void Register(IWebDriver webDriver)
        {
            _objectContainer.RegisterInstanceAs<IWebDriver>(webDriver);
        }
    }
}
