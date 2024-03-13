/* Author: Abid Miah */
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using uk.co.nfocus.ecommerceproject.POMClasses;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)] // Can only parallelise Features
[assembly: LevelOfParallelism(4)] // Worker thread i.e. max amount of Features to run in Parallel

namespace uk.co.nfocus.ecommerceproject.Utils
{
    [Binding]
    public class Hooks
    {
        private IWebDriver? _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public Hooks(ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        /* SetUp()
        - Sets up the WebDriver instance based on the environment variable "BROWSER".
        - If the "BROWSER" environment variable is not set, defaults to Edge browser.
        - Supported browsers include Chrome, Firefox, and their respective headless versions.
        - This method runs once before each scenario feature. 
        */
        [Before]
        public void SetUp()
        {
            string? browser = Environment.GetEnvironmentVariable("BROWSER");

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
                case "chromeheadless":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--headless");
                    _driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefoxheadless":
                    FirefoxOptions firefoxoptions = new FirefoxOptions();
                    firefoxoptions.AddArgument("--headless");
                    _driver = new FirefoxDriver(firefoxoptions);
                    break;
                default:
                    _specFlowOutputHelper.WriteLine("BROWSER env not set: Setting to Edge..");
                    _driver = new EdgeDriver();
                    break;
            }

            _scenarioContext["myDriver"] = _driver; // Putting driver into the sContext box and give label of myDriver
            _driver.Manage().Window.Maximize(); // Maximize window to full screen
        }

        /* AfterStep()
        - Takes screenshot if a Test Fails within a step.
        */
        [AfterStep]
        public void AfterStep()
        {
            // Scenario Context is a context object that provides info about the current scenario being executed
            // Test Error property of SC. Holds info about ex or error that occured during exceution of a scenario
            if (_scenarioContext.TestError != null) // not null = error occured
            {
                string errorMessage = _scenarioContext.TestError.Message;

                // Only gets Error Message before Assert word..
                string firstLine = errorMessage.Substring(0, errorMessage.IndexOf("Assert"));
                // Removes leading and trailing white space. 
                string error = firstLine.Trim().Replace(" ", "-");

                new HelperLib(_specFlowOutputHelper).TakeScreenshot(_driver!, error); // Screenshot report
            }
        }

        /* TearDown()
        - Tears down the WebDriver instance after each scenario feature.
        - This method logs out of the application if the current URL contains "my-account".
        - If not, it navigates to the account page and then logs out.
        */
        [After]
        public void TearDown()
        {
            //Logout
            try
            {
                if (_driver!.Url.Contains("my-account"))
                {
                    new MyAccountPOM(_driver, _specFlowOutputHelper).Logout();
                }
                else
                {
                    new NavPOM(_driver, _specFlowOutputHelper).GoToAccount();
                    new MyAccountPOM(_driver, _specFlowOutputHelper).Logout();
                }

                _specFlowOutputHelper.WriteLine("Successfully Logged Out");

            }
            catch (Exception e)
            {
                _specFlowOutputHelper.WriteLine($"Logout Failed: {e.Message}");
            }

            _specFlowOutputHelper.WriteLine("Test Passed & Completed!");
            _driver!.Quit(); // Null forgiving operator (!)
        }

    }
}
