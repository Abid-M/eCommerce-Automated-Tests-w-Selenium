Feature: GoogleSearch
In order to get more business
Tom (CEO) wants
us to appear high in testing search results
 
Scenario: Search Google for Edgewords
	Given I am on the Google homepage
	When I search for 'Edgewords'
	Then 'Edgewords' is the top result

Scenario Outline: Search Google for stuff
	Given I am on the Google homepage
	When I search for '<searchTerm>'
	Then '<expectedSearchResult>' is the top result

	Examples:
		| searchTerm | expectedSearchResult |
		| edgewords  | edgewords            |
		| BBC        | BBC                  |
		| News       | BBC                  |

Scenario: Verify edgewords title and description in google search
	Given I am on the Google homepage
	When I search for 'Edgewords'
	Then I should see in results
		| url                                 | title                                                    |
		| https://www.edgewordstraining.co.uk | Edgewords Training - Automated Software Testing Training |
		| https://github.com > edgewords      | Edgewords Training edgewords                             |
 #Belongs to a particular step, passes as an object table

