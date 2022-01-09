using Meiday.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.ViewModel.adminViewModel
{
    public class LogIssueViewModel : LogData
    {
        ObservableCollection<LogData> logDatas = null;

        public ObservableCollection<LogData> LogDatas
        {
            get 
            { 
                if(logDatas == null)
                {
                    logDatas = new ObservableCollection<LogData>();
                }
                return logDatas;
            }
            set
            {
                logDatas = value;
            }
        }

        public RelayCommand LoadLog { get; set;}
        public LogIssueViewModel()
        {
            LoadLog = new RelayCommand(LogSearch);
        }

        private void LogSearch()
        {
            LogDatas.Clear();
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT LOG_NO, LOG_LEVEL, CURRENTDATE, CLASS, ETC, IPADDRESS, PATIENT_ID
                                FROM LOG 
                                WHERE LOG_LEVEL='ERROR' OR LOG_LEVEL='FATAL' 
                                ORDER BY LOG_NO ASC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    LogData obj = new LogData
                    {
                        Log_no = int.Parse(ds.Tables[0].Rows[idx]["LOG_NO"].ToString()),
                        Log_level = ds.Tables[0].Rows[idx]["LOG_LEVEL"].ToString(),
                        Log_date = Convert.ToDateTime(ds.Tables[0].Rows[idx]["CURRENTDATE"]),
                        Log_class = ds.Tables[0].Rows[idx]["CLASS"].ToString(),
                        Log_method = ds.Tables[0].Rows[idx]["ETC"].ToString(),
                        Log_ip = ds.Tables[0].Rows[idx]["IPADDRESS"].ToString(),
                        Patient_id = ds.Tables[0].Rows[idx]["PATIENT_ID"].ToString()
                    };
                    LogDatas.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "LogSearch");
            }
        }
    }

}
