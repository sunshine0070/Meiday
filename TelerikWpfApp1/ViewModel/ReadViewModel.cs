using System.Windows.Input;
using System.IO;

namespace Meiday
{
    public class ReadViewModel : ViewModelBase
    {
        private string detailed01 = File.ReadAllText(@"../../Resource/Insu01Detailed01.txt");
        public string Detailed01
        {
            get { return detailed01; }
            set
            {
                Log.Debug("Detailed01");
                OnPropertyChanged("Detailed01");
            }
        }

        private string detailed02 = File.ReadAllText(@"../../Resource/Insu01Detailed02.txt");
        public string Detailed02
        {
            get { return detailed02; }
            set
            {
                Log.Debug("Detailed02");
                OnPropertyChanged("Detailed02");
            }
        }

        private string detailed03 = File.ReadAllText(@"../../Resource/Insu01Detailed02.txt");
        public string Detailed03
        {
            get { return detailed03; }
            set
            {
                Log.Debug("Detailed03");
                OnPropertyChanged("Detailed03");
            }
        }
    }
}
