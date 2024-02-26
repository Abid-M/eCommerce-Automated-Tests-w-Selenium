/* Author: Abid Miah */
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;
using uk.co.nfocus.ecommerceproject.POMClasses;

namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class BaseTest
    {
        //Field to hold a WebDriver that is in scope for all methods in this class
        protected IWebDriver driver; 

        [SetUp] //Runs before each and every [Test] in this class
        public void Setup()
        {
            string? browser = Environment.GetEnvironmentVariable("BROWSER"); 

            //Instantiate a browser based on environment variable
            switch (browser)
            {
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.BrowserVersion = "canary";
                    driver = new ChromeDriver(options);
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chromeheadless":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--headless"); 
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefoxheadless":
                    FirefoxOptions firefoxoptions = new FirefoxOptions();
                    firefoxoptions.AddArgument("--headless");
                    driver = new FirefoxDriver(firefoxoptions);
                    break;
                default:
                    driver = new EdgeDriver();
                    break;
            }

            driver.Manage().Window.Maximize(); //Maximize window to full screen
            driver.Url = TestContext.Parameters["WebAppURL"]; //Direct to URL in test parameters, not env variable as not a secret
            
            DismissBanner(driver); //Dismisses the blue dialog

            //Initial Login
            LoginPOM login = new LoginPOM(driver);

            bool loggedIn = login.ValidLogin(Environment.GetEnvironmentVariable("USERNAME"), Environment.GetEnvironmentVariable("PASSWORD"));
            Assert.That(loggedIn, "We did not login");

            //Empty Cart for default state
            new NavPOM(driver).GoToCart();
            new CartPOM(driver).EmptyCart();
        }

        [TearDown]
        public void Teardown()
        {
            //Determining in teardown test result
            //https://docs.nunit.org/articles/nunit/writing-tests/TestContext.html#result
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
                if (driver.Url.Contains("my-account"))
                {
                    new MyAccountPOM(driver).Logout();
                } 
                else
                {
                    new NavPOM(driver).GoToAccount();
                    new MyAccountPOM(driver).Logout();
                }

                Console.WriteLine("Successfully Logged Out");

            } 
            catch (Exception e)
            {
                Console.WriteLine($"Logout Failed {e.Message}");
            }

            driver.Close();
            driver.Quit(); 
        }
    }
}

