﻿using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.ecommerceproject.Utils;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;
using uk.co.nfocus.ecommerceproject.POMClasses;
using OpenQA.Selenium.DevTools.V119.FedCm;

namespace uk.co.nfocus.ecommerceproject
{
    internal class eCommerce : BaseTest
    {

        [Test]
        public void TestCouponDiscount()
        {
            //Calls helper method as common to both test cases.
            //Navigates to shop, Add and Checks item is in the cart
            AddItemToCart(driver);

            CartPOM cart = new CartPOM(driver);
            string coupon = "edgewords";

            //Apply coupon check
            cart.EnterCoupon(coupon).ApplyCoupon();
            Assert.That(cart.ValidateCoupon(coupon), "Coupon does not exist!");

            //Reports discount percentage
            Console.WriteLine($"Applied a {cart.GetDiscountPercentage()}% discount"); 

            //Verify discount check
            Assert.That(cart.GetGrandTotalPrice(), Is.EqualTo(cart.ValidateTotal()), "Discount not applied correctly"); 
            Console.WriteLine($"Verified that the discount was correctly applied to the cart..");
            Console.WriteLine($"Expected total value: £{cart.GetGrandTotalPrice()}, Actual total value: £{cart.ValidateTotal()}");

            TakeScreenshot(driver, By.CssSelector(".cart_totals"), "Coupon-Discount-Price"); //Screenshot report
        }

        [Test]
        public void TestCheckoutOrder()
        {
            //Calls helper method as common to both test cases.
            //Navigates to shop, Add and Checks item is in the cart
            AddItemToCart(driver);

            //Navigate to Checkout Page
            new CartPOM(driver).GoToCheckout();

            //Create customer object
            Customer testCustomer = new Customer("Abid", "Miah", "17 Sui Lane", "London", "SW19 2JY", "07365827365", "test.email@nfocus.co.uk");
            
            //Fill in Billing Input Fields with testCustomer object
            CheckoutPOM checkout = new CheckoutPOM(driver);
            checkout.FillInBillingDetails(testCustomer);
            Console.WriteLine("Billing Details filled in");

            //Selecting Cheque payment and placing the order
            checkout.SelectChequePayment().PlaceOrder(); 
            Console.WriteLine("Cheque Payment Selected\nOrder Placed..");

            string newOrderNumber = new OrderInfoPOM(driver).GetOrderNumber(); //fetch order number on page
            Console.WriteLine($"New Order Number: {newOrderNumber}");
            TakeScreenshot(driver, By.CssSelector("li.woocommerce-order-overview__order.order"), "New-Order-Number"); //Screenshot of newly placed order

            //Navigate to orders page from account
            new NavPOM(driver).GoToAccount(); 
            new MyAccountPOM(driver).GoToOrders();
            string orderNoCheck = new AllOrdersPOM(driver).GetNewOrderNumber();

            //Verifying order numbers are the same from Checkout and in Account Orders
            Assert.That(orderNoCheck, Is.EqualTo(newOrderNumber), "Order numbers do not match!");
            Console.WriteLine($"Verified that the order numbers match..");
            Console.WriteLine($"Expected order number: {orderNoCheck}, Actual order number: {newOrderNumber}");

            TakeScreenshot(driver, By.CssSelector(".woocommerce-orders-table"), "Orders"); //Screenshot report
        }
    }
}