using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CheckoutProcessSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public CheckoutProcessSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        [Given(@"that the cart contains '(.*)'")]
        public void GivenThatTheCartContains(string item)
        {
            _scenarioContext.Pending();
        }

        [When(@"I proceed to checkout")]
        public void WhenIProvideTheBillingDetails()
        {
            _scenarioContext.Pending();
        }

        [When(@"I provide the billing details:")]
        public void WhenIProvideTheBillingDetails(Table table)
        {
            _scenarioContext.Pending();
        }

        [When(@"I place the order")]
        public void WhenIPlaceTheOrder()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the order should appear in my accounts order history")]
        public void ThenTheOrderShouldAppearInMyAccountsOrderHistory()
        {
            _scenarioContext.Pending();
        }
    }
}