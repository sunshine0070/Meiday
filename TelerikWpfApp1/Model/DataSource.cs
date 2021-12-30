using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.Model
{
    public class ment
    {
		public string regNum { get; set; }
		public int iDNum { get; set; }
		public string patientName { get; set; }
		public string InsuName { get; set; }
		public string InsuProduct { get; set; }
		public bool IsChecked02 { get; set; }
		public string validCheck { get; set; }
	}

	public class payment
	{
		public string Name { get; set; }
		public string Doctor { get; set; }
		public string Group { get; set; }
		public string Date { get; set; }
		public string Price { get; set; }
		public bool Checked { get; set; }
	}
	public enum AccidentType
    {
		None,
		AccidentTypeDisease,
		AccidentTypeInjury,
		AccidentTypeCar
	}
}
