using System;
using System.Net.Http.Headers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Refit;

using WireMock.Net.Api.Client;
using WireMock.Net.Api.Configuration;

namespace WireMock.Net.Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherClient(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("WeatherClient").Get<WeatherSettings>();
            services.AddRefitClient<IWeatherClient>().ConfigureHttpClient((sp, client) =>
            {
                client.BaseAddress = new Uri(settings.BaseAddress);
                client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            return services;
        }
    }
}