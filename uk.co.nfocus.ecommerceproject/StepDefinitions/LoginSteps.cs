using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.ecommerceproject.POMClasses;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;


namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        [Given(@"I am on the eCommerce Website")]
        public void GivenIAmOnTheECommerceWebsite()
        {
            _driver.Url = TestContext.Parameters["WebAppURL"];

            new NavPOM(_driver).DismissBanner();
        }

        [Given(@"I am logged in as a registered user")]
        public void GivenIAmLoggedInAsARegisteredUser()
        {
            LoginPOM login = new LoginPOM(_driver);

            bool loggedIn = login.ValidLogin(Environment.GetEnvironmentVariable("USERNAME"), Environment.GetEnvironmentVariable("PASSWORD"));
            Assert.That(loggedIn, "We did not login");
        }
    }
}
