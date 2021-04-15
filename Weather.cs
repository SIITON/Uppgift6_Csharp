using System;

namespace Uppgift6_Csharp
{
    public class Weather
    {
        public string? Identifier { get; set; }
        public int? Timestamp { get; set; }
        public DateTime Date { get; set; }
        public double TemperatureC { get; set; }
    }
}
