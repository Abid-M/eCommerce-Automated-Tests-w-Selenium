# 🛒 eCommerce Automated Tests w/ Selenium <picture> <source media="(prefers-color-scheme: dark)" src="https://github.com/Abid-M/eCommerce-Automated-Tests-w-Selenium/assets/77882906/16e6064a-3cde-4ee3-8107-60aa59f5dc1f" alt="nfocus_logo" align="right" width="125"> <source media="(prefers-color-scheme: light)" src="https://github.com/Abid-M/eCommerce-Automated-Tests-w-Selenium/assets/77882906/a4f32659-49a5-4fbe-8c49-7b5e55c1212e" alt="nfocus_logo" align="right" width="125"><img alt="nFocus Logo" src="https://github.com/Abid-M/eCommerce-Automated-Tests-w-Selenium/assets/77882906/16e6064a-3cde-4ee3-8107-60aa59f5dc1f" height="25"></picture>

User BDD end-to-end tests written in C#, with the use of .NET Core Framework, NUnit and SpecFlow WebDriver.

![Visual Studio Badge](https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge)
![C# Badge](https://img.shields.io/badge/C%23-512BD4?logo=csharp&logoColor=fff&style=for-the-badge)
![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)
![Selenium Badge](https://img.shields.io/badge/Selenium-43B02A?logo=selenium&logoColor=fff&style=for-the-badge)
![Specflow Badge](https://img.shields.io/badge/Cucumber-23D96C?logo=cucumber&logoColor=fff&style=for-the-badge)
![Git Badge](https://img.shields.io/badge/Git-F05032?logo=git&logoColor=fff&style=for-the-badge)

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
   - `BROWSER`
     - `USERNAME` and `PASSWORD` must be the registered login details
     - Specify `BROWSER` as `edge`, `chrome`, or `firefox` (If none specified, default is `edge`)
       
4. Create test run parameter:
   - `name="WebAppURL" value="https://www.edgewordstraining.co.uk/demo-site/my-account/"`
   		- For Example, can create a `settings.runsettings` file:
   		- Configure the runsettings, so it is detected and selected for the project, or able to run the test with `dotnet test --settings "settings.runsettings"`
```xml
<?xml version="1.0" encoding="utf-8" ?>
<RunSettings>
	<!-- config elements -->
	<RunConfiguration>
		<EnvironmentVariables>
			<BROWSER>edge</BROWSER>
			<USERNAME>test.email@nfocus.co.uk</USERNAME>
			<PASSWORD>Forgotmypass!</PASSWORD>
		</EnvironmentVariables>
	</RunConfiguration>
	<TestRunParameters>
		<!-- NUnit config params, only tests have access to, describes tests -->
		<Parameter name="WebAppURL" value="https://www.edgewordstraining.co.uk/demo-site/my-account/" />
	</TestRunParameters>
</RunSettings>
```
     
5. Run the test with `dotnet test` or via the VS Test Explorer
