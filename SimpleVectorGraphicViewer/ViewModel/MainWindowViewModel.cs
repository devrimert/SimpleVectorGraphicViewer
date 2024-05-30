using SimpleVectorGraphicViewer.Methods;
using SimpleVectorGraphicViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _windowTitle = "Simple Vector Graphic Viewer", _statusBarText = "Plase open a file.", _activeFilePath = "", _mousePositionText = "";
        private ObservableCollection<GraphicModel> _graphics;
        private GraphicModel _selectedGraphic;
        private double _scale = 1, _scaledWidth, _scaledHeight;
        private bool _fileOpened = false;


        public string WindowTitle { get => _windowTitle + " | " + _activeFilePath; set { _windowTitle = value; OnPropertyChanged(nameof(WindowTitle)); } }
        public ObservableCollection<GraphicModel> Graphics { get => _graphics; set { _graphics = value; OnPropertyChanged(nameof(Graphics)); } }
        public GraphicModel SelectedGraphic { get => _selectedGraphic; set { _selectedGraphic = value; OnPropertyChanged(nameof(SelectedGraphic)); } }
        public double Scale { get => _scale; set { _scale = value; OnPropertyChanged(nameof(Scale)); } }
        public double ScaledHeight { get => _scaledHeight; set { _scaledHeight = value; OnPropertyChanged(nameof(ScaledHeight)); } }
        public double ScaledWidth { get => _scaledWidth; set { _scaledWidth = value; OnPropertyChanged(nameof(_scaledWidth)); } }
        public string StatusBarText { get => _statusBarText; set { _statusBarText = value; OnPropertyChanged(nameof(StatusBarText)); } }
        public string ActiveFilePath { get => _activeFilePath; set { _activeFilePath = value; OnPropertyChanged(nameof(ActiveFilePath)); } }
        public bool FileOpened { get => !string.IsNullOrEmpty(_activeFilePath); }
        public string MousePositionText { get => _mousePositionText; set { _mousePositionText = value; OnPropertyChanged(nameof(MousePositionText)); } }


        public ICommand OpenFileCommand { get; }
        public ICommand ExitCommand { get; }


        public MainWindowViewModel()
        {
            Graphics = new ObservableCollection<GraphicModel>();
            OpenFileCommand = new ViewModelCommand(ExecuteOpenFileCommand);           
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
            Common.MainWindow.SizeChanged += MainWindow_SizeChanged;
            Common.MainWindow.Loaded += MainWindow_Loaded;
            Common.MainCanvas.SizeChanged += MainCanvas_SizeChanged;
            //Common.MainCanvas.LayoutUpdated += MainCanvas_LayoutUpdated;
        }

        private void ExecuteOpenFileCommand(object obj)
        {
            Graphics.Clear();
            string filepath = FileProcesses.SelectSourceFile();
            Graphics = FileProcesses.GetGraphics(filepath) ?? Graphics;
            SetCanvas();
        }
        private void SetCanvas()
        {
            Common.MainCanvas.Children.Clear();
            ScrollViewer sviewer = (ScrollViewer)Common.MainCanvas.Parent;
            Border brdr = (Border)sviewer.Parent;
            if (Scale == 1)
            {
                double newheight = brdr.ActualHeight;
                double newwidth = brdr.ActualWidth;
                Common.MainCanvas.Height = newheight;
                Common.MainCanvas.Width = newwidth;
            }
            if (this.Graphics.Count > 0)
            {
                foreach (GraphicModel model in this.Graphics)
                    model.GenerateValues();
                ImplementShapes();
            }
            CreateAxisLines();
            sviewer.ScrollToVerticalOffset(sviewer.ScrollableHeight / 2);
            sviewer.ScrollToHorizontalOffset(sviewer.ScrollableWidth / 2);
        }
        private void CreateAxisLines()
        {
            Line xLine = new Line();
            xLine.X1 = (Common.MainCanvas.Width / 2) * -1;
            xLine.Y1 = 0;
            xLine.X2 = (Common.MainCanvas.Width / 2);
            xLine.Y2 = 0;
            xLine.Stroke = Brushes.Blue;
            xLine.StrokeThickness = 1;
            double left = (Common.MainCanvas.Width - xLine.RenderSize.Width) / 2;
            double top = (Common.MainCanvas.Height - xLine.RenderSize.Height) / 2;
            Canvas.SetLeft(xLine, left);
            Canvas.SetTop(xLine, top);
            Common.MainCanvas.Children.Add(xLine);
            Line yLine = new Line();
            yLine.X1 = 0;
            yLine.Y1 = (Common.MainCanvas.Height / 2) * -1;
            yLine.X2 = 0;
            yLine.Y2 = (Common.MainCanvas.Height / 2);
            yLine.Stroke = Brushes.Green;
            yLine.StrokeThickness = 1;
            left = (Common.MainCanvas.Width - yLine.RenderSize.Width) / 2;
            top = (Common.MainCanvas.Height - yLine.RenderSize.Height) / 2;
            Canvas.SetLeft(yLine, left);
            Canvas.SetTop(yLine, top);
            Common.MainCanvas.Children.Add(yLine);
        }
        private void ImplementShapes()
        {
            List<Rect> boundcollection = new List<Rect>();
            foreach (GraphicModel graphicModel in Graphics)
                boundcollection.Add(graphicModel.Bounds);
            Rect bounds = Common.GetBoundingRect(boundcollection);
            Rect canvasBounds = new Rect(0, 0, Common.MainCanvas.Width, Common.MainCanvas.Height);
            double width, height;
            double newscale = Common.CalculateOptimalCanvas(canvasBounds, bounds, out height, out width);
            Common.MainCanvas.Height = height;
            Common.MainCanvas.Width = width;
            foreach (GraphicModel graphicModel in Graphics)
            {
                graphicModel.GenerateValues();
                Shape shape = graphicModel.GraphicShape;
                Common.MainCanvas.Children.Add(shape);
            }
            Scale = newscale;
        }

        private void ExecuteExitCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void MainCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            MessageBox.Show("MainCanvas_LayoutUpdated");
        }

        private void MainCanvas_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            MessageBox.Show("CanvasSizeChanged");
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("MainWindowLoaded");
        }

        private void MainWindow_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            MessageBox.Show("MainWindow SizeChanged");
        }
    }
}
