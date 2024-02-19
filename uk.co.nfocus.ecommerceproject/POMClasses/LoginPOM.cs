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
            //Assert.That(_driver.FindElement(By.TagName("h1")).Text, Does.Contain("Access and Authentication"), "Must be wrong page");
            //Assert.That(_driver.Url, Does.Contain("whatever"));
            //Assert.That(_driver.title, Is.EqualTo("whatever"));
        }

        //Locators
        private IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        private IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));

        //Service methods
        public LoginPOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);

            return this;
        }
        public LoginPOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);

            return this;
        }

        public void GoLogin()
        {
            _loginButton.Click();
        }

        public bool ValidLogin(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            GoLogin();

            try
            {
                _driver.FindElement(By.ClassName("woocommerce-error"));
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Successfully logged in.");
                return true; //Login success
            }
        }
    }
}

