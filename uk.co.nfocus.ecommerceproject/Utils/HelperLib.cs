/* Author: Abid Miah */
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using uk.co.nfocus.ecommerceproject.POMClasses;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow;

namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class HelperLib
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public HelperLib(ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        /* Waits for an element to be displayed on the page. */
        public static IWebElement WaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            // Create a new WebDriverWait instance with the specified timeout
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            // Wait for the element to be displayed
            myWait.Until(drv => drv.FindElement(locator).Displayed);

            // Return the element
            return driver.FindElement(locator);
        }

        /* Scrolls the specified element into view. */
        public static void ScrollElIntoView(IWebDriver driver, IWebElement element)
        {
            // Check if the driver supports JavaScript execution
            IJavaScriptExecutor? jsdriver = driver as IJavaScriptExecutor;

            // If the driver supports JavaScript execution, scroll the element into view
            if (jsdriver != null)
            {
                // Use the JavaScript executor to scroll the element into view
                jsdriver.ExecuteScript("arguments[0].scrollIntoView()", element);
            }
        }
     
        /* Takes a screenshot of the specified element and saves it. */
        public void TakeScreenshot(IWebDriver driver, IWebElement element, string el)
        {
            try
            {
                // Scroll the element into view
                ScrollElIntoView(driver, element);

                // Check if the element supports taking screenshots
                var ssElm = element as ITakesScreenshot;

                // If the element supports taking screenshots, take a screenshot
                if (ssElm != null)
                {
                    // Take the screenshot
                    Screenshot screenshotElm = ssElm.GetScreenshot();

                    // Get the current date and time
                    string date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

                    // Create the directory for the screenshots if it doesn't exist
                    //current dir: in bin->Debug>net6.0->screenshots. From net6.0 back to project files
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Screenshots\"));

                    // Create the file path for the screenshot
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Screenshots\", $"{el}_{date}.png");

                    // Save the screenshot to the file
                    screenshotElm.SaveAsFile(filePath);

                    // Write a message to the test output
                    _specFlowOutputHelper.WriteLine($"Attaching '{el}' screenshot to report");

                    // Add the screenshot as a test attachment
                    _specFlowOutputHelper.AddAttachment(filePath);
                }
            }
            catch (Exception e)
            {
                // Write an error message to the console
                _specFlowOutputHelper.WriteLine($"Screenshot Failed {e.Message}");
            }
        }
    }
}
