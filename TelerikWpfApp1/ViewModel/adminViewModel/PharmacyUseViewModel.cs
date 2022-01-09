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
    public class PharmacyUseViewModel
    {
        public ObservableCollection<DataPoint> pharmacyData = null;

        public ObservableCollection<DataPoint> PharmacyData
        {
            get
            {
                if (pharmacyData == null)
                {
                    pharmacyData = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    String query = @"SELECT PHARMACY_NAME, COUNT(*) 갯수 
                                    FROM PHARMACY_RESERVE 
                                    GROUP BY PHARMACY_NAME 
                                    ORDER BY 갯수 DESC";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        DataPoint obj = new DataPoint()
                        {
                            pharmacy_name = ds.Tables[0].Rows[idx]["PHARMACY_NAME"].ToString(),
                            Value = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString())
                        };
                        pharmacyData.Add(obj);
                    }
                }
                return pharmacyData;
            }
            set
            {
                pharmacyData = value;
            }
        }
    }

}
