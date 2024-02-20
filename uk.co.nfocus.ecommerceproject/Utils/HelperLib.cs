using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class HelperLib
    {
        public static IWebElement StaticWaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            myWait.Until(drv => drv.FindElement(locator).Displayed);
            return driver.FindElement(locator);
        }

        public static void ScrollElIntoView (IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor? jsdriver = driver as IJavaScriptExecutor;
            jsdriver?.ExecuteScript("arguments[0].scrollIntoView()", element);
        }

        public static void TakeScreenshot(IWebDriver driver, By locator, string el)
        {
            IWebElement element = driver.FindElement(locator);
            ScrollElIntoView(driver, element);

            var ssElm = element as ITakesScreenshot;
            Screenshot screenshotElm = ssElm.GetScreenshot();

            string date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            //Saves in bin->Debug>net6.0->screenshots. From net6.0 back to project files
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\screenshots\"));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\screenshots\", $"{el}_{date}.png");

            //screenshotElm.SaveAsFile(@$"C:\Users\AbidMiah\OneDrive - nFocus Limited\Documents\screenshots\{el}_{date}.png");
            screenshotElm.SaveAsFile(filePath);
            TestContext.WriteLine($"Attaching '{el}' screenshot to report");
            TestContext.AddTestAttachment(filePath, $"{el} png");
            //TestContext.AddTestAttachment(@$"C:\Users\AbidMiah\OneDrive - nFocus Limited\Documents\screenshots\{el}_{date}.png", $"{el} png");
        }


        public static void DismissBanner(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.LinkText("Dismiss")).Click();
            } catch (Exception e)
            {
                //No Blue Banner shown
            }
        }


    }
}
