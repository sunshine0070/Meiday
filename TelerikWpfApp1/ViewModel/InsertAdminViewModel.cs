using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using Meiday.Model;

namespace Meiday.ViewModel
{
    public class InsertAdminViewModel : ViewModelBase
    {
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

        private string _deptnum;
        public string deptnum
        {
            get => _deptnum;
            set
            {
                Log.Debug("deptnum");
                _deptnum = value;
                OnPropertyChanged("deptnum");
            }
        }



        ObservableCollection<AdminModel> _sampleDatas1 = null;
        public ObservableCollection<AdminModel> SampleDatas1
        {
            get
            {
                if (_sampleDatas1 == null)
                {
                    _sampleDatas1 = new ObservableCollection<AdminModel>();
                }
                return _sampleDatas1;
            }
            set
            {
                Log.Debug("SampleDatas1");
                _sampleDatas1 = value;
            }
        }

        public RelayCommand AddAdmin { get; set; }
        public RelayCommand LoadAdmin { get; set; }

        public InsertAdminViewModel()
        {
            Log.Debug("InsertAdminViewModel");
            AddAdmin = new RelayCommand(AddNewAdmin);
            LoadAdmin = new RelayCommand(DataSearch);
        }

        private void DataSearch()
        {
            try
            {
                DataSet ds = new DataSet();
                string query2 = @"SELECT DR_LICENSE, DR_NAME, DR_EMAIL, DR_POSITION, DR_DEPTNUM
                            FROM     DOCTOR
                            ORDER BY DR_LICENSE DESC";

                OracleDBManager.Instance.ExecuteDsQuery(ds, query2);

                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    AdminModel obj = new AdminModel
                    {
                        License = ds.Tables[0].Rows[idx]["DR_LICENSE"].ToString(),
                        Name = ds.Tables[0].Rows[idx]["dr_name"].ToString(),
                        Email = ds.Tables[0].Rows[idx]["DR_EMAIL"].ToString(),
                        Position = ds.Tables[0].Rows[idx]["DR_POSITION"].ToString(),
                        Deptnum = ds.Tables[0].Rows[idx]["DR_DEPTNUM"].ToString(),
                    };
                    SampleDatas1.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "DataSearch");
            }

        }

        private void AddNewAdmin()
        {
            try
            {
                AdminModel a = new AdminModel()
                {
                    License = this.license,
                    Name = this.name,
                    Email = this.email,
                    Position = this.position,
                    Deptnum = this.deptnum,
                };
                if (this.name != null && this.email != null && this.position != null && this.deptnum != null)
                {
                    if (this.license == null)
                    {
                        string query = @"MERGE INTO DOCTOR USING dual ON (DR_LICENSE = '#License') 
                                WHEN MATCHED THEN UPDATE SET DR_NAME = '#Name', DR_EMail = '#Email', DR_POSITION = '#Position' ,  DR_DEPTNUM = '#Deptnum' 
                                WHEN NOT MATCHED THEN INSERT (DR_NAME,DR_EMail,DR_POSITION,DR_DEPTNUM) VALUES ('#Name', '#Email', '#Position', '#Deptnum') ";
                        string query1 = @"commit";
                        query = query.Replace("#License", this.license);
                        query = query.Replace("#Name", this.name);
                        query = query.Replace("#Email", this.email);
                        query = query.Replace("#Position", this.position);
                        query = query.Replace("#DR_DEPTNUM", this.deptnum);
                        OracleDBManager.Instance.ExecuteNonQuery(query);
                        OracleDBManager.Instance.ExecuteNonQuery(query1);

                        this.license = string.Empty;
                        this.name = string.Empty;
                        this.email = string.Empty;
                        this.position = string.Empty;
                        this.deptnum = string.Empty;
                    }
                    else
                    {
                        
                        string query = @"MERGE INTO DOCTOR USING dual ON (DR_LICENSE = '#License') 
                                WHEN MATCHED THEN UPDATE SET DR_NAME = '#Name', DR_EMAIL = '#Email', DR_POSITION = '#Position' ,  DR_DEPTNUM = '#Deptnum' 
                                WHEN NOT MATCHED THEN INSERT (DR_LICENSE,DR_NAME,DR_EMail,DR_POSITION,DR_DEPTNUM) VALUES ('#License', '#Name', '#Email', '#Position', '#Deptnum') ";
                        string query1 = @"commit";
                        query = query.Replace("#License", this.license);
                        query = query.Replace("#Name", this.name);
                        query = query.Replace("#Email", this.email);
                        query = query.Replace("#Position", this.position);
                        query = query.Replace("#DR_DEPTNUM", this.deptnum);
                        OracleDBManager.Instance.ExecuteNonQuery(query);
                        OracleDBManager.Instance.ExecuteNonQuery(query1);

                        this.license = string.Empty;
                        this.name = string.Empty;
                        this.email = string.Empty;
                        this.position = string.Empty;
                        this.deptnum = string.Empty;
                    }
                }
                else
                {
                    // textbox가 비었을 때 미충족 항목 MessageBox 팝업 띄워서 표시하기
                    string result = "";
                    string last = " 을(를) 입력해주세요.";
                    if (this.name != string.Empty) { result += " 이름,"; }
                    if (this.email != string.Empty) { result += " 이메일,"; }
                    if (this.position != string.Empty) { result += " 직급,"; }
                    if (this.deptnum != string.Empty) { result += " 부서번호,"; }
                    if (result.Contains(','.ToString()))
                    {
                        result = result.Remove(result.LastIndexOf(','.ToString()));
                    }
                    if (result != null)
                    {
                        MessageBox.Show(result + last);
                    }

                }

                if (this.license.IsNumeric() || this.email.IsNumeric() || this.position.IsNumeric() || this.deptnum.IsNumeric())
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
