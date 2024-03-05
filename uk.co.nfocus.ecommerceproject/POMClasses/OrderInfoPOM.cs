/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class OrderInfoPOM
    {
        // Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        // Constructor to get the driver from the test
        public OrderInfoPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;

        }

        // Locators
        private IWebElement _orderNumber => WaitForElement(_driver, By.CssSelector(".order strong"));
        public IWebElement SsOrderNumber => _driver.FindElement(By.CssSelector("li.woocommerce-order-overview__order.order")); // public for screenshots

        /* Gets the order number from the order confirmation page. */
        public string GetOrderNumber()
        {
            _specFlowOutputHelper.WriteLine($"New Order Number: {_orderNumber.Text}");
            return _orderNumber.Text;
        }
    }
}
