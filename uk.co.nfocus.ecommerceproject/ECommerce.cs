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
using OpenQA.Selenium.DevTools.V119.FedCm;

namespace uk.co.nfocus.ecommerceproject
{
    internal class ECommerce : BaseTest
    {

        [Test]
        public void TestCouponDiscount()
        {
            //Calls helper method as common to both test cases.
            //Navigates to Shop, add and check Item in Cart
            AddItemToCart(driver);

            CartPOM cart = new CartPOM(driver);
            string coupon = "edgewords";

            //Apply coupon check
            cart.EnterCoupon(coupon).ApplyCoupon();
            Assert.That(cart.ValidateCoupon(coupon), "Coupon does not exist!"); 

            Console.WriteLine($"Applied a {cart.GetDiscountPercentage()}% discount"); //Reports discount percentage

            Assert.That(cart.GetGrandTotalPrice(), Is.EqualTo(cart.ValidateTotal()), "Discount not applied correctly"); //Verify discount check
            Console.WriteLine($"Verified that the discount was correctly applied to the cart");
            Console.WriteLine($"Expected total value: £{cart.GetGrandTotalPrice()}, Actual total value: £{cart.ValidateTotal()}");

            TakeScreenshot(driver, By.CssSelector(".cart_totals"), "Coupon-Discount-Price"); //Screenshot report
        }

        [Test]
        public void TestCheckoutOrder()
        {
            //Calls helper method as common to both test cases.
            //Navigates to Shop, add and check Item in Cart
            AddItemToCart(driver);

            //Navigate to Checkout Page
            new CartPOM(driver).GoToCheckout();

            //Create customer object
            Customer testCustomer = new Customer("Abid", "Miah", "17 Sui Lane", "London", "SW19 2JY", "07365827365", "test.email@nfocus.co.uk");
            
            CheckoutPOM checkout = new CheckoutPOM(driver);
            checkout.FillInBillingDetails(testCustomer);
            Console.WriteLine("Billing Details filled in");

            checkout.SelectChequePayment().PlaceOrder(); //Selecting Cheque payment and placing the order
            Console.WriteLine("Cheque Payment Selected\nOrder Placed");

            string newOrderNumber = new OrderInfoPOM(driver).GetOrderNumber(); //fetch order number on page
            Console.WriteLine($"New Order Number: {newOrderNumber}");
            TakeScreenshot(driver, By.CssSelector("li.woocommerce-order-overview__order.order"), "New-Order-Number"); //Screenshot of newly placed order

            //Navigate to orders page from account
            new NavPOM(driver).GoToAccount(); 
            new MyAccountPOM(driver).GoToOrders();
            string orderNoCheck = new AllOrdersPOM(driver).GetNewOrderNumber();

            Assert.That(orderNoCheck, Is.EqualTo(newOrderNumber), "Order numbers do not match!");
            Console.WriteLine($"Verified that the order numbers match");
            Console.WriteLine($"Expected order number: {orderNoCheck}, Actual order number: {newOrderNumber}");
            TakeScreenshot(driver, By.CssSelector(".woocommerce-orders-table"), "Orders");
        }
    }
}
