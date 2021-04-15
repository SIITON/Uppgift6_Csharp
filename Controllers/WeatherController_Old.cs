using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift6_Csharp.Features.Random;

namespace Uppgift6_Csharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController_Old : ControllerBase
    {
        private readonly ILogger<WeatherController_Old> _logger;
        //private readonly ISomeService _someServices;
        public IEnumerable<Weather> _weatherData {get; private set; }

        public WeatherController_Old(ILogger<WeatherController_Old> logger)
        {
            _logger = logger;
            //_someServices = someServices;
            _weatherData = ParseWeatherInput("temperatures.csv");
        }
        private IEnumerable<Weather> ParseWeatherInput(string csvPath)
        {
            var csvData = System.IO.File.ReadAllLines(csvPath).Select(lines => lines.Split(';').ToArray());
            return csvData.Select(data => new Weather
            {
                Timestamp = int.Parse(data[0]),
                TemperatureC = double.Parse(data[1], CultureInfo.InvariantCulture),
                Date = DateTime.Parse(data[2])
            })
            .AsEnumerable();
        }

        [HttpGet]
        public IEnumerable<Weather> Get()
        {
            return _weatherData;
        }
        [HttpGet("Warmest")]
        public IEnumerable<Weather> GetWarmest()
        {
            return (from d in _weatherData
                    orderby d.TemperatureC descending
                    select d).Take(1);
        }
        [HttpGet("Coldest")]
        public IEnumerable<Weather> GetColdest()
        {
            return (from d in _weatherData
                    orderby d.TemperatureC ascending
                    select d).Take(1);
        }
        [HttpGet("Average")]
        public double GetAverage()
        {
            var sumOfTemps = _weatherData.Select(d => d.TemperatureC).Sum();
            var numOfMeasures = _weatherData.Count();
            return sumOfTemps / numOfMeasures;
        }
        [HttpGet("HourInterval")]
        public IEnumerable<Weather> GetInterval(int lower, int upper)
        {
            return from d in _weatherData
                   where d.Date.Hour >= lower && d.Date.Hour < upper
                   select d;
        }
        [HttpGet("Daytime")]
        public IEnumerable<Weather> GetDaytime()
        {
            return GetInterval(8, 17);
        }
        [HttpGet("Nighttime")]
        public IEnumerable<Weather> GetNighttime()
        {
            return GetInterval(0, 5);
        }
        [HttpPost("NewSource")]
        public string SetDataSource(string csvPath)
        {
            try
            {
                _weatherData = ParseWeatherInput(csvPath);
            }
            catch (System.IO.FileNotFoundException)
            {
                return "Error, file not found. Enter a locally stored csv file.";
            }
            
            return "Success, weather data source changed to " + csvPath;
        }
    }
}
