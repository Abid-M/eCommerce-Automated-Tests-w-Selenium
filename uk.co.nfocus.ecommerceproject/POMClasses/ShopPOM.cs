/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class ShopPOM
    {
        // Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        // Constructor to get the driver from the test
        public ShopPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        //Locators
        IList<IWebElement> _allItems => _driver.FindElements(By.CssSelector("li h2")); //Collates all items in the shop
        IWebElement _viewCartButton => WaitForElement(_driver, By.CssSelector(".added_to_cart"));

        /* This method checks if a shop item with a given name exists in the list of all items.
        If the item is found, it is added to the cart and the method returns true.
        If the item is not found, the method returns false. */
        public bool FindAndAddItem(string name)
        {
            // Iterate through each item in the list of all items
            foreach (IWebElement item in _allItems)
            {
                // Check if the text of the current item matches the given name (case-insensitive)
                if (item.Text.ToLower().Equals((name).ToLower()))
                {
                    // If the item is found, report to the console
                    _specFlowOutputHelper.WriteLine($"Verified that the '{name}' item exists on the shop page");

                    // Call the AddToCart method to add the item to the cart
                    AddToCart(item, name); 

                    // Return true to indicate that the item was found
                    return true;
                }
            }

            // If the item was not found, return false
            return false;
        }

        public void AddToCart(IWebElement item, string name)
        {
            // Parent element of h2 text, then following 'a' element clicked
            item.FindElement(By.XPath("../following-sibling::a")).Click(); //Not locator as needs reference to item name
            
            _specFlowOutputHelper.WriteLine($"Added the '{name}' item to the cart");
        }

        // Navigates to the cart page.
        public void GoToCart()
        {
            _viewCartButton.Click();
        }
    }
}
