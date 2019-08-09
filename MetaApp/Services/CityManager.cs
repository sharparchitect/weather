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

        public CityList Parse()
        {
            CityList result = new CityList(_args.Length - 2);
            for (int i = 2; i < _args.Length; i++)
            {
                string city = _args[i];
                if (city[city.Length - 1] == ',')
                    city = city.Substring(0, city.Length - 1);

                result.Add(city);
            }

            return result;
        }
    }
}
