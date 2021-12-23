using System.Windows.Input;
using System.ComponentModel;
using Meiday.Model;
using System.Data;
using System.Collections.ObjectModel;
using System;
using System.Windows;

namespace Meiday
{
    public class LoginViewModel : ViewModelBase
    {
        insuaranceM _pa = new insuaranceM();


        //아래 두 필드는 속성으로 구현되어 있다. //출력될 문자들을 담아둘 변수
        static string inputString = string.Empty;
        //계산기 화면의 출력 텍스트박스에 대응되는 필드
        string displayText = "";
        public static string patient_id = "";
        public static bool connect_fail_flag = false; //

        //생성자, 명령 객체들을 초기화 //명령 객체들은 UI쪽 버튼의 Command에 바인딩되어 있다.
        public LoginViewModel()
        {
            //이벤트 핸들러 정의 
            //숫자 버튼을 클릭할 때 실행
            this.Append = new Append(this);
            //백 스페이스 버튼을 클릭할 때 실행, 한글자 삭제
            this.BackSpace = new BackSpace(this);
        }
        public string PatientName
        {

            get
            {
                DataSet ds = new DataSet();
                string query = @" select a.PT_NAME data_Name
                              from patient a
                              where a.pt_idnum = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    connect_fail_flag = true;
                }
                else
                {
                    _pa.patientName = ds.Tables[0].Rows[0]["data_Name"].ToString();
                }
                /*try
                {
                    _pa.patientName = ds.Tables[0].Rows[0]["data_Name"].ToString();
                }
                catch (Exception ex)
                {
                    connect_fail_flag = true;
                    MessageBox.Show("hi");

                }*/

                return _pa.patientName;
            }
            set
            {
                if (value != _pa.patientName)
                {

                    _pa.patientName = value; // query
                    OnPropertyChanged("PatientName");
                }
            }

        }

        public string InputString
        {
            set
            {
                if (inputString != value)
                {
                    inputString = value;
                    OnPropertyChanged("InputString");
                    if (value != "")
                    {
                        DisplayText = value;
                    }
                }
            }
            get { return inputString; }
        }

        /// <summary> 
        ///  계산기의 출력창과 바인딩된 속성 
        ///  </summary>

        public string DisplayText // oneway
        {
            internal set
            {
                if (displayText != value)
                {
                    /*                    displayText = value;*/
                    displayText = value;
                    if (displayText.Length > 6)
                    {
                        displayText = displayText.Substring(0, 6) + '-' + displayText.Substring(6);
                    }
                    OnPropertyChanged("DisplayText");
                }
            }
            get
            {
                return displayText;
            }
        }
        public ICommand Append { protected set; get; }
        public ICommand BackSpace { protected set; get; }

        /*private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return (this.loginCommand) ?? (this.loginCommand = new RelayCommand(Login));
            }
        }*/
        static public void Login()
        {
            patient_id = inputString;
        }

        /*private ICommand initCommand;
        public ICommand InitCommand
        {
            get
            {
                return (this.initCommand) ?? (this.initCommand = new RelayCommand(Init));
            }
        }*/
        static public void Init()
        {
            inputString = "";
        }


        /*
                private ICommand connectCommand;
                public ICommand ConnectCommand
                {
                    get
                    {
                        return (this.connectCommand) ?? (this.connectCommand = new RelayCommand(Connect));
                    }
                }
                public void Connect()
                { //Connect to DB
                    if (OracleDBManager.Instance.GetConnection() == false)
                    {
                        string msg = $"Failed to Connect to Database";
                        MessageBox.Show(msg, "Error");
                        return;
                    }
                    else
                    {
                        string msg = $"Success to Connect to Database";
                        MessageBox.Show(msg, "Inform");
                    }
                }*/
    }
}
