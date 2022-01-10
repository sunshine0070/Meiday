using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Meiday.View;
using System.ComponentModel;
using System.Data;
using static Meiday.LoginViewModel;

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
            TimeRemainingString = TimeRemainingString;
            if (TimeRemaining < 0) 
            {
                SessionTimer_Reset();
                SwitchView = 5;
            }
        }
        public void SessionTimer_Reset()
        {
            Log.Debug("SessionTimer_Reset");
            TimeRemaining = 600; // 세션 시간(10분)
        }
        private string timeRemainingString = string.Empty;
        public string TimeRemainingString
        {
            get
            {
                int minutes = TimeRemaining / 60;
                int seconds = TimeRemaining % 60;
                timeRemainingString = minutes.ToString() + ":" + seconds.ToString("00");
                return timeRemainingString;
            }
            set
            {
                timeRemainingString = value;
                OnPropertyChanged("TimeRemainingString");
            }
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
                timeRemaining = value;
                OnPropertyChanged("TimeRemaining");
            }
        }
        private string managerName = string.Empty;
        public string ManagerName
        {
            get
            {
                DataSet ds = new DataSet();
                string query = @" select d.DR_NAME dr_name
                              from doctor d
                              where d.DR_LICENSE = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                managerName = ds.Tables[0].Rows[0]["dr_name"].ToString();
                return managerName;
            }
            set
            {
                Log.Debug("ManagerName");
                if (value != managerName)
                {
                    managerName = value;
                    OnPropertyChanged("ManagerName");
                }
            }
        }
        private string managerimage = string.Empty;
        public string ManagerImage
        {
            get
            {
                DataSet ds = new DataSet();
                string query = @" select d.DR_IMAGE dr_image
                              from doctor d
                              where d.DR_LICENSE = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                managerimage = ds.Tables[0].Rows[0]["dr_image"].ToString();
                return managerimage;
            }
            set
            {
                Log.Debug("ManagerImage");
                if (value != managerimage)
                {
                    managerimage = value;
                    OnPropertyChanged("ManagerImage");
                }
            }
        }
    }
}
