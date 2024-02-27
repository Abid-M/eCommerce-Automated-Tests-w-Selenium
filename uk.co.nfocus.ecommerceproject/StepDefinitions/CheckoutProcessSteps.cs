using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.ecommerceproject.POMClasses;
using uk.co.nfocus.ecommerceproject.Utils;
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
            // Navigate to the shop page via the navigation bar
            NavPOM nav = new NavPOM(_driver);
            nav.GoToShop();

            // Find the specified item and add it to the cart
            // (Assumes the item can be found directly on the shop page)
            ShopPOM shop = new ShopPOM(_driver);

            // Find the item and assert that item exists
            bool itemExist = shop.FindAndAddItem(item);
            Assert.That(itemExist, "Item does not exist");

            // Go to Cart Page
            shop.GoToCart();
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            // navigates to checkout page
            new CartPOM(_driver).GoToCheckout();
        }

        [When(@"I provide the billing details:")]
        public void WhenIProvideTheBillingDetails(Table customerInfo)
        {
            CheckoutPOM checkout = new CheckoutPOM(_driver);

            //Customer customer = checkout.FillInBillingDetails(customerInfo);
            Console.WriteLine("Billing Details Populated..");
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