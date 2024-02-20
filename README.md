# eCommerce Automated Tests
### User end-to-end tests written in C#, with the use of .NET Core Framework, NUnit and Selenium WebDriver
## Overview
###### This project aims to develop two end-to-end tests using WebDriver for an e-commerce website. The tests will simulate user interactions such as purchasing items, applying discounts, and verifying order details.

## Test Case 1:
* Login to the e-commerce site as a registered user
* Add an item to the cart
* Apply a discount code ('edgewords') and verify the discount
* Check that the totalafter discount & shipping is correct
* Log out

## Test Case 2:
* Login to the e-commerce site as a registered user
* Add an item to the cart and proceed to checkout
* Complete billing details and select payment method
* Capture the order number and verify its presense in the 'My Orders' section
* Log out

## Pre-requisites
Before running the tests, make sure to:
* Manually register a new account on the [e-commerce site](https://www.edgewordstraining.co.uk/demo-site/my-account/).

## Website
The tests will be conducted on the following e-commerce site:
- [Edgewords Demo Site](https://www.edgewordstraining.co.uk/demo-site/)

## Setup Instructions
1. Clone this repo to your local machine.

2. Install the following packages:
* NUnit (4.0.1)
* NUnit.Analyzers (4.0.1)
* NUnit3TestAdapter (4.5.0)
* Selenium.WebDriver (4.17.0)
* Selenium.Support(4.17.0)

3. Create environment variables:
* USERNAME
* PASSWORD
* BROWSER

###### *USERNAME and PASSWORD must be the login details*
*Specify BROWSER as edge or firefox* (If none specified, default is edge)*

4. Create test run parameters
- name="WebAppURL" value="https://www.edgewordstraining.co.uk/demo-site/my-account/"

5. Run the test with `dotnet test`
