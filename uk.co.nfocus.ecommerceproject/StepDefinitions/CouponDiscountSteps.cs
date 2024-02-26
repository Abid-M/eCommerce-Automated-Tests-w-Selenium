using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CouponDiscountSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public CouponDiscountSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        [When(@"I add an '(.*)' into my cart")]
        public void WhenIAddAnIntoMyCart(string item)
        {
            _scenarioContext.Pending();
        }

        [When(@"I apply the coupon code '(.*)' to the cart")]
        public void WhenIApplyTheCouponCodeToTheCart(string couponCode)
        {
            _scenarioContext.Pending();
        }

        [Then(@"I recieve '(.*)'% discount off my total, excluding shipping")]
        public void ThenIRecieveDiscountOffMyTotalExcludingShipping(int discountPercentage)
        {
            _scenarioContext.Pending();
        }
    }
}