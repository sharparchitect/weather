using MetaApp.WeatherClient;

namespace MetaApp.Data
{
    public class WeatherFormat
    {
        private readonly CityWeather _weather;

        public WeatherFormat(CityWeather weather)
        {
            _weather = weather;
        }

        public static string NoWeather(string city)
        {
            return $"No weather is available for the city: {city}";
        }

        public string DisplayWeather()
        {
            return $"{nameof(_weather.City)}: {_weather.City}, {nameof(_weather.Precipitation)}: {_weather.Precipitation}," +
                   $" {nameof(_weather.Temperature)}: {_weather.Temperature}, {nameof(_weather.Weather)}: {_weather.Weather}";
        }
    }
}
