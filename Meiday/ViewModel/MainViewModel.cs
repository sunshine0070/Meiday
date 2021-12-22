using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        AccidentViewModel accidentViewModel = new AccidentViewModel();
        LoginViewModel loginViewModel = new LoginViewModel();

        private int switchView;
        public int SwitchView
        {
            get
            {
                return switchView;
            }
            set
            {
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }


        private bool _isChecked01 = false;
        public bool IsChecked01
        {
            get => _isChecked01;
            set
            {
                _isChecked01 = value;
                OnPropertyChanged("IsChecked01");
            }
        }

        private bool _isChecked02 = false;
        public bool IsChecked02
        {
            get => _isChecked02;
            set
            {
                _isChecked02 = value;
                OnPropertyChanged("IsChecked02");
            }
        }

        public ICommand SwitchViewCommand { get; }

        public MainViewModel()
        {
            SwitchView = 0;

            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));
        }

        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());

            if (SwitchView == 3 && _isChecked01 == false)
            {
                SwitchView = 99;
                //MessageBox.Show("개인정보 처리 동의가 필요합니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                /*                RadWindow rad = new RadWindow();
                                rad.Content = "개인정보 동의가 필요합니다.";
                                rad.HorizontalAlignment = HorizontalAlignment.Center;
                                rad.VerticalAlignment = VerticalAlignment.Center;
                                rad.HorizontalContentAlignment = HorizontalAlignment.Center;
                                rad.VerticalContentAlignment = VerticalAlignment.Center;
                                rad.Left = 500;
                                rad.Top = 300;
                                rad.Width = 300;
                                rad.Height = 150;
                                rad.Show();*/
            }
            else if (SwitchView == 2 && loginViewModel.InputString != "00000")
            {
                _isChecked01 = false;
                LoginViewModel.Login();
            }
            else if (SwitchView == 2 && loginViewModel.InputString == "00000")
            {
                SwitchView = 90;
                LoginViewModel.Init();
            }
            else if (SwitchView == 0 && _isChecked02 == true) // radgridview checkbox 체크필요
            {
                LoginViewModel.Init();
                MessageBox.Show("실비보험 청구서류가 안전하게 전달되었습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (SwitchView == 0 && _isChecked02 == false)
            {
                SwitchView = 4;
                MessageBox.Show("보험 목록을 선택해주십시오.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
