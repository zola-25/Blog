Feature: Search
Background: 
	Given I am navigated to http://localhost:57243

@UsingChrome
Scenario: Search for an article that exists
	Given I have entered New Blog Site into the search box
	When I click search submit
	Then The h3 text should equal Search Results
	And The search result h4 text should contain New Blog Site

@UsingFirefox
Scenario: Search for an article that doesn't exist
	Given I have entered Text with no article into the search box
	When I click search submit
	Then The h3 text should equal No results available for your search
	