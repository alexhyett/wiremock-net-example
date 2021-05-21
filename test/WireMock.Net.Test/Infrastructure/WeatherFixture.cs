using System;
using System.IO;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMock.Net.Test.Infrastructure
{
    public class WeatherFixture : IDisposable
    {
        protected readonly WireMockServer _mockApi;

        public WeatherFixture()
        {
            _mockApi = WireMockServer.Start(50000);
        }

        public void Dispose()
        {
            _mockApi.Stop();
        }

        public void Reset()
        {
            _mockApi.Reset();
        }

        public IRequestBuilder SetupGetWeather(string responseBodyResource, int statusCode = 200)
        {
            var request = Request.Create()
                .UsingGet()
                .WithPath("/bin/civillight.php*");

            var responseBody = string.IsNullOrWhiteSpace(responseBodyResource) ? new byte[0] : File.ReadAllBytes(responseBodyResource);

            _mockApi.Given(request)
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(statusCode)
                    .WithHeader("content-type", "application/json")
                    .WithBody(responseBody)
                );

            return request;
        }
    }
}