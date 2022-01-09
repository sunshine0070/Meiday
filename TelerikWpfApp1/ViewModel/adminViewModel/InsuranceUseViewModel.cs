using Meiday.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Meiday.ViewModel.adminViewModel
{
    public class InsuranceUseViewModel
    {
        public ObservableCollection<DataPoint> insuranceData = null;
        public ObservableCollection<DataPoint> InsuranceData
        {
            get
            {
                if (insuranceData == null)
                {
                    insuranceData = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    String query = @"SELECT i.INSURANCE_NAME AS INSURANCE_NAME, count(*) AS count FROM CHECKINSURANCE c 
                                     JOIN INSURANCE i ON c.INSURANCE_NUM = i.INSURANCE_NUM
                                     GROUP BY i.INSURANCE_NAME 
                                     ORDER BY count desc";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        DataPoint obj = new DataPoint()
                        {
                            insurance_name = ds.Tables[0].Rows[idx]["INSURANCE_NAME"].ToString(),
                            Value = Int32.Parse(ds.Tables[0].Rows[idx]["count"].ToString())
                        };
                        insuranceData.Add(obj);
                    }
                }
                return insuranceData;
            }
            set
            {
                insuranceData = value;
            }
        }
    }
}
