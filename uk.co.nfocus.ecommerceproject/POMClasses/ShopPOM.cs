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
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        // Constructor to get the driver from the test
        public ShopPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        //Locators
        IList<IWebElement> _allItems => _driver.FindElements(By.CssSelector("li h2")); //Collates all items in the shop
        IWebElement _viewCartButton => WaitForElement(_driver, By.LinkText("View cart"));
        IWebElement _addToCart(string name) => _driver.FindElement(By.CssSelector($"[aria-label=\"Add “{name}” to your cart\"]"));

        /* FindAndAddItem(string)
         - This method checks if a shop item with a given name exists in the list of all items.
         - If the item is found, it is added to the cart and the method returns true.
         - If the item is not found, the method returns false. 
        */
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
                    AddToCart(name); 

                    // Return true to indicate that the item was found
                    return true;
                }
            }

            // If the item was not found, return false
            return false;
        }

        /* Clicks the "Add to Cart" button for the specified item. */
        public void AddToCart(string itemName)
        {
            _addToCart(itemName).Click(); // Ref locator passing the specific item name
            _specFlowOutputHelper.WriteLine($"Added '{itemName}' item to the cart");
        }

        /* Navigates to the cart page. */
        public void GoToCart()
        {
            _viewCartButton.Click();
        }
    }
}
