# RestCountries.QA
API Tests for https://restcountries.eu/

# Precondition
Ensure you have Visual Studio 2017 with extension "SpecFlow for VisualStudio 2017"

# Used technologies
- C# - as a programming language
- MsTest test framework - to run tests
- SpecFlow test framework - to write scenarios in readable format
- RestSharp - to work with requests / responses
- FluentAssertions - to make flexible assertions

# How to run tests
 - Open the solution in Visual Studio 2017 
 - Open menu Test>Windows>Test Explorer
 - Restore NuGet packages (SpecFlow, RestSharp, FluentAssertions)
 - Build the solution
 - In the Test Explorer, run the tests

# Expected result
All tests should run and pass
