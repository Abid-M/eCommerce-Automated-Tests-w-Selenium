/* Author: Abid Miah */
using OpenQA.Selenium;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class NavPOM
    {
        private IWebDriver _driver;

        public NavPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        private IWebElement _homeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _shopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _cartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountLink => _driver.FindElement(By.LinkText("My account"));
        private IWebElement _dismissBannerLink => _driver.FindElement(By.LinkText("Dismiss"));


        // Navigates to the shop page.
        public void GoToShop()
        {
            _shopLink.Click();
            Console.WriteLine("Navigated to the Shop Page");
        }

        // Navigates to the cart page.
        public void GoToCart()
        {
            _cartLink.Click();
            Console.WriteLine("Navigated to the Cart Page");
        }

        // Navigates to the checkout page.
        public void GoToCheckout()
        {
            _checkoutLink.Click();
        }

        // Navigates to the my account page.
        public void GoToAccount()
        {
            _myAccountLink.Click();
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
