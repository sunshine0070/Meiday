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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Meiday
{
    /// <summary>
    /// WindowPharmacy02.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowPharmacy02 : UserControl
    {
        public WindowPharmacy02()
        {
            Log.Debug("WindowPharmacy02 Start!");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "WindowPharmacy02 Error!!");
            }
        }
    }
}
