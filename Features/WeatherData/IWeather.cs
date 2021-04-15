using System.Collections.Generic;

namespace Uppgift6_Csharp.Features.WeatherData
{
    public interface IWeather
    {
        Weather GetData(IEnumerable<Weather> data);
    }
}