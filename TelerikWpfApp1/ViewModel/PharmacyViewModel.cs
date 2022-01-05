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
                if (int.Parse(wait) < 3) //접수자수 많은 약국 세곳은 글씨 빨간색으로
                {
                    fontcolor = "red";
                }
                PHAR_MODEL.Add(new Pharmacy { Name = name, Phone = phone, Address = address, Latitude = latitude, Logitude = logitude, Image = image, Email = email, WaitPerson = wait, Fontcolor = fontcolor });
            }
        }

        public void DataSearch()
        {
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
            return true;
        }

        public void CheckButtonExecuted(object Sender)
        {
            if (Sender is Pharmacy)
            {
                selectedmodel = Sender as Pharmacy;
            }
        }

        public static void PharmacySubmit() //MainViewModel에서 확인
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

                    MessageBox.Show("약국이름 : "+ PharmacyViewModel.selectedmodel.Name+ " 처방전 번호 : " + PaymentViewModel.TREATE_NUM[idx]);
                    string query = @"INSERT INTO PHARMACY_RESERVE(PHARMACY_NAME, TREATMENT_NUM) VALUES('" + PharmacyViewModel.selectedmodel.Name + "' , "+  PaymentViewModel.TREATE_NUM[idx] + ")";
                    string query1 = @"commit";
                    OracleDBManager.Instance.ExecuteNonQuery(query);
                    OracleDBManager.Instance.ExecuteNonQuery(query1);

                    MessageBox.Show(oracleDBManager.CheckDBConnected().ToString());
                }
            }
        }
    }
}
