using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.ecommerceproject.Utils;
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
        private IWebElement _chequePaymentButton => StaticWaitForElement(_driver, By.CssSelector("li.wc_payment_method.payment_method_cheque > label"), 1);
        private IWebElement _placeOrderButton => StaticWaitForElement(_driver, By.Id("place_order"), 1);

        // Clears and sets the value of the fields.
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

        public void FillInBillingDetails(Customer customer)
        {
            SetFirstName(customer._fName);
            SetLastName(customer._lName);
            SetAddress(customer._address);
            SetCity(customer._city);
            SetPostcode(customer._postcode);
            SetPhone(customer._phone);
            SetEmail(customer._email);

        }

        public bool ValidateDetails(Customer customer)
        {
            if (customer._fName == _fNameField.GetAttribute("value") &&
                customer._lName == _lNameField.GetAttribute("value") &&
                customer._address == _streetAddressField.GetAttribute("value") &&
                customer._city == _cityField.GetAttribute("value") &&
                customer._postcode == _postcodeField.GetAttribute("value") &&
                customer._phone == _phoneField.GetAttribute("value") &&
                customer._email == _emailField.GetAttribute("value")
                )
            {
                return true;
            }

            else
            {
                return false; //Failed
            }
        }

        public CheckoutPOM SelectChequePayment()
        {
            //For loop with a maximum of 10 iterations to try clicking the cheque payment button.
            //Button might not be immediately available or might be stale, so we try a few times.
            for (int i = 0; i < 25; i++)
            {
                try
                {
                    // If the button is found and clickable, this will succeed and exit the loop.
                    _chequePaymentButton.Click();
                    break;
                }
                catch
                {
                    //Try again
                }
            }

            return this;
        }

        public void PlaceOrder()
        {
            //For loop with a maximum of 10 iterations to try clicking the cheque payment button.
            //Button might not be immediately available or might be stale, so we try a few times.
            for (int i = 0; i < 25; i++)
            {
                try
                {
                    // If the button is found and clickable with order url, this will succeed and exit the loop.
                    _placeOrderButton.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(3)).Until(drv => drv.Url.Contains("order"));
                    break;
                }
                catch
                {
                    //Try again
                }
            }
        }
    }
}
