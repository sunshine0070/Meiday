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
    public class Admin_Chart_Log
    {
        ObservableCollection<Log_Model> _logs_debug = null;
        ObservableCollection<Log_Model> _logs_fatal = null;
        ObservableCollection<Log_Model> _logs_total = null;
        public ObservableCollection<Log_Model> Logs_CHART_DEBUG
        {
            get
            {
                if (_logs_debug == null)
                {
                    _logs_debug = new ObservableCollection<Log_Model>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT TO_CHAR(CURRENTDATE,'HH') 시간,COUNT(*) 갯수,LOG_LEVEL 레벨 
                                        FROM LOG GROUP BY  TO_CHAR(CURRENTDATE,'HH'),LOG_LEVEL ORDER BY TO_CHAR(CURRENTDATE,'HH') ASC";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["레벨"].ToString() == "DEBUG")
                        {
                            Log_Model obj = new Log_Model()
                            {
                                str_level = ds.Tables[0].Rows[idx]["레벨"].ToString(),
                                log_count = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString()),
                                str_date = ds.Tables[0].Rows[idx]["시간"].ToString(),
                            };
                            Logs_CHART_DEBUG.Add(obj);
                        }
                    }
                }
                return _logs_debug;
            }
            set
            { _logs_debug = value; }
        }

        public ObservableCollection<Log_Model> Logs_CHART_FATAL
        {
            get
            {
                if (_logs_fatal == null)
                {
                    _logs_fatal = new ObservableCollection<Log_Model>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT TO_CHAR(CURRENTDATE,'HH') 시간,COUNT(*) 갯수,LOG_LEVEL 레벨 
                                        FROM LOG GROUP BY  TO_CHAR(CURRENTDATE,'HH'),LOG_LEVEL ORDER BY TO_CHAR(CURRENTDATE,'HH') ASC";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["레벨"].ToString() == "FATAL")
                        {
                            Log_Model obj = new Log_Model()
                            {
                                str_level = ds.Tables[0].Rows[idx]["레벨"].ToString(),
                                log_count = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString()),
                                str_date = ds.Tables[0].Rows[idx]["시간"].ToString(),
                            };
                            Logs_CHART_FATAL.Add(obj);
                        }
                    }
                }
                return _logs_fatal;
            }
            set
            { _logs_fatal = value; }
        }

        public ObservableCollection<Log_Model> LOGS_CHART_TOTAL
        {
            get
            {
                if (_logs_total == null)
                {
                    _logs_total = new ObservableCollection<Log_Model>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT TO_CHAR(CURRENTDATE,'HH') 시간,COUNT(*) 갯수
                                        FROM LOG GROUP BY  TO_CHAR(CURRENTDATE,'HH') ORDER BY TO_CHAR(CURRENTDATE,'HH') ASC";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {

                            Log_Model obj = new Log_Model()
                            {
                                
                                log_count = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString()),
                                str_date = ds.Tables[0].Rows[idx]["시간"].ToString(),
                            };
                            LOGS_CHART_TOTAL.Add(obj);
                        
                    }
                }
                return _logs_total;
            }
            set
            { _logs_total = value; }
        }
    }
}
