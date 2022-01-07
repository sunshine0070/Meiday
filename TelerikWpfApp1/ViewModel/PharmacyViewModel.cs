using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
using Meiday.Models;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Web;
using System.Reflection;
using Meiday.ViewModel;
using System.Net.Mail;
using static Meiday.LoginViewModel;

namespace Meiday
{
    public class PharmacyViewModel : Pharmacy
    {
        public ICommand CheckCommand => new RelayCommand<object>(CheckButtonExecuted, CheckCanExecuted); // 약국 클릭하면 확대 기능 and 제출하기 버튼 활성화
        public ObservableCollection<Pharmacy> PHAR_MODEL { get; set; }
        public static Pharmacy selectedmodel { get; set; }

        //생성자 생성하기 //자동으로 불러지는 데이터
        public PharmacyViewModel()
        {
            Log.Debug("PharmacyViewModel");
            this.PHAR_MODEL = new ObservableCollection<Pharmacy>();
            DataSet ds = new DataSet();
            string query = @" SELECT * FROM PHARMACY_WAIT";
            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                String name = ds.Tables[0].Rows[idx]["PHARMACY_NAME"].ToString();
                String phone = ds.Tables[0].Rows[idx]["PHARMACY_PHONE"].ToString();
                String address = ds.Tables[0].Rows[idx]["PHARMACY_ADDRESS"].ToString();
                String latitude = ds.Tables[0].Rows[idx]["PHARMACY_LATITUDE"].ToString();
                String logitude = ds.Tables[0].Rows[idx]["PHARMACY_LOGITUDE"].ToString();
                String image = ds.Tables[0].Rows[idx]["PHARMACY_IMAGE"].ToString();
                String email = ds.Tables[0].Rows[idx]["PHARMACY_EMAIL"].ToString();
                String wait = ds.Tables[0].Rows[idx]["WAIT_PERSON"].ToString();
                String fontcolor = "ForestGreen";
                if (int.Parse(wait) >= 10) //접수자수 많은 약국 세곳은 글씨 빨간색으로
                {
                    fontcolor = "red";
                } else if (int.Parse(wait) >= 5 && int.Parse(wait) < 9)
                {
                    fontcolor = "orange";
                }
                else
                {
                    fontcolor = "ForestGreen";
                }
                PHAR_MODEL.Add(new Pharmacy { Name = name, Phone = phone, Address = address, Latitude = latitude, Logitude = logitude, Image = image, Email = email, WaitPerson = wait, Fontcolor = fontcolor });
            }
            //로그 기록남기기
        }

        public void DataSearch()
        {
            Log.Debug("DataSearch");
            DataSet ds = new DataSet();
            string query = @" SELECT * FROM PHARMACY_WAIT ";
            OracleDBManager.Instance.ExecuteDsQuery(ds, query);

            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                Pharmacy obj = new Pharmacy
                {
                    Name = ds.Tables[0].Rows[idx]["PHARMACY_NAME"].ToString(),
                    Phone = ds.Tables[0].Rows[idx]["PHARMACY_PHONE"].ToString(),
                    Address = ds.Tables[0].Rows[idx]["PHARMACY_ADDRESS"].ToString(),
                    Latitude = ds.Tables[0].Rows[idx]["PHARMACY_LATITUDE"].ToString(),
                    Logitude = ds.Tables[0].Rows[idx]["PHARMACY_LOGITUDE"].ToString(),
                    Image = ds.Tables[0].Rows[idx]["PHARMACY_IMAGE"].ToString(),
                    Email = ds.Tables[0].Rows[idx]["PHARMACY_EMAIL"].ToString(),
                    WaitPerson = ds.Tables[0].Rows[idx]["WAIT_PERSON"].ToString()
            };
                PHAR_MODEL.Add(obj);
            }
        }
        #region
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return (this.connectCommand) ?? (this.connectCommand = new DelegateCommand(Connect));
            }
        }

        private ICommand selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                return (this.selectCommand) ?? (this.selectCommand = new DelegateCommand(DataSearch));
            }
        }

        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return (this.loadedCommand) ?? (this.loadedCommand = new DelegateCommand(LoadEvent));
            }
        }
        #endregion
        #region
        public static void Connect()
        { //Connect to DB
            if (OracleDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                //string msg = $"Success to Connect to Database";
                //MessageBox.Show(msg, "Inform");
            }
        }
        private void LoadEvent()
        { //Connect to DB
            if (OracleDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                //string msg = $"Success to Connect to Database";
                //MessageBox.Show(msg, "Inform");
            }
        }
        #endregion
        // RelayCommand 파트
        public bool CheckCanExecuted(object sender)
        {
            Log.Debug("CheckCanExecuted");
            return true;
        }

        public void CheckButtonExecuted(object Sender)
        {
            if (Sender is Pharmacy)
            {
                selectedmodel = Sender as Pharmacy;
            }
            Log.Debug("CheckButtonExecuted");
        }
        private string _pharmacySequence;
        public string PharmacySequence
        {
            get
            {
                Log.Debug("PharmacySequence");

                DataSet ds = new DataSet();
                string query = @"select p.PT_NAME data_Name, PHARMACY_SEQ.nextval SeqSubmit
                              from patient p
                              where p.pt_idnum = " + patient_id + "or p.pt_regnum = " + patient_id;
                OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                try
                {
                    _pharmacySequence = ds.Tables[0].Rows[0]["SeqSubmit"].ToString();
                }catch (Exception ex)
                {
                    Log.Fatal(ex, "PharmacySequence");
                }
                return _pharmacySequence;
            }
            set
            {
                if (value != _pharmacySequence)
                {
                    _pharmacySequence = value;
                    OnPropertyChanged("PharmacySequence");
                }
                Log.Debug("_pharmacySequence");
            }
        }

        public static void PharmacySubmit() //MainViewModel에서 확인
        {
            //로그 기록남기기
            Log.Debug("PharmacySubmit");
            try
            {
                //MessageBox.Show(PaymentViewModel.TREATE_NUM.Count.ToString());
                OracleDBManager oracleDBManager = new OracleDBManager();
                oracleDBManager.GetConnection();
                if (PaymentViewModel.TREATE_NUM.Count > 0)
                {
                    //insert 할 때 ; 끝에 붙이면 안됨 
                    //MessageBox.Show("db 조건문 진입");
                    for (int idx = 0; idx < PaymentViewModel.TREATE_NUM.Count; idx++)
                    {

                        //MessageBox.Show("약국이름 : "+ selectedmodel.Name+ " 처방전 번호 : " + PaymentViewModel.TREATE_NUM[idx]);
                        string query = @"INSERT INTO PHARMACY_RESERVE(PHARMACY_NAME, TREATMENT_NUM) VALUES('" + PharmacyViewModel.selectedmodel.Name + "' , "+  PaymentViewModel.TREATE_NUM[idx] + ")";
                        string query1 = @"commit";
                        OracleDBManager.Instance.ExecuteNonQuery(query);
                        OracleDBManager.Instance.ExecuteNonQuery(query1);
                        //MessageBox.Show(oracleDBManager.CheckDBConnected().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex,"PharmacySubmit");
            }
        }

        public void Pharmacy_SendEmail()
        {
            //로그 기록남기기
            Log.Debug("Pharmacy_SendEmail");
            try
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.naver.com");
                mail.From = new MailAddress("ezsun0070@naver.com", "SNUH_Meiday", Encoding.UTF8);
                mail.To.Add(selectedmodel.Email);
                //mail.To.Add("hcsong95@naver.com");
                mail.Subject = "Meiday_약국처방전_서류_제출번호(" + PharmacySequence + ")" + loginViewModel.PatientName + "_" + selectedmodel.Name; // 제목
                mail.Body = "Patient_ID(pdf_pw): " + patient_id;
                Attachment attachment;
                attachment = new Attachment(@"C:\Users\user\Desktop\savefile\" + loginViewModel.PatientName + "_전자처방전.pdf");
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("ezsun0070", "1q2w3e4r!");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Pharmacy_SendEmail");
            }
        }
    }
}
