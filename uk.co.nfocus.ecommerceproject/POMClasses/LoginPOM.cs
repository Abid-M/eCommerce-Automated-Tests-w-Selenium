using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class LoginPOM
    {
        private IWebDriver _driver;

        public LoginPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        private IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));

        // Clears and sets the value of the username field.
        public LoginPOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);

            return this;
        }

        // Clears and sets the value of the password field.
        public LoginPOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);

            return this;
        }

        // Clicks the login button.
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
                // Checks if error banner is displayed, Login Failed
                _driver.FindElement(By.ClassName("woocommerce-error"));
                return false;
            }
            catch
            {
                Console.WriteLine("Successfully Logged In");
                return true; //Login success
            }
        }
    }
}

