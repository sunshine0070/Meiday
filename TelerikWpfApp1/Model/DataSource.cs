using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

	public class prescription_ment
	{
		public string P_Name { get; set; }
		public string P_Number { get; set; }
		public string P_Date { get; set; }
		public string P_Doctor { get; set; }
		public string P_DoctorLicense { get; set; }
		public string P_Medication { get; set; }
		public string P_MedicationDose { get; set; }
		public string P_MedicationCount { get; set; }
		public string P_DoctorPosition { get; set; }

	}

	public class receipt_ment
	{
		public string R_Name { get; set; }
		public string R_Id { get; set; }
		public string R_Pay { get; set; }
		public string R_Doctor { get; set; }
		public string R_DoctorPosition { get; set; }
		public string R_Date { get; set; }
		public string R_Year { get; set; }
		public string R_Month { get; set; }
		public string R_Day { get; set; }


	}

	public enum AccidentType
    {
		None,
		AccidentTypeDisease,
		AccidentTypeInjury,
		AccidentTypeCar
	}

	public class InsuranceSubmit
    {
		public DateTime AccidentDate { get; set; }
		public AccidentType AccidentType { get; set; }
		public DateTime AccidentSubmitDate { get; set; }
    }
}
