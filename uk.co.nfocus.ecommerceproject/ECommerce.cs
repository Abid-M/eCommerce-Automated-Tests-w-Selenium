using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.ecommerceproject.Utils;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;
using uk.co.nfocus.ecommerceproject.POMClasses;

namespace uk.co.nfocus.ecommerceproject
{
    internal class ECommerce : BaseTest
    {

        [Test]
        public void TestCouponDiscountPOM()
        {
            //Move login to helper or setup
            LoginPOM login = new LoginPOM(driver);

            bool loggedIn = login.ValidLogin(Environment.GetEnvironmentVariable("USERNAME"), Environment.GetEnvironmentVariable("PASSWORD"));
            Assert.That(loggedIn, "We did not login");

            //Navigate to Shop Page via navbar
            NavPOM nav = new NavPOM(driver);
            nav.goToShop();
            Console.WriteLine("Navigated to the Shop Page");

            //Find Item and Add to Cart
            //(Use does not need to search, just finding an item on the shop page) 
            ShopPOM shop = new ShopPOM(driver);
            string item = "sunglasses";

            bool itemExist = shop.FindItem(item); //Find Item and Add to cart
            Assert.That(itemExist, "Item does not exist");

            nav.goToCart();
            Console.WriteLine("Navigated to the Cart Page");



        }

        [Test]
        public void TestCouponDiscount()
        {
            Login(driver);

            driver.FindElement(By.LinkText("Shop")).Click(); //Visit Shop Page

            IList<IWebElement> allItems = driver.FindElements(By.CssSelector("li h2"));

            string item = "sunglasses";

            foreach (IWebElement el in allItems)
            {
                if(el.Text.ToLower().Equals((item).ToLower()))
                {
                    Console.WriteLine("win");
                    el.FindElement(By.XPath("../following-sibling::a")).Click(); //Parent element of h2 text, then following a element clicked
                    break;
                }
            }

            //driver.FindElement(By.CssSelector(".post-30 > .button")).Click(); //Add Sunglasses to Cart
            StaticWaitForElement(driver, By.CssSelector(".added_to_cart")).Click(); //Navigate to the cart page

            StaticWaitForElement(driver, By.Name("coupon_code")).SendKeys("edgewords"); //Wait check that coupon code input is displayed in order to SendKeys
            driver.FindElement(By.Name("apply_coupon")).Click(); //Apply coupon

            Assert.That(StaticWaitForElement(driver, By.LinkText("[Remove]")).Displayed); //Check that the coupon takes off the discount

            //Scroll page to see the action in a X-Browser friendly way.
            IWebElement cartTotal = driver.FindElement(By.CssSelector(".cart_totals"));
            IJavaScriptExecutor? jsdriver = driver as IJavaScriptExecutor; //Historically not all drivers could execute JS, so there is a need to cast a capable drievr to a type that can run JS.
            jsdriver?.ExecuteScript("arguments[0].scrollIntoView()", cartTotal); //footer is the 0th argument passed in
            
            TakeScreenshot(driver, cartTotal, "Coupon-Discount-Price");


            //Get Subtotal price in decimal
            decimal price = StringToDecimal(driver, By.CssSelector("td:nth-child(2) > .woocommerce-Price-amount > bdi"));

            //Get Shipping price and convert to decimal
            decimal shippingPrice = StringToDecimal(driver, By.CssSelector("#shipping_method > li > label > span > bdi"));

            //Get Total price and convert to decimal
            decimal total = StringToDecimal(driver, By.CssSelector(".order-total > td"));

            //Check total calculated after coupon & shipping is correct
            decimal checkTotal = (price - (price * (decimal)0.15)) + shippingPrice;
            Assert.That(total, Is.EqualTo(checkTotal), "They are not equal");
        }

        [Test]
        public void TestCheckoutOrder()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            Login(driver);

            driver.FindElement(By.LinkText("Shop")).Click(); //Visit Shop Page
            driver.FindElement(By.CssSelector(".post-31 > .button")).Click(); //Add Hoodie with Logo to Cart
            StaticWaitForElement(driver, By.CssSelector(".added_to_cart")).Click(); //Navigate to the Cart Page

            driver.FindElement(By.LinkText("Proceed to checkout")).Click(); //Proceed to checkout page

            //Get rid of any pre-filled in text
            IList<IWebElement> inputFields = driver.FindElements(By.CssSelector(".woocommerce-input-wrapper input"));
            foreach (IWebElement el in inputFields)
            {
                if (el.Displayed && el.Enabled && el.GetAttribute("id") != "billing_email") //Same email as account email pre-filled
                {
                    el.Clear();

                }
            }

            //Fill in billing details
            driver.FindElement(By.Id("billing_first_name")).SendKeys("Abid");
            driver.FindElement(By.Id("billing_last_name")).SendKeys("Miah");
            driver.FindElement(By.Id("billing_address_1")).SendKeys("123 Waterfall Lane");
            driver.FindElement(By.Id("billing_city")).SendKeys("London");
            driver.FindElement(By.Id("billing_postcode")).SendKeys("SW19 2JY");
            driver.FindElement(By.Id("billing_phone")).SendKeys("07854632591");

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(driver => !driver.FindElement(By.CssSelector(".blockUI.blockOverlay")).Displayed);
                //Wait until the block overlay is NOT displayed! OR timeout ends
            }
            catch (Exception)
            {
                //nothing
            }


            StaticWaitForElement(driver, By.CssSelector("li.wc_payment_method.payment_method_cheque > label")).Click(); //Payment method by check
            StaticWaitForElement(driver, By.Id("place_order")).Click(); //Place the order

            string orderNo = StaticWaitForElement(driver, By.CssSelector(".order strong")).Text; //Get Order number
            Console.WriteLine($"Order number: {orderNo}");

            //Navigate to orders page
            driver.FindElement(By.LinkText("My account")).Click();
            driver.FindElement(By.LinkText("Orders")).Click();

            string orderNoCheck = driver.FindElement(By.XPath("//tr/td[1]/a")).Text;
            orderNoCheck = orderNoCheck.Replace("#", ""); //Remove # from string, to only save order number

            Assert.That(orderNoCheck, Is.EqualTo(orderNo), "Order numbers do not match!");
        }
    }
}
