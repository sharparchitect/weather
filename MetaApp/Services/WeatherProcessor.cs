using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MetaApp.Data;
using MetaApp.WeatherClient;
using Microsoft.Extensions.Logging;

namespace MetaApp.Services
{
    public class WeatherProcessor : IWeatherProcessor
    {
        private readonly CityList _cities;
        private readonly IMetasiteWeatherAPIv1Client _service;
        private readonly MetasiteConfiguration _config;

        private readonly ILogger _logger;

        public WeatherProcessor(CityList cities, IHttpClientFactory clientFactory, ILogger<WeatherProcessor> logger)
        {
            _cities = cities;
            _config = new MetasiteConfiguration();

            HttpClient client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(_config.Domain);

            _service = new MetasiteWeatherAPIv1Client(client);

            _logger = logger;
        }

        public async Task Process()
        {
            _logger.LogInformation("Weather Processing Service is working.");

            var response = await _service.AuthorizeAsync(new AuthorizationRequest() {Username = _config.UserName, Password = _config.Password});
            if (response == null)
            {
                _logger.LogError("No authorize response");
                return;
            }

            string authorization = $"bearer {response.Bearer}";

            ICollection<string> availableCities = await _service.GetCitiesAsync(authorization);

            /* a single-threaded solution
             foreach (string city in _cities)
            {
                await ProcessCity(availableCities, city, authorization);
            }*/

            // a multi-threaded solution
            Parallel.ForEach(_cities, async (city) => await ProcessCity(availableCities, city, authorization) );
        }

        private async Task ProcessCity(ICollection<string> availableCities, string city, string authorization)
        {
            _logger.LogInformation($"Processing a city: {city}");

            if (availableCities.Contains(city))
            {
                CityWeather weatherResponse = await _service.GetWeatherForCityAsync(city, authorization);
                string text = new WeatherFormat(weatherResponse).DisplayWeather();
                _logger.LogInformation(text);
            }
            else
            {
                string text = WeatherFormat.NoWeather(city);
                _logger.LogInformation(text);
            }
        }
    }
}
