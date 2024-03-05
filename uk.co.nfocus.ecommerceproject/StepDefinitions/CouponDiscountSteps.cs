/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus.ecommerceproject.POMClasses;
using uk.co.nfocus.ecommerceproject.Utils;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CouponDiscountSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public CouponDiscountSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = specFlowOutputHelper;

            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        /*
         [When] "I add item to my cart"
         - Adds an item to the cart. 
         - Examples include:. 
        */
        [When(@"I add '(.*)' into my cart")]
        public void WhenIAddAnIntoMyCart(string item)
        {
            // Navigate to the shop page via the navigation bar
            NavPOM nav = new NavPOM(_driver, _specFlowOutputHelper);
            nav.GoToShop();

            // Find the specified item and add it to the cart
            // (Assumes the item can be found directly on the shop page)
            ShopPOM shop = new ShopPOM(_driver, _specFlowOutputHelper); 

            _scenarioContext["itemName"] = item; // Store item for later use in other step

            // Find the item and assert that item exists
            bool itemExist = shop.FindAndAddItem(item);
            Assert.That(itemExist, "Item does not exist");

            // Go to Cart Page
            shop.GoToCart();
        }

        /*
         [When] "I apply the coupon code 'edgewords' to the cart"
         - Verifies that the item added is actually in the cart.
         - Applies the 15% coupon 'edgewords' to the cart.
        */
        [When(@"I apply the coupon code '(.*)' to the cart")]
        public void WhenIApplyTheCouponCodeToTheCart(string couponCode)
        {
            // Check that the item is present in the cart before applying coupon
            CartPOM cart = new CartPOM(_driver, _specFlowOutputHelper);

            string item = (string)_scenarioContext["itemName"];
            Assert.That(cart.CheckItemInCart(item), "Item added, not in cart!");

            // Apply coupon check
            cart.EnterCoupon(couponCode).ApplyCoupon();
            Assert.That(cart.ValidateCoupon(couponCode), "Coupon does not exist!");
        }

        /*
         [Then] "I receive '15'% discount off my total, excluding shipping "
         - Calculates the coupon discount and verifies it with the grand total.
        */
        [Then(@"I recieve '(.*)'% discount off my total, excluding shipping")]
        public void ThenIRecieveDiscountOffMyTotalExcludingShipping(int expectedDiscount)
        {
            // Reports discount percentage applied from previous step
            CartPOM cart = new CartPOM(_driver, _specFlowOutputHelper);
            int discount = cart.GetDiscountPercentage();

            // Checks that the calculated discount is equal to the expectedDiscount passed by feature file
            Assert.That(discount, Is.EqualTo(expectedDiscount), $"Expected {expectedDiscount}% off, Actual {discount}% off instead");

            // Verify discount check
            Assert.That(cart.GetGrandTotalPrice(), Is.EqualTo(cart.ValidateTotal()), "Discount not applied correctly");
            _specFlowOutputHelper.WriteLine($"Verified that the discount was correctly applied to the cart..");
            _specFlowOutputHelper.WriteLine($"Expected total value: £{cart.GetGrandTotalPrice()}, Actual total value: £{cart.ValidateTotal()}");

            new HelperLib(_specFlowOutputHelper).TakeScreenshot(_driver, cart.CartTotal, "Coupon_Discount_Price"); // Screenshot report
        }
    }
}