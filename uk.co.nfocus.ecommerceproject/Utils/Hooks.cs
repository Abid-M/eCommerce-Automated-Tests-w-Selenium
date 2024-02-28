/* Author: Abid Miah */
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using uk.co.nfocus.ecommerceproject.POMClasses;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)] //Can only parallelise Features
[assembly: LevelOfParallelism(4)] //Worker thread i.e. max amount of Features to run in Parallel

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

        [Before]
        public void SetUp()
        {
            string? browser = Environment.GetEnvironmentVariable("BROWSER");

            if (browser == null)
            {
                _specFlowOutputHelper.WriteLine("BROWSER env not set: Setting to Edge..");
                browser = "edge";
            }

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
                    _driver = new EdgeDriver();
                    break;
            }

            _scenarioContext["myDriver"] = _driver; //putting driver into the sContext box and give label of myDriver
            _driver.Manage().Window.Maximize(); //Maximize window to full screen
        }

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
