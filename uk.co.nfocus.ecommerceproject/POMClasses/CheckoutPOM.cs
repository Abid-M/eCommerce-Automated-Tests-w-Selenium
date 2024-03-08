/* Author: Abid Miah */
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus.ecommerceproject.Utils;
using static uk.co.nfocus.ecommerceproject.Utils.HelperLib;

namespace uk.co.nfocus.ecommerceproject.POMClasses
{
    internal class CheckoutPOM
    {
        // Field that will hold a driver for Service Methods in this test to work with
        private IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper; // Shows Test Output in LivingDoc HTML Report, rather than CWs


        /* Constructor to get the driver from the test */
        public CheckoutPOM(IWebDriver driver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._driver = driver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        /* Locators */
        private IWebElement _fNameField => WaitForElement(_driver, By.Id("billing_first_name"));
        private IWebElement _lNameField => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement _streetAddressField => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.Id("billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement _phoneField => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement _emailField => _driver.FindElement(By.Id("billing_email"));
        private IWebElement _chequePaymentButton => WaitForElement(_driver, By.CssSelector("label[for='payment_method_cheque']"), 1);
        private IWebElement _cashPaymentButton => WaitForElement(_driver, By.CssSelector("label[for='payment_method_cod']"), 1);
        private IWebElement _placeOrderButton => WaitForElement(_driver, By.Id("place_order"), 1);

        /* Clears and sets the value of the fields. */
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

        /* FillInBillingDetails(Customer)
         - Fills in the billing details using the provided Customer object.
         - Sets the first name, last name, address, city, postcode, phone, and email. 
        */
        public void FillInBillingDetails(Customer customer)
        {
            SetFirstName(customer._fName);
            SetLastName(customer._lName);
            SetAddress(customer._address);
            SetCity(customer._city);
            SetPostcode(customer._postcode);
            SetPhone(customer._phone);
            SetEmail(customer._email);

            _specFlowOutputHelper.WriteLine("Billing Details Populated..");
        }

        /* Validates whether the billing details within the input fields match those provided in the Customer object. */
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

        /* Selects the payment method for checkout (check or cash). */
        public CheckoutPOM SelectPayment(string paymentMethod)
        {

            if (paymentMethod.ToLower().Equals("cash"))
            {
                //Button might not be immediately available or might be stale, so we try a few times.
                while (true)
                {
                    try
                    {
                        // If the button is found and clickable, this will succeed and exit the loop.
                        _cashPaymentButton.Click();
                        _specFlowOutputHelper.WriteLine("Cash Payment Selected");
                        break;
                    }
                    catch
                    {
                        // Try again
                    }
                }
            }
            // Default option is paying by cheque
            else
            {
                // Button might not be immediately available or might be stale, so we try a few times.
                while (true)
                {
                    try
                    {
                        // If the button is found and clickable, this will succeed and exit the loop.
                        _chequePaymentButton.Click();
                        _specFlowOutputHelper.WriteLine("Cheque Payment Selected");
                        break;
                    }
                    catch
                    {
                        // Try again
                    }
                }
            }

            return this;
        }

        /* PlaceOrder()
         - Places the order by clicking on the place order button.
         - Waits for the order URL to appear after clicking the button. 
        */
        public void PlaceOrder()
        {
            //Button might not be immediately available or might be stale, so we try a few times.
            while(true)
            {
                try
                {
                    // If the button is found and clickable with order url, this will succeed and exit the loop.
                    _placeOrderButton.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(3)).Until(drv => drv.Url.Contains("order"));
                    _specFlowOutputHelper.WriteLine("Order Placed..");
                    break;
                }
                catch
                {
                    //Try again
                }
            }
        }

        /* Creates a Customer object using the information provided from the feature table. */
        public Customer CreateCustomer(Table CustomerInfo)
        {
            var customerRow = CustomerInfo.Rows[0]; // Only one row in table

            string fName = customerRow["first name"];
            string lName = customerRow["last name"];
            string address = customerRow["address"];
            string city = customerRow["city"];
            string postcode = customerRow["postcode"];
            string phoneNumber = customerRow["phone number"];
            string email = customerRow["email"];

            return new Customer(fName, lName, address, city, postcode, phoneNumber, email);
        }
    }
}
