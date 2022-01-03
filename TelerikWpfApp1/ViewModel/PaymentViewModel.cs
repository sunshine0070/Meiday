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
        public static List<string> PRESCRIPTION_NUM;

        payment _pa = new payment();
        prescription_ment _pre = new prescription_ment();
        receipt_ment _re = new receipt_ment();

        // MainWindow 객체 선언

        #region payment 선언
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

        #endregion

        #region prescription_ment 선언
        public string P_Name
        {
            get { return _pre.P_Name; }
            set
            {
                if (value != _pre.P_Name)
                {
                    _pre.P_Name = value;
                    this.OnPropertyChanged("P_Name");
                }
            }
        }

        public string P_Number
        {
            get {return _pre.P_Number; }
            set
            {
                if (value != _pre.P_Number)
                {
                    _pre.P_Number = value;
                    this.OnPropertyChanged("P_Number");
                }
            }
        }

        public string P_Date
        {
            get { return _pre.P_Date; }
            set
            {
                if (value != _pre.P_Date)
                {
                    _pre.P_Date = value;
                    this.OnPropertyChanged("P_Date");
                }
            }
        }

        public string P_Doctor
        {
            get { return _pre.P_Doctor; }
            set
            {
                if (value != _pre.P_Doctor)
                {
                    _pre.P_Doctor = value;
                    this.OnPropertyChanged("P_Doctor");
                }
            }
        }

        public string P_DoctorLicense
        {
            get { return _pre.P_DoctorLicense; }
            set
            {
                if (value != _pre.P_DoctorLicense)
                {
                    _pre.P_DoctorLicense = value;
                    this.OnPropertyChanged("P_DoctorLicense");
                }
            }
        }

        public string P_Medication
        {
            get { return _pre.P_Medication; }
            set
            {
                if (value != _pre.P_Medication)
                {
                    _pre.P_Medication = value;
                    this.OnPropertyChanged("P_Medication");
                }
            }
        }

        public string P_MedicationDose
        {
            get { return _pre.P_MedicationDose; }
            set
            {
                if (value != _pre.P_MedicationDose)
                {
                    _pre.P_MedicationDose = value;
                    this.OnPropertyChanged("P_MedicationDose");
                }
            }
        }

        public string P_MedicationCount
        {
            get { return _pre.P_MedicationCount; }
            set
            {
                if (value != _pre.P_MedicationCount)
                {
                    _pre.P_MedicationCount = value;
                    this.OnPropertyChanged("P_MedicationCount");
                }
            }
        }

        public string P_DoctorPosition
        {
            get { return _pre.P_DoctorPosition; }
            set
            {
                if (value != _pre.P_DoctorPosition)
                {
                    _pre.P_DoctorPosition = value;
                    this.OnPropertyChanged("P_DoctorPosition");
                }
            }
        }
        #endregion

        #region receipt_ment 선언
        public string R_Name
        {
            get { return _re.R_Name; }
            set
            {
                if (value != _re.R_Name)
                {
                    _re.R_Name = value;
                    this.OnPropertyChanged("R_Name");
                }
            }
        }

        public string R_Id
        {
            get { return _re.R_Id; }
            set
            {
                if (value != _re.R_Id)
                {
                    _re.R_Id = value;
                    this.OnPropertyChanged("R_Id");
                }
            }
        }

        public string R_Pay
        {
            get { return _re.R_Pay; }
            set
            {
                if (value != _re.R_Pay)
                {
                    _re.R_Pay = value;
                    this.OnPropertyChanged("R_Pay");
                }
            }
        }

        public string R_Doctor
        {
            get { return _re.R_Doctor; }
            set
            {
                if (value != _re.R_Doctor)
                {
                    _re.R_Doctor = value;
                    this.OnPropertyChanged("R_Doctor");
                }
            }
        }

        public string R_DoctorPosition
        {
            get { return _re.R_DoctorPosition; }
            set
            {
                if (value != _re.R_DoctorPosition)
                {
                    _re.R_DoctorPosition = value;
                    this.OnPropertyChanged("R_DoctorPosition");
                }
            }
        }

        public string R_Date
        {
            get { return _re.R_Date; }
            set
            {
                if (value != _re.R_Date)
                {
                    _re.R_Date = value;
                    this.OnPropertyChanged("R_Date");
                }
            }
        }


        public string R_Year
        {
            get { return _re.R_Year; }
            set
            {
                if (value != _re.R_Year)
                {
                    _re.R_Year = value;
                    this.OnPropertyChanged("R_Year");
                }
            }
        }

        public string R_Month
        {
            get { return _re.R_Month; }
            set
            {
                if (value != _re.R_Month)
                {
                    _re.R_Month = value;
                    this.OnPropertyChanged("R_Month");
                }
            }
        }


        public string R_Day
        {
            get { return _re.R_Day; }
            set
            {
                if (value != _re.R_Day)
                {
                    _re.R_Day = value;
                    this.OnPropertyChanged("R_Day");
                }
            }
        }

        #endregion

        ObservableCollection<payment> _sampleDatas = null;
        ObservableCollection<prescription_ment> _prescriptionDatas = null;
        ObservableCollection<receipt_ment> _receiptDatas = null;

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
                              and b.PAY_STATUS = '0'
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

        public ObservableCollection<prescription_ment> PrescriptionDatas
        {
            get
            {
                if (_prescriptionDatas == null)
                {
                    _prescriptionDatas = new ObservableCollection<prescription_ment>();
                    DataSet ds = new DataSet();


                    string query = @" SELECT a.PT_NAME p_name, a.PT_REGNUM p_number, a.PT_REGDATE p_date, b.DR_NAME p_doctor, b.DR_LICENSE p_doctorlicense, e.MED_NAME p_medication, e.MED_DOSE p_medicationdose, e.MED_COUNT p_medicationcount, f.DR_DEPTNAME p_doctorposition
                                      FROM PATIENT a, DOCTOR b, TREATMENT c, PRESCRIPTION d, DETAILMED e, DEPARTMENT f
                                      WHERE a.PT_REGNUM = c.PT_REGNUM AND c.TREATMENT_NUM = d.TREATMENT_NUM AND c.DR_LICENSE = b.DR_LICENSE AND d.MED_CODE = e.MED_CODE AND b.DR_DEPTNUM = f.DR_DEPTNUM AND a.PT_IDNUM =" +patient_id;

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                    try
                    {
                        for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                        {
                            prescription_ment obj = new prescription_ment()
                            {

                                P_Name = ds.Tables[0].Rows[idx]["p_name"].ToString(),
                                P_Number = (ds.Tables[0].Rows[idx]["p_number"].ToString()).Substring(0, 6) + '-' + (ds.Tables[0].Rows[idx]["p_number"].ToString()).Substring(6),
                                P_Date = ds.Tables[0].Rows[idx]["p_date"].ToString(),
                                P_Doctor = ds.Tables[0].Rows[idx]["p_doctor"].ToString(),
                                P_DoctorLicense = ds.Tables[0].Rows[idx]["p_doctorlicense"].ToString(),
                                P_Medication = ds.Tables[0].Rows[idx]["p_medication"].ToString(),
                                P_MedicationDose = ds.Tables[0].Rows[idx]["p_medicationdose"].ToString(),
                                P_MedicationCount = ds.Tables[0].Rows[idx]["p_medicationcount"].ToString(),
                                P_DoctorPosition = ds.Tables[0].Rows[idx]["p_doctorposition"].ToString(),

                        };
                            PrescriptionDatas.Add(obj);

                        }
                    }
                    catch
                    {
                        connect_fail_flag = true;
                    }
                }
                return _prescriptionDatas;
            }
            set
            { _prescriptionDatas = value; OnPropertyChanged("_prescriptionDatas"); }
        }

        public ObservableCollection<receipt_ment> ReceiptDatas
        {
            get
            {
                if (_receiptDatas == null)
                {
                    _receiptDatas = new ObservableCollection<receipt_ment>();
                    DataSet ds = new DataSet();


                    string query = @" SELECT a.PT_NAME r_name, a.PT_IDNUM r_id, c.TREATMENT_PAY r_pay, b.DR_NAME r_doctor, d.DR_DEPTNAME r_doctorposition, c.TREATMENT_TIME r_date
                                      FROM PATIENT a, DOCTOR b, TREATMENT c, DEPARTMENT d
                                      WHERE a.PT_REGNUM = c.PT_REGNUM  AND c.DR_LICENSE = b.DR_LICENSE AND b.DR_DEPTNUM = d.DR_DEPTNUM AND a.PT_IDNUM =" + patient_id;

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);
                    try
                    {
                        for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                        {
                            receipt_ment obj = new receipt_ment()
                            {

                                R_Name = ds.Tables[0].Rows[idx]["r_name"].ToString(),
                                R_Id = ds.Tables[0].Rows[idx]["r_id"].ToString(),
                                R_Pay = ds.Tables[0].Rows[idx]["r_pay"].ToString(),
                                R_Doctor = ds.Tables[0].Rows[idx]["r_doctor"].ToString(),
                                R_DoctorPosition = ds.Tables[0].Rows[idx]["r_doctorposition"].ToString(),
                                R_Date = ds.Tables[0].Rows[idx]["r_date"].ToString().ToString().Substring(0, 10),
                                R_Year = ds.Tables[0].Rows[idx]["r_date"].ToString(),
                                R_Month = ds.Tables[0].Rows[idx]["r_date"].ToString(),
                                R_Day = ds.Tables[0].Rows[idx]["r_date"].ToString()


                            };
                            ReceiptDatas.Add(obj);

                        }
                    }
                    catch
                    {
                        connect_fail_flag = true;
                    }
                }
                return _receiptDatas;
            }
            set
            { _receiptDatas = value; OnPropertyChanged("_receiptDatas"); }
        }

        public static void PaymentSubmit() //한번 결제 한 진료를 확인하기 위한 업데이트
        {
            //MessageBox.Show(PaymentViewModel.TREATE_NUM.Count.ToString());
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

                //MessageBox.Show("db 조건문 진입");

                    string query = @"UPDATE (SELECT * FROM PATIENT a, TREATMENT b WHERE a.PT_REGNUM = b.PT_REGNUM AND a.PT_IDNUM =" + patient_id +") SET PAY_STATUS = '1'";
                    string query1 = @"commit";
                    OracleDBManager.Instance.ExecuteNonQuery(query);
                    OracleDBManager.Instance.ExecuteNonQuery(query1);

       
            
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
