using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.Model
{
    public class Chart_RealTime
    {
        public DateTime Argument { get; set; }
        public double Value { get; set; }
        public Chart_RealTime(DateTime argument, double value)
        {
            int hour = Int32.Parse(argument.ToString("HH"));
            int minite = Int32.Parse(argument.ToString("mm"));
            int second = Int32.Parse(argument.ToString("ss"));
            this.Argument = new DateTime(2022, 01, 12, hour, minite, second);
            this.Value = value;
        }
    }
}
