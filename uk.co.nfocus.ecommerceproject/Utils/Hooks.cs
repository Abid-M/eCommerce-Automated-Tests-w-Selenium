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
            _driver = new EdgeDriver();
            _scenarioContext["myDriver"] = _driver; //putting something into the scontect box and give label of myDriver

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

            _driver.Manage().Window.Maximize(); //Maximize window to full screen

            _scenarioContext["myDriver"] = _driver; //putting driver into the sContext box and give label of myDriver
        }

        [After]
        public void TearDown()
        {
            _driver.Quit();
        }

    }
}
