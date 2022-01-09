using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.Model
{
    public class LogData : INotifyPropertyChanged
    {
        public LogData() { } //생성자만들기

        private int log_no;
        public int Log_no
        {
            get { return log_no; }
            set
            {
                log_no = value;
                this.OnPropertyChanged("Log_no");
            }
        }

        private string log_level;
        public string Log_level
        {
            get { return log_level; }
            set
            {
                log_level = value;
                this.OnPropertyChanged("Log_level");
            }
        }

        private DateTime log_date;

        public DateTime Log_date
        {
            get { return log_date; }
            set
            {
                log_date = value;
                this.OnPropertyChanged("Log_date");
            }
        }

        private String log_class;
        public String Log_class
        {
            get { return log_class; }
            set
            {
                log_class = value;
                this.OnPropertyChanged("Log_class");
            }
        }

        private String log_method;
        public String Log_method
        {
            get { return log_method; }
            set
            {
                log_method = value;
                this.OnPropertyChanged("Log_method");
            }
        }
        private String log_ip;
        public String Log_ip
        {
            get { return log_ip; }
            set
            {
                log_ip = value;
                this.OnPropertyChanged("Log_ip");
            }
        }

        private String patient_id;
        public String Patient_id
        {
            get { return patient_id; }
            set
            {
                patient_id = value;
                this.OnPropertyChanged("Patient_id");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                if (PropertyChanged != null)
                {
                    if (PropertyChanged != null)
                    {
                        try
                        {
                            PropertyChanged(this, new PropertyChangedEventArgs(name));
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }


    }
}
