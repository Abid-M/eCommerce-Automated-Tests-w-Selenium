/* Author: Abid Miah */
using OpenQA.Selenium;
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
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public LoginSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = specFlowOutputHelper;

            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        /*
         [Given] "I am on the eCommerce website"
         - Navigates to the eCommerce website account/login page.
         - Dismisses the banner.
        */
        [Given(@"I am on the eCommerce Website")]
        public void GivenIAmOnTheECommerceWebsite()
        {
            // Direct to URL in test parameters, not env variable as not a secret
            _driver.Url = TestContext.Parameters["WebAppURL"] ?? throw new Exception("BASE_URL not set."); 

            // Dismisses the notice banner (Not in feature file as its an implementation detail)
            NavPOM nav = new NavPOM(_driver, _specFlowOutputHelper);
            nav.DismissBanner();
        }

        /*
         [Given] "I am logged in as a registered user"
         - Logs into the eCommerce website as a registered user.
        */
        [Given(@"I am logged in as a registered user")]
        public void GivenIAmLoggedInAsARegisteredUser()
        {
            LoginPOM login = new LoginPOM(_driver, _specFlowOutputHelper);

            // Retrieves sensitive email and password from environment. If variable is null, throw error.
            string? username = Environment.GetEnvironmentVariable("USERNAME") ?? throw new Exception("USERNAME env variable is not set.");
            string? password = Environment.GetEnvironmentVariable("PASSWORD") ?? throw new Exception("PASSWORD env variable is not set.");

            // Validate the login details
            bool loggedIn = login.ValidLogin(username, password);
            Assert.That(loggedIn, "We did not login");

            // Emptying cart after login to have fix state at the start of test
            new NavPOM(_driver, _specFlowOutputHelper).GoToCart();
            new CartPOM(_driver, _specFlowOutputHelper).EmptyCart();
        }
    }
}
