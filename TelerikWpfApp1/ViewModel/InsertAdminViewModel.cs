using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using Meiday.Model;

namespace Meiday.ViewModel
{
    public class InsertAdminViewModel : ViewModelBase
    {
        public ObservableCollection<ComboBox> COMBO_MODEL { get; set; }

        //콤보박스
        List<string> _combobox = new List<string> { };
        public List<string> Combobox
        {
            get { return _combobox; }
            set { _combobox = value; }

        }

        string _theSelectedItem = null;
        public string TheSelectedItem
        {
            get { return _theSelectedItem; }
            set { _theSelectedItem = value; } // NotifyPropertyChanged
        }


        //public ObservableCollection<PatientModel> patients { get; set; }
        private string _license;
        public string license
        {
            get => _license;
            set
            {
                Log.Debug("license");
                _license = value;
                OnPropertyChanged("license");
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

        private string _email;
        public string email
        {
            get => _email;
            set
            {
                Log.Debug("email");
                _email = value;
                OnPropertyChanged("email");
            }
        }

        //public string RegNumSplit(string a)
        //{
        //    a = a.Substring(0, 5) + "-" + a.Substring(6, 12);
        //    return a;
        //}
        private string _position;
        public string position
        {
            get => _position;
            set
            {
                Log.Debug("position");
                _position = value;
                OnPropertyChanged("position");
            }
        }

        private string _Deptname;
        public string Deptname
        {
            get => _Deptname;
            set
            {
                Log.Debug("Deptname");
                _Deptname = value;
                OnPropertyChanged("Deptname");
            }
        }

        private string _SelectDeptname;
        public string SelectDeptname
        {
            get => _SelectDeptname;
            set
            {
                Log.Debug("SelectDeptname");
                _SelectDeptname = value;
                OnPropertyChanged("SelectDeptname");
            }
        }

        ObservableCollection<AdminModel> _sampleDatas = null;
        public ObservableCollection<AdminModel> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<AdminModel>();
                }
                return _sampleDatas;
            }
            set
            {
                Log.Debug("SampleDatas");
                _sampleDatas = value;
            }
        }

        private ComboBox _selectCom;
        public ComboBox SelectedComboBox
        {
            get { return _selectCom; }
            set { _selectCom = value; }
        }


        public RelayCommand AddAdmin { get; set; }
        public RelayCommand LoadAdmin { get; set; }
        public RelayCommand ComboBox { get; set; }


        public InsertAdminViewModel()
        {
            Log.Debug("InsertAdminViewModel");
            AddAdmin = new RelayCommand(AddNewAdmin);
            LoadAdmin = new RelayCommand(DataSearch);
            DataSearch1();
        }

        private void DataSearch()
        {
            try
            {
                SampleDatas.Clear();
                DataSet ds = new DataSet();
                string query2 = @"SELECT   a.DR_LICENSE, a.DR_NAME,a.DR_AGE, a.DR_EMAIL, a.DR_POSITION, b.DR_DEPTNAME, a.DR_DEPTNUM
                                  FROM     DOCTOR a, DEPARTMENT b
                                  WHERE    a.dr_deptnum = b.dr_deptnum
                                  ORDER BY DR_LICENSE DESC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    AdminModel obj = new AdminModel
                    {
                        License = ds.Tables[0].Rows[idx]["DR_LICENSE"].ToString(),
                        Name = ds.Tables[0].Rows[idx]["dr_name"].ToString(),
                        Age = ds.Tables[0].Rows[idx]["dr_age"].ToString(),
                        Email = ds.Tables[0].Rows[idx]["DR_EMAIL"].ToString(),
                        Position = ds.Tables[0].Rows[idx]["DR_POSITION"].ToString(),
                        Deptname = ds.Tables[0].Rows[idx]["DR_DEPTNAME"].ToString(),
                        Deptnum = ds.Tables[0].Rows[idx]["DR_DEPTNUM"].ToString(),
                    };
                    SampleDatas.Add(obj);

                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "DataSearch");
            }

        }

        private void DataSearch1()
        {
            try
            {
                COMBO_MODEL = new ObservableCollection<ComboBox>();
                DataSet ds = new DataSet();
                string query2 = @"SELECT DR_DEPTNUM, DR_DEPTNAME FROM DEPARTMENT";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    ComboBox obj = new ComboBox
                    {
                        Deptnum = ds.Tables[0].Rows[idx]["DR_DEPTNUM"].ToString(),
                        Deptname = ds.Tables[0].Rows[idx]["DR_DEPTNAME"].ToString(),
                    };

                    COMBO_MODEL.Add(obj);
                    Combobox.Add(obj.Deptname);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "DataSearch1");
            }

        }

        private void AddNewAdmin()
        {
            MessageBox.Show(TheSelectedItem);
            try
            {
                AdminModel a = new AdminModel()
                {
                    Name = name,
                    Age = age,
                    Email = email,
                    Position = position,
                    Deptname = TheSelectedItem

                };
                if (a.Name != null && a.Age != null && a.Email != null && a.Position != null && a.Deptname != null)
                {
                    string query = @"INSERT INTO DOCTOR D( D.DR_NAME, D.DR_EMAIL, D.DR_POSITION, D.DR_DEPTNUM, D.DR_AGE) 
                                        VALUES('#Name', '#Email', '#Position',(SELECT d.DR_DEPTNUM FROM DEPARTMENT d WHERE d.DR_DEPTNAME ='#Deptname'), '#Age')";
                    string query1 = @"commit";
                    query = query.Replace("#Name", a.Name);
                    query = query.Replace("#Email", a.Email);
                    query = query.Replace("#Position", a.Position);
                    query = query.Replace("#Deptname", a.Deptname);
                    query = query.Replace("#Age", a.Age);
                    OracleDBManager.Instance.ExecuteNonQuery(query);
                    OracleDBManager.Instance.ExecuteNonQuery(query1);

                    this.license = string.Empty;
                    this.name = string.Empty;
                    this.age = string.Empty;
                    this.email = string.Empty;
                    this.position = string.Empty;
                    this.Deptname = string.Empty;
                    TheSelectedItem = String.Empty;

                    MessageBox.Show("새 관리자가 등록되었습니다");
                }
                else
                {
                    // textbox가 비었을 때 미충족 항목 MessageBox 팝업 띄워서 표시하기
                    string result = "";
                    string last = " 을(를) 입력해주세요.";
                    if (name == null) { result += " 이름,"; }
                    if (age == null) { result += " 나이,"; }
                    if (email == null) { result += " 이메일,"; }
                    if (position == null) { result += " 직급,"; }
                    if (Deptname == null) { result += " 부서명,"; }
                    if (result.Contains(','.ToString()))
                    {
                        result = result.Remove(result.LastIndexOf(','.ToString()));
                    }
                    if (result != null)
                    {
                        MessageBox.Show(result + last);
                    }

                }

                if (this.email.IsNumeric() || this.position.IsNumeric())
                {
                    MessageBox.Show("정보를 정확하게 입력해주세요.");
                }
                Log.Debug("AddNewAdmin");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AddNewAdmin");
            }
        }
    }

}
