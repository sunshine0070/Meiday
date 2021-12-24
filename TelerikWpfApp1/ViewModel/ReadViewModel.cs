using System.Windows.Input;
using System.IO;

namespace Meiday
{
    public class ReadViewModel : ViewModelBase
    {
        private string detailed01 = System.IO.File.ReadAllText(@"../../Resource/Insu01Detailed01.txt");
        public string Detailed01
        {
            get { return detailed01; }
            set
            {
                OnPropertyChanged("Detailed01");
            }
        }

        private string detailed02 = System.IO.File.ReadAllText(@"../../Resource/Insu01Detailed02.txt");
        public string Detailed02
        {
            get { return detailed02; }
            set
            {
                OnPropertyChanged("Detailed02");
            }
        }

        private string detailed03 = System.IO.File.ReadAllText(@"../../Resource/Insu01Detailed02.txt");
        public string Detailed03
        {
            get { return detailed03; }
            set
            {
                OnPropertyChanged("Detailed03");
            }
        }
    }
}
