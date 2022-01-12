using Meiday.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Meiday.ViewModel.AdminChartViewModel
{
    public class Chart_RealTimeViewModel
    {
        const int MaxPointCount = 50000;
        readonly DispatcherTimer timer = new DispatcherTimer();
        int count = -1;

        public ObservableCollection<Chart_RealTime> DataPoints { get; } = new ObservableCollection<Chart_RealTime>();

        public Chart_RealTimeViewModel()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime now = DateTime.Now.AddMinutes(-52);
            string length = now.ToString("HH-mm-ss");
            string   tx_now = (now.ToString()).Substring(14, 4);
            string aaa = DateTime.Now.ToString();
            string ampm = aaa.Substring(11, 2);
            count++;
            
            this.DataPoints.Add(new Chart_RealTime(now, GenerateValue()));
            if (DataPoints.Count == MaxPointCount) { timer.Stop(); }



        }

        public double GenerateValue()
        {
            
            //string hour    = b.Substring(0, 2);
            //string minute  = b.Substring(3, 2);
            //string second  = b.Substring(6, 2);
            //string query_b = hour + ":" + minute + ":" + second;

                DataSet ds = new DataSet();
               // string query = @"SELECT TO_CHAR(CURRENTDATE, 'HH24:MI:SS') 시간,COUNT(*) 갯수
               //                FROM LOG WHERE TO_CHAR(CURRENTDATE, 'HH24:MI')>= " + "'" + query_b + "'" + "AND TO_CHAR(CURRENTDATE,'YYYY-MM-DD')=" + "'" +c + "'" + "GROUP BY TO_CHAR(CURRENTDATE, 'HH24:MI:SS')ORDER BY TO_CHAR(CURRENTDATE, 'HH24:MI:SS') ASC";
               string query = @"select to_char(chk_date,'HH24:mi:ss') 시간, count(chk_date) 갯수
                                from(
                                SELECT DISTINCT SYSDATE - (LEVEL / 24 / 60 / 60 * 1 - 1) AS CHK_DATE
                                FROM DUAL
                                CONNECT BY LEVEL <= ((24 * 60 * 60) / 1)
                                ORDER BY CHK_DATE
                                ) A LEFT OUTER JOIN LOG L ON TO_CHAR(A.CHK_DATE,'HH24:mi:ss') = TO_CHAR(L.CURRENTDATE, 'HH24:mi:ss')
                                  WHERE TO_CHAR(A.CHK_DATE,'HH24:mi') >= '16:39' group by CHK_DATE ORDER BY CHK_DATE";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            if (ds.Tables[0].Rows.Count == 0) { timer.Stop(); }
                return double.Parse(ds.Tables[0].Rows[count]["갯수"].ToString());

        }
    }
}
