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
		And I apply the coupon code '<coupon>' to the cart
	Then I recieve <discount>% discount off my total, excluding shipping

Examples:
	| item       | coupon    | discount |
	| Beanie     | nfocus    | 25       |
	| Polo       | edgewords | 15       |
	| Sunglasses | invalid   | 10       | 
	# Sunglasses as negative testing


@TestCase2_Checkout
Scenario Outline: Order checkout process, and verify in order history
	Given that the cart contains '<item>'
	When I proceed to checkout
		And I provide the billing details:
		| FName | LName | Address      | City   | Postcode | Phone		  | Email                   |
		| Abid  | Miah  | 17 Sewi Lane | London | SW19 2JY | 07365827365  | test.email@nfocus.co.uk |
		And I place the order with '<payment method>' payment
	Then the order should appear in my accounts order history

Examples:
	| payment method | item	  |
	| Check          | Hoodie |
	| Cash           | Cap	  |
