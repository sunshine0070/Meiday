using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using Meiday.Model;
using System.Collections.ObjectModel;
using System.Data;
using static Meiday.LoginViewModel;
using System.Windows.Input;
using System.Windows;

namespace Meiday.ViewModel
{
    public class AccidentViewModel : ViewModelBase
    {
        private DateTime _accidentTodayDateTime = DateTime.Now;
        public DateTime AccidentTodayDateTime
        {
            get => _accidentTodayDateTime;

            set
            {
                _accidentTodayDateTime = value;
                OnPropertyChanged(nameof(AccidentTodayDateTime));
            }
        }

        private DateTime _accidentSelectedDateTime = DateTime.Now;
        public DateTime AccidentSelectedDateTime
        {
            get => _accidentSelectedDateTime;
            set
            {
                _accidentSelectedDateTime = value;
                OnPropertyChanged(nameof(AccidentSelectedDateTime));
            }
        }

        // 실비보험 청구 가능기간 (3년) 제한
        private DateTime _accidentLimitedDateTime = DateTime.Today.AddYears(-3);
        public DateTime AccidentLimitedDateTime
        {
            get => _accidentLimitedDateTime;
            set
            {
                _accidentLimitedDateTime = value;
                OnPropertyChanged(nameof(AccidentLimitedDateTime));
            }
        }
        private bool _accidentTypeDisease;
        public bool AccidentTypeDisease
        {
            get => _accidentTypeDisease;
            set
            {
                _accidentTypeDisease = value;
                OnPropertyChanged(nameof(AccidentTypeDisease));
            }
        }

        private bool _accidentTypeInjury;
        public bool AccidentTypeInjury
        {
            get => _accidentTypeInjury;
            set
            {
                _accidentTypeInjury = value;
                OnPropertyChanged(nameof(AccidentTypeInjury));
            }
        }

        private bool _accidentTypeCar;
        public bool AccidentTypeCar
        {
            get => _accidentTypeCar;
            set
            {
                _accidentTypeCar = value;
                OnPropertyChanged(nameof(AccidentTypeCar));
            }
        }
        ment _pa = new ment();
        public string InsuName
        {
            get
            {
                return _pa.InsuName;
            }
            set
            {
                if (value != _pa.InsuName)
                {
                    _pa.InsuName = value;
                    OnPropertyChanged("InsuName");
                }
            }
        }
        public string InsuProduct
        {
            get
            {
                return _pa.InsuProduct;
            }
            set
            {
                if (value != _pa.InsuProduct)
                {
                    _pa.InsuProduct = value;
                    OnPropertyChanged("InsuProduct");
                }
            }
        }
        private bool isChecked02 = false;
        public bool IsChecked02
        {
            get => isChecked02;
            set
            {
                isChecked02 = value;
                OnPropertyChanged("IsChecked02");
                if (isChecked02 == true)
                {
                    MessageBox.Show("성공");
                }
            }
        }

        private ICommand checkCommand;
        public ICommand CheckCommand
        {
            get
            {
                return (this.checkCommand) ?? (this.checkCommand = new RelayCommand(Check));
            }
        }

        public void Check()
        {
            _pa.IsChecked02 = true;
        }



        ObservableCollection<ment> _sampleDatas = null;
        public ObservableCollection<ment> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<ment>();
                    DataSet ds = new DataSet();
                    string query = @" select i.INSURANCE_NAME InsuName, i.INSURANCE_PRODUCT InsuProduct
                              from insurance i
                              where i.pt_idnum = " + patient_id;

                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        ment obj = new ment()
                        {
                            InsuName = ds.Tables[0].Rows[idx]["InsuName"].ToString(),
                            InsuProduct = ds.Tables[0].Rows[idx]["InsuProduct"].ToString(),
                        };
                        SampleDatas.Add(obj);
                    }
                }
                return _sampleDatas;
            }
            set
            {
                _sampleDatas = value;
                OnPropertyChanged("_sampleDatas");
            }
        }
    }
}
