using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Meiday
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Log.Debug("App Start!");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "App Error!!");
            }
        }
    }
}
