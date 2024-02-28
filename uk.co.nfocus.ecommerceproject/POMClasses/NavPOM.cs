/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class NavPOM
    {
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public NavPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;

        }

        // Locators
        private IWebElement _shopLink => WaitForElement(_driver, By.LinkText("Shop"));
        private IWebElement _cartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountLink => _driver.FindElement(By.LinkText("My account"));
        private IWebElement _dismissBannerLink => _driver.FindElement(By.LinkText("Dismiss"));


        // Navigates to the shop page.
        public void GoToShop()
        {
            _shopLink.Click();
            _specFlowOutputHelper.WriteLine("Navigated to the Shop Page");
        }

        // Navigates to the cart page.
        public void GoToCart()
        {
            _cartLink.Click();
            _specFlowOutputHelper.WriteLine("Navigated to the Cart Page");
        }

        // Navigates to the checkout page.
        public void GoToCheckout()
        {
            _checkoutLink.Click();
            _specFlowOutputHelper.WriteLine("Navigated to Checkout page");
        }

        // Navigates to the my account page.
        public void GoToAccount()
        {
            _myAccountLink.Click();
            _specFlowOutputHelper.WriteLine("Navigated to Account page");

        }

        /* Clicks the "Dismiss" link in the blue banner, if it is present. */
        public void DismissBanner()
        {
            try
            {
                // Try to find the "Dismiss" link and click it
                _dismissBannerLink.Click();
            }
            catch
            {
                // If the "Dismiss" link is not found, do nothing
                // (No Blue Banner shown)
            }
        }


    }
}
