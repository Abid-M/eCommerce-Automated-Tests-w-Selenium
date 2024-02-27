﻿using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.ecommerceproject.POMClasses;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

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

        [When(@"I add '(.*)' into my cart")]
        public void WhenIAddAnIntoMyCart(string item)
        {
            // Navigate to the shop page via the navigation bar
            NavPOM nav = new NavPOM(_driver);
            nav.GoToShop();

            // Find the specified item and add it to the cart
            // (Assumes the item can be found directly on the shop page)
            ShopPOM shop = new ShopPOM(_driver);

            _scenarioContext["itemName"] = item; //store item for later use in other step

            // Find the item and assert that item exists
            bool itemExist = shop.FindAndAddItem(item);
            Assert.That(itemExist, "Item does not exist");

            // Go to Cart Page
            shop.GoToCart();
        }

        [When(@"I apply the coupon code '(.*)' to the cart")]
        public void WhenIApplyTheCouponCodeToTheCart(string couponCode)
        {
            // Check that the item is present in the cart before applying coupon
            CartPOM cart = new CartPOM(_driver);

            string item = (string)_scenarioContext["itemName"];
            Assert.That(cart.CheckItemInCart(item), "Item added, not in cart!");

            //Apply coupon check
            cart.EnterCoupon(couponCode).ApplyCoupon();
            Assert.That(cart.ValidateCoupon(couponCode), "Coupon does not exist!");
        }

        [Then(@"I recieve '(.*)'% discount off my total, excluding shipping")]
        public void ThenIRecieveDiscountOffMyTotalExcludingShipping(int expectedDiscount)
        {
            //Reports discount percentage applied from previous step
            CartPOM cart = new CartPOM(_driver);
            int discount = cart.GetDiscountPercentage();

            // Checks that the calculated discount is equal to the expectedDiscount passed by feature file
            Assert.That(discount, Is.EqualTo(expectedDiscount), $"Expected {expectedDiscount}% off, Actual {discount}% off instead");

            //Verify discount check
            Assert.That(cart.GetGrandTotalPrice(), Is.EqualTo(cart.ValidateTotal()), "Discount not applied correctly");
            Console.WriteLine($"Verified that the discount was correctly applied to the cart..");
            Console.WriteLine($"Expected total value: £{cart.GetGrandTotalPrice()}, Actual total value: £{cart.ValidateTotal()}");

            TakeScreenshot(_driver, cart.CartTotal, "Coupon-Discount-Price"); //Screenshot report
        }
    }
}