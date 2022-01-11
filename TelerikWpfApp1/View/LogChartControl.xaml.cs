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

namespace Meiday.View
{
    /// <summary>
    /// LogChartControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LogChartControl : UserControl
    {
        public LogChartControl()
        {
            InitializeComponent();
        }
        private void chart_BoundDataChanged(object sender, RoutedEventArgs e)
        {
            // Adjust the visual range.
            AxisX2D axisX = ((XYDiagram2D)chart.Diagram).ActualAxisX;
            DateTime maxRangeValue = (DateTime)axisX.ActualWholeRange.ActualMaxValue;
            axisX.ActualVisualRange.SetMinMaxValues(maxRangeValue.AddSeconds(-10), maxRangeValue);
        }
    }
}
