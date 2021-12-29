﻿using System;
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
            string query = @" SELECT * FROM PHARMACY ";
            OracleDBManager.Instance.ExecuteDsQuery(ds, query);
            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                String name = ds.Tables[0].Rows[idx]["PHARMACY_NAME"].ToString();
                String phone = ds.Tables[0].Rows[idx]["PHARMACY_PHONE"].ToString();
                String address = ds.Tables[0].Rows[idx]["PHARMACY_ADDRESS"].ToString();
                String latitude = ds.Tables[0].Rows[idx]["PHARMACY_LATITUDE"].ToString();
                String logitude = ds.Tables[0].Rows[idx]["PHARMACY_LOGITUDE"].ToString();
                String image = ds.Tables[0].Rows[idx]["PHARMACY_IMAGE"].ToString();
                PHAR_MODEL.Add(new Pharmacy { Name = name, Phone = phone, Address = address, Latitude = latitude, Logitude = logitude, Image = image });
            }
        }




        public void DataSearch()
        {
            DataSet ds = new DataSet();
            string query = @" SELECT * FROM PHARMACY ";
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
                    Image = ds.Tables[0].Rows[idx]["PHARMACY_IMAGE"].ToString()
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
    }
}