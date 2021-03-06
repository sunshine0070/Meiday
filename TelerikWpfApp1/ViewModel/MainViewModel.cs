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
using static Meiday.PaymentViewModel;
using System.Data;
using System;
using System.Windows.Threading;
using System.Net.Mail;


namespace Meiday
{
    public class MainViewModel : ViewModelBase
    {
        AccidentViewModel accidentViewModel = new AccidentViewModel();
        LoginViewModel loginViewModel = new LoginViewModel();
        PharmacyViewModel pharmacyViewModel = new PharmacyViewModel();
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
                Log.Debug("SwitchView");
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }


        private bool _isChecked01 = false;
        private bool _isChoice01 = false;
        private bool _isChoice02 = false;
        private bool _prescription_Choice01 = false;
        private bool _prescription_Choice02 = false;

        private bool _isPayChoice = false;

        public bool IsChecked01
        {
            get => _isChecked01;
            set
            {
                Log.Debug("IsChecked01");
                _isChecked01 = value;
                OnPropertyChanged("IsChecked01");
            }
        }


        public bool IsChoice01
        {
            get => _isChoice01;
            set
            {
                Log.Debug("PharmacyChoice(IsChoice01)");
                _isChoice01 = value;
                OnPropertyChanged("IsChoice01");
            }
        }

        public bool IsChoice02
        {
            get => _isChoice02;
            set
            {
                Log.Debug("InsuranceChoice(IsChoice02)");
                _isChoice02 = value;
                OnPropertyChanged("IsChoice02");
            }
        }

        public bool PrescriptionChoice01
        {
            get => _prescription_Choice01;
            set
            {
                Log.Debug("PrescriptionChoice01");
                _prescription_Choice01 = value;
                OnPropertyChanged("PrescriptionChoice01");
            }
        }

        public bool PrescriptionChoice02
        {
            get => _prescription_Choice02;
            set
            {
                Log.Debug("PrescriptionChoice02");
                _prescription_Choice02 = value;
                OnPropertyChanged("PrescriptionChoice02");
            }
        }


        public bool IsPayChoice
        {
            get => _isPayChoice;
            set
            {
                Log.Debug("IsPayChoice");
                _isPayChoice = value;
                OnPropertyChanged("IsPayChoice");
            }
        }


        public ICommand SwitchViewCommand { get; }
        public ICommand SwitchViewCommand2 => new RelayCommand<object>(OnSwitchView, CheckCanExecuted); // 약국 클릭하면 확대 기능 and 제출하기 버튼 활성화 함수
        public ICommand SwitchViewCommand3 => new RelayCommand<object>(OnSwitchView, CheckCanExecuted1);
        public MainViewModel()
        {
            Log.Debug("MainViewModel");
            SwitchView = 0;
            sessionTimer.Interval = TimeSpan.FromSeconds(1);
            sessionTimer.Tick += SessionTimer_Tick; // 1번만 실행
            endPageTimer.Interval = TimeSpan.FromSeconds(1);
            endPageTimer.Tick += EndPageTimer_Tick; // 1번만 실행
            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));

        }
        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());
            Log.Debug("OnSwitchView");

            if (SwitchView == 0) // 종료 case01, 다음에 하기 로그인 정보 초기화
            {
                ViewInit();
                ret = false;
                ret1 = false;
                pay_end_flag = false;
                PharmacyViewModel.selectedmodel = null;
                endPageTimer.Stop();
                EndPageTimer_Reset();
            }
            if(SwitchView == 1)
            {
                LoginViewModel.LoginInit();
            }
            if (SwitchView >= 1 && SwitchView != 111) // 키패드 화면 들어가면 세션 타이머 시작
            {
                SessionTimer_Reset(); // 화면 갱신때마다 남은 초 초기화
                SessionTimer_Start(); // 화면 갱신때마다 초 세기 시작
            }
            if (SwitchView == 2 && loginViewModel.InputString != "00000") // 환자등록번호 입력 시 정상진행
            {
                _isChecked01 = false;
                LoginViewModel.Login(); // 순서 ValidCheck() 앞
                if (loginViewModel.ValidCheck())
                {
                    SwitchView = 2;
                    //PaymentViewModel.Select_Price();
                }
                else
                {
                    SwitchView = 104;
                    LoginViewModel.LoginInit();
                }
            }
            else if (SwitchView == 2 && loginViewModel.InputString == "00000") // 00000 입력 시 관리자 입력 모드 진행
            {
                SwitchView = 94;
                LoginViewModel.LoginInit();
            }
            if (SwitchView == 99) // 관리자번호 제대로 입력 시 창 닫히고 관리자 페이지 생성
            {
                LoginViewModel.Login();
                if (loginViewModel.ManageCheck())
                {
                    SwitchView = 90; // 90 AdminMainControl.xaml.cs에 close 메소드 호출
                    AdminDashboard adminDashboard = new AdminDashboard();
                    adminDashboard.ShowDialog();
                }
                else
                {
                    LoginViewModel.LoginInit();
                    SwitchView = 114;
                }
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
            else if (SwitchView == 6)
            {
                AccidentDateSaved();
                LoginViewModel.Login();
                accidentViewModel.AccCheck();

                if (loginViewModel.ValidInsuCheck() == false)
                {
                    SwitchView = 113;
                    LoginViewModel.LoginInit();
                }
                else
                {
                    SwitchView = 6;
                }
            }

            if (SwitchView == 102 && _isChecked02 == true) // 보험목록 체크 시 Dialog 화면
            {
                LoginViewModel.LoginInit();
                accidentViewModel.AccidentSubmit();
                _isChecked02 = false;
                accidentViewModel.AccidentSendEmail();
                if (accident_email_fail_flag == true)
                {
                    SwitchView = 116;
                    accident_email_fail_flag = false;
                }

            }
            else if (SwitchView == 102 && _isChecked02 == false) // 보험목록 미체크 시 Dialog 화면
            {
                SwitchView = 103;
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
                pharmacyViewModel.Pharmacy_SendEmail();
                if(pharmacy_email_fail_flag == true)
                {
                    SwitchView = 115;
                    pharmacy_email_fail_flag = false;
                }
            }


            if (SwitchView == 108 && _isPayChoice == true)
            {
                SwitchView = 112;
                _isPayChoice = false;
            }

            if (SwitchView == 109)
            {
                ReceiptControl receiptControl = new ReceiptControl();
                //receiptControl.SendReceipt();
                EndPageTimer_Reset();
                EndPageTimer_Start();
            }
            
            if (SwitchView == 112 && (PrescriptionChoice01 == true || PrescriptionChoice02 == true)) // 결제 다음 눌렀을 때 넘어가면 결제할거리 사라짐
            {
                //PaymentViewModel.PaymentSubmit();
            }
            
            if (SwitchView == 112 && PrescriptionChoice01 == false && PrescriptionChoice02 == false) // 처방전 수령방법 체크 아무것도 안했을때
            {
                SwitchView = 117;
            }
        }

        static public bool pharmacy_email_fail_flag = false; // 약국 메일 전송 실패 경우 사용
        static public bool accident_email_fail_flag = false; // 보험 메일 전송 실패 경우 사용
        private bool ret = false;
        private bool ret1 = false;
        //약국 선택하기 버튼 활성화 부분
        public bool CheckCanExecuted(object sender)
        {
            if (PharmacyViewModel.selectedmodel != null)
            {
                ret = true;
            }
            return ret;
        }

        public bool CheckCanExecuted1(object sender)
        {
            if (pay_end_flag == true)
            {
                ret1 = true;
            }
            return ret1;
        }

        // 세션 타이머 설정 부분
        DispatcherTimer sessionTimer = new DispatcherTimer();
        public void SessionTimer_Start()
        {
            Log.Debug("SessionTimer_Start");
            sessionTimer.Start();
        }
        public void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeRemaining -= 1;
            if (TimeRemaining == 16)
            {
                SwitchViewtmp = SwitchView; // 원래 화면 넘버 임시저장
            }
            else if (TimeRemaining <= 15 && TimeRemaining >= 0) // 남은 시간 표시 15초부터
            {
                SwitchView = 111;
            }
            else if (TimeRemaining < 0) // 종료 case02, 세션 타임아웃, 타이머 종료
            {
                SwitchView = 0;
                SessionTimer_Reset();
                sessionTimer.Stop();
                ViewInit();
            }
        }
        public void SessionTimer_Reset()
        {
            Log.Debug("SessionTimer_Reset");
            TimeRemaining = 300; // 세션 시간(5분)
        }

        DispatcherTimer endPageTimer = new DispatcherTimer();
        private void EndPageTimer_Start()
        {
            Log.Debug("EndPageTimer_Start");
            endPageTimer.Start();
        }

        private void EndPageTimer_Tick(object sender, EventArgs e)
        {
            EndPageTimeRemaining -= 1;
            if (EndPageTimeRemaining < 0) // 종료 case03, 세션 타임아웃, 타이머 종료, 초기화 (SwithView ==0 연동)
            {
                SwitchView = 0;
                EndPageTimer_Reset();
                endPageTimer.Stop();
                ViewInit();
            }
        }
        private void EndPageTimer_Reset()
        {
            Log.Debug("EndPageTimer_Reset");
            EndPageTimeRemaining = 7; // 마지막 페이지 세션 시간 7초
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
                //Log.Debug("TimeRemaining");
                timeRemaining = value;
                OnPropertyChanged("TimeRemaining");
            }
        }

        private int endPagetimeRemaining;
        public int EndPageTimeRemaining
        {
            get
            {
                return endPagetimeRemaining;
            }
            set
            {
                Log.Debug("EndPageTimeRemaining");
                endPagetimeRemaining = value;
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
                Log.Debug("SwitchViewtmp");
                switchViewtmp = value;
                OnPropertyChanged("SwitchViewtmp");
            }
        }

        private void ViewInit()
        {
            Log.Debug("ViewInit");
            LoginViewModel.LoginInit();
            _accidentType = AccidentType.None;
            _accidentSelectedDateTime = DateTime.Now;
            _isChoice01 = false;
            _isChoice02 = false;
            _prescription_Choice01 = false;
            _prescription_Choice02 = false;
        }
    }
}
