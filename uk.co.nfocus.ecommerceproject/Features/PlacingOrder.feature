@GUI
Feature: PlacingOrder

A pre-registered user can access the ecommerce website by logging in, 
select a clothing item, apply a coupon to receive a discount, before checking out and placing the order.

Background:
	Given I am on the eCommerce Website
		And I am logged in as a registered user
		# Omitting actual username and password. Stored as env secrets for security

@TestCase1_Coupon
Scenario Outline: Applying a discount to the shopping cart
	When I add '<item>' into my cart
		And I apply the coupon code 'edgewords' to the cart
	Then I recieve '15'% discount off my total, excluding shipping

Examples:
	| item       |
	| Beanie     |
	| Belt       |
	| Polo       |
	| Sunglasses |


@TestCase2_Checkout
Scenario: Order checkout process, and verify in order history
	Given that the cart contains 'Beanie'
	When I proceed to checkout
		And I provide the billing details:
		| first name | last name | address     | city   | postcode | phone number | email                   |
		| Abid       | Miah      | 17 Sui Lane | London | SW19 2JY | 07365827365  | test.email@nfocus.co.uk |
	When I place the order
	Then the order should appear in my accounts order history
