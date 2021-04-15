using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.WeatherData
{
    public class Coldest : IWeather
    {
        public Weather GetData(IEnumerable<Weather> data)
        {
            var result = (from d in data
                          orderby d.TemperatureC ascending
                          select d).First();
            result.Identifier = "Coldest";
            return result;
        }
    }
}
