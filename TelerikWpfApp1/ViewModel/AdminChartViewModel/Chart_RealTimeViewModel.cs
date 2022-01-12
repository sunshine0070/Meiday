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
            string temp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
            DateTime now_01 = DateTime.Now;
            DateTime now = DateTime.Now.AddSeconds(-15);
            DateTime test1 = DateTime.Now.AddHours(-2);
            string tx_test1 = (test1.ToString()).Substring(14, 5);
            count++;
            this.DataPoints.Add(new Chart_RealTime(test1, GenerateValue(tx_test1)));
            if (DataPoints.Count == MaxPointCount) { timer.Stop(); }



        }

        public double GenerateValue(string a)
        {
            DataSet ds = new DataSet();
            string query = @"SELECT TO_CHAR(CURRENTDATE, 'HH:MI:SS') 시간,COUNT(*) 갯수
                               FROM LOG WHERE TO_CHAR(CURRENTDATE, 'HH:MI')>= " + "'" + a + "'" + "GROUP BY TO_CHAR(CURRENTDATE, 'HH:MI:SS')ORDER BY TO_CHAR(CURRENTDATE, 'HH:MI:SS') ASC";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);

            if(ds.Tables[0].Rows.Count==0) { timer.Stop(); }
            return Double.Parse(ds.Tables[0].Rows[count]["갯수"].ToString());
        }
    }
}
