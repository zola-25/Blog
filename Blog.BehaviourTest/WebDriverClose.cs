using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Blog.BehaviourTest
{
    [Binding]
    public class WebDriverClose
    {
        private IWebDriver _webDriver;

        public WebDriverClose(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [AfterScenario]
        public void ExecuteBrowserQuit()
        {
            _webDriver.Quit();
        } 
    }

}
