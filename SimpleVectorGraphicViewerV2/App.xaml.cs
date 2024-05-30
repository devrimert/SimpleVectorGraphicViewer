using SimpleVectorGraphicViewerV2.Statics;
using SimpleVectorGraphicViewerV2.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleVectorGraphicViewerV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainView main = new MainView();
            Common.MainView = main;
            main.DataContext = new ViewModel.MainViewModel();
            main.Show();
        }
    }
}
