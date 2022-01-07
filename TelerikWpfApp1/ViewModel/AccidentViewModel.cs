using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using Meiday.Model;
using System.Collections.ObjectModel;
using System.Data;
using static Meiday.LoginViewModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace Meiday
{
    public class AccidentViewModel : ViewModelBase
    {
        private DateTime _accidentTodayDateTime = DateTime.Now;
        public DateTime AccidentTodayDateTime
        {
            get => _accidentTodayDateTime;

            set
            {
                Log.Debug("AccidentTodayDateTime");
                _accidentTodayDateTime = value;
                OnPropertyChanged(nameof(AccidentTodayDateTime));
            }
        }
        private static DateTime _accidentSelectedDateTime2;

        public static DateTime _accidentSelectedDateTime = DateTime.Now;
        public DateTime AccidentSelectedDateTime
        {
            get
            {
                return _accidentSelectedDateTime;
            }
            set
            {
                Log.Debug("AccidentSelectedDateTime");
                _accidentSelectedDateTime = value;
                OnPropertyChanged(nameof(AccidentSelectedDateTime));
            }
        }

        // 실비보험 청구 가능기간 (3년) 제한
        private DateTime _accidentLimitedDateTime = DateTime.Today.AddYears(-3);
        public DateTime AccidentLimitedDateTime
        {
            get => _accidentLimitedDateTime;
            set
            {
                Log.Debug("AccidentLimitedDateTime");
                _accidentLimitedDateTime = value;
                OnPropertyChanged(nameof(AccidentLimitedDateTime));
            }
        }

        public static AccidentType _accidentType;
        public AccidentType AccidentTypes
        {
            get => _accidentType;
            set
            {
                Log.Debug("AccidentTypes");
                if (_accidentType != value)
                {
                    _accidentType = value;
                    OnPropertyChanged("AccidentTypes");
                }
            }
        }

        public DateTime _accidentSubmitDates;
        public DateTime AccidentSubmitDates
        {
            get => _accidentSubmitDates;
            set
            {
                Log.Debug("AccidentSubmitDates");
                _accidentSubmitDates = value;
                OnPropertyChanged("AccidentSubmitDates");
            }
        }

        ment _pa = new ment();
        public string InsuName
        {
            get
            {
                return _pa.InsuName;
            }
            set
            {
                Log.Debug("InsuName");
                if (value != _pa.InsuName)
                {
                    _pa.InsuName = value;
                    OnPropertyChanged("InsuName");
                }
            }
        }
        public string InsuProduct
        {
            get
            {
                return _pa.InsuProduct;
            }
            set
            {
                Log.Debug("InsuProduct");
                if (value != _pa.InsuProduct)
                {
                    _pa.InsuProduct = value;
                    OnPropertyChanged("InsuProduct");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked02
        {
            get { return _pa.IsChecked02; }
            set
            {
                Log.Debug("IsChecked02");
                if (value != _pa.IsChecked02)
                {
                    _pa.IsChecked02 = value;
                    this.OnPropertyChanged("IsChecked02");
                }
            }
        }
        private string check_InsuName = string.Empty;
        private static string CheckInsuName2;
        public string CheckInsuName
        {
            get
            {
                return check_InsuName;
            }
            set
            {
                Log.Debug("CheckInsuName");
                if (value != check_InsuName)
                {
                    check_InsuName = value;
                    OnPropertyChanged("CheckInsuName");
                }
            }
        }

        private ICommand checkCommand;
        public ICommand CheckCommand
        {
            get
            {
                return (this.checkCommand) ?? (this.checkCommand = new RelayCommand(AccCheck));
            }
        }
        public static bool _isChecked02;
        public void AccCheck()
        {
            Log.Debug("AccCheck");
            foreach (ment ob in InsuranceData)
            {
                if (ob.IsChecked02 == true)
                {
                    _isChecked02 = ob.IsChecked02;
                    CheckInsuName = ob.InsuName;
                    CheckInsuName2 = CheckInsuName;
                    break;
                }
                else
                {
                    _isChecked02 = ob.IsChecked02;
                    CheckInsuName = string.Empty;
                }
            }
        }
        private string _selectedInsuranceMail = String.Empty;
        public string selectedInsuranceMail
        { 
            get
            {
                try
                {
                    DataSet ds = new DataSet();
                    string query = @" SELECT i.INSURANCE_MANAGEEMAIL InsuranceMail
                                        FROM INSURANCE i 
                                        JOIN CHECKINSURANCE c ON i.INSURANCE_NUM = c.INSURANCE_NUM 
                                        JOIN PATIENT        p ON p.PT_REGNUM     = c.PT_REGNUM
                                        WHERE p.pt_idnum =  " + patient_id + "or p.pt_regnum = " + patient_id;
                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                    _selectedInsuranceMail = ds.Tables[0].Rows[0]["InsuranceMail"].ToString();
                    Log.Debug("selectedInsuranceMail");
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "selectedInsuranceMail");
                }
                return _selectedInsuranceMail;
            }
            set
            {
                if (value != _selectedInsuranceMail)
                {
                    _selectedInsuranceMail = value;
                    OnPropertyChanged("selectedInsuranceMail");
                }
            }
        }
        private string _insuranceSequence;
        public string InsuranceSequence
        {
            get
            {
                try
                {
                    DataSet ds = new DataSet();
                    string query = @"select p.PT_NAME data_Name, INSURANCE_SEQ.nextval SeqSubmit
                                  from patient p
                                  where p.pt_idnum = " + patient_id + "or p.pt_regnum = " + patient_id;
                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                    _insuranceSequence = ds.Tables[0].Rows[0]["SeqSubmit"].ToString();
                    Log.Debug("InsuranceSequence");
                }
                catch(Exception ex)
                {
                    Log.Fatal(ex, "InsuranceSequence");
                }
                return _insuranceSequence;
            }
            set
            {
                if (value != _insuranceSequence)
                {
                    _insuranceSequence = value;
                    OnPropertyChanged("InsuranceSequence");
                }
            }
        }


        ObservableCollection<ment> _sampleDatas = null;
        public ObservableCollection<ment> InsuranceData
        {
            get
            {
                try
                {
                    if (_sampleDatas == null)
                    {
                        _sampleDatas = new ObservableCollection<ment>();
                        DataSet ds = new DataSet();

                        string query = @" SELECT i.INSURANCE_NAME InsuName, i.INSURANCE_PRODUCT InsuProduct
                                        FROM INSURANCE i 
                                        JOIN CHECKINSURANCE c ON i.INSURANCE_NUM = c.INSURANCE_NUM 
                                        JOIN PATIENT        p ON p.PT_REGNUM     = c.PT_REGNUM
                                        WHERE p.pt_idnum =  " + patient_id + "or p.pt_regnum = " + patient_id;
                        OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                        for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                        {
                            ment obj = new ment()
                            {
                                InsuName = ds.Tables[0].Rows[idx]["InsuName"].ToString(),
                                InsuProduct = ds.Tables[0].Rows[idx]["InsuProduct"].ToString(),
                            };
                            InsuranceData.Add(obj);
                        }
                    }
                    Log.Debug("InsuranceData");
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "InsuranceData");
                }
                return _sampleDatas;
            }
            set
            {
                _sampleDatas = value;
                OnPropertyChanged("_sampleDatas");
            }
        }

        // 보험 EMAIL 보내기
        public void AccidentSendEmail()
        {
            try
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ezsun0070@naver.com", "SNUH_Meiday", Encoding.UTF8);
                // 보내는 계정 주소
                mailMessage.To.Add(selectedInsuranceMail); // 받는이 메일 주소
                //mailMessage.To.Add("hcsong95@naver.com"); // 받는이 메일 주소
                //mailMessage.CC.Add("zzz@naver.com"); // 참조 메일 주소
                mailMessage.Subject = "Meiday_실비청구_서류_제출번호(" + InsuranceSequence + ")" + loginViewModel.PatientName + "_" + CheckInsuName2; // 제목
                mailMessage.SubjectEncoding = Encoding.UTF8; // 메일 제목 인코딩 타입(UTF-8) 선택
                mailMessage.Body = "사고(발병)일: " + _accidentSelectedDateTime2
                                   + "\n사고유형: " + _accidentType
                                   + "\n환자번호: " + patient_id
                                   + "\n보험사명: " + CheckInsuName2; // 본문
                mailMessage.IsBodyHtml = false; // 본문의 포맷에 따라 선택
                mailMessage.BodyEncoding = Encoding.UTF8; // 본문 인코딩 타입(UTF-8) 선택
                mailMessage.Attachments.Add(new Attachment(new FileStream(@"C:\Users\user\Desktop\savefile\" + loginViewModel.PatientName + "_전자처방전.pdf", FileMode.Open, FileAccess.Read), loginViewModel.PatientName + "_전자처방전(보험용)" + ".pdf"));
                // 파일 첨부
                SmtpClient SmtpServer = new SmtpClient("smtp.naver.com");
                // SMTP 서버 주소
                SmtpServer.Port = 587;
                // SMTP 포트
                SmtpServer.EnableSsl = true; // SSL 사용 여부
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new NetworkCredential("ezsun0070", "1q2w3e4r!");
                SmtpServer.Send(mailMessage);
                Log.Debug("AccidentSendEmail");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AccidentSendEmail");
            }
        }
        private string _query;
        public void AccidentSubmit()
        {
            try
            {
                if(patient_id.Length == 5) // PT_IDNUM
                {
                    _query = @"INSERT INTO 
                                INSURANCE_SUBMIT (SUBMIT_NUM, ACCIDENT_DATE, ACCIDENT_SUBMITDATE, ACCIDENT_TYPE, INSURANCE_NAME, PT_IDNUM)
                                VALUES (SUBMIT_NUM.nextval, '#AccidentDate', '#AccidentSubmitDate', '#AccidentType', '#InsuranceName', " + patient_id + ")";
                }
                else if(patient_id.Length == 13) // PT_REGNUM
                {
                    _query = @"INSERT INTO 
                                INSURANCE_SUBMIT (SUBMIT_NUM, ACCIDENT_DATE, ACCIDENT_SUBMITDATE, ACCIDENT_TYPE, INSURANCE_NAME, PT_REGNUM)
                                VALUES (SUBMIT_NUM.nextval, '#AccidentDate', '#AccidentSubmitDate', '#AccidentType', '#InsuranceName', " + patient_id + ")";
                }
                string query1 = @"commit";
                _query = _query.Replace("#AccidentDate", _accidentSelectedDateTime2.ToString());
                _query = _query.Replace("#AccidentSubmitDate", _accidentTodayDateTime.ToString());
                _query = _query.Replace("#AccidentType", _accidentType.ToString());
                _query = _query.Replace("#InsuranceName", CheckInsuName2);
                OracleDBManager.Instance.ExecuteNonQuery(_query);
                OracleDBManager.Instance.ExecuteNonQuery(query1);
                Log.Debug("AccidentSubmit");
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "AccidentSubmit");
            }
        }
        public static void AccidentDateSaved()
        {
            Log.Debug("AccidentDateSaved");
            _accidentSelectedDateTime2 = _accidentSelectedDateTime;
        }
    }
    public class RadioBoolToAccidentTypeConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Log.Debug("RadioConvert");
            string parameterString = parameter as string;

            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Log.Debug("RadioConvertBack");
            string parameterString = parameter as string;

            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
        #endregion
    }
}