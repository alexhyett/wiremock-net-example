using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using FluentAssertions;

using WireMock.Net.Api;
using WireMock.Net.Test.Infrastructure;

using Xunit;

namespace WireMock.Net.Test
{
    public class IntegrationTest : IntegrationBase, IDisposable
    {
        private readonly WeatherFixture _weatherFixture;
        public IntegrationTest(ApiWebFactory<Startup> factory) : base(factory)
        {
            _weatherFixture = new WeatherFixture();
        }

        public void Dispose()
        {
            _weatherFixture.Reset();
            _weatherFixture.Dispose();
        }

        [Fact]
        public async Task Given_weather_api_successful_returns_weather()
        {
            // Arrange
            _weatherFixture.SetupGetWeather("Resources/success.json");

            // Act
            var request = CreateGetRequest("/weatherforecast");
            var result = await HttpClient.SendAsync(request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var response = await ReadResponseAsync<IEnumerable<WeatherForecast>>(result);
            response.Should().HaveCount(7);

            response.Should().Contain(x =>
                x.Date == DateTime.Parse("2021-05-25") &&
                x.Summary == "rain" &&
                x.TemperatureC.Max == 12 &&
                x.TemperatureC.Min == 7);
        }

        [Fact]
        public async Task Given_weather_api_unsuccessful_returns_500()
        {
            // Arrange
            _weatherFixture.SetupGetWeather(null, 503);

            // Act
            var request = CreateGetRequest("/weatherforecast");
            var result = await HttpClient.SendAsync(request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}