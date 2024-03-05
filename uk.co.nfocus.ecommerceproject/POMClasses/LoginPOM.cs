/* Author: Abid Miah */
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class LoginPOM
    {
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs

        public LoginPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        // Locators
        private IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        private IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));
        private IWebElement _errorBanner => _driver.FindElement(By.ClassName("woocommerce-error"));

        /* Clears and sets the value of the username field. */
        public LoginPOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);

            return this;
        }

        /* Clears and sets the value of the password field. */
        public LoginPOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);

            return this;
        }

        /* Clicks the login button. */
        public void GoLogin()
        {
            _loginButton.Click();
        }


        /* Attempts to log in with the specified username and password. */
        public bool ValidLogin(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            GoLogin();

            try
            {
                bool displayed = _errorBanner.Displayed;
                return false;
            }
            catch
            {
                _specFlowOutputHelper.WriteLine("Successfully Logged In");
                return true;
            }
        }
    }
}

