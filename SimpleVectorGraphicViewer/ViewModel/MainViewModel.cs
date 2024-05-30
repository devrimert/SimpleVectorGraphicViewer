using SimpleVectorGraphicViewer.Model;
using SimpleVectorGraphicViewer.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.CodeDom.Compiler;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Windows.Media;

namespace SimpleVectorGraphicViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _windowTitle = "Simple Vector Graphic Viewer", _statusBarText ="Plase open a file.", _activeFilePath = "", _mousePositionText = "";
        private ObservableCollection<GraphicModel> _graphics;
        private GraphicModel _selectedGraphic;
        private double _scale = 1, _scaledWidth, _scaledHeight;
        private bool _fileOpened = false;
        
        
        public string WindowTitle { get => _windowTitle + " | " + _activeFilePath ; set { _windowTitle = value; OnPropertyChanged(nameof(WindowTitle)); } } 
        public ObservableCollection<GraphicModel> Graphics { get => _graphics; set {_graphics = value; OnPropertyChanged(nameof(Graphics));   } }
        public GraphicModel SelectedGraphic { get => _selectedGraphic; set { _selectedGraphic = value; OnPropertyChanged(nameof(SelectedGraphic)); } }
        public double Scale { get => _scale; set { _scale = value; OnPropertyChanged(nameof(Scale));  } }
        public double ScaledHeight { get => _scaledHeight; set { _scaledHeight = value; OnPropertyChanged(nameof(ScaledHeight)); } }
        public double ScaledWidth { get => _scaledWidth; set { _scaledWidth = value; OnPropertyChanged(nameof(_scaledWidth)); } }
        public string StatusBarText { get => _statusBarText; set { _statusBarText = value; OnPropertyChanged(nameof(StatusBarText)); } }
        public string ActiveFilePath { get => _activeFilePath; set { _activeFilePath = value; OnPropertyChanged(nameof(ActiveFilePath)); } }
        public bool FileOpened { get => !string.IsNullOrEmpty(_activeFilePath); }
        public string MousePositionText { get => _mousePositionText; set { _mousePositionText = value; OnPropertyChanged(nameof(MousePositionText)); } }    
        

        public ICommand OpenFileCommand { get; }
        public ICommand CloseFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveAsXMLCommand { get; }
        public ICommand SaveAsJSONCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand OpenDevsLinkedInCommand { get; }
        public ICommand OpenDevsGitHubCommand { get; }
        public ICommand OpenDevsXingCommand { get; }
        public ICommand ZoomInCommand { get; }
        public ICommand ZoomOutCommand { get; }

        public MainViewModel()
        {
            LoadPage();
            Graphics = new ObservableCollection<GraphicModel>();
            OpenFileCommand = new ViewModelCommand(ExecuteOpenFileCommand);
            CloseFileCommand = new ViewModelCommand(ExecuteCloseFileCommand, IsFileOpened);
            SaveFileCommand = new ViewModelCommand(ExecuteSaveFileCommand, IsFileOpened);
            SaveAsXMLCommand = new ViewModelCommand(ExecuteSaveAsXMLCommand, IsFileChanged);
            SaveAsJSONCommand = new ViewModelCommand(ExecuteSaveAsJSONCommand, IsFileChanged);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
            ShowAboutCommand = new ViewModelCommand(ExecuteShowAboutCommand);
            OpenDevsLinkedInCommand = new ViewModelCommand(ExecuteOpenDevsLinkedInCommand);
            OpenDevsGitHubCommand = new ViewModelCommand(ExecuteOpenDevsGitHubCommand);
            OpenDevsXingCommand = new ViewModelCommand(ExecuteOpenDevsXingCommand);
            ZoomInCommand = new ViewModelCommand(ExecuteZoomInCommand);
            ZoomOutCommand = new ViewModelCommand(ExecuteZoomOutCommand);
            Common.MainWindow.SizeChanged += MainWindow_SizeChanged;
            Common.MainWindow.Loaded += MainWindow_Loaded;
            Common.MainCanvas.SizeChanged += MainCanvas_SizeChanged;
        }


        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Common.MainCanvas = (Canvas)sender;
            Common.CanvasCenter = new Point(Common.MainCanvas.ActualWidth / 2, Common.MainCanvas.ActualHeight/2);
            if (Common.MainCanvas.IsLoaded)
                SetCanvas();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetCanvas();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (Common.MainCanvas.IsLoaded)
                //SetCanvas();
        }

        private void SetStatusBarText()
        {
            string statusBarText = "";
            if (FileOpened)
                statusBarText += ActiveFilePath + " | ";
            statusBarText += $"Scale = {Scale} | Canvas Width = {Common.MainCanvas.Width} | Canvas Height = {Common.MainCanvas.Height} | Canvas Render Size Width = {Common.MainCanvas.RenderSize.Width} | Canvas Render Size Height = {Common.MainCanvas.RenderSize.Height}";
            StatusBarText = statusBarText;
        }

        private void ExecuteZoomOutCommand(object obj)
        {
            double oldscale = Scale;
            Scale -= 0.1;
            SetStatusBarText();

        }

        private void ExecuteZoomInCommand(object obj)
        {
            double oldscale = Scale;
            Scale += 0.1;
            SetStatusBarText();
        }

        private void LoadPage()
        {
            _graphics = new ObservableCollection<GraphicModel>();
            _selectedGraphic = null;
        }

        private void SetCanvas()
        {
            foreach (UIElement element in Common.MainCanvas.Children)
            {
                element.MouseDown -= Element_MouseDown;
            }
            Common.MainCanvas.Children.Clear();
            ScrollViewer sviewer = (ScrollViewer)Common.MainCanvas.Parent;
            Border brdr = (Border)sviewer.Parent;
            //if(Scale ==1)
            //{
            //    double newheight = brdr.ActualHeight;
            //    double newwidth = brdr.ActualWidth;
            //    Common.MainCanvas.Height = newheight;
            //    Common.MainCanvas.Width = newwidth;
            //}
            
        
            
            if(this.Graphics.Count >0)
            {
                foreach(GraphicModel model in this.Graphics)
                    model.GenerateValues();
                ImplementShapes();
            }
            //CreateAxisLines();
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
            Rect canvasBounds = new Rect(0, 0, Common.MainCanvas.ActualWidth, Common.MainCanvas.ActualHeight);
            double width, height;
            double newscale = Common.CalculateOptimalCanvas(canvasBounds, bounds,  out height, out width);
           
            //Common.MainCanvas.Height = height;
            //Common.MainCanvas.Width = width;
            foreach (GraphicModel graphicModel in Graphics)
            {
                //graphicModel.GenerateValues();
                Shape shape = graphicModel.GraphicShape;
                Common.MainCanvas.Children.Add(shape);
            }
            Common.MainCanvas.Arrange(canvasBounds);
            //Scale = newscale;
        }

        #region 'MenuBarCommands'
        #region 'FileTab'
        private void ExecuteOpenFileCommand(object obj)
        {

            Graphics.Clear();
            string filepath = FileProcesses.SelectSourceFile();
            Graphics = FileProcesses.GetGraphics(filepath) ?? Graphics;
            SetCanvas();
            ActiveFilePath = filepath;
            SetStatusBarText();         
            foreach(UIElement element in Common.MainCanvas.Children)
            {
                element.MouseDown += Element_MouseDown;
            }

        }

        private void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Shape shape = (Shape)sender;
            MessageBox.Show(shape.RenderSize.Width.ToString());
            Point mousePos = e.GetPosition(Common.MainCanvas);
        }              

        private void ExecuteCloseFileCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteSaveFileCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteSaveAsXMLCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private bool CanExecuteSaveAsJSONCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteSaveAsJSONCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteExitCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private bool IsFileChanged(object obj)
        {
            return false ;
        }

        private bool IsFileOpened(object obj)
        {
            return false ;
        }

        #endregion
        #region 'ViewTab'
        #endregion
        #region 'HelpTab'
        private void ExecuteOpenDevsXingCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteOpenDevsGitHubCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteOpenDevsLinkedInCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowAboutCommand(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region 'CanvasCommands'
        #endregion

        #region 'StatusBarCommands'
        #endregion

    }
}
