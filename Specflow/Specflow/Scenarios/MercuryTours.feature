Feature: MercuryTours
	
@BookFlight
Scenario Outline: Book a Flight
	Given the user navigates to "http://newtours.demoaut.com/"
	And the user log in with mercury
	And the user is on Home page 
	When the user selects <flight_type> flight
	And the user books a flight
	# Then the user recieves flight confirmation 	
	Examples: 
	| flight_type |
	| one_way     |
	| two_way     |
	

@Login
Scenario: Valid Login Functionality
	Given the user navigates to "http://newtours.demoaut.com/"
	And the user log in with mercury
	And the user is on Home page 

@Login
Scenario: Invalid Login Functionality
	Given the user navigates to "http://newtours.demoaut.com/"
	And the user log in with test
	And the user is on Login page