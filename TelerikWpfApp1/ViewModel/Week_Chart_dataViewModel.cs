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
    public class Week_Chart_dataViewModel
    {
        ObservableCollection<DataPoint> _womanDatas = null;
        ObservableCollection<DataPoint> _manDatas = null;
        ObservableCollection<DataPoint> _sampleDatas = null;

        public ObservableCollection<DataPoint> Woman_Data
        {
            get
            {
                if (_womanDatas == null)
                {
                    _womanDatas = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT COUNT(*) 갯수 , A 날짜, BB,
                                        CASE WHEN BB = '1' THEN '남자'
                                             WHEN BB = '2' THEN '여자'
                                                           ELSE '미정'
                                         END AS                   성별
                                      FROM (
                                      SELECT TO_CHAR( treatment_time ,'DAY') A , TO_CHAR(treatment_time-1,'D') B,SUBSTR(t.PT_REGNUM,7,1) BB
                                      FROM TREATMENT T INNER JOIN PATIENT P ON T.PT_REGNUM = P.PT_REGNUM) 
                                      GROUP BY A,B,BB ORDER BY B";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["성별"].ToString() == "여자")
                        {
                            DataPoint obj = new DataPoint()
                            {
                                str_date = ds.Tables[0].Rows[idx]["날짜"].ToString(),
                                Value = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString()),
                            };
                            Woman_Data.Add(obj);
                        }
                    }
                }
                return _womanDatas;
            }
            set
            { _womanDatas = value; }
        }

        public ObservableCollection<DataPoint> Man_Data
        {
            get
            {
                if (_manDatas == null)
                {
                    _manDatas = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT COUNT(*) 갯수 , A 날짜, BB,
                                        CASE WHEN BB = '1' THEN '남자'
                                             WHEN BB = '2' THEN '여자'
                                                           ELSE '미정'
                                         END AS                   성별
                                      FROM (
                                      SELECT TO_CHAR( treatment_time ,'DAY') A , TO_CHAR(treatment_time-1,'D') B,SUBSTR(t.PT_REGNUM,7,1) BB
                                      FROM TREATMENT T INNER JOIN PATIENT P ON T.PT_REGNUM = P.PT_REGNUM) 
                                      GROUP BY A,B,BB ORDER BY B";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["성별"].ToString() == "남자")
                        {
                            DataPoint obj = new DataPoint()
                            {
                                str_date = ds.Tables[0].Rows[idx]["날짜"].ToString(),
                                Value = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString()),
                            };
                            Man_Data.Add(obj);
                        }
                    }
                }
                return _manDatas;
            }
            set
            { _manDatas = value; }
        }


        public ObservableCollection<DataPoint> Data
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT COUNT(*) 갯수 , A 날짜 
                                        FROM (
                                                SELECT TO_CHAR( treatment_time ,'DAY') A, TO_CHAR(treatment_time-1,'D') B
                                                FROM TREATMENT)
                                       GROUP BY A,B ORDER BY B ";

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        DataPoint obj = new DataPoint()
                        {
                            str_date = ds.Tables[0].Rows[idx]["날짜"].ToString(),
                            Value = Int32.Parse(ds.Tables[0].Rows[idx]["갯수"].ToString()),
                        };
                        Data.Add(obj);
                    }
                }
                return _sampleDatas;
            }
            set
            { _sampleDatas = value; }
        }

        public Week_Chart_dataViewModel()
        {
            this.Data = _sampleDatas;
            this.Man_Data = _manDatas;
        }
    }
}
