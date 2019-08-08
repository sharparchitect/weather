using System;

namespace MetaApp.Services
{
    public class CityManager
    {
        private readonly string[] _args;

        public CityManager(string[] args)
        {
            _args = args;
        }

        public string Validate()
        {
            if (_args == null || _args.Length == 0)
                return "Parameters cannot be empty.";

            if (_args.Length < 2 || _args[0] != "weather" || _args[1] != "--city")
                return "Wrong parameters.";

            if (_args.Length < 3)
                return "Please provide a city list";

            return null;
        }

        public CityList ParseCities()
        {
            CityList result = new CityList(_args.Length - 2);
            for (int i = 2; i < _args.Length; i++)
            {
                result.Add(_args[i]);
            }

            return result;
        }
    }
}
