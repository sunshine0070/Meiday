using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Meiday.ViewModel
{
    public class SessionViewModel
    {
        DispatcherTimer sessionTimer = new DispatcherTimer();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            sessionTimer.Interval = TimeSpan.FromSeconds(10);
            sessionTimer.Tick += new EventHandler(sessionTimer_Tick);
            sessionTimer.Start();
        }

        void sessionTimer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("세션이 만료되었습니다.");
        }
        public void ResetSessionTimer()
        {
            this.sessionTimer.Interval = TimeSpan.FromSeconds(10);
        }
    }
}
