using System.Windows;
using SimpleVectorGraphicViewerV2.View;
using SimpleVectorGraphicViewerV2.Statics;

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
            main.Show();
        }
    }
}
