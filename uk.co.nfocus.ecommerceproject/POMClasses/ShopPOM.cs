using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class ShopPOM
    {
        private IWebDriver _driver;

        public ShopPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        IList<IWebElement> _allItems => _driver.FindElements(By.CssSelector("li h2")); //Collates all items in the shop
        IWebElement _viewCartButton => StaticWaitForElement(_driver, By.CssSelector(".added_to_cart"));


        public bool FindItem(string name)
        {
            foreach (IWebElement item in _allItems)
            {
                if (item.Text.ToLower().Equals((name).ToLower()))
                {
                    Console.WriteLine("Shop Item Exists");
                    AddToCart(item); //Parent element of h2 text, then following 'a' element clicked
                    return true;
                } 
            }

            return false;
        }

        public void AddToCart(IWebElement item)
        {
            //Cannot be locator as needs reference to item name
            item.FindElement(By.XPath("../following-sibling::a")).Click();
            Console.WriteLine("Item Added to Cart");
        }
    }
}
