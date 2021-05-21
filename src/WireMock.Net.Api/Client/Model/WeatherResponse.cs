using System.Collections.Generic;

using Newtonsoft.Json;

namespace WireMock.Net.Api.Client.Model
{
    public class WeatherResponse
    {
        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("init")]
        public string Init { get; set; }

        [JsonProperty("dataseries")]
        public IEnumerable<WeatherData> Dataseries { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("weather")]
        public string Weather { get; set; }

        [JsonProperty("temp2m")]
        public Temperature Temp2m { get; set; }
    }

    public class Temperature
    {
        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }
    }
}