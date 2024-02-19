using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class CartPOM
    {
        private IWebDriver _driver; //Field that will hold a driver for Service Methods in this test to work with

        public CartPOM(IWebDriver driver) //Constructor to get the driver from the test
        {
            this._driver = driver; //Assigns passed driver into private field in this class
        }

        //Locators
        private IWebElement _couponCodeField => StaticWaitForElement(_driver, By.Name("coupon_code"));
        private IWebElement _applyCouponButton => _driver.FindElement(By.Name("apply_coupon"));
        private IWebElement _subtotalPrice => _driver.FindElement(By.CssSelector("td:nth-child(2) > .woocommerce-Price-amount > bdi"));
        private IWebElement _shippingPrice => _driver.FindElement(By.CssSelector("Label > span > bdi"));
        private IWebElement _grandTotal => _driver.FindElement(By.CssSelector(".order-total > td"));


        public CartPOM EnterCoupon(string coupon)
        {
            _couponCodeField.Clear();
            _couponCodeField.SendKeys(coupon);

            return this;
        }

        public void ApplyCoupon()
        {
            _applyCouponButton.Click();
        }

        public bool ValidateCoupon()
        {
            try
            {
                StaticWaitForElement(_driver, By.ClassName("woocommerce-error"));
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Valid Coupon Applied");
                return true; //Coupon applied
            }
        }
    }
}
