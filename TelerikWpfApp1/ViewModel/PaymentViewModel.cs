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
        /*
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
        */

        public string Price_str
        {
            get { return _pa.Price_str; }
            set
            {
                if (value != _pa.Price_str)
                {
                    _pa.Price_str = value;
                    this.OnPropertyChanged("Price_str");
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
            get { return _pre.P_Number; }
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

        public string P_MedicationTotal
        {
            get { return _pre.P_MedicationTotal; }
            set
            {
                if (value != _pre.P_MedicationTotal)
                {
                    _pre.P_MedicationTotal = value;
                    this.OnPropertyChanged("P_MedicationTotal");
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


        public string P_Stamp
        {
            get { return _pre.P_Stamp; }
            set
            {
                if (value != _pre.P_Stamp)
                {
                    _pre.P_Stamp = value;
                    this.OnPropertyChanged("P_Stamp");
                }
            }
        }

        public string P_Code
        {
            get { return _pre.P_Code; }
            set
            {
                if (value != _pre.P_Code)
                {
                    _pre.P_Code = value;
                    this.OnPropertyChanged("P_Code");
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

        ObservableCollection<payment> _paymentData = null;
        ObservableCollection<prescription_ment> _prescriptionData = null;
        ObservableCollection<receipt_ment> _receiptData = null;

        public ObservableCollection<payment> PaymentData
        {
            get
            {
                if (_paymentData == null)
                {
                    _paymentData = new ObservableCollection<payment>();
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
                              and (a.pt_idnum = " + patient_id + "or a.pt_regnum = " + patient_id + ")";

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
                                Price_str = ds.Tables[0].Rows[idx]["data_Pay"].ToString()
                            };
                            PaymentData.Add(obj);

                        }
                        Log.Debug("PaymentData");
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "PaymentData");
                    }
                }
                return _paymentData;
            }
            set
            {
                _paymentData = value; OnPropertyChanged("PaymentData");
            }
        }

        public ObservableCollection<prescription_ment> PrescriptionData
        {
            get
            {
                if (_prescriptionData == null)
                {
                    _prescriptionData = new ObservableCollection<prescription_ment>();
                    DataSet ds = new DataSet();


                    string query = @" SELECT a.PT_NAME p_name, a.PT_REGNUM p_number, d.PRESCRIPTION_DATE p_date, b.DR_NAME p_doctor, b.DR_LICENSE p_doctorlicense, e.MED_NAME p_medication, e.MED_DOSE p_medicationdose, e.MED_COUNT p_medicationcount, e.MED_DOSE*E.MED_COUNT p_medicationtotal, f.DR_DEPTNAME p_doctorposition, b.DR_STAMP p_stamp, c.DIAGNOSIS_CODE p_code
                                      FROM PATIENT a, DOCTOR b, TREATMENT c, PRESCRIPTION d, DETAILMED e, DEPARTMENT f
                                      WHERE a.PT_REGNUM = c.PT_REGNUM AND c.TREATMENT_NUM = d.TREATMENT_NUM AND c.DR_LICENSE = b.DR_LICENSE AND d.MED_CODE = e.MED_CODE AND b.DR_DEPTNUM = f.DR_DEPTNUM AND (a.pt_idnum = " + patient_id + "or a.pt_regnum = " + patient_id + ")";

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
                                P_MedicationTotal = ds.Tables[0].Rows[idx]["p_medicationtotal"].ToString(),
                                P_DoctorPosition = ds.Tables[0].Rows[idx]["p_doctorposition"].ToString(),
                                P_Stamp = ds.Tables[0].Rows[idx]["p_stamp"].ToString(),
                                P_Code = ds.Tables[0].Rows[idx]["p_code"].ToString(),

                            };
                            PrescriptionData.Add(obj);

                        }
                        Log.Debug("PrescriptionData");
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "PrescriptionData");
                    }
                }
                return _prescriptionData;
            }
            set
            { _prescriptionData = value; OnPropertyChanged("PrescriptionData"); }
        }

        public ObservableCollection<receipt_ment> ReceiptData
        {
            get
            {
                if (_receiptData == null)
                {
                    _receiptData = new ObservableCollection<receipt_ment>();
                    DataSet ds = new DataSet();


                    string query = @" SELECT a.PT_NAME r_name, a.PT_IDNUM r_id, c.TREATMENT_PAY r_pay, b.DR_NAME r_doctor, d.DR_DEPTNAME r_doctorposition, c.TREATMENT_TIME r_date
                                      FROM PATIENT a, DOCTOR b, TREATMENT c, DEPARTMENT d
                                      WHERE a.PT_REGNUM = c.PT_REGNUM  AND c.DR_LICENSE = b.DR_LICENSE AND b.DR_DEPTNUM = d.DR_DEPTNUM AND (a.pt_idnum = " + patient_id + "or a.pt_regnum = " + patient_id + ")";

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
                                R_Year = ds.Tables[0].Rows[idx]["r_date"].ToString().ToString().Substring(0, 4),
                                R_Month = ds.Tables[0].Rows[idx]["r_date"].ToString().ToString().Substring(5, 2),
                                R_Day = ds.Tables[0].Rows[idx]["r_date"].ToString().ToString().Substring(8, 2)


                            };
                            ReceiptData.Add(obj);

                        }
                        Log.Debug("ReceiptData");
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "ReceiptData");
                    }
                }
                return _receiptData;
            }
            set
            { _receiptData = value; OnPropertyChanged("ReceiptData"); }
        }

        public static void PaymentSubmit() //한번 결제 한 진료를 확인하기 위한 업데이트
        {
            //MessageBox.Show(PaymentViewModel.TREATE_NUM.Count.ToString());
            try
            {
                OracleDBManager oracleDBManager = new OracleDBManager();
                oracleDBManager.GetConnection();
                string query = @"UPDATE (SELECT * FROM PATIENT a, TREATMENT b WHERE a.PT_REGNUM = b.PT_REGNUM AND (a.pt_idnum = " + patient_id + "or a.pt_regnum = " + patient_id + ")) SET PAY_STATUS = '1'";
                string query1 = @"commit";
                OracleDBManager.Instance.ExecuteNonQuery(query);
                OracleDBManager.Instance.ExecuteNonQuery(query1);
                Log.Debug("PaymentSubmit");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "PaymentSubmit");
            }
        }

        /*
        private ICommand _paystartCommand;

        public ICommand PaystartCommand
        {
            get
            {
                return (this._paystartCommand) ?? (this._paystartCommand = new RelayCommand(Select_Price));
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
        static public bool pay_end_flag = false;
        private void payShow()
        {
            Log.Debug("payShow");
            pay pay = new pay();
            pay.Owner = Application.Current.MainWindow;
            pay.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            pay.ShowDialog();
            pay_end_flag = true;
        }
        /*
        public void Select_Price()
        {
            Log.Debug("Select_Price");
            int temp_total = 0;
            foreach (payment ob in PaymentData)
            {
                if (ob.Checked == true)
                {
                    int temp = Int32.Parse(ob.Price);
                    temp_total += temp;
                }
            }
            Total_Price = "총 결제금액은 " + temp_total.ToString() + "원 입니다.";
        }
        */
    }
}
