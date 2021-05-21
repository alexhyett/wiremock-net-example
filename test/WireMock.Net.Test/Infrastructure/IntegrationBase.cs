using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Testing;

using Newtonsoft.Json;

using WireMock.Net.Api;

using Xunit;

namespace WireMock.Net.Test.Infrastructure
{
    [Collection("sequential")]
    public abstract class IntegrationBase : IClassFixture<ApiWebFactory<Startup>>
    {
        protected HttpClient HttpClient { get; }
        protected IntegrationBase(WebApplicationFactory<Startup> factory)
        {
            HttpClient = factory.CreateClient();
            factory.Server.AllowSynchronousIO = true;
        }

        protected HttpRequestMessage CreateGetRequest(string url)
        {
            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        protected static async Task<T> ReadResponseAsync<T>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}