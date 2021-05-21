using System;

namespace WireMock.Net.Api
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public Temperature TemperatureC { get; set; }

        public Temperature TemperatureF => ConvertToF(TemperatureC);

        public string Summary { get; set; }

        private Temperature ConvertToF(Temperature tempC)
        {
            return new Temperature
            {
                Min = 32 + (int) (tempC.Min / 0.5556), Max = 32 + (int) (tempC.Max / 0.5556)
            };
        }
    }
}