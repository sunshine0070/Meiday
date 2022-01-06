﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Meiday.ViewModel;
using Meiday.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Meiday
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowPharmacy01 : UserControl
    {

        public WindowPharmacy01()
        {
            InitializeComponent();

            //약국 데이터 DB에서 불러오기
            PharmacyViewModel.Connect();
            PharmacyViewModel model = new PharmacyViewModel();
            //model.DataSearch();

            //매우 중요 ** 자동화 메서드 **
            ButtonAutomationPeer peer = new ButtonAutomationPeer(pharmacyInfo);

            IInvokeProvider invokeProv =
              peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;


            Log.Debug("WindowPharmacy01 Start!");
            try
            {
                invokeProv.Invoke();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WindowPharmacy01 Invoke Error!!");
            }
        }

        //약국 마커 생성
        private void pharmacyInfo_Click(object sender, RoutedEventArgs e)
        {
            PharmacyViewModel model = new PharmacyViewModel();

            for (int i = 0; i < model.PHAR_MODEL.Count; i++)
            {
                //MessageBox.Show(model.PHAR_MODEL[i].Name);
                string name = model.PHAR_MODEL[i].Name;
                string phone = model.PHAR_MODEL[i].Phone;
                string address = model.PHAR_MODEL[i].Address;
                string latitude = model.PHAR_MODEL[i].Latitude;
                string logitude = model.PHAR_MODEL[i].Logitude;
                string wait = model.PHAR_MODEL[i].WaitPerson;

                //배열저장
                object[] pd = new object[] { name, latitude, logitude, wait };

                Log.Debug("pharmacyInfo_Click Start!");
                try
                {
                    wb.InvokeScript("pharmacyMarker", pd);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "pharmacyInfo_Click Invoke Error!!");
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pharmacy model = new Pharmacy();

            if (((Button)sender).DataContext is Pharmacy)
            {
                model = ((Button)sender).DataContext as Pharmacy;
                //MessageBox.Show(model.Name);
            }

            object[] pd = new object[] { model.Name, model.Latitude, model.Logitude };

            Log.Debug("Button_Click Start!");
            try
            {
                wb.InvokeScript("choice", pd);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Button_Click Invoke Error!!");
            }

        }
    }
}
