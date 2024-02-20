using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class MyAccountPOM
    {
        private IWebDriver _driver; // Field that will hold a driver for Service Methods in this test to work with

        public MyAccountPOM(IWebDriver driver) // Constructor to get the driver from the test
        {
            this._driver = driver; // Assigns passed driver into private field in this class
        }

        // Locator fields (finding elements on the page)
        private IWebElement _logoutLink => _driver.FindElement(By.LinkText("Logout"));
        private IWebElement _ordersLink => _driver.FindElement(By.LinkText("Orders"));


        // Logouts
        public void Logout()
        {
            _logoutLink.Click();
        }
        
        // Navigates to Orders page
        public void GoToOrders()
        {
            _ordersLink.Click();
        }
    }
}
