﻿using System;
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
using System.Windows.Shapes;

namespace Meiday
{
    /// <summary>
    /// WindowInsurance02.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowInsurance02 : UserControl
    {
        public WindowInsurance02()
        {
            Log.Debug("WindowInsurance02");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "WindowInsurance02");
            }
        }
    }
}
