using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeather.ApiClient.Demo.Extensions;
using OpenWeather.ApiClient.Demo.Models;
using OpenWeather.ApiClient.Demo.Services;
using OpenWeather.ApiClient.Demo.Settings;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddOptions();

// NOTE: Don't forget to obtain Api key and store it to the MySettings configuration: "ApiKey": "<api-key>" in appsettings.json file.
builder.Services.AddConfiguration<MyAppSettings>(builder.Configuration, "MySettings");

builder.Services.ConfigureHttpClientDefaults(http =>
{
  // Turn on resilience by default
  http.AddStandardResilienceHandler();
  http.UseSocketsHttpHandler();
});

builder.Services.AddTransient<IWeatherService, WeatherService>();

builder.Services.RegisterNewsSearchApiClient(builder.Configuration);

using IHost host = builder.Build();

MyAppSettings mySettings = host.Services.GetRequiredService<MyAppSettings>();

IWeatherService weatherService = host.Services.GetRequiredService<IWeatherService>();
Weather? weather = await weatherService.GetCurrentWeather(mySettings.QueryParameters?.CityName);

if (weather == null)
{
  Console.WriteLine($"There is no weather information for a city {mySettings.QueryParameters?.CityName}.");
}
else
{
  Console.WriteLine($"\n\rWeather in {mySettings.QueryParameters?.CityName} is: '{weather?.Description}'.");
}

Console.WriteLine("\n\rPress <Enter> key for exit.");
Console.ReadLine();

await host.StopAsync();
