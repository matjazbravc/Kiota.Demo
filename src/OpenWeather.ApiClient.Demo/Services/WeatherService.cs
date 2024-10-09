using Microsoft.Extensions.Options;
using OpenWeather.ApiClient.Demo.Models;
using OpenWeather.ApiClient.Demo.Settings;
using OpenWeather.ApiClient.Demo.Weather;

namespace OpenWeather.ApiClient.Demo.Services;

public class WeatherService : IWeatherService
{
  private readonly OpenWeatherApiClient _httpClient;
  private readonly MyAppSettings _mySettings;

  public WeatherService(OpenWeatherApiClient httpClient, IOptions<MyAppSettings> mySettings)
  {
    _httpClient = httpClient;
    _mySettings = mySettings.Value;
  }

  public async Task<Models.Weather?> GetCurrentWeather(string? cityName)
  {
    // API request by city name
    // You can call by city name or city name, state code and country code.
    // Please note that searching by states available only for the USA locations.
    // API Docs:
    // https://openweathermap.org/current
    // Swagger UI:
    // https://idratherbewriting.com/assets/files/swagger/#/Current%20Weather%20Data/CurrentWeatherData

    TwoZeroZero? result = await _httpClient.Weather.GetAsync(rc =>
    {
      rc.QueryParameters.Q = cityName;
      rc.QueryParameters.Units = GetUnitsQueryParameter();
      rc.QueryParameters.Lang = GetLanguageQueryParameter();
    });

    return result?.Weather?.FirstOrDefault();
  }

  private GetLangQueryParameterType GetLanguageQueryParameter()
  {
    GetLangQueryParameterType result = GetLangQueryParameterType.En;
    if (Enum.TryParse(_mySettings?.QueryParameters?.Lang, out GetLangQueryParameterType langParam))
    {
      result = langParam;
    }
    return result;
  }

  private GetUnitsQueryParameterType GetUnitsQueryParameter()
  {
    GetUnitsQueryParameterType result = GetUnitsQueryParameterType.Metric;
    if (Enum.TryParse(_mySettings?.QueryParameters?.Units, out GetUnitsQueryParameterType unitParam))
    {
      result = unitParam;
    }
    return result;
  }
}