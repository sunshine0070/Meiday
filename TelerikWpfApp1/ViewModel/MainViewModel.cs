using System.Windows.Input;
using System.Windows;
using Telerik.Windows.Controls;
using Meiday.View;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Meiday.Model;
using static Meiday.AccidentViewModel;
using System.Data;

namespace Meiday
{
    public class MainViewModel : ViewModelBase
    {
        AccidentViewModel accidentViewModel = new AccidentViewModel();
        LoginViewModel loginViewModel = new LoginViewModel();
        PharmacyViewModel PharmacyViewModel = new PharmacyViewModel();

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
        private bool _isChoice01 = false;
        private bool _isChoice02 = false;

        public bool IsChecked01
        {
            get => _isChecked01;
            set
            {
                _isChecked01 = value;
                OnPropertyChanged("IsChecked01");
            }
        }


        public bool IsChoice01
        {
            get => _isChoice01;
            set
            {
                _isChoice01 = value;
                OnPropertyChanged("IsChoice01");
            }
        }

        public bool IsChoice02
        {
            get => _isChoice02;
            set
            {
                _isChoice02 = value;
                OnPropertyChanged("IsChoice02");
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
        public ICommand SwitchViewCommand2 => new RelayCommand<object>(OnSwitchView, CheckCanExecuted); // 약국 클릭하면 확대 기능 and 제출하기 버튼 활성화 함수

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
                _accidentType = AccidentType.None;
            }

            if (SwitchView == 2 && loginViewModel.InputString != "00000") // 환자등록번호 입력 시 정상진행
            {
                _isChecked01 = false; // 다시 진행할때 초기화할거 많을듯
                LoginViewModel.Login(); // 순서 ValidCheck() 앞
                if (loginViewModel.ValidCheck())
                {
                    SwitchView = 2;
                }
                else
                {
                    SwitchView = 104;
                    LoginViewModel.Init();
                }
            }
            else if (SwitchView == 2 && loginViewModel.InputString == "00000") // 관리자번호 입력 시 관리자 페이지 진행
            {
                SwitchView = 90;
                LoginViewModel.Init();
            }

            if (SwitchView == 5 && _isChecked01 == false) // 개인정보 동의 미체크 시 Dialog 화면
            {
                SwitchView = 101;
            }

            if (SwitchView == 6 && _accidentType.ToString() == "None") // 사고유형 미체크 시 Dialog 화면
            {
                SwitchView = 107;
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
            if(LoginViewModel.connect_fail_flag == true) // DB에서 환자정보 못받아올 때 화면
            {
                SwitchView = 104;
                LoginViewModel.Init();
                LoginViewModel.connect_fail_flag = false;
            }

            if (SwitchView == 3 && _isChoice01 == false && _isChoice02 == false) 
            {
                SwitchView = 0;
            }

            else if (SwitchView == 3 && _isChoice01 == false && _isChoice02 == true) 
            {
                SwitchView = 4;
            }

            else if (SwitchView == 3 && _isChoice01 == true && _isChoice02 == true)
            {
                SwitchView = 3;
            }


            if (SwitchView == 4 && _isChoice02 == false) 
            {
                SwitchView = 0;
            }
            
            if (SwitchView == 106)
            {
                MessageBox.Show(PharmacyViewModel.selectedmodel.Name);
                MessageBox.Show(PaymentViewModel.TREATE_NUM[0]);
                PharmacyViewModel.PharmacySubmit();

            }

        }


        //약국 선택하기 버튼 활성화 부분
        public bool CheckCanExecuted(object sender)
        {
            bool ret = false;
            if (PharmacyViewModel.selectedmodel != null)
            {
                ret = true;
            }
            return ret;
        }


    }
}
