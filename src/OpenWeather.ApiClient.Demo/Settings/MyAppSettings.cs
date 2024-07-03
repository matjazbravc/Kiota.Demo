namespace OpenWeather.ApiClient.Demo.Settings
{
  public sealed class MyAppSettings
  {
    public string? ApiKey { get; set; }
    
    public string? BaseUrl { get; set; }
    
    public QueryParameters? QueryParameters { get; set; }
  }

  public sealed class QueryParameters
  {
    public string? CityName { get; set; }

    public string? Lang { get; set; }

    public string? Units { get; set; }
  }
}
