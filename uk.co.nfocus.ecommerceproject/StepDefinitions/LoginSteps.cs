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
            // Direct to URL in test parameters, not env variable as not a secret
            _driver.Url = TestContext.Parameters["WebAppURL"];
            
            // Dismisses the notice banner
            new NavPOM(_driver).DismissBanner();
        }

        [Given(@"I am logged in as a registered user")]
        public void GivenIAmLoggedInAsARegisteredUser()
        {
            LoginPOM login = new LoginPOM(_driver);

            string? username = Environment.GetEnvironmentVariable("USERNAME");
            string? password = Environment.GetEnvironmentVariable("PASSWORD");
            bool loggedIn = false;

            if (username !=null && password != null)
            {
                loggedIn = login.ValidLogin(username, password);
            }

            Assert.That(loggedIn, "We did not login");
        }
    }
}
