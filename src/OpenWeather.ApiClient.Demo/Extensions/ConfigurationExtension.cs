using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenWeather.ApiClient.Demo.Settings;

namespace OpenWeather.ApiClient.Demo.Extensions
{
  public static class ConfigurationExtension
  {
    public static void AddConfiguration<T>(this IServiceCollection services,
      IConfiguration configuration, string? configurationTag = null)
      where T : class
    {
      if (string.IsNullOrEmpty(configurationTag))
      {
        configurationTag = typeof(T).Name;
      }

      T instance = Activator.CreateInstance<T>();
      IConfigurationSection section = configuration.GetSection(configurationTag);
      new ConfigureFromConfigurationOptions<T>(section).Configure(instance);
      services.AddSingleton(instance);
      services.Configure<MyAppSettings>(section);
    }
  }
}
