using System.Collections.Generic;

namespace Uppgift6_Csharp.Features.WeatherData
{
    public interface IWeatherInputParser
    {
        IEnumerable<Weather> GetData();
        IEnumerable<Weather> ParseWeatherInput();
        void SetDataSource(string path);
    }
}