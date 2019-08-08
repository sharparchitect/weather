using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MetaApp.Services
{
    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly CityList _cities;

        private readonly ILogger _logger;

        public ScopedProcessingService(CityList cities, ILogger<ScopedProcessingService> logger)
        {
            _cities = cities;
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.LogInformation("Weather Processing Service is working.");

            string data = JsonConvert.SerializeObject(_cities);
            _logger.LogInformation(data);
        }
    }
}
