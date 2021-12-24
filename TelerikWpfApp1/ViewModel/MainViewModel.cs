using System.Windows.Input;
using System.Windows;
using Telerik.Windows.Controls;
using TelerikWpfApp1.View;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using TelerikWpfApp1.Model;
using static TelerikWpfApp1.AccidentViewModel;

namespace TelerikWpfApp1
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

        /*private bool _isChecked02 = false;
        public bool IsChecked02
        {
            get => _isChecked02;
            set
            {
                _isChecked02 = value;
                OnPropertyChanged("IsChecked02");
            }
        }*/

        public ICommand SwitchViewCommand { get; }

        public MainViewModel()
        {
            SwitchView = 0;

            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));
        }
        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());

            if (SwitchView == 0) // 다음에 하기 로그인 정보 초기화
            {
                LoginViewModel.Init();
            }

            if (SwitchView == 2 && loginViewModel.InputString != "00000") // 환자등록번호 입력 시 정상진행
            {
                _isChecked01 = false;
                LoginViewModel.Login();
            }
            else if (SwitchView == 2 && loginViewModel.InputString == "00000") // 관리자번호 입력 시 관리자 페이지 진행
            {
                SwitchView = 90;
                LoginViewModel.Init();
            }

            if (SwitchView == 3 && _isChecked01 == false) // 개인정보 동의 미체크 시 Dialog 화면
            {
                SwitchView = 101;
            }

            if (SwitchView == 102 && _isChecked02 == true) // 보험목록 체크 시 Dialog 화면
            {
                LoginViewModel.Init();
                SwitchView = 102;
                _isChecked02 = false;
            }
            else if (SwitchView == 102 && _isChecked02 == false) // 보험목록 미체크 시 Dialog 화면
            { 
                SwitchView = 103;
            }
        }
    }
}
