/* Author: Abid Miah */
using OpenQA.Selenium;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class OrderInfoPOM
    {
        // Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;

        // Constructor to get the driver from the test
        public OrderInfoPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        private IWebElement _orderNumber => StaticWaitForElement(_driver, By.CssSelector(".order strong"));
        public IWebElement SsOrderNumber => _driver.FindElement(By.CssSelector("li.woocommerce-order-overview__order.order")); //public for screenshots

        // Gets the order number from the order confirmation page.
        public string GetOrderNumber()
        {
            Console.WriteLine($"New Order Number: {_orderNumber.Text}");
            return _orderNumber.Text;
        }
    }
}
