/* Author: Abid Miah */
using OpenQA.Selenium;
using System.Xml.Linq;
using TechTalk.SpecFlow.Infrastructure;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class ShopPOM
    {
        // Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs
        //private IList<IWebElement> _allItems;

        /* Constructor to get the driver from the test */
        public ShopPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;
            //_allItems = _driver.FindElements(By.CssSelector("li h2"));
        }

        // Locators
        IWebElement _viewCartButton => WaitForElement(_driver, By.LinkText("View cart"));
        IWebElement _addToCart(string name) => _driver.FindElement(By.CssSelector($"[aria-label=\"Add “{name}” to your cart\"]"));


        /* AddToCart(string)
         - Clicks the "Add to Cart" button for the specified item. Returns true.
         - If the item is not found, the method returns false. 
        */
        public bool AddToCart(string itemName)
        {
            try
            {
                _addToCart(itemName).Click(); // Ref locator passing the specific item name

                _specFlowOutputHelper.WriteLine($"'{itemName}' exists on the shop page..");
                _specFlowOutputHelper.WriteLine("Added to Cart");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /* Navigates to the cart page. */
        public void GoToCart()
        {
            _viewCartButton.Click();
        }
    }
}
