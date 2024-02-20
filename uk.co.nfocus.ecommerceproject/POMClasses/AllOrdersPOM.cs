using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        IWebElement _newOrderNumber =>  StaticWaitForElement(_driver, By.XPath("//tr/td[1]/a"));


        // Gets the new order number from the order confirmation page.
        public string GetNewOrderNumber()
        {
            string newOrderNo = _newOrderNumber.Text;
            return newOrderNo.Replace("#", ""); //Remove # from string, only return order number
        }
    }
}
