using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Meiday.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        private int switchView;

        public int SwitchView
        {
            get { 
                return switchView; 
            }
            set 
            { 
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }

        public ICommand SwitchViewCommand { get; }

        public AdminViewModel()
        {
            SwitchView = 1;
            adminsessionTimer.Interval = TimeSpan.FromSeconds(1);
            adminsessionTimer.Tick += SessionTimer_Tick; // 1번만 실행
            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));
            SessionTimer_Reset();
            SessionTimer_Start();
        }

        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());

            if (SwitchView == 1) 
            {
                SwitchView = 1;
                SessionTimer_Reset();
            }
            else if (SwitchView == 2)
            {
                SwitchView = 2;
                SessionTimer_Reset();
            }
            else if (SwitchView == 3)
            {
                SwitchView = 3;
                SessionTimer_Reset();
            }
            else if (SwitchView == 4)
            {
                SwitchView = 4;
                SessionTimer_Reset();
            }
        }

        DispatcherTimer adminsessionTimer = new DispatcherTimer();
        public void SessionTimer_Start()
        {
            Log.Debug("SessionTimer_Start");
            adminsessionTimer.Start();
        }
        public void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeRemaining -= 1;
            if (TimeRemaining <= 15 && TimeRemaining >= 0) // 남은 시간 15초부터 빨간색
            {
                //ChangeColorAction();
            }
            else if (TimeRemaining < 0) 
            {
                SessionTimer_Reset();
                adminsessionTimer.Stop();
                //CloseAction();
            }
        }
        public void SessionTimer_Reset()
        {
            Log.Debug("SessionTimer_Reset");
            TimeRemaining = 30; // 세션 시간(3분)
        }


        private int timeRemaining;
        public int TimeRemaining
        {
            get
            {
                return timeRemaining;
            }
            set
            {
                Log.Debug("TimeRemaining");
                timeRemaining = value;
                OnPropertyChanged("TimeRemaining");
            }
        }
/*        public Action ChangeColorAction // 15초 아래로 떨어지면 남은 시간 빨간색... 시도중
        {
            get; set;
        }
        public Action CloseAction 
        { 
            get; set; 
        }*/
    }
}
