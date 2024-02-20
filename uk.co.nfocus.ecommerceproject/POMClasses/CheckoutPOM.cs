using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class CheckoutPOM
    {
        //Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;

        //Constructor to get the driver from the test
        public CheckoutPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        private IWebElement _fNameField => _driver.FindElement(By.Id("billing_first_name"));
        private IWebElement _lNameField => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement _streetAddressField => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.Id("billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement _phoneField => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement _emailField => _driver.FindElement(By.Id("billing_email"));
        private IWebElement _placeOrderButton => StaticWaitForElement(_driver, By.Id("place_order"));


        private void SetFirstName(string firstName)
        {
            _fNameField.Clear();
            _fNameField.SendKeys(firstName);
        }
        private void SetLastName(string lastName)
        {
            _lNameField.Clear();
            _lNameField.SendKeys(lastName);
        }
        private void SetAddress(string address)
        {
            _streetAddressField.Clear();
            _streetAddressField.SendKeys(address);
        }
        private void SetCity(string city)
        {
            _cityField.Clear();
            _cityField.SendKeys(city);
        }
        private void SetPostcode(string postcode)
        {
            _postcodeField.Clear();
            _postcodeField.SendKeys(postcode);
        }
        private void SetPhone(string phone)
        {
            _phoneField.Clear();
            _phoneField.SendKeys(phone);
        }
        private void SetEmail(string email)
        {
            _emailField.Clear();
            _emailField.SendKeys(email);
        }
    }
}
