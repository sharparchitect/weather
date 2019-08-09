using MetaApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MetaApp
{
    public class Program
    {
        private static readonly string Info = "The format is: metaapp.exe weather --city city1, city2,...,cityn"
                                              + Environment.NewLine + "Example: metaapp.exe weather --city Vilnius, Riga";

        static async Task Main(string[] args)
        {
            var manager = new CityManager(args);

            var validationResult = manager.Validate();
            if (validationResult != null)
            {
                Console.WriteLine(validationResult);
                Console.WriteLine(Info);
                Console.WriteLine("Press any key to Exit...");
                Console.ReadKey();
                return;
            }

            CityList cities = manager.Parse();

            var host = ConfigureHost(cities);

            await host.RunAsync();

            Console.WriteLine("This is .NET test task.");
        }

        private static IHost ConfigureHost(CityList cities)
        {
            return new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<CityList>(cities);

                    services.AddHostedService<TimerHostedService>();

                    services.AddScoped<IWeatherProcessor, WeatherProcessor>();

                    services.AddHttpClient();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .UseConsoleLifetime()
                .Build();
        }
    }
}
