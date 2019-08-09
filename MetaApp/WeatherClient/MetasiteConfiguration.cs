namespace MetaApp.WeatherClient
{
    //TODO: move the config values into the appsettings.config. Need to figure out how to call it in Program.cs for a console app
    public class MetasiteConfiguration
    {
        public string Domain { get; set; } = "https://metasite-weather-api.herokuapp.com/";
        public string UserName { get; set; } = "meta";
        public string Password { get; set; } = "site";
    }
}
