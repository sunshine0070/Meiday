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
    /// WindowInsurance01.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 



    public partial class WindowInsurance01 : UserControl
    {

        public WindowInsurance01()
        {
            Log.Debug("WindowInsurance01 Start!");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "WindowInsurance01 Error!!");
            }
        }
    }
}
