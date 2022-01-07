using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meiday.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        private int switchView;

        public int SwitchView
        {
            get { 
                return switchView; 
            }
            set 
            { 
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }

        public ICommand SwitchViewCommand { get; }

        public AdminViewModel()
        {
            SwitchView = 1;
            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));
        }

        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());

            if (SwitchView == 1) 
            {
                SwitchView = 1;
            }
            else if (SwitchView == 2)
            {
                SwitchView = 2;
            }
            else if (SwitchView == 3)
            {
                SwitchView = 3;
            }
            else if (SwitchView == 4)
            {
                SwitchView = 4;
            }



        }

    }
}
