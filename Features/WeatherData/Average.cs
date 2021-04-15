using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.WeatherData
{
    public class Average : IWeather
    {
        public Weather GetData(IEnumerable<Weather> data)
        {
            var sumOfTemperatures = data.Select(d => d.TemperatureC).Sum();
            var numOfMeasurements = data.Count();
            var avg = sumOfTemperatures / numOfMeasurements;

            return new Weather
            {
                Identifier = "Average",
                TemperatureC = Math.Round(avg, 2)
            };
        }
    }
}
