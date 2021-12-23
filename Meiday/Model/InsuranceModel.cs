using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.Model
{
	public class insuaranceM //결제
	{
		public string regNum { get; set; } //주민등록번호 
		public int iDNum { get; set; } //환자번호
		public string patientName { get; set; } //환자명
		public bool IsChecked02 { get; set; } //보험체크 
		public string InsuName { get; set; } //보험회사명
		public string InsuProduct { get; set; } //보험상품
	}
}
