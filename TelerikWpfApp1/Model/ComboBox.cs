using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meiday.Model
{
    public class ComboBox : ViewModelBase
    {

        private String Deptnum_;
        public String Deptnum
        {
            get { return Deptnum_; }
            set
            {
                Deptnum_ = value;
                this.OnPropertyChanged(Deptnum_);
            }
        }

        private String Deptname_;
        public String Deptname
        {
            get { return Deptname_; }
            set
            {
                Deptname_ = value;
                this.OnPropertyChanged(Deptname_);
            }
        }

    }
}
