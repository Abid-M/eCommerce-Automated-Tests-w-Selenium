/* Author: Abid Miah */
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;

namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class HelperLib
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs
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

        /* Waits until the url contains the location. */
        public static void WaitToNavigate(IWebDriver driver, string location)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.Url.Contains(location));
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
        public void TakeScreenshot(IWebDriver driver, string ssName, IWebElement? element = null)
        {
            try
            {
                // Create the directory for the screenshots if it doesn't exist
                // Current dir: in bin->Debug>net6.0->screenshots. From net6.0 back to project files
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Screenshots\"));

                // Get the current date and time
                string date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

                // Taking screenshot on Test Fails!
                if (element == null)
                {
                    // Take the screenshot
                    var ssDriver = driver as ITakesScreenshot;
                    Screenshot failedScreenshot = ssDriver!.GetScreenshot();

                    // Create the file path for the screenshot and save
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Screenshots\", $"FAILED_{ssName}_{date}.png");
                    // Save the screenshot to the file
                    failedScreenshot.SaveAsFile(filePath);

                    // Test output with screenshot as test attachment
                    _specFlowOutputHelper.WriteLine($"Attaching failed '{ssName}' screenshot to report");
                    _specFlowOutputHelper.AddAttachment(filePath);
                }

                // Normal Reporting Screenshots
                else
                {
                    // Scroll the element into view
                    ScrollElIntoView(driver, element);

                    var ssElm = element as ITakesScreenshot;
                    Screenshot screenshotElm = ssElm!.GetScreenshot();

                    // Create the file path for the screenshot
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Screenshots\", $"{ssName}_{date}.png");
                    // Save the screenshot to the file
                    screenshotElm.SaveAsFile(filePath);

                    // Test output with screenshot as test attachment
                    _specFlowOutputHelper.WriteLine($"Attaching '{ssName}' screenshot to report");
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
