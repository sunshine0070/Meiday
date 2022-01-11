using Meiday.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.ViewModel
{
    public class Age_Chart_dataViewModel
    {
        public ObservableCollection<DPoint> Data { get; private set; }
        public Age_Chart_dataViewModel()
        {
            this.Data = DPoint.GetDataPoints();
        }
    }
    public class DPoint
    {
        public string Argument { get; set; }
        public int Value { get; set; }
        public static ObservableCollection<DPoint> GetDataPoints()
        {
            string[] arguement = new string[5];
            int[] val = new int[5];
            DataSet ds = new DataSet();
            string query = @"SELECT category_age, count(*) 
                                    FROM (SELECT PATIENT.PT_AGE,
			                                    CASE WHEN PATIENT.PT_AGE < 20 THEN '20대 미만'
				                                     WHEN PATIENT.PT_AGE >= 20 AND  PATIENT.PT_AGE < 30 THEN '20대'
				                                     WHEN PATIENT.PT_AGE >= 30 AND  PATIENT.PT_AGE < 40 THEN '30대'
				                                     WHEN PATIENT.PT_AGE >= 40 AND  PATIENT.PT_AGE < 50 THEN '40대'
				                                     WHEN PATIENT.PT_AGE >= 50 AND  PATIENT.PT_AGE < 60 THEN '50대'
				                                     WHEN PATIENT.PT_AGE >= 60 THEN '60대 이상'
			                                    ELSE 'null'
			                                    END AS category_age
		                                    FROM PATIENT)
                                    group BY category_age";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);

            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                arguement[idx] = ds.Tables[0].Rows[idx]["category_age"].ToString();
                val[idx] = Int32.Parse(ds.Tables[0].Rows[idx]["count(*)"].ToString());
            }

            return new ObservableCollection<DPoint> {
                    new DPoint { Argument = arguement[2], Value = val[2]},
                    new DPoint { Argument = arguement[0], Value = val[0]},
                    new DPoint { Argument = arguement[1], Value = val[1]},
                    new DPoint { Argument = arguement[3], Value = val[3]},
                    new DPoint { Argument = arguement[4], Value = val[4]}
                   };
        }
    }
}
