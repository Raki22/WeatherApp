using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class Temperature
    {
        public Measure Metric { get; set; }
        public Measure Imperial { get; set; }
    }
}
