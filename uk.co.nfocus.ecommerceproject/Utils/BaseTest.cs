using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class BaseTest
    {
        //Field to hold a WebDriver that is in scope for all methods in this class
        protected IWebDriver driver; //ChromeDrivers/EdgeDrivers/FirefoxDrivers are all IWebDrivers

        [SetUp] //Runs before each and every [Test] in this class
        public void Setup()
        {
            string browser = Environment.GetEnvironmentVariable("BROWSER"); //Change me to instantiate different browsers

            //Instantiate a browser based on variable
            switch (browser)
            {
                case "edge":
                    driver = new EdgeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Url = TestContext.Parameters["WebAppURL"];
        }

        [TearDown]
        public void Teardown()
        {
            //Determining in teardown test result
            //https://docs.nunit.org/articles/nunit/writing-tests/TestContext.html#result
            if (TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Passed")
            {
                Console.WriteLine("Test passed and Completed");
                //Might be better to take a screenshot on fail
            }

            //Except for passing Assertions - results during execution are logged and can be read back in TearDown
            var assertionResults = TestContext.CurrentContext.Result.Assertions;
            foreach (var assertionResult in assertionResults)
            {
                Console.WriteLine("Status: " + assertionResult.Status.ToString());
                Console.WriteLine("Message: " + assertionResult.Message.ToString());
            }

            //Logout
            StaticWaitForElement(driver, By.LinkText("My account")).Click(); 
            driver.FindElement(By.LinkText("Logout")).Click();
            Console.WriteLine("Completed Logout Process");

            driver.Quit(); //Quits browser and DriverServer and disposes of objects (although NUnit Analyser does not know this without further config)
        }
    }
}

