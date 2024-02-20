using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        IList<IWebElement> _items => _driver.FindElements(By.CssSelector("td.product-name a")); //Collates all items in the cart (Tests for multiple items in cart)
        private IWebElement _couponCodeField => StaticWaitForElement(_driver, By.Name("coupon_code"));
        private IWebElement _applyCouponButton => _driver.FindElement(By.Name("apply_coupon"));
        private IWebElement _subtotalPrice => _driver.FindElement(By.CssSelector("td:nth-child(2) > .woocommerce-Price-amount > bdi"));
        private IWebElement _shippingPrice => _driver.FindElement(By.CssSelector("Label > span > bdi"));
        private IWebElement _grandTotalPrice => _driver.FindElement(By.CssSelector(".order-total > td"));
        private IWebElement _couponDiscount => _driver.FindElement(By.CssSelector("tr.cart-discount > td > span"));
        private IWebElement _removeItemButton => StaticWaitForElement(_driver, By.ClassName("remove"), 1);
        private IWebElement _cartEmptyDialog => StaticWaitForElement(_driver, By.ClassName("cart-empty"), 1);
        private IWebElement _checkoutLink => StaticWaitForElement(_driver, By.ClassName("checkout-button"));


        //Empty cart on initial load of test, loop 100 times in order to try and catch and hope it removes all items before 25 tries.
        public void EmptyCart()
        {
            //NOT while loop, just in case it gets stuck in an infinite loop
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    // Check if the cart empty dialog is displayed after clicking the remove button
                    try
                    {
                        if (_cartEmptyDialog.Displayed)
                        {
                            break; //break out of loop (cart empty)
                        }
                    }
                    catch
                    {
                       //Need additional try catch so doesn't loop 100 times (false positives)
                    }

                    // Check if the remove button is present before clicking it
                    if (_removeItemButton.Displayed && _removeItemButton.Enabled)
                    {
                        _removeItemButton.Click();
                    } 
                }
                catch (Exception e)
                {
                    // Try again
                }
            }

            Console.WriteLine("Cart Cleared");
        }
        public bool CheckItemInCart(string name)
        {
            foreach (IWebElement item in _items)
            {
                if (item.Text.ToLower().Equals(name.ToLower()))
                {
                    Console.WriteLine($"Verified that the '{name}' item is in the cart");
                    return true;
                }
            }
            return false;
        }
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

        public bool ValidateCoupon(string coupon)
        {
            try
            {
                string noticeText = StaticWaitForElement(_driver, By.ClassName("woocommerce-error")).Text;

                if (noticeText.Contains("does not exist!"))
                {
                    return false;
                }

                //notice text mentions coupon already applied
                Console.WriteLine($"Valid Coupon Applied: '{coupon}'");
                return true;
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Valid Coupon Applied: '{coupon}'");
                return true; //Coupon applied
            }
        }

        public decimal GetSubtotalPrice()
        {
            string strValue = _subtotalPrice.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        public decimal GetShippingPrice()
        {
            string strValue = _shippingPrice.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        public decimal GetGrandTotalPrice()
        {
            string strValue = _grandTotalPrice.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        public decimal GetCouponDiscount()
        {
            string strValue = _couponDiscount.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        public int GetDiscountPercentage()
        {
            decimal discountPercentageAsDecimal = GetCouponDiscount() / GetSubtotalPrice() * 100;
            int discountPercentage = (int)discountPercentageAsDecimal;
            
            return discountPercentage;
        }

        public decimal ValidateTotal()
        {
            decimal checkTotal = GetSubtotalPrice() - GetCouponDiscount() + GetShippingPrice();
            return checkTotal;
        }

        public void GoToCheckout()
        {
            _checkoutLink.Click();
        }
    }
}
