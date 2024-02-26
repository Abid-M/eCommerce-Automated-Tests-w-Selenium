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

            if (browser == null)
            {
                Console.WriteLine("BROWSER env not set: Setting to Edge..");
                browser = "edge";
            }

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

            //Instantiate a browser based on environment variable
            driver.Manage().Window.Maximize(); //Maximize window to full screen
            driver.Url = TestContext.Parameters["WebAppURL"]; //Direct to URL in test parameters, not env variable as not a secret

            new NavPOM(driver).DismissBanner();

            //Initial Login
            LoginPOM login = new LoginPOM(driver);

            bool loggedIn = login.ValidLogin(Environment.GetEnvironmentVariable("USERNAME"), Environment.GetEnvironmentVariable("PASSWORD"));
            Assert.That(loggedIn, "Login Failed!");

            //Empty Cart for default state
            new NavPOM(driver).GoToCart();
            new CartPOM(driver).EmptyCart();
        }

        [TearDown]
        public void Teardown()
        {
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

