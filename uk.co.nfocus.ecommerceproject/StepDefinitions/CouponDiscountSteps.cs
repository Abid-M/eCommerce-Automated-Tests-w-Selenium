using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.ecommerceproject.POMClasses;

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
            // Navigate to the shop page via the navigation bar
            NavPOM nav = new NavPOM(_driver);
            nav.GoToShop();
            Console.WriteLine("Navigated to the Shop Page");

            // Find the specified item and add it to the cart
            // (Assumes the item can be found directly on the shop page)
            ShopPOM shop = new ShopPOM(_driver);

            _scenarioContext["itemName"] = item;

            // Find the item and assert that item exists
            bool itemExist = shop.FindAndAddItem(item);
            Assert.That(itemExist, "Item does not exist");
            shop.GoToCart();
            Console.WriteLine("Navigated to the Cart Page");
        }

        [When(@"I apply the coupon code '(.*)' to the cart")]
        public void WhenIApplyTheCouponCodeToTheCart(string couponCode)
        {
            // Check that the item is present in the cart before applying coupon
            CartPOM cart = new CartPOM(_driver);

            string item = (string)_scenarioContext["itemName"];
            Assert.That(cart.CheckItemInCart(item), "Item added, not in cart!");
        }

        [Then(@"I recieve '(.*)'% discount off my total, excluding shipping")]
        public void ThenIRecieveDiscountOffMyTotalExcludingShipping(int discountPercentage)
        {
            _scenarioContext.Pending();
        }
    }
}