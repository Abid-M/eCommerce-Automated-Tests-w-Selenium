using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class OrderInfoPOM
    {
        //Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;

        //Constructor to get the driver from the test
        public OrderInfoPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _orderNumber => StaticWaitForElement(_driver, By.CssSelector(".order strong"));

        public string GetOrderNumber()
        {
            return _orderNumber.Text;
        }
    }
}
