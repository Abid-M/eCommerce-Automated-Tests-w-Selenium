/* Author: Abid Miah */
using OpenQA.Selenium;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class AllOrdersPOM
    {
        //Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;

        //Constructor to get the driver from the test
        public AllOrdersPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        //Gets the first td in the table row (latest top order)
        private IWebElement _newOrderNumber => StaticWaitForElement(_driver, By.CssSelector("td[data-title=\"Order\"] a"));
        public IWebElement OrdersTable => _driver.FindElement(By.CssSelector(".woocommerce-orders-table")); //public for screenshot
        


        // Gets the new order number from the order confirmation page.
        public string GetNewOrderNumber()
        {
            string newOrderNo = _newOrderNumber.Text;
            return newOrderNo.Replace("#", ""); //Remove # from string, only return order number
        }
    }
}
