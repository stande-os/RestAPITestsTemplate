@api
Feature: Search for countries
	In order to get information about countries
	As a user
	I want to find the information using different search criteria


Scenario: Search all countries
	When I search for all countries
	Then the number of countries should be: 250
	
Scenario Outline: Search by valid country name
	When I search by country name: <Name>
	Then the Status Code of the response should be: <StatusCode>
	And the number of countries should be: <NumberOfCountries>
	And the all the countries names or native names or alternative names should contain: <Name>

Examples: 
| Name   | StatusCode | NumberOfCountries |
| united | OK         | 6                 |
| eesti  | OK         | 1                 |


Scenario Outline: Search by invalid country name 
	When I search by country name: <Name>
	Then the Status Code of the response should be: <StatusCode>
	
Examples: 
| Name    | StatusCode |
| Unknown | NotFound   |

 
Scenario Outline: Search by country full name
	When I search by country full name: <FullName>
	Then the Status Code of the response should be: <StatusCode>
	And the number of countries should be: <NumberOfCountries>
	
Examples: 
| FullName      | StatusCode | NumberOfCountries |
| France        | OK         | 1                 |
| Great Britain | OK         | 1                 |


Scenario Outline: Search by list of codes
	When I search by country list of codes: <ListOfCodes>
	Then the Status Code of the response should be: <StatusCode>
	And the number of countries should be: <NumberOfCountries>
	And the countries names should be: <CountriesNames>

Examples: 
| ListOfCodes | StatusCode | NumberOfCountries | CountriesNames          |
| col;no;ee   | OK         | 3                 | Colombia;Norway;Estonia |