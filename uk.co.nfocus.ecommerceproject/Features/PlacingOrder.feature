Feature: PlacingOrder

A pre-registered user can access the ecommerce website via login, 
select a clothing item, apply a coupon to receive a discount on the order before checkout.

Background:
	Given I am on the eCommerce Website
		And I am logged in as a registered user


@TestCase1_Coupon
Scenario Outline: Applying a discount to the shopping cart
	When I add an '<item>' into my cart
		And I apply the coupon code 'edgewords' to the cart
	Then I should recieve '15'% off my total, excluding shipping

Examples:
	| item       |
	| Beanie     |
	| Belt       |
	| Polo       |
	| Sunglasses |


@TestCase2_Checkout
Scenario: Order checkout process, and verify in order history
	Given I am on the cart page
		And the cart contains 'Beanie'
	When I provide the billing details:
	| first name | last name | address     | city   | postcode | phone number | email                   |
	| Abid       | Miah      | 17 Sui Lane | London | SW19 2JY | 07365827365  | test.email@nfocus.co.uk |
		And I place the order
	Then the order should appear in my accounts order history



# When I add an '<item>' into my cart
# And I apply the coupon code 'edgewords' to the cart
# Then I should recieve '15'% off my total excluding shipping

#Examples:

# Given I am on the cart page
# And the cart contains 'beanie'
# When I provide billing details:
	| first name |
# And place the order
# Then the order should appear in my accounts order history
