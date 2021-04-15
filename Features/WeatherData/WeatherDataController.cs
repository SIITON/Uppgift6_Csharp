using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.WeatherData
{
    [ApiController]
    [Route("Data")]
    public class WeatherDataController : Controller
    {
        public IEnumerable<Weather> _weatherData { get; private set; }
        private readonly IEnumerable<IWeather> _weatherServices;
        private readonly IWeatherInputParser _inputParser;
        public WeatherDataController(IEnumerable<IWeather> weatherServices, IWeatherInputParser inputParser)
        {
            _inputParser = inputParser;
            _weatherServices = weatherServices;
            _weatherData = _inputParser.ParseWeatherInput();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Weather>> Get()
        {
            return Ok(_weatherData);
        }
        [HttpGet("Analyze")]
        public ActionResult<IEnumerable<Weather>> GetStuff()
        {
            var result = new List<Weather>();
            foreach (var service in _weatherServices)
            {
                result.Add(service.GetData(_weatherData));
            }
            return Ok(result);
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
            var weather = GetInterval(8, 17);
            return SetIdentifier(weather, "Dayttime");
        }
        [HttpGet("Nighttime")]
        public IEnumerable<Weather> GetNighttime()
        {
            var weather = GetInterval(0, 5);
            return SetIdentifier(weather, "Nighttime");
        }
        public IEnumerable<Weather> SetIdentifier(IEnumerable<Weather> weather, string id)
        {
            foreach (var measurement in weather)
            {
                measurement.Identifier = id;
                yield return measurement;
            }
        }
        [HttpPost("NewCSVSource")]
        public string SetDataSource(string csvPath)
        {
            try
            {
                _inputParser.SetDataSource(csvPath);
                _weatherData = _inputParser.ParseWeatherInput();
            }
            catch (System.IO.FileNotFoundException)
            {
                return "Error, file not found. Enter a locally stored csv file.";
            }

            return "Success, weather data source changed to " + csvPath;
        }
    }
}
