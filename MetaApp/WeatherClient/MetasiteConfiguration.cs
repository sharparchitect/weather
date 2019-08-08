namespace MetaApp.WeatherClient
{
    //TODO: move the config values into the appsettings.config. Need to figure out how to call it in Program.cs for a console app
    public class MetasiteConfiguration
    {
        public string Domain { get; set; } = "https://metasite-weather-api.herokuapp.com/api/";
        public string UserName { get; set; } = "meta";
        public string Password { get; set; } = "site;";

        public string AuthorizationMethod { get; set; } = "authorize";

        public string CitiesMethod { get; set; } = "Cities";

        public string WeatherMethod { get; set; } = "Weather";
    }
}
