using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus.ecommerceproject.POMClasses;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public LoginSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = specFlowOutputHelper;

            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        [Given(@"I am on the eCommerce Website")]
        public void GivenIAmOnTheECommerceWebsite()
        {
            // Direct to URL in test parameters, not env variable as not a secret
            _driver.Url = TestContext.Parameters["WebAppURL"]; // starting on cart page to clear

            // Dismisses the notice banner
            NavPOM nav = new NavPOM(_driver, _specFlowOutputHelper);
            nav.DismissBanner();
        }

        [Given(@"I am logged in as a registered user")]
        public void GivenIAmLoggedInAsARegisteredUser()
        {
            LoginPOM login = new LoginPOM(_driver, _specFlowOutputHelper);

            string? username = Environment.GetEnvironmentVariable("USERNAME");
            string? password = Environment.GetEnvironmentVariable("PASSWORD");
            bool loggedIn = false;

            if (username !=null && password != null)
            {
                loggedIn = login.ValidLogin(username, password);
            }

            Assert.That(loggedIn, "We did not login");

            // Emptying cart after login to have fix state at the start of test
            new NavPOM(_driver, _specFlowOutputHelper).GoToCart();
            new CartPOM(_driver, _specFlowOutputHelper).EmptyCart();
        }
    }
}
