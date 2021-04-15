using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.WeatherData
{
    public class Warmest : IWeather
    {
        public Weather GetData(IEnumerable<Weather> data)
        {
            var result = (from d in data
                          orderby d.TemperatureC descending
                          select d).First();
            result.Identifier = "Warmest";
            return result;
        }
    }
}
