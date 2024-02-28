using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.ecommerceproject.POMClasses;
using uk.co.nfocus.ecommerceproject.Utils;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CheckoutProcessSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public CheckoutProcessSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        /* Adding an item to cart for this context */
        [Given(@"that the cart contains '(.*)'")]
        public void GivenThatTheCartContains(string item)
        {
            // Navigate to the shop page via the navigation bar
            NavPOM nav = new NavPOM(_driver);
            nav.GoToShop();

            // Find the specified item and add it to the cart
            // (Assumes the item can be found directly on the shop page)
            ShopPOM shop = new ShopPOM(_driver);

            // Find the item and assert that item exists
            bool itemExist = shop.FindAndAddItem(item);
            Assert.That(itemExist, "Item does not exist");

            // Go to Cart Page
            shop.GoToCart();
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            // navigates to checkout page
            new CartPOM(_driver).GoToCheckout();
        }

        [When(@"I provide the billing details:")]
        public void WhenIProvideTheBillingDetails(Table customerInfo)
        {
            CheckoutPOM checkout = new CheckoutPOM(_driver);
            // Creates a customer with the details passed from the feature table
            Customer customer = checkout.CreateCustomer(customerInfo);
            // Fill in Billing Input Fields with customer object
            checkout.FillInBillingDetails(customer);

            // Validate billing fields have been entered
            Assert.That(checkout.ValidateDetails(customer), "Billing input fields not entered!");
            Console.WriteLine("Validated Billing Details have actually been populated");
        }

        [When(@"I place the order with '(.*)' payment")]
        public void WhenIPlaceTheOrder(string paymentMethod)
        {
            CheckoutPOM checkout = new CheckoutPOM(_driver);
            checkout.SelectPayment(paymentMethod).PlaceOrder();

            OrderInfoPOM orderInfo = new OrderInfoPOM(_driver);
            string newOrderNumber = orderInfo.GetOrderNumber(); // Fetch order number on page

            _scenarioContext["newOrderNumber"] = newOrderNumber; // Store new Order Number for use in next step

            TakeScreenshot(_driver, orderInfo.SsOrderNumber, "New_Order_Number"); // Screenshot of newly placed order
        }

        [Then(@"the order should appear in my accounts order history")]
        public void ThenTheOrderShouldAppearInMyAccountsOrderHistory()
        {
            // Navigate to all orders page from account
            new NavPOM(_driver).GoToAccount();
            new MyAccountPOM(_driver).GoToOrders();

            AllOrdersPOM allOrders = new AllOrdersPOM(_driver);
            string newOrderNumber = (string)_scenarioContext["newOrderNumber"];
            string orderNoCheck = allOrders.GetLatestOrder(); // Capture order no. on All Orders Page

            // Verifying order numbers are the same from Checkout and in Account Orders
            Assert.That(orderNoCheck, Is.EqualTo(newOrderNumber), "Order numbers do not match!");
            Console.WriteLine($"Verified that the order numbers match from checkout page..");
            Console.WriteLine($"Expected order number: {orderNoCheck}, Actual order number: {newOrderNumber}");

            TakeScreenshot(_driver, allOrders.OrdersTable, "Orders"); //Screenshot report
        }
    }
}