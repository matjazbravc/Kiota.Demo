namespace OpenWeather.ApiClient.Demo.Services;

internal interface IWeatherService
{
  Task<Models.Weather?> GetCurrentWeather(string? cityName);
}