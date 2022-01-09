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
    public class Dep_per_Ages
    {
        ObservableCollection<DataPoint> _dep_ages = null;
        ObservableCollection<DataPoint> _dep_ages_1 = null;
        ObservableCollection<DataPoint> _dep_ages_2 = null;
        public ObservableCollection<DataPoint> Dep_Ages
        {
            get
            {
                if (_dep_ages == null)
                {
                    _dep_ages = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    string query = @" select  진료과,COUNT(init_value) Counting,init_value Ages
                                        from  (
                                                select pa.PT_AGE 연령, de.DR_DEPTNAME 진료과, CASE  WHEN pa.PT_AGE >=10 AND pa.PT_AGE < 20 THEN 10
                                                                                                  WHEN pa.PT_AGE >=20 AND pa.PT_AGE < 30  THEN 20
                                                                                                  WHEN pa.PT_AGE >=30 AND pa.PT_AGE < 40  THEN 30
                                                                                                  WHEN pa.PT_AGE >=40 AND pa.PT_AGE < 50  THEN 40
                                                                                                  WHEN pa.PT_AGE >=50 AND pa.PT_AGE < 60  THEN 50
                                                                                                  WHEN pa.PT_AGE >=60 AND pa.PT_AGE < 70  THEN 60
                                                                                                  WHEN pa.PT_AGE >=70 AND pa.PT_AGE < 80  THEN 70
                                                                                                  WHEN pa.PT_AGE >=80 AND pa.PT_AGE < 90  THEN 80
                                                                                                  WHEN pa.PT_AGE >=90 AND pa.PT_AGE < 100 THEN 90
                                                                                                  ELSE 0
                                                                                              END AS init_value
                                                  from treatment tr join patient    pa on tr.PT_REGNUM  = pa.PT_REGNUM
                                                                    join doctor     dc on tr.DR_LICENSE = dc.DR_LICENSE
                                                                    join department de on dc.DR_DEPTNUM = de.DR_DEPTNUM )   GROUP BY 진료과, init_value order by 진료과";



                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["진료과"].ToString() == "내과")
                        {
                            DataPoint obj = new DataPoint()
                            {
                                dep_name = ds.Tables[0].Rows[idx]["진료과"].ToString(),
                                dep_Ages = ds.Tables[0].Rows[idx]["Ages"].ToString() + "대",
                                dep_count = Int32.Parse(ds.Tables[0].Rows[idx]["Counting"].ToString()),
                            };
                            Dep_Ages.Add(obj);
                        }
                    }
                }
                return _dep_ages;
            }
            set
            { _dep_ages = value; }
        }

        public ObservableCollection<DataPoint> Dep_Ages1
        {
            get
            {
                if (_dep_ages_1 == null)
                {
                    _dep_ages_1 = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    string query = @" select  진료과,COUNT(init_value) Counting,init_value Ages
                                        from  (
                                                select pa.PT_AGE 연령, de.DR_DEPTNAME 진료과, CASE  WHEN pa.PT_AGE >=10 AND pa.PT_AGE < 20 THEN 10
                                                                                                  WHEN pa.PT_AGE >=20 AND pa.PT_AGE < 30  THEN 20
                                                                                                  WHEN pa.PT_AGE >=30 AND pa.PT_AGE < 40  THEN 30
                                                                                                  WHEN pa.PT_AGE >=40 AND pa.PT_AGE < 50  THEN 40
                                                                                                  WHEN pa.PT_AGE >=50 AND pa.PT_AGE < 60  THEN 50
                                                                                                  WHEN pa.PT_AGE >=60 AND pa.PT_AGE < 70  THEN 60
                                                                                                  WHEN pa.PT_AGE >=70 AND pa.PT_AGE < 80  THEN 70
                                                                                                  WHEN pa.PT_AGE >=80 AND pa.PT_AGE < 90  THEN 80
                                                                                                  WHEN pa.PT_AGE >=90 AND pa.PT_AGE < 100 THEN 90
                                                                                                  ELSE 0
                                                                                              END AS init_value
                                                  from treatment tr join patient    pa on tr.PT_REGNUM  = pa.PT_REGNUM
                                                                    join doctor     dc on tr.DR_LICENSE = dc.DR_LICENSE
                                                                    join department de on dc.DR_DEPTNUM = de.DR_DEPTNUM )   GROUP BY 진료과, init_value order by 진료과";



                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["진료과"].ToString() == "안과")
                        {
                            DataPoint obj = new DataPoint()
                            {
                                dep_name = ds.Tables[0].Rows[idx]["진료과"].ToString(),
                                dep_Ages = ds.Tables[0].Rows[idx]["Ages"].ToString() + "대",
                                dep_count = Int32.Parse(ds.Tables[0].Rows[idx]["Counting"].ToString()),
                            };
                            Dep_Ages1.Add(obj);
                        }
                    }
                }
                return _dep_ages_1;
            }
            set
            { _dep_ages_1 = value; }
        }

        public ObservableCollection<DataPoint> Dep_Ages2
        {
            get
            {
                if (_dep_ages_2 == null)
                {
                    _dep_ages_2 = new ObservableCollection<DataPoint>();
                    DataSet ds = new DataSet();
                    string query = @" select  진료과,COUNT(init_value) Counting,init_value Ages
                                        from  (
                                                select pa.PT_AGE 연령, de.DR_DEPTNAME 진료과, CASE  WHEN pa.PT_AGE >=10 AND pa.PT_AGE < 20 THEN 10
                                                                                                  WHEN pa.PT_AGE >=20 AND pa.PT_AGE < 30  THEN 20
                                                                                                  WHEN pa.PT_AGE >=30 AND pa.PT_AGE < 40  THEN 30
                                                                                                  WHEN pa.PT_AGE >=40 AND pa.PT_AGE < 50  THEN 40
                                                                                                  WHEN pa.PT_AGE >=50 AND pa.PT_AGE < 60  THEN 50
                                                                                                  WHEN pa.PT_AGE >=60 AND pa.PT_AGE < 70  THEN 60
                                                                                                  WHEN pa.PT_AGE >=70 AND pa.PT_AGE < 80  THEN 70
                                                                                                  WHEN pa.PT_AGE >=80 AND pa.PT_AGE < 90  THEN 80
                                                                                                  WHEN pa.PT_AGE >=90 AND pa.PT_AGE < 100 THEN 90
                                                                                                  ELSE 0
                                                                                              END AS init_value
                                                  from treatment tr join patient    pa on tr.PT_REGNUM  = pa.PT_REGNUM
                                                                    join doctor     dc on tr.DR_LICENSE = dc.DR_LICENSE
                                                                    join department de on dc.DR_DEPTNUM = de.DR_DEPTNUM )   GROUP BY 진료과, init_value order by 진료과";



                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        if (ds.Tables[0].Rows[idx]["진료과"].ToString() == "순환기내과")
                        {
                            DataPoint obj = new DataPoint()
                            {
                                dep_name = ds.Tables[0].Rows[idx]["진료과"].ToString(),
                                dep_Ages = ds.Tables[0].Rows[idx]["Ages"].ToString() + "대",
                                dep_count = Int32.Parse(ds.Tables[0].Rows[idx]["Counting"].ToString()),
                            };
                            Dep_Ages2.Add(obj);
                        }
                    }
                }
                return _dep_ages_2;
            }
            set
            { _dep_ages_2 = value; }
        }


    }
}
