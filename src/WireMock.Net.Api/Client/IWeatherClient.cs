using System.Threading;
using System.Threading.Tasks;

using Refit;

using WireMock.Net.Api.Client.Model;

namespace WireMock.Net.Api.Client
{
    public interface IWeatherClient
    {
        [Get("/bin/civillight.php?lat={latitude}&lon={longitude}&ac=0&unit=metric&output=json&tzshift=0")]
        Task<ApiResponse<WeatherResponse>> GetWeatherAsync(decimal latitude, decimal longitude, CancellationToken ct);
    }
}