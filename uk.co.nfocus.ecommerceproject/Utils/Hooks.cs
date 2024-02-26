using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using uk.co.nfocus.ecommerceproject.POMClasses;

[assembly: Parallelizable(ParallelScope.Fixtures)] //Can only parallelise Features
[assembly: LevelOfParallelism(4)] //Worker thread i.e. max amount of Features to run in Parallel

namespace uk.co.nfocus.ecommerceproject.Utils
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Before]
        public void SetUp()
        {
            string? browser = Environment.GetEnvironmentVariable("BROWSER");

            //Instantiate a browser based on environment variable
            switch (browser)
            {
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.BrowserVersion = "canary";
                    _driver = new ChromeDriver(options);
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                default:
                    _driver = new EdgeDriver();
                    break;
            }

            _scenarioContext["myDriver"] = _driver; //putting driver into the sContext box and give label of myDriver
            _driver.Manage().Window.Maximize(); //Maximize window to full screen
        }

        [After]
        public void TearDown()
        {
            //Determining in teardown test result
            if (TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Failed")
            {
                Console.WriteLine("Test Failed: " + TestContext.CurrentContext.Result.Message);
            }
            else
            {
                Console.WriteLine("Test Passed and Completed");
            }

            //Except for passing Assertions - results during execution are logged and can be read back in TearDown
            var assertionResults = TestContext.CurrentContext.Result.Assertions;
            foreach (var assertionResult in assertionResults)
            {
                Console.WriteLine("Status: " + assertionResult.Status.ToString());
                Console.WriteLine("Message: " + assertionResult.Message.ToString());
            }

            //Logout
            try
            {
                if (_driver.Url.Contains("my-account"))
                {
                    new MyAccountPOM(_driver).Logout();
                }
                else
                {
                    new NavPOM(_driver).GoToAccount();
                    new MyAccountPOM(_driver).Logout();
                }

                Console.WriteLine("Successfully Logged Out");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Logout Failed {e.Message}");
            }

            _driver.Quit();
        }

    }
}
