using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WireMock.Net.Api.Client;

namespace WireMock.Net.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherClient _weatherClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherClient weatherClient)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _weatherClient = weatherClient ??
                throw new ArgumentNullException(nameof(weatherClient));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken ct)
        {

            var weatherResponse = await _weatherClient.GetWeatherAsync(51.5074m, 0.1278m, ct);
            if (!weatherResponse.IsSuccessStatusCode || weatherResponse?.Content?.Dataseries == null)
            {
                _logger.LogError("Unexpected status code from Weather API {StatusCode}", weatherResponse.StatusCode);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(weatherResponse.Content.Dataseries.Select(weather => new WeatherForecast
            {
                Date = DateTime.ParseExact(weather.Date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture),
                    TemperatureC = new Temperature { Min = weather.Temp2m.Min, Max = weather.Temp2m.Max },
                    Summary = weather.Weather
            }));
        }
    }
}