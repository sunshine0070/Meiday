using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Windows.Input;
using System.Data;
using System.Globalization;
using Meiday.Model;
using Meiday.View;
using static Meiday.LoginViewModel;
using Meiday.ViewModel;
using System.Windows.Controls;

namespace Meiday
{
    public class PaymentViewModel : ViewModelBase
    {
        Total_Price tot_price = new Total_Price();


        public static List<string> TREATE_NUM;

        payment _pa = new payment();
        // MainWindow 객체 선언

        public string Name
        {
            get { return _pa.Name; }
            set
            {
                if (value != _pa.Name)
                {
                    _pa.Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public string Doctor
        {
            get { return _pa.Doctor; }
            set
            {
                if (value != _pa.Doctor)
                {
                    _pa.Doctor = value;
                    this.OnPropertyChanged("Doctor");
                }
            }
        }


        public string Group
        {
            get { return _pa.Group; }
            set
            {
                if (value != _pa.Group)
                {
                    _pa.Group = value;
                    this.OnPropertyChanged("Group");
                }
            }
        }

        public string Date
        {
            get { return _pa.Date; }
            set
            {
                if (value != _pa.Date)
                {
                    _pa.Date = value;
                    this.OnPropertyChanged("Date");
                }
            }
        }

        public bool Checked
        {
            get { return _pa.Checked; }
            set
            {
                if (value != _pa.Checked)
                {
                    //Select_Price();
                    _pa.Checked = value;
                    this.OnPropertyChanged("Checked");
                }
            }
        }

        public string Price
        {
            get { return _pa.Price; }
            set
            {
                if (value != _pa.Price)
                {
                    _pa.Price = value;
                    this.OnPropertyChanged("Price");
                }
            }
        }

        public string Total_Price
        {
            get { return tot_price.total_price; }
            set
            {
                if (value != tot_price.total_price)
                {

                    tot_price.total_price = value;
                    this.OnPropertyChanged("Total_Price");
                    //Select_Price();
                }
            }
        }

        ObservableCollection<payment> _sampleDatas = null;
        public ObservableCollection<payment> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<payment>();
                    DataSet ds = new DataSet();
                    /*
                    string query = @" select a.PT_NAME data_Name , c.DR_NAME data_Doctor , d.DR_DEPTNAME data_Depart, to_char(b.TREATMENT_TIME,'yyyy-mm-dd') data_Date, b.TREATMENT_PAY data_Pay
                              from patient a, treatment b , doctor c , department d
                              where a.pt_idnum ="+ patient_id + "and a.PT_REGNUM = b.PT_REGNUM  and a.PT_REGNUM = b.PT_REGNUM and c.DR_DEPTNUM = d.DR_DEPTNUM ";
                    */

                    string query = @" select b.TREATMENT_NUM tr_num, a.PT_NAME data_Name , c.DR_NAME data_Doctor , d.DR_DEPTNAME data_Depart, to_char(b.TREATMENT_TIME,'yyyy-mm-dd') data_Date, b.TREATMENT_PAY data_Pay
                              from patient a, treatment b , doctor c , department d
                              where 
                              a.PT_REGNUM = b.PT_REGNUM
                              and b.DR_LICENSE = c.DR_LICENSE
                              and c.DR_DEPTNUM = d.DR_DEPTNUM
                              and a.pt_idnum = " + patient_id;

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                    try
                    {
                        TREATE_NUM = new List<string>();
                        for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                        {
                            TREATE_NUM.Add(ds.Tables[0].Rows[idx]["tr_num"].ToString());
                            payment obj = new payment()
                            {
                                Name = ds.Tables[0].Rows[idx]["data_Name"].ToString(),
                                Doctor = ds.Tables[0].Rows[idx]["data_Doctor"].ToString(),
                                Group = ds.Tables[0].Rows[idx]["data_Depart"].ToString(),
                                Date = ds.Tables[0].Rows[idx]["data_Date"].ToString(),
                                Price = ds.Tables[0].Rows[idx]["data_Pay"].ToString(),
                                Checked = true,
                            };
                            SampleDatas.Add(obj);

                        }
                    }
                    catch
                    {
                        connect_fail_flag = true;
                    }
                }
                return _sampleDatas;
            }
            set
            { _sampleDatas = value; OnPropertyChanged("_sampleDatas"); }
        }


        private ICommand _paystartCommand;

        public ICommand PaystartCommand
        {
            get
            {
                return (this._paystartCommand) ?? (this._paystartCommand = new RelayCommand(Select_Price));
            }
        }

        #region 커맨드 테스트한거

        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return (this.connectCommand) ?? (this.connectCommand = new RelayCommand(Connect));
            }
        }

        private ICommand selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                return (this.selectCommand) ?? (this.selectCommand = new RelayCommand(DataSearch));
            }
        }

        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return (this.loadedCommand) ?? (this.loadedCommand = new RelayCommand(LoadEvent));
            }
        }

        /*
        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return (this.nextCommand) ?? (this.nextCommand = new RelayCommand(ButtonShow));
            }
        }
        */
        private ICommand payCommand;
        public ICommand PayCommand
        {
            get
            {
                return (this.payCommand) ?? (this.payCommand = new RelayCommand(payShow));
            }
        }
        #endregion

        /*
        public void ButtonShow()
        {
            prescription prescription = new prescription();
            prescription.Owner = Application.Current.MainWindow;
            prescription.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            prescription.ShowDialog();
        }
        */
        private void payShow()
        {
            pay pay = new pay();
            pay.Owner = Application.Current.MainWindow;
            pay.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            pay.ShowDialog();
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
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }
        }

        public void Select_Price()
        {
            int temp_total = 0;
            foreach (payment ob in SampleDatas)
            {
                if (ob.Checked == true)
                {
                    int temp = Int32.Parse(ob.Price);
                    temp_total += temp;
                }
            }
            Total_Price = temp_total.ToString();
        }

        private void DataSearch()
        {
            DataSet ds = new DataSet();
            string query = @" select a.PT_NAME data_Name , c.DR_NAME data_Doctor , d.DR_DEPTNAME data_Depart, to_char(b.TREATMENT_TIME,'yyyy-mm-dd') data_Date, b.TREATMENT_PAY data_Pay
                              from patient a, treatment b , doctor c , department d
                              where a.pt_idnum = " + patient_id +
                    "and a.PT_REGNUM = b.PT_REGNUM and b.DR_LICENSE = c.DR_LICENSE and c.DR_DEPTNUM = d.DR_DEPTNUM ";

            OracleDBManager.Instance.ExecuteDsQuery(ds, query);

            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                payment obj = new payment()
                {
                    Name = ds.Tables[0].Rows[idx]["data_Name"].ToString(),
                    Doctor = ds.Tables[0].Rows[idx]["data_Doctor"].ToString(),
                    Group = ds.Tables[0].Rows[idx]["data_Depart"].ToString(),
                    Date = ds.Tables[0].Rows[idx]["data_Date"].ToString(),


                };
                SampleDatas.Add(obj);
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
        }
    }
}
