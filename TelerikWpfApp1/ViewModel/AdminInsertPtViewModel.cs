using System;
using System.Collections.ObjectModel;
using System.Data;
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
                _sampleDatas = value;
            }
        }

        public RelayCommand AddPatient { get; set; }
        public RelayCommand LoadPatient { get; set; }

        public AdminInsertPtViewModel()
        {
            AddPatient = new RelayCommand(AddNewPatient);
            LoadPatient = new RelayCommand(DataSearch);
        }

        private void DataSearch()
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

        private void AddNewPatient()
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

    }
}
