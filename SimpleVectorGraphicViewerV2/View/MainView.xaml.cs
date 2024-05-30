using SimpleVectorGraphicViewerV2.Model;
using SimpleVectorGraphicViewerV2.Statics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SimpleVectorGraphicViewerV2.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        GraphicModel _selectedGraphicModel;
        ObservableCollection<GraphicModel> _graphics;
        public MainView()
        {
            InitializeComponent();
        }

        //private void Canvas_MouseMove(object sender, MouseEventArgs e)
        //{
        //        Point mousePos = e.GetPosition(canvas);
        //        Point canvasPos = Common.MainView.canvas.GetMousePositionOnCanvas(mousePos);
        //        mousePoText.Text = $"X: {canvasPos.X} , Y: {canvasPos.Y}";
        //}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void canvas_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(canvas);
            Point canvasPos = Common.MainView.canvas.GetMousePositionOnCanvas(mousePos);
            mousePoText.Text = $"X: {canvasPos.X} , Y: {canvasPos.Y}";
        }
    }
}
