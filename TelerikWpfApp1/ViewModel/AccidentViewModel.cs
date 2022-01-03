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
                _accidentTodayDateTime = value;
                OnPropertyChanged(nameof(AccidentTodayDateTime));
            }
        }
        private static DateTime _accidentSelectedDateTime2;

        private static DateTime _accidentSelectedDateTime = DateTime.Now;
        public DateTime AccidentSelectedDateTime
        {
            get
            {
                return _accidentSelectedDateTime;
            }
            set
            {
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
                return (this.checkCommand) ?? (this.checkCommand = new RelayCommand(Check));
            }
        }
        public static bool _isChecked02;
        public void Check()
        {
            foreach (ment ob in SampleDatas)
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
        /// <summary>
        /// 
        /// </summary>



        ObservableCollection<ment> _sampleDatas = null;
        public ObservableCollection<ment> SampleDatas
        {
            get
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
                        SampleDatas.Add(obj);
                    }
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
        public void SendEmail()
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ezsun0070@naver.com", "SNUH_Meiday", Encoding.UTF8);
                // 보내는 계정 주소
                mailMessage.To.Add("hcsong95@naver.com"); // 받는이 메일 주소
                //mailMessage.CC.Add("zzz@naver.com"); // 참조 메일 주소
                mailMessage.Subject = "Meiday_" + patient_id + "_실비청구_서류"; // 제목
                mailMessage.SubjectEncoding = Encoding.UTF8; // 메일 제목 인코딩 타입(UTF-8) 선택
                mailMessage.Body = "사고(발병)일: " + _accidentSelectedDateTime2
                                   + "\n환자번호: " + patient_id
                                   + "\n사고유형: " + _accidentType
                                   + "\n보험사명: " + CheckInsuName2; // 본문
                mailMessage.IsBodyHtml = false; // 본문의 포맷에 따라 선택
                mailMessage.BodyEncoding = Encoding.UTF8; // 본문 인코딩 타입(UTF-8) 선택
                mailMessage.Attachments.Add(new Attachment(new FileStream(@"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.png", FileMode.Open, FileAccess.Read), "test" + patient_id + ".png"));
                mailMessage.Attachments.Add(new Attachment(new FileStream(@"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.pdf", FileMode.Open, FileAccess.Read), "test" + patient_id + ".pdf"));
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string _query;
        public void AccidentSubmit()
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
        }
        public static void AccidentDateSaved()
        {
            _accidentSelectedDateTime2 = _accidentSelectedDateTime;
        }
    }
    public class RadioBoolToAccidentTypeConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
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
            string parameterString = parameter as string;

            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
        #endregion
    }
}