using SimpleVectorGraphicViewer.Methods;
using SimpleVectorGraphicViewer.View;
using SimpleVectorGraphicViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleVectorGraphicViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainView main = new MainView();
            Common.MainWindow = main;
            Common.MainCanvas = main.Viewport;
            main.DataContext = new MainViewModel();
            main.Show();
        }
    }
}
