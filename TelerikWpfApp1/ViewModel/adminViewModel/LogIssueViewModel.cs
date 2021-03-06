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
    public class LogIssueViewModel : LogData
    {

        public static String total_log_count { get; set; } // 총 로그 수 
        public static String week_log_count { get; set; } // 7일간 로그 수 
        public static String week_Error_count { get; set; } // 7일간 Error수 
        public static String week_Fatal_count { get; set; } // 7일간 Fatal 수 
        

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

        public RelayCommand TotalLoadLog { get; set;} // 총 로그 리스트
        public RelayCommand WeekLoadLog { get; set; } // 오늘의 로그 리스트
        public RelayCommand WeekErrorLog { get; set; } // 오늘의 에러 리스트
        public RelayCommand WeekFatalLog { get; set; } // 오늘의 결함 리스트 

        public LogIssueViewModel()
        {
            TotalLoadLog = new RelayCommand(TotalLogSearch);
            WeekLoadLog = new RelayCommand(WeekLogSearch);
            WeekErrorLog = new RelayCommand(WeekErrorSearch);
            WeekFatalLog = new RelayCommand(WeekFatalSearch);
            LogCountSearch();
        }

        private void TotalLogSearch()
        {
            LogDatas.Clear();
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT LOG_NO, LOG_LEVEL, CURRENTDATE, CLASS, METHOD, ETC, IPADDRESS, PATIENT_ID
                                FROM LOG  
                                ORDER BY LOG_NO ASC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    LogData obj = new LogData
                    {
                        Log_no = int.Parse(ds.Tables[0].Rows[idx]["LOG_NO"].ToString()),
                        Log_level = ds.Tables[0].Rows[idx]["LOG_LEVEL"].ToString(),
                        Log_date = ds.Tables[0].Rows[idx]["CURRENTDATE"].ToString(),
                        Log_class = ds.Tables[0].Rows[idx]["CLASS"].ToString(),
                        Log_method = ds.Tables[0].Rows[idx]["METHOD"].ToString(),
                        Log_etc = ds.Tables[0].Rows[idx]["ETC"].ToString(),
                        Log_ip = ds.Tables[0].Rows[idx]["IPADDRESS"].ToString(),
                        Patient_id = ds.Tables[0].Rows[idx]["PATIENT_ID"].ToString()
                    };
                    LogDatas.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "TotalLogSearch");
            }
        }
        private void WeekLogSearch()
        {
            LogDatas.Clear();
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT LOG_NO, LOG_LEVEL, CURRENTDATE, CLASS, METHOD, ETC, IPADDRESS, PATIENT_ID
                                FROM LOG 
                                WHERE CURRENTDATE >= TO_CHAR(CURRENT_DATE-7,'YYYYMMDD') 
                                ORDER BY LOG_NO ASC";


                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    LogData obj = new LogData
                    {
                        Log_no = int.Parse(ds.Tables[0].Rows[idx]["LOG_NO"].ToString()),
                        Log_level = ds.Tables[0].Rows[idx]["LOG_LEVEL"].ToString(),
                        Log_date = ds.Tables[0].Rows[idx]["CURRENTDATE"].ToString(),
                        Log_class = ds.Tables[0].Rows[idx]["CLASS"].ToString(),
                        Log_method = ds.Tables[0].Rows[idx]["METHOD"].ToString(),
                        Log_etc = ds.Tables[0].Rows[idx]["ETC"].ToString(),
                        Log_ip = ds.Tables[0].Rows[idx]["IPADDRESS"].ToString(),
                        Patient_id = ds.Tables[0].Rows[idx]["PATIENT_ID"].ToString()
                    };
                    LogDatas.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WeekLogSearch");
            }
        }
        private void WeekErrorSearch()
        {
            LogDatas.Clear();
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT LOG_NO, LOG_LEVEL, CURRENTDATE, CLASS, METHOD, ETC, IPADDRESS, PATIENT_ID
                                FROM LOG 
                                WHERE LOG_LEVEL='ERROR' AND CURRENTDATE >= TO_CHAR(CURRENT_DATE-7,'YYYYMMDD') 
                                ORDER BY LOG_NO ASC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    LogData obj = new LogData
                    {
                        Log_no = int.Parse(ds.Tables[0].Rows[idx]["LOG_NO"].ToString()),
                        Log_level = ds.Tables[0].Rows[idx]["LOG_LEVEL"].ToString(),
                        Log_date = ds.Tables[0].Rows[idx]["CURRENTDATE"].ToString(),
                        Log_class = ds.Tables[0].Rows[idx]["CLASS"].ToString(),
                        Log_method = ds.Tables[0].Rows[idx]["METHOD"].ToString(),
                        Log_etc = ds.Tables[0].Rows[idx]["ETC"].ToString(),
                        Log_ip = ds.Tables[0].Rows[idx]["IPADDRESS"].ToString(),
                        Patient_id = ds.Tables[0].Rows[idx]["PATIENT_ID"].ToString()
                    };
                    LogDatas.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WeekErrorSearch");
            }
        }
        private void WeekFatalSearch()
        {
            LogDatas.Clear();
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT LOG_NO, LOG_LEVEL, CURRENTDATE, CLASS, METHOD, ETC, IPADDRESS, PATIENT_ID
                                FROM LOG 
                                WHERE LOG_LEVEL='FATAL' AND CURRENTDATE >= TO_CHAR(CURRENT_DATE-7,'YYYYMMDD')  
                                ORDER BY LOG_NO ASC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    LogData obj = new LogData
                    {
                        Log_no = int.Parse(ds.Tables[0].Rows[idx]["LOG_NO"].ToString()),
                        Log_level = ds.Tables[0].Rows[idx]["LOG_LEVEL"].ToString(),
                        Log_date = ds.Tables[0].Rows[idx]["CURRENTDATE"].ToString(),
                        Log_class = ds.Tables[0].Rows[idx]["CLASS"].ToString(),
                        Log_method = ds.Tables[0].Rows[idx]["METHOD"].ToString(),
                        Log_etc = ds.Tables[0].Rows[idx]["ETC"].ToString(),
                        Log_ip = ds.Tables[0].Rows[idx]["IPADDRESS"].ToString(),
                        Patient_id = ds.Tables[0].Rows[idx]["PATIENT_ID"].ToString()
                    };
                    LogDatas.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WeekFatalSearch");
            }
        }

        public void LogCountSearch()
        {
            total_log_count = "0";
            try
            {
                // 총 로그수 기록 
                DataSet ds = new DataSet();
                string query = @"SELECT Count(*) as COUNT FROM log";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                total_log_count = ds.Tables[0].Rows[0]["COUNT"].ToString();

                DataSet ds2 = new DataSet();
                string query2 = @"SELECT Count(*) as COUNT FROM log	
                                  WHERE CURRENTDATE >= TO_CHAR(SYSDATE-7,'YYYYMMDD')";

                OracleDBManager.Instance.ExecuteDsQuery(ds2, query2);
                week_log_count = ds2.Tables[0].Rows[0]["COUNT"].ToString();

                DataSet ds3 = new DataSet();
                string query3 = @"SELECT Count(*) as COUNT FROM log	
                                  WHERE CURRENTDATE >= TO_CHAR(SYSDATE-7,'YYYYMMDD') 
                                  AND log.LOG_LEVEL = 'ERROR'";

                OracleDBManager.Instance.ExecuteDsQuery(ds3, query3);
                week_Error_count = ds3.Tables[0].Rows[0]["COUNT"].ToString();

                DataSet ds4 = new DataSet();
                string query4 = @"SELECT Count(*) as COUNT FROM log	
                                  WHERE CURRENTDATE >= TO_CHAR(SYSDATE-7,'YYYYMMDD') 
                                  AND log.LOG_LEVEL = 'FATAL'";

                OracleDBManager.Instance.ExecuteDsQuery(ds4, query4);
                week_Fatal_count = ds4.Tables[0].Rows[0]["COUNT"].ToString();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "LogCountSearch");
            }

        }


    }

}
