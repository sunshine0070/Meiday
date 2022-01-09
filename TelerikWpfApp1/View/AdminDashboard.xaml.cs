using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Meiday.ViewModel;

namespace Meiday.View
{
    /// <summary>
    /// AdminDashboard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }
        //Insert the code needed to create the object below this point.
        bool _closinganimation = true; //closing 기본값 true 
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = _closinganimation; //기본값 true
            _closinganimation = false; //false 수정
            //base.OnClosing(e);

            System.Windows.Media.Animation.Storyboard sb = new System.Windows.Media.Animation.Storyboard();
            System.Windows.Media.Animation.DoubleAnimation dh = new System.Windows.Media.Animation.DoubleAnimation();
            System.Windows.Media.Animation.DoubleAnimation dw = new System.Windows.Media.Animation.DoubleAnimation();
            System.Windows.Media.Animation.DoubleAnimation dop = new System.Windows.Media.Animation.DoubleAnimation();
            dop.Duration = dh.Duration = dw.Duration = sb.Duration = new Duration(new TimeSpan(0, 0, 2));
            dop.To = dh.To = dw.To = 0;
            System.Windows.Media.Animation.Storyboard.SetTarget(dop, this); //투명도
            System.Windows.Media.Animation.Storyboard.SetTarget(dh, this);  // 높이
            System.Windows.Media.Animation.Storyboard.SetTarget(dw, this);  // 너비
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(dop, new PropertyPath("Opacity", new object[] { }));
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(dh, new PropertyPath("Height", new object[] { }));
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(dw, new PropertyPath("Width", new object[] { }));
            sb.Children.Add(dh);
            sb.Children.Add(dw);
            sb.Children.Add(dop);
            sb.Completed += new EventHandler(sb_Completed);//(a, b) => {this.Close(); };
            sb.Begin();
        }

        void sb_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
