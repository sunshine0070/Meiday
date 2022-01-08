using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using Meiday.Model;

namespace Meiday.ViewModel
{
    public class AdminInsertPtViewModel : ViewModelBase
    {
        //public ObservableCollection<PatientModel> patients { get; set; }

        private string _idNum;
        public string idNum
        {
            get => _idNum;
            set
            {
                Log.Debug("idNum");
                _idNum = value;
                OnPropertyChanged("idNum");
            }
        }

        private string _name;
        public string name
        {
            get => _name;
            set
            {
                Log.Debug("name");
                _name = value;
                OnPropertyChanged("name");
            }
        }

        private string _age;
        public string age
        {
            get => _age;
            set
            {
                Log.Debug("age");
                _age = value;
                OnPropertyChanged("age");
            }
        }

        //public string RegNumSplit(string a)
        //{
        //    a = a.Substring(0, 5) + "-" + a.Substring(6, 12);
        //    return a;
        //}

        private string _regNum;
        public string regNum
        {
            get => _regNum;
            set
            {
                Log.Debug("regNum");
                _regNum = value;
                OnPropertyChanged("regNum");
            }
        }

        private string _phone;
        public string phone
        {
            get => _phone;
            set
            {
                Log.Debug("phone");
                _phone = value;
                OnPropertyChanged("phone");
            }
        }

        private string _addr;
        public string addr
        {
            get => _addr;
            set
            {
                Log.Debug("addr");
                _addr = value;
                OnPropertyChanged("addr");
            }
        }

        ObservableCollection<PatientModel> _sampleDatas = null;
        public ObservableCollection<PatientModel> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<PatientModel>();
                }
                return _sampleDatas;
            }
            set
            {
                Log.Debug("SampleDatas");
                _sampleDatas = value;
            }
        }

        public RelayCommand AddPatient { get; set; }
        public RelayCommand LoadPatient { get; set; }

        public AdminInsertPtViewModel()
        {
            Log.Debug("AdminInsertPtViewModel");
            AddPatient = new RelayCommand(AddNewPatient);
            LoadPatient = new RelayCommand(DataSearch);
        }

        private void DataSearch()
        {
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT pt_idnum, pt_age, pt_regnum, pt_phone, pt_addr, pt_name
                            FROM     patient
                            ORDER BY pt_idnum DESC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    PatientModel obj = new PatientModel
                    {
                        IdNum = ds.Tables[0].Rows[idx]["pt_idnum"].ToString(),
                        Name = ds.Tables[0].Rows[idx]["pt_name"].ToString(),
                        Age = ds.Tables[0].Rows[idx]["pt_age"].ToString(),
                        RegNum = ds.Tables[0].Rows[idx]["pt_regnum"].ToString(),
                        Phone = ds.Tables[0].Rows[idx]["pt_phone"].ToString(),
                        Addr = ds.Tables[0].Rows[idx]["pt_addr"].ToString(),
                    };
                    SampleDatas.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "DataSearch");
            }
            
        }

        private void AddNewPatient()
        {
            try
            {
                PatientModel p = new PatientModel()
                {
                    IdNum = this.idNum,
                    Name = this.name,
                    Age = this.age,
                    RegNum = this.regNum,
                    Phone = this.phone,
                    Addr = this.addr,
                };
                if (this.name != null && this.age != null && this.regNum != null && this.phone != null && this.addr != null)
                {
                    if (this.idNum == null)
                    {
                        string query = @"MERGE INTO PATIENT USING dual ON (PT_IDNUM = '#IdNum') 
                                WHEN MATCHED THEN UPDATE SET PT_NAME = '#Name', PT_ADDR = '#Addr', PT_PHONE = '#Phone' ,  PT_AGE = '#Age' , PT_REGNUM = '#RegNum' 
                                WHEN NOT MATCHED THEN INSERT (PT_REGNUM,PT_NAME,PT_ADDR,PT_PHONE,PT_AGE) VALUES ('#RegNum','#Name', '#Addr', '#Phone', '#Age') ";
                        string query1 = @"commit";
                        query = query.Replace("#IdNum", this.idNum);
                        query = query.Replace("#Name", this.name);
                        query = query.Replace("#Age", this.age);
                        query = query.Replace("#RegNum", this.regNum);
                        query = query.Replace("#Phone", this.phone);
                        query = query.Replace("#Addr", this.addr);
                        OracleDBManager.Instance.ExecuteNonQuery(query);
                        OracleDBManager.Instance.ExecuteNonQuery(query1);

                        this.idNum = string.Empty;
                        this.name = string.Empty;
                        this.age = string.Empty;
                        this.regNum = string.Empty;
                        this.addr = string.Empty;
                        this.phone = string.Empty;
                    }
                    else
                    {
                        string query = @"MERGE INTO PATIENT USING dual ON (PT_IDNUM = '#IdNum') 
                                WHEN MATCHED THEN UPDATE SET PT_NAME = '#Name', PT_ADDR = '#Addr', PT_PHONE = '#Phone' ,  PT_AGE = '#Age' , PT_REGNUM = '#RegNum' 
                                WHEN NOT MATCHED THEN INSERT (PT_IDNUM,PT_REGNUM,PT_NAME,PT_ADDR,PT_PHONE,PT_AGE) VALUES ('#IdNum','#RegNum','#Name', '#Addr', '#Phone', '#Age') ";
                        string query1 = @"commit";
                        query = query.Replace("#IdNum", this.idNum);
                        query = query.Replace("#Name", this.name);
                        query = query.Replace("#Age", this.age);
                        query = query.Replace("#RegNum", this.regNum);
                        query = query.Replace("#Phone", this.phone);
                        query = query.Replace("#Addr", this.addr);
                        OracleDBManager.Instance.ExecuteNonQuery(query);
                        OracleDBManager.Instance.ExecuteNonQuery(query1);

                        this.idNum = string.Empty;
                        this.name = string.Empty;
                        this.age = string.Empty;
                        this.regNum = string.Empty;
                        this.addr = string.Empty;
                        this.phone = string.Empty;
                    }
                }
                else
                {
                    // textbox가 비었을 때 미충족 항목 MessageBox 팝업 띄워서 표시하기
                    string result = "";
                    string last = " 을(를) 입력해주세요.";
                    if (this.name != string.Empty) { result += " 이름,"; }
                    if (this.age != string.Empty) { result += " 나이,"; }
                    if (this.regNum != string.Empty) { result += " 주민등록번호,"; }
                    if (this.phone != string.Empty) { result += " 전화번호,"; }
                    if (this.addr != string.Empty) { result += " 주소,"; }
                    if (result.Contains(','.ToString()))
                    {
                        result = result.Remove(result.LastIndexOf(','.ToString()));
                    }
                    if (result != null)
                    {
                        MessageBox.Show(result + last);
                    }

                }

                if(this.idNum.IsNumeric() || this.age.IsNumeric() || this.regNum.IsNumeric() || this.phone.IsNumeric())
                {
                    MessageBox.Show("정보를 정확하게 입력해주세요.");
                }
                Log.Debug("AddNewPatient");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AddNewPatient");
            }
        }
    }
    public static class StringExtensions
    {
        public static bool IsNumeric(this string input)
        {
            Log.Debug("IsNumeric");
            int number;
            return int.TryParse(input, out number);
        }
    }
}
