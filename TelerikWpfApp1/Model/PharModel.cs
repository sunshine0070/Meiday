using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Meiday.Models
{
    public class Pharmacy : INotifyPropertyChanged
    {
        public Pharmacy() { } //생성자 만들기

        /*
        public Pharmacy(String Name, String Latitude, String Logitude)
       {
            this.Name_ = Name;
            this.Latitude_ = Latitude;
            this.Logitude_ = Logitude;
       }

        public Pharmacy(String Name, String Phone, String Address, String Latitude, String  Logitude)
        {
            this.Name_ = Name;
            this.Phone_ = Phone;
            this.Address_ = Address;
            this.Latitude_ = Latitude;
            this.Logitude_ = Logitude;
        }
        */


        private String Name_;
        public String Name
        {
            get { return Name_; }  
            set { Name_ = value;
                this.OnPropertyChanged("Name");
            }
        }

        private String Phone_;
        public String Phone
        {
            get { return Phone_; }
            set
            {
                Phone_ = value;
                this.OnPropertyChanged("Phone");
            }
        }

        private String Address_;
        public String Address
        {
            get { return Address_; }
            set
            {
                Address_ = value;
                this.OnPropertyChanged(Address_);
            }
        }
        

        private String Latitude_;
        public String Latitude
        {
            get { return Latitude_; }   
            set { Latitude_ = value;
                this.OnPropertyChanged("Latitude");    
            }
        }

        private String Logitude_;
        public String Logitude
        {
            get { return Logitude_; }
            set { 
                Logitude_ = value;
                this.OnPropertyChanged("Logitude");
                }
        }

        public Brush COLOR_ = Brushes.White;

        public Brush COLOR
        {
            get { return COLOR_; }
            set
            {
                COLOR_ = value;
                this.OnPropertyChanged("COLOR");
            }
        }

        private String Image_;

        public String Image
        {
            get { return Image_;}
            set { Image_ = value;
                this.OnPropertyChanged("Image");
                }
        }

        private String Email_;
        public String Email
        {
            get { return Email_; }
            set { Email_ = value;
                this.OnPropertyChanged("Email");
            }
        }

        private String WaitPerson_;
        public String WaitPerson
        {
            get { return WaitPerson_; }
            set
            {
                WaitPerson_ = value;
                this.OnPropertyChanged("WaitPerson");
            }
        }

        private String Fontcolor_;
        public String Fontcolor
        {
            get { return Fontcolor_; }
            set
            {
                Fontcolor_ = value;
                this.OnPropertyChanged("Fontcolor");
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
