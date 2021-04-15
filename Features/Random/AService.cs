using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.Random
{
    public class AService : ISomeService
    {
        private readonly DateTime _date;
        private System.Random _r;
        public AService()
        {
            _date = DateTime.Now;
            _r = new System.Random();
        }
        public Weather GetData()
        {
            return new Weather
            {
                TemperatureC = _r.NextDouble()*30,
                Date = _date,
                Timestamp = 12345678
            };
        }
    }
}
