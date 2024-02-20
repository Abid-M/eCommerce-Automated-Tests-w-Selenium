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
        private IWebElement HomeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement ShopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement CartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement CheckoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement MyAccountLink => _driver.FindElement(By.LinkText("My account"));

        // Navigates to the shop page.
        public void GoToShop()
        {
            ShopLink.Click();
        }

        // Navigates to the cart page.
        public void GoToCart()
        {
            CartLink.Click();
        }

        // Navigates to the checkout page.
        public void GoToCheckout()
        {
            CheckoutLink.Click();
        }

        // Navigates to the my account page.
        public void GoToAccount()
        {
            MyAccountLink.Click();
        }

    }
}
