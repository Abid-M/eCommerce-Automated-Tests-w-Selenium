using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;
//using static uk.co.nfocus.specflow.Utils.Hooks;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class GoogleSearchStepDefinitions
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext; //with context can do parallel execution, if not static and just one

        public GoogleSearchStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        [Given(@"I am on the Google homepage")]
        [Given(@"i am on the google homepage")]
        public void GivenIAmOnTheGoogleHomepage()
        {
            _driver.Url = "https://google.co.uk";
            //Also need to accept cookies
            _driver.FindElement(By.Id("L2AGLb")).Click();

            _scenarioContext["someValueToBePassedAround"] = _driver.FindElement(By.Id("L2AGLb")); //pput value in scneariocontext
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchForEdgewords(string searchTerm) //Capture the search term from feature and pass into the method
        {
            _driver.FindElement(By.CssSelector("textarea[aria-label=Search]")).SendKeys(searchTerm + Keys.Enter);

            IWebElement passedValueThruContext = (IWebElement)_scenarioContext["someValueToBePassedAround"];
        }

        [Then(@"'(.*)' is the top result")]
        public void ThenEdgewordsIsTheTopResult(string searchResult)
        {
            string topResult = _driver.FindElement(By.CssSelector("#search h3:not(div > *)")).Text;

            //NUnit Style
            Assert.That(topResult, Does.Contain(searchResult), $"{searchResult} is not the top result!");

            //FluentAssertion style
            //topResult.Should().Contain(searchResult);
        }

        [Then(@"I should see in results")]
        public void ThenIShouldSeeInTheResults(Table table)
        {
            string searchResults = _driver.FindElement(By.Id("rso")).Text;

            foreach (var row in table.Rows)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(searchResults, Does.Contain(row["url"]), "URL not found");
                    //Assert.That(searchResults, Does.Contain(row[0]), "URL not found"); //Using column index
                    Assert.That(searchResults, Does.Contain(row["title"]), "Title not found");
                });

            }
        }
    }
}
