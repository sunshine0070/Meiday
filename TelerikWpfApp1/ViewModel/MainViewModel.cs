using System.Windows.Input;
using System.Windows;
using Telerik.Windows.Controls;
using Meiday.View;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Meiday.Model;
using Meiday.ViewModel;
using static Meiday.AccidentViewModel;
using System.Data;
using System;
using System.Windows.Threading;

namespace Meiday
{
    public class MainViewModel : ViewModelBase
    {
        AccidentViewModel accidentViewModel = new AccidentViewModel();
        LoginViewModel loginViewModel = new LoginViewModel();
        PharmacyViewModel PharmacyViewModel = new PharmacyViewModel();
        PaymentViewModel PaymentViewModel = new PaymentViewModel();
        
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

        public ICommand SwitchViewCommand { get; }
        public ICommand SwitchViewCommand2 => new RelayCommand<object>(OnSwitchView, CheckCanExecuted); // 약국 클릭하면 확대 기능 and 제출하기 버튼 활성화 함수

        public MainViewModel()
        {
            SwitchView = 0;
            sessionTimer.Interval = TimeSpan.FromSeconds(1);
            sessionTimer.Tick += SessionTimer_Tick; // 1번만 실행
            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));
            
        }
        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());
            //SessionTimer_Reset();

            if (SwitchView == 0) // 다음에 하기 로그인 정보 초기화
            {
                LoginViewModel.Init();
                _accidentType = AccidentType.None;
                _isChoice01 = false;
                _isChoice02 = false;
            }
            if (SwitchView >= 1 && SwitchView != 111) // 키패드 화면 들어가면 세션 타이머 시작
            {
                SessionTimer_Reset(); // 화면 갱신때마다 남은 초 초기화
                SessionTimer_Start(); // 화면 갱신때마다 초 세기 시작
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

            if (SwitchView == 4 && _isChecked01 == true) // 개인정보 동의 체크박스 초기화
            {
                _isChecked01 = false;
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
                SwitchView = 109;
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
                SwitchView = 109;
            }
            
            if (SwitchView == 106)
            {
                PharmacyViewModel.PharmacySubmit();
            }

            if (SwitchView == 3)
            {
                PaymentViewModel.PaymentSubmit();
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

        // 세션 타이머 설정 부분
        DispatcherTimer sessionTimer = new DispatcherTimer();
        public void SessionTimer_Start()
        {
            sessionTimer.Start();
        }
        public void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeRemaining -= 1;
            if(TimeRemaining == 16)
            {
                SwitchViewtmp = SwitchView; // 원래 화면 넘버 임시저장
            }
            else if(TimeRemaining <= 15 && TimeRemaining >= 0) // 남은 시간 표시 15초부터
            {
                SwitchView = 111;
            }
            else if(TimeRemaining < 0) // 세션 타임아웃, 타이머 종료
            {
                SwitchView = 0;
                SessionTimer_Reset();
                sessionTimer.Stop();
            }
        }
        public void SessionTimer_Reset()
        {
            TimeRemaining = 180; // 세션 시간(3분)
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
        private int switchViewtmp;
        public int SwitchViewtmp
        {
            get
            {
                return switchViewtmp;
            }
            set
            {
                switchViewtmp = value;
                OnPropertyChanged("SwitchViewtmp");
            }
        }
    }
}
