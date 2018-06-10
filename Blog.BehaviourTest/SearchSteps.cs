using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Blog.BehaviourTest
{
    [Binding]
    public class SearchSteps
    {
        IWebDriver _webDriver;

        public SearchSteps(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [Given(@"I am navigated to (.*)")]
        public void GivenIAmNavigatedTo(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        [Given(@"I have entered (.*) into the search box")]
        public void GivenIHaveEnteredIntoTheSearchBox(string searchText)
        {
            var searchInput = _webDriver.FindElement(By.CssSelector(".blogPostSearchBox input[type=text]"));
            searchInput.SendKeys(searchText);
        }

        [When(@"I click search submit")]
        public void AndIClickSearchSubmit()
        {
            var searchSubmit = _webDriver.FindElement(By.CssSelector(".blogPostSearchBox input[type=submit]"));
            searchSubmit.Click();
        }
        
        [Then(@"The h2 text should equal (.*)")]
        public void ThenTheH2HeaderShouldBe(string expectedText)
        {
            var h2element = _webDriver.FindElement(By.CssSelector("h2"));
            Assert.AreEqual(expectedText, h2element.Text);
        }

        [Then(@"The search result h3 text should contain (.*)")]
        public void ThenSearchResultH3TextShouldContain(string expectedText)
        {
            var searchResultH3 = _webDriver.FindElement(By.CssSelector(".searchResult h3"));
            Assert.IsTrue(searchResultH3.Text.Contains(expectedText));
        }
    }
}
