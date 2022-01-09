    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.Model
{
    public class DataPoint
    {
        public string str_date { get; set; }
        public string pharmacy_name { get; set; } // 약국 이름
        public string insurance_name { get; set; } // 약국 이름
        public int Value { get; set; }
        public string dep_name { get; set; }

        //test1234
        public string dep_Ages { get; set; }
        public int dep_count { get; set; }
    }
}
