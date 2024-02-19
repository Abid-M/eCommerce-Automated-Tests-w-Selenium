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

        public static void TakeScreenshot(IWebDriver driver, IWebElement element, string el)
        {
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

        public static void Login(IWebDriver driver)
        {
            driver.FindElement(By.Id("username")).SendKeys(Environment.GetEnvironmentVariable("USERNAME"));
            driver.FindElement(By.Id("password")).SendKeys(Environment.GetEnvironmentVariable("PASSWORD"));
            driver.FindElement(By.Name("login")).Click();

            driver.FindElement(By.LinkText("Dismiss")).Click(); //Dismisses the bottom blue banner

            Console.WriteLine("Completed Login Process");
        }

        public static decimal StringToDecimal(IWebDriver driver, By locator)
        {
            string strValue = driver.FindElement(locator).Text;
            return decimal.Parse(strValue, NumberStyles.Currency);
        }
    }
}
