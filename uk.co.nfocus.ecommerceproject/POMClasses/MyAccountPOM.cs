﻿/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class MyAccountPOM
    {
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public MyAccountPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper) 
        {
            this._driver = driver; // Assigns passed driver into private field in this class
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        // Locator fields (finding elements on the page)
        private IWebElement _logoutLink => _driver.FindElement(By.LinkText("Logout"));
        private IWebElement _ordersLink => _driver.FindElement(By.LinkText("Orders"));

        public void Logout()
        {
            _logoutLink.Click();
        }
        
        /* Navigates to Orders page */
        public void GoToOrders()
        {
            _ordersLink.Click();
            _specFlowOutputHelper.WriteLine("Navigated to All Orders on account page");
        }
    }
}
