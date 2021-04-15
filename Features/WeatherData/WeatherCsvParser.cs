using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.WeatherData
{
    public class WeatherCsvParser : IWeatherInputParser
    {
        private IEnumerable<Weather> _weatherData;
        public string _csvPath { get; set; }
        public WeatherCsvParser() //string csvPath
        {
            //_csvPath = csvPath;
            _csvPath = "temperatures.csv";
        }
        public IEnumerable<Weather> GetData()
        {
            return _weatherData;
        }
        public IEnumerable<Weather> ParseWeatherInput()
        {
            var csvData = System.IO.File.ReadAllLines(_csvPath).Select(lines => lines.Split(';').ToArray());
            _weatherData = csvData.Select(data => new Weather
            {
                Timestamp = int.Parse(data[0]),
                TemperatureC = double.Parse(data[1], CultureInfo.InvariantCulture),
                Date = DateTime.Parse(data[2])
            })
            .AsEnumerable();
            return _weatherData;
        }
        public void SetDataSource(string csvPath)
        {
            _csvPath = csvPath;
        }
    }
}
