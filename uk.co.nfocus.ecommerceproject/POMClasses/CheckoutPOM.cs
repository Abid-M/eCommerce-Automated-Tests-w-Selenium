/* Author: Abid Miah */
using OpenQA.Selenium;
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
            SetFirstName(customer.FirstName);
            SetLastName(customer.LastName);
            SetAddress(customer.Address);
            SetCity(customer.City);
            SetPostcode(customer.Postcode);
            SetPhone(customer.PhoneNumber);
            SetEmail(customer.Email);

            _specFlowOutputHelper.WriteLine("Billing Details Populated..");
        }

        /* Validates whether the billing details within the input fields match those provided in the Customer object. */
        public string ValidateDetails(Customer customer)
        {
            List<string> mismatch = new List<string>();

            if (customer.FirstName != _fNameField.GetAttribute("value"))
                mismatch.Add($"FirstName field with {_fNameField.GetAttribute("value")}");

            if (customer.LastName != _lNameField.GetAttribute("value"))
                mismatch.Add($"LastName field with {_lNameField.GetAttribute("value")}");

            if (customer.Address != _streetAddressField.GetAttribute("value"))
                mismatch.Add($"Address field with {_streetAddressField.GetAttribute("value")}");

            if (customer.City != _cityField.GetAttribute("value"))
                mismatch.Add($"City field with {_cityField.GetAttribute("value")}");

            if (customer.Postcode != _postcodeField.GetAttribute("value"))
                mismatch.Add($"Postcode field with {_postcodeField.GetAttribute("value")}");

            if (customer.PhoneNumber != _phoneField.GetAttribute("value"))
                mismatch.Add($"Phone field with {_phoneField.GetAttribute("value")}");

            if (customer.Email != _emailField.GetAttribute("value"))
                mismatch.Add($"Email field with {_emailField.GetAttribute("value")}");

            // Join all invalid inputs into a string, comma seperation
            return string.Join(", ", mismatch);
        }

        /* Selects the payment method for checkout (check or cash). */
        public CheckoutPOM SelectPayment(string paymentMethod)
        {
            int timeoutMinutes = 1; // Fail-safe timeout of 1 minute
            DateTime startTime = DateTime.Now;

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
                    catch {};


                    // Fail-safe timeout of 1 minute so doesn't loop forever
                    // Over a minute to clear cart? There's a problem!
                    if (ExitLoopTimeout(startTime, timeoutMinutes))
                    {
                        _specFlowOutputHelper.WriteLine("Timeout Reached. Exiting Loop..\nCash Payment NOT Selected");
                        _specFlowOutputHelper.WriteLine("Cheque Payment Selected as DEFAULT");


                        break; // Exits loop/method
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
                    catch {};

                    // Fail-safe timeout of 1 minute so doesn't loop forever
                    // Over a minute to clear cart? There's a problem!
                    if (ExitLoopTimeout(startTime, timeoutMinutes))
                    {
                        _specFlowOutputHelper.WriteLine("Timeout Reached. Exiting Loop..");
                        _specFlowOutputHelper.WriteLine("Cheque Payment Selected as DEFAULT");

                        break; // Exits loop/method
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
            int timeoutMinutes = 1; // Fail-safe timeout of 1 minute
            DateTime startTime = DateTime.Now;

            // Button might not be immediately available or might be stale, so we try a few times.
            while (true)
            {
                try
                {
                    // If the button is found and clickable with order url, this will succeed and exit the loop.
                    _placeOrderButton.Click();

                    WaitToNavigate(_driver, "order");
                    _specFlowOutputHelper.WriteLine("Order Placed..");

                    break;
                }
                catch {};

                // Fail-safe timeout of 1 minute so doesn't loop forever
                // Over a minute to clear cart? There's a problem!
                if (ExitLoopTimeout(startTime, timeoutMinutes))
                {
                    _specFlowOutputHelper.WriteLine("Timeout Reached. Exiting Loop..");
                    Assert.Fail("Placing Order Failed!");

                    break; // Exits loop/method
                }
            }
        }
    }
}
