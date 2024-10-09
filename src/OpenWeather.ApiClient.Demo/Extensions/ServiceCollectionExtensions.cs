using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using OpenWeather.ApiClient.Demo.Settings;

namespace OpenWeather.ApiClient.Demo.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterNewsSearchApiClient(this IServiceCollection services, IConfiguration configuration)
  {
    MyAppSettings mySettings = new MyAppSettings();
    configuration.GetSection("MySettings").Bind(mySettings);

    string apiKey = mySettings.ApiKey ?? string.Empty;
    string baseUrl = mySettings.BaseUrl ?? string.Empty;

    services.AddSingleton<IAuthenticationProvider, ApiKeyAuthenticationProvider>(sp =>
    {
      // Add api key as query parameter
      return new(apiKey, "appid", ApiKeyAuthenticationProvider.KeyLocation.QueryParameter);
    });

    services.AddHttpClient<OpenWeatherApiClient>()
    .AddTypedClient((httpClient, sp) =>
    {
      var authenticationProvider = sp.GetRequiredService<IAuthenticationProvider>();
      var requestAdapter = new HttpClientRequestAdapter(authenticationProvider, httpClient: httpClient)
      {
        BaseUrl = baseUrl
      };
      return new OpenWeatherApiClient(requestAdapter);
    })
    .ConfigurePrimaryHttpMessageHandler(_ =>
    {
      IList<DelegatingHandler> defaultHandlers = KiotaClientFactory.CreateDefaultHandlers();

      // Get default HttpMessageHandler
      HttpMessageHandler defaultHttpMessageHandler = KiotaClientFactory.GetDefaultHttpMessageHandler();

      // Or, if your generated client is long-lived, respond to DNS updates using:
      // HttpMessageHandler defaultHttpMessageHandler = new SocketsHttpHandler();

      return KiotaClientFactory.ChainHandlersCollectionAndGetFirstLink(defaultHttpMessageHandler, [.. defaultHandlers])!;
    })
    .AddStandardResilienceHandler().Configure(cfg =>
    {
      cfg.Retry.MaxRetryAttempts = 3;
      cfg.Retry.UseJitter = true;
      cfg.Retry.BackoffType = Polly.DelayBackoffType.Exponential;
    });
  }
}