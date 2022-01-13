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

        public ObservableCollection<Chart_RealTime> DataPoints { get; } = new ObservableCollection<Chart_RealTime>();

        public Chart_RealTimeViewModel()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            DateTime now = DateTime.Now;
            string day_today = now.ToString("yyyy-MM-dd");
            string hour = DateTime.Now.ToString("HH");
            string minute = DateTime.Now.ToString("mm");
            string second = DateTime.Now.ToString("ss");

            string str_time = hour + minute + second;
            this.DataPoints.Add(new Chart_RealTime(now, GenerateValue(str_time,day_today)));
            if (DataPoints.Count == MaxPointCount) { timer.Stop(); }
        }

        public double GenerateValue(string a,string b)
        {
                DataSet ds = new DataSet();
                string query = @"select to_char(chk_date,'HH24:mi:ss') 시간, count(chk_date) 갯수
                                   from(
                                        SELECT DISTINCT SYSDATE - (LEVEL / 24 / 60 / 60 * 1 - 1) AS CHK_DATE
                                        FROM DUAL
                                        CONNECT BY LEVEL <= ((24 * 60 * 60) / 1)
                                        ORDER BY CHK_DATE
                                       ) A LEFT OUTER JOIN LOG L 
                                                       ON TO_CHAR(A.CHK_DATE,'HH24:mi:ss') = TO_CHAR(L.CURRENTDATE, 'HH24:mi:ss')
                                  WHERE TO_CHAR(CHK_DATE,'yyyy-mm-dd') = " + "'" + b + "'" + "AND TO_CHAR(A.CHK_DATE,'HH24miss') >=" + "'" + a + "'" + "group by CHK_DATE ORDER BY CHK_DATE";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            if (ds.Tables[0].Rows.Count == 0) { timer.Stop(); }
                return double.Parse(ds.Tables[0].Rows[0]["갯수"].ToString())-1;

        }
    }
}
