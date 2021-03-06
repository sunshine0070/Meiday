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
        ment _pa = new ment();
        PatientModel patientModel = new PatientModel();
        //MainViewModel mainViewModel = new MainViewModel();

        //아래 두 필드는 속성으로 구현되어 있다. //출력될 문자들을 담아둘 변수
        static string inputString = string.Empty;
        private const int maxLength = 14; // 번호입력 항목 제한
        static string managerinputString = string.Empty;
        //계산기 화면의 출력 텍스트박스에 대응되는 필드
        string displayText = "";
        string managerdisplayText = "";
        // patient_id는 PT_IDNUM or PT_REGNUM 둘 중 하나로 입력해도 가능
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
        private string ptRegnum = string.Empty;
        public string PtRegnum
        {
            get
            {
                DataSet ds = new DataSet();
                string query = @" select p.PT_REGNUM pt_regnum
                              from patient p
                              where p.pt_idnum = " + patient_id + "or p.pt_regnum = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                ptRegnum = ds.Tables[0].Rows[0]["pt_regnum"].ToString();
                return ptRegnum;
            }
            set
            {
                Log.Debug("PtRegnum");
                if (value != ptRegnum)
                {
                    ptRegnum = value;
                    OnPropertyChanged("PtRegnum");
                }
            }
        }
        public string PtPhone
        {
            get
            {
                DataSet ds = new DataSet();
                string query = @" select p.PT_PHONE pt_phone
                              from patient p
                              where p.pt_idnum = " + patient_id + "or p.pt_regnum = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                patientModel.Phone = ds.Tables[0].Rows[0]["pt_phone"].ToString();
                return patientModel.Phone;
            }
            set
            {
                Log.Debug("PtPhone");
                if (value != patientModel.Phone)
                {
                    patientModel.Phone = value;
                    OnPropertyChanged("PtPhone");
                }
            }
        }


        public string PatientName
        {
            get
            {
                DataSet ds = new DataSet();
                string query = @" select p.PT_NAME data_Name
                              from patient p
                              where p.pt_idnum = " + patient_id + "or p.pt_regnum = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                _pa.patientName = ds.Tables[0].Rows[0]["data_Name"].ToString();
                return _pa.patientName;
            }
            set
            {
                Log.Debug("PatientName");
                if (value != _pa.patientName)
                {
                    _pa.patientName = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }

        public string InputString
        {
            set
            {
                Log.Debug("InputString");
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

        public string ManagerInputString
        {
            set
            {
                Log.Debug("ManagerInputString");
                if (managerinputString != value)
                {
                    managerinputString = value;
                    OnPropertyChanged("ManagerInputString");
                    if (value != "")
                    {
                        ManagerDisplayText = value;
                    }
                }
            }
            get { return managerinputString; }
        }
        /// <summary> 
        ///  계산기의 출력창과 바인딩된 속성 
        ///  </summary>

        public string DisplayText
        {
            internal set
            {
                Log.Debug("DisplayText");
                if (displayText != value)
                {
                    displayText = value;
                    if (displayText.Length > 6)
                    {
                        displayText = displayText.Substring(0, 6) + '-' + displayText.Substring(6);
                    }
                    if(displayText.Length > maxLength)
                    {
                        displayText = displayText.Substring(0, maxLength);
                    }
                    OnPropertyChanged("DisplayText");
                }
            }
            get
            {
                return displayText;
            }
        }
        public string ManagerDisplayText
        {
            internal set
            {
                Log.Debug("ManagerDisplayText");
                if (managerdisplayText != value)
                {
                    managerdisplayText = value;
                    OnPropertyChanged("ManagerDisplayText");
                }
            }
            get
            {
                return managerdisplayText;
            }
        }

        public ICommand Append { protected set; get; }
        public ICommand BackSpace { protected set; get; }

        static public void Login()
        {
            Log.Debug("Login");
            patient_id = inputString;
        }

        static public void ManagerLogin()
        {
            Log.Debug("ManagerLogin");
            patient_id = managerinputString;
        }

        public static void LoginInit()
        {
            Log.Debug("LoginInit");
            inputString = "";
            managerinputString = "";
        }
        private string validCheck;
        public bool ValidCheck() //MainViewModel에서 확인
        {

            DataSet ds = new DataSet();
            string query = @" select count(*) PT_COUNT
                              from patient p
                              where p.pt_idnum = " + patient_id + "or p.pt_regnum = " + patient_id;

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            try
            {
                validCheck = ds.Tables[0].Rows[0]["PT_COUNT"].ToString();
                Log.Debug("ValidCheck");
                return int.Parse(validCheck) > 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ValidCheck");
                return false;
            }
        }

        private string manageCheck;
        public bool ManageCheck()
        {
            DataSet ds = new DataSet();
            string query = @" select count(*) MANAGE_COUNT
                              from doctor d
                              where d.DR_LICENSE = " + patient_id;
            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            try
            {
                manageCheck = ds.Tables[0].Rows[0]["MANAGE_COUNT"].ToString();
                Log.Debug("ManageCheck");
                return int.Parse(manageCheck) > 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ManageCheck");
                return false;
            }
        }

        string validInsuCheck;
        public bool ValidInsuCheck() //MainViewModel에서 확인
        {

            DataSet ds = new DataSet();
            string query = @" SELECT  count(*) InsuCount
                              FROM    insurance       i
                                     ,patient         p
                                     ,checkinsurance  c
                              WHERE   p.pt_regnum     = c.pt_regnum
                              AND     i.insurance_num = c.insurance_num
                              AND     p.pt_idnum =" + patient_id + "or p.pt_regnum = " + patient_id;

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            try
            {
                validInsuCheck = ds.Tables[0].Rows[0]["InsuCount"].ToString();
                Log.Debug("ValidInsuCheck");
                return int.Parse(validInsuCheck) > 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ValidInsuCheck");
                return false;
            }
        }
    }
}
