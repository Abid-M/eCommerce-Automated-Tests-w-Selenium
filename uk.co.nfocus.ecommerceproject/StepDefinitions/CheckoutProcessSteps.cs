/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus.ecommerceproject.POMClasses;
using uk.co.nfocus.ecommerceproject.Utils;

namespace uk.co.nfocus.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CheckoutProcessSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public CheckoutProcessSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = specFlowOutputHelper;

            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        /*
         [Given] "that the cart contains 'hoodie'"
         - Adds an item to the cart for the given context.
         - Examples include: Hoodie, Cap. 
        */
        [Given(@"that the cart contains '(.*)'")]
        public void GivenThatTheCartContains(string item)
        {
            // Navigate to the shop page via the navigation bar
            NavPOM nav = new NavPOM(_driver, _specFlowOutputHelper);
            nav.GoToShop();

            // Find the specified item and add it to the cart
            // (Assumes the item can be found directly on the shop page)
            ShopPOM shop = new ShopPOM(_driver, _specFlowOutputHelper);

            // Find the item and assert that item exists
            bool itemExist = shop.FindAndAddItem(item);
            Assert.That(itemExist, "Item does not exist");

            // Go to Cart Page
            shop.GoToCart();
        }

        /*
         [When] "I proceed to checkout"
         - Redirects to the checkout page on button click.
        */
        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            // navigates to checkout page
            new CartPOM(_driver, _specFlowOutputHelper).GoToCheckout();
        }

        /*
         [When] "I provide the billing details"
         - Uses the table passed from feature file.
         - Creates a customer object which is used to populate the billing fields.
        */
        [When(@"I provide the billing details:")]
        public void WhenIProvideTheBillingDetails(Table customerInfo)
        {
            CheckoutPOM checkout = new CheckoutPOM(_driver, _specFlowOutputHelper);
            // Creates a customer with the details passed from the feature table
            Customer customer = checkout.CreateCustomer(customerInfo);
            // Fill in Billing Input Fields with customer object
            checkout.FillInBillingDetails(customer);

            // Validate billing fields have been entered
            Assert.That(checkout.ValidateDetails(customer), "Billing input fields not entered!");
            _specFlowOutputHelper.WriteLine("Validated Billing Details have actually been populated");
        }

        /*
         [When] "I place the order with 'payment method' payment"
         - Selects payment method, either 'cash' or 'check' and places the order.
         - New order number is captured and stored in scenario context.
        */
        [When(@"I place the order with '(.*)' payment")]
        public void WhenIPlaceTheOrder(string paymentMethod)
        {
            CheckoutPOM checkout = new CheckoutPOM(_driver, _specFlowOutputHelper);
            checkout.SelectPayment(paymentMethod).PlaceOrder();

            OrderInfoPOM orderInfo = new OrderInfoPOM(_driver, _specFlowOutputHelper);
            string newOrderNumber = orderInfo.GetOrderNumber(); // Fetch order number on page

            _scenarioContext["newOrderNumber"] = newOrderNumber; // Store new Order Number for use in next step

            new HelperLib(_specFlowOutputHelper).TakeScreenshot(_driver, orderInfo.SsOrderNumber, "New_Order_Number"); // Screenshot of newly placed order
        }

        /*
         [Then] "the order should appear in my accounts order history"
         - Verifies the new order number in the previous step is shown on the order history.
        */
        [Then(@"the order should appear in my accounts order history")]
        public void ThenTheOrderShouldBeInHistory()
        {
            // Navigate to all orders page from account
            new NavPOM(_driver, _specFlowOutputHelper).GoToAccount();
            new MyAccountPOM(_driver, _specFlowOutputHelper).GoToOrders();

            AllOrdersPOM allOrders = new AllOrdersPOM(_driver);
            string newOrderNumber = (string)_scenarioContext["newOrderNumber"];
            string orderNoCheck = allOrders.GetLatestOrder(); // Capture order no. on All Orders Page

            // Verifying order numbers are the same from Checkout and in Account Orders
            Assert.That(orderNoCheck, Is.EqualTo(newOrderNumber), "Order numbers do not match!");
            _specFlowOutputHelper.WriteLine($"Verified that the order numbers match from checkout page..");
            _specFlowOutputHelper.WriteLine($"Expected order number: {orderNoCheck}, Actual order number: {newOrderNumber}");

            new HelperLib(_specFlowOutputHelper).TakeScreenshot(_driver, allOrders.OrdersTable, "Orders"); //Screenshot report
        }
    }
}