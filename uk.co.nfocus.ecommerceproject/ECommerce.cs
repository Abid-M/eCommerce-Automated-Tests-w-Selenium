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
        public void TestCouponDiscount()
        {
            //Navigate to Shop Page via navbar
            new NavPOM(driver).GoToShop();
            Console.WriteLine("Navigated to the Shop Page");

            //Find Item and Add to Cart
            //(User does not need to search, finding the item directly on the shop page) 
            ShopPOM shop = new ShopPOM(driver);
            string item = "beanie";

            //Find Item and Add to cart
            bool itemExist = shop.FindItem(item); 
            Assert.That(itemExist, "Item does not exist");

            shop.GoToCart();
            Console.WriteLine("Navigated to the Cart Page");

            CartPOM cart = new CartPOM(driver);
            Assert.That(cart.CheckItemInCart(item), "Item added, not in cart!"); //Check added item is in the cart page

            string coupon = "edgewords";
            cart.EnterCoupon(coupon).ApplyCoupon();
            Assert.That(cart.ValidateCoupon(coupon), "Coupon does not exist!"); //Apply coupon check

            Console.WriteLine($"Applied a {cart.GetDiscountPercentage()}% discount"); //Reports discount percentage

            Assert.That(cart.GetGrandTotalPrice(), Is.EqualTo(cart.ValidateTotal()), "Discount not applied correctly"); //Verify discount check
            Console.WriteLine($"Verified that the discount was correctly applied to the cart");
            Console.WriteLine($"Expected total value: £{cart.GetGrandTotalPrice()}, Actual total value: £{cart.ValidateTotal()}");

            TakeScreenshot(driver, By.CssSelector(".cart_totals"), "Coupon-Discount-Price"); //Screenshot report
        }

        [Test]
        public void TestCheckoutOrderPOM()
        {
            //Navigate to Shop Page via navbar
            new NavPOM(driver).GoToShop();
            Console.WriteLine("Navigated to the Shop Page");

            //Find Item and Add to Cart
            //(User does not need to search, finding the item directly on the shop page) 
            ShopPOM shop = new ShopPOM(driver);
            string item = "beanie";

            //Find Item and Add to cart
            bool itemExist = shop.FindItem(item);
            Assert.That(itemExist, "Item does not exist");

            shop.GoToCart();
            Console.WriteLine("Navigated to the Cart Page");

            CartPOM cart = new CartPOM(driver);
            Assert.That(cart.CheckItemInCart(item), "Item added, not in cart!"); //Check added item is in the cart page

            cart.GoToCheckout();
            Thread.Sleep(2000);
        }

        [Test]
        public void TestCheckoutOrder()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            //Login(driver);

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
