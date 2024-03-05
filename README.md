# 🛒 eCommerce Automated Tests
User BDD end-to-end tests written in C#, with the use of .NET Core Framework, NUnit and SpecFlow WebDriver

## Overview
This project develops end-to-end tests in a Behavior-Driven Development (BDD) style using SpecFlow for an e-commerce website. The tests simulate user interactions like purchasing items, applying discounts, and verifying order details.

### Test Cases:
#### Test Case 1:
- Login to the e-commerce site as a registered user
- Add an item to the cart
- Apply a discount code 'edgewords' and verify the discount
- Verify that the total after discount & shipping is correct
- Log out

#### Test Case 2:
- Login to the e-commerce site as a registered user
- Add an item to the cart and proceed to checkout
- Complete billing details and select payment method
- Capture the order number and verify its presence in the 'My Orders' section
- Log out

## Pre-requisites
Before running the tests, make sure to manually register a new account on the [e-commerce site](https://www.edgewordstraining.co.uk/demo-site/my-account/).

## Website
The tests will be conducted on the following e-commerce site:
- [Edgewords Demo Site](https://www.edgewordstraining.co.uk/demo-site/)

## Setup Instructions
1. Clone this repo to your local machine.
```
git clone https://github.com/Abid-M/uk.co.nfocus.ecommerceproject.git
```
   
2. Install the following packages:
   - NUnit (4.0.1)
   - NUnit.Analyzers (4.0.1)
   - NUnit3TestAdapter (4.5.0)
   - Selenium.WebDriver (4.17.0)
   - Selenium.Support(4.17.0)
   - SpecFlow.NUnit (3.9.74)
   - SpecFlow.Plus.LivingDocPlugin (3.9.57)
     
3. Create 3 environment variables:
   - `USERNAME`
   - `PASSWORD`
     - `USERNAME` and `PASSWORD` must be the registered login details
   - `BROWSER`
     - Specify `BROWSER` as `edge`, `chrome`, or `firefox` (If none specified, default is `edge`)
       
4. Create test run parameter:
   - `name="WebAppURL" value="https://www.edgewordstraining.co.uk/demo-site/my-account/"`
     
5. Run the test with `dotnet test` or via the VS Test Explorer
