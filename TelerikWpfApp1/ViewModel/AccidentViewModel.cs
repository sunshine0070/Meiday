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
using System.Windows.Data;



namespace Meiday
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

        public static AccidentType _accidentType;
        public AccidentType AccidentTypes
        {
            get => _accidentType;
            set
            {
                if (_accidentType != value)
                {
                    _accidentType = value;
                    OnPropertyChanged("AccidentTypes");
                }
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

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked02
        {
            get { return _pa.IsChecked02; }
            set
            {
                if (value != _pa.IsChecked02)
                {
                    _pa.IsChecked02 = value;
                    this.OnPropertyChanged("IsChecked02");
                }
            }
        }
        private string check_InsuName = string.Empty;
        public string CheckInsuName
        {
            get
            {
                return check_InsuName;
            }
            set
            {
                if (value != check_InsuName)
                {
                    check_InsuName = value;
                    OnPropertyChanged("CheckInsuName");
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
        public static bool _isChecked02;
        public void Check()
        {
            foreach (ment ob in SampleDatas)
            {
                if (ob.IsChecked02 == true)
                {
                    _isChecked02 = ob.IsChecked02;
                    CheckInsuName = ob.InsuName;
                    break;
                }
                else
                {
                    _isChecked02 = ob.IsChecked02;
                    CheckInsuName = string.Empty;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>



        ObservableCollection<ment> _sampleDatas = null;
        public ObservableCollection<ment> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<ment>();
                    DataSet ds = new DataSet();
                    string query1 = @"SELECT i.insurance_name       InsuName
                                            ,i.insurance_product    InsuProduct
                                      FROM   patient p, insurance i, checkinsurance c
                                      WHERE  p.PT_REGNUM=c.PT_REGNUM(+)
                                      AND    p.PT_IDNUM=" + patient_id;

                    //string query = @" SELECT i.INSURANCE_NAME InsuName, i.INSURANCE_PRODUCT InsuProduct
                    //                FROM INSURANCE i 
                    //                JOIN CHECKINSURANCE c ON i.INSURANCE_NUM = c.INSURANCE_NUM 
                    //                JOIN PATIENT        p ON p.PT_REGNUM     = c.PT_REGNUM
                    //                WHERE p.pt_idnum =  " + patient_id + "or p.pt_regnum = " + patient_id;
                    OracleDBManager.Instance.ExecuteDsQuery(ds, query1);

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
    public class RadioBoolToAccidentTypeConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>

}
