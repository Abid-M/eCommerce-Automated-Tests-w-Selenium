﻿using OpenQA.Selenium;
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


        //Empty cart on initial load of test, loop max. 50 times.
        public void EmptyCart()
        {
            //NOT while loop, just in case it gets stuck in an infinite loop
            for (int i = 0; i < 50; i++)
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
                       //Need additional try catch so doesn't loop 50 times (false positives)
                    }

                    _removeItemButton.Click();
                }
                catch
                {
                    // Try again
                }
            }

            Console.WriteLine("Cart Cleared");
        }

        // Checks if the specified item is in the cart.
        public bool CheckItemInCart(string name)
        {
            // Iterate through each item in the cart
            foreach (IWebElement item in _items)
            {
                // Check if the item's text matches the specified name (case-insensitive)
                if (item.Text.ToLower().Equals(name.ToLower()))
                {
                    // If a match is found, print a message to the console and return true
                    Console.WriteLine($"Verified that the '{name}' item is in the cart");
                    return true;
                }
            }

            // If no match is found, return false
            return false;
        }

        // Clears and sets the value coupon from argument value.
        public CartPOM EnterCoupon(string coupon)
        {
            _couponCodeField.Clear();
            _couponCodeField.SendKeys(coupon);

            return this;
        }

        // Clicks apply coupon button
        public void ApplyCoupon()
        {
            _applyCouponButton.Click();
        }

        // Validates that the specified coupon code can be applied to the cart.
        public bool ValidateCoupon(string coupon)
        {
            try
            {
                // Get the notice text from the page
                string noticeText = StaticWaitForElement(_driver, By.ClassName("woocommerce-error")).Text;

                // Check if the notice text indicates that the coupon does not exist
                if (noticeText.Contains("does not exist!"))
                {
                    return false;
                }

                // Check if the notice text mentions that the coupon has already been applied
                if (noticeText.Contains("has been applied", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Valid Coupon Applied: '{coupon}'");
                    return true;
                }

                // If the notice text does not indicate that the coupon is invalid or applied, return false
                return false;
            }
            catch
            {
                // If an exception is thrown (e.g. if the notice element is not found), assume that the coupon was applied successfully
                Console.WriteLine($"Valid Coupon Applied: '{coupon}'");
                return true; //Coupon applied
            }
        }

        // Gets the subtotal price from the cart page.
        public decimal GetSubtotalPrice()
        {
            string strValue = _subtotalPrice.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        // Gets the shipping price from the cart page.
        public decimal GetShippingPrice()
        {
            string strValue = _shippingPrice.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        // Gets the grand total price from the cart page.
        public decimal GetGrandTotalPrice()
        {
            string strValue = _grandTotalPrice.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        // Gets the coupon discount from the cart page.
        public decimal GetCouponDiscount()
        {
            string strValue = _couponDiscount.Text;
            return decimal.Parse(strValue, NumberStyles.Currency); //Removes pound symbol
        }

        // Gets the coupon discount percentage as an integer.
        public int GetDiscountPercentage()
        {
            // Calculate the coupon discount percentage as a decimal
            decimal discountPercentageAsDecimal = GetCouponDiscount() / GetSubtotalPrice() * 100;

            // Convert the decimal to an integer
            int discountPercentage = (int)discountPercentageAsDecimal;

            return discountPercentage;
        }

        // Validates that the calculated total matches the grand total price.
        public decimal ValidateTotal()
        {
            decimal checkTotal = GetSubtotalPrice() - GetCouponDiscount() + GetShippingPrice();
            return checkTotal;
        }

        // Navigates to the checkout page.
        public void GoToCheckout()
        {
            _checkoutLink.Click();
        }
    }
}