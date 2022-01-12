using DevExpress.Xpf.Charts;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Meiday.View.AdminChartView
{
    /// <summary>
    /// Admin_RealChart_View.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Admin_RealChart_View : UserControl
    {
        public Admin_RealChart_View()
        {
            InitializeComponent();
        }

        private void chart_BoundDataChanged(object sender, RoutedEventArgs e)
        {
            // Adjust the visual range.
            AxisX2D axisX = ((XYDiagram2D)chart.Diagram).ActualAxisX;
            DateTime maxRangeValue = new DateTime(2022, 01, 12, 23, 59, 59);
            axisX.ActualVisualRange.SetMinMaxValues(maxRangeValue.AddHours(-13), maxRangeValue);
        }
    }
}
