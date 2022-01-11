using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Meiday.ViewModel
{
    public class LogChartViewModel
    {
        const int MaxPointCount = 3000;
        readonly DispatcherTimer timer = new DispatcherTimer();
        public ObservableCollection<LogPoint> DataPoints { get; } = new ObservableCollection<LogPoint>();
        public LogChartViewModel()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }
        private void Timer_Tick(object sender, System.EventArgs e)  {
            this.DataPoints.Add(new LogPoint(GenerateArguement(0), GenerateValue(0)));
            if (DataPoints.Count == MaxPointCount) { timer.Stop(); }
        }
        private DateTime GenerateArguement(int x)
        {
            DataSet ds = new DataSet();
            string query = @"SELECT CURRENTDATE 시간
                                 FROM LOG GROUP BY CURRENTDATE ORDER BY CURRENTDATE ASC";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            DateTime a = Convert.ToDateTime(ds.Tables[0].Rows[x]["시간"].ToString());
            return a;
        }

        private int GenerateValue(int y)
        {
            DataSet ds = new DataSet();
            string query = @"SELECT COUNT(*) 갯수 
                                 FROM LOG GROUP BY CURRENTDATE ORDER BY CURRENTDATE ASC";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            int b = int.Parse(ds.Tables[0].Rows[y]["갯수"].ToString());
            return b;
        }
    }
    public class LogPoint
    {
        public DateTime Argument { get; set; }
        public int Value { get; set; }
        public LogPoint(DateTime argument, int value)
        {
            this.Argument = argument;
            this.Value = value;
        }
    }
}
