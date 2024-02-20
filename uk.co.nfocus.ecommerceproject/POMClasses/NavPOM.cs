using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class NavPOM
    {
        private IWebDriver _driver;

        public NavPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _homeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _shopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _cartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountLink => _driver.FindElement(By.LinkText("My account"));


        public void GoToShop()
        {
            _shopLink.Click();
        }

        public void GoToCart()
        {
            _cartLink.Click();
        }

        public void GoToCheckout()
        {
            _checkoutLink.Click();
        }

        public void GoToAccount()
        {
            _myAccountLink.Click();
        }

    }
}
