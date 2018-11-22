using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;

namespace RestCountries.QA.Tests.Specs
{
    [Binding]
    public class FindCountriesSteps
    {
        private static readonly string _baseUrl = "https://restcountries.eu/rest/v2";
        private IRestResponse _response;
        private JArray _jsonData;


        [When(@"I search for all countries")]
        public void WhenISearchForAllCountries()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("all");
            _response = client.Execute(request);

            if (_response.StatusCode != HttpStatusCode.OK) return;

            _jsonData = JsonConvert.DeserializeObject<JArray>(_response.Content);
        }

        [When(@"I search by country name: (.*)")]
        public void WhenISearchByCountryName(string name)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest($"name/{name.Trim()}");
            _response = client.Execute(request);
            if (_response.StatusCode != HttpStatusCode.OK) return;

            _jsonData = JsonConvert.DeserializeObject<JArray>(_response.Content);
        }

        [When(@"I search by country full name: (.*)")]
        public void WhenISearchByCountryFullNameFrance(string name)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest($"name/{name.Trim()}");
            request.Parameters.Add(new Parameter("fullText", true, ParameterType.QueryString));
            _response = client.Execute(request);

            if (_response.StatusCode != HttpStatusCode.OK) return;

            _jsonData = JsonConvert.DeserializeObject<JArray>(_response.Content);
        }

        [When(@"I search by country list of codes: (.*)")]
        public void WhenISearchByCountryListOfCodesColNoEe(string codes)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("alpha");
            request.Parameters.Add(new Parameter("codes", codes, ParameterType.QueryString));
            _response = client.Execute(request);

            if (_response.StatusCode != HttpStatusCode.OK) return;

            _jsonData = JsonConvert.DeserializeObject<JArray>(_response.Content);
        }
    


        [Then(@"the Status Code of the response should be: (.*)")]
        public void ThenTheStatusOfTheResponseShouldBe(HttpStatusCode statusCode)
        {
            _response.StatusCode.Should().Be(statusCode);
        }

        [Then(@"the number of countries should be: (.*)")]
        public void ThenTheNumberOfResultsShouldBe(int number)
        {
            _jsonData.Count.Should().Be(number);
        }

        [Then(@"the all the countries names or native names or alternative names should contain: (.*)")]
        public void ThenTheAllTheCountriesNamesShouldContainUnited(string name)
        {
            
            var countriesNames = _jsonData.Select(e => e["name"].ToString().ToLower()).ToList();
            var countriesNativeNames = _jsonData.Select(e => e["nativeName"].ToString().ToLower()).ToList();
            var countriesAltSpellings = _jsonData.Select(e => e["altSpellings"].ToString().ToLower()).ToList();

            for (int index = 0; index < countriesNames.Count; index++)
            {
                (countriesNames[index].Contains(name) 
                 || countriesNativeNames[index].Contains(name) 
                 || countriesAltSpellings[index].Contains(name))
                    .Should().BeTrue();
            }
        }

        [Then(@"the countries names should be: (.*)")]
        public void ThenTheCountriesNamesShouldBeColombiaNorwayEstonia(string names)
        {
            var expectedNames = names.Split(';').ToList();
            var actualNames = _jsonData.Select(e => e["name"].ToString()).ToList();

            actualNames.Should().BeEquivalentTo(expectedNames, options => options.WithStrictOrdering());
        }

    }
}
