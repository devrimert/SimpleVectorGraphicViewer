using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SimpleVectorGraphicViewerV2.Model;
using SimpleVectorGraphicViewerV2.Statics;

namespace SimpleVectorGraphicViewerV2.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        string _mousePositionString = "", _windowTitle = "Simple Vector Graphic Viewer", _activeFilePath = "", _currentScale="";        
        GraphicModel _selectedGraphicModel;
        ObservableCollection<GraphicModel> _graphics;

        public GraphicModel SelectedGraphicModel { get => _selectedGraphicModel; set {  _selectedGraphicModel = value; OnPropertyChanged(nameof(SelectedGraphicModel)); } }
        public string MousePositionString { get => _mousePositionString; set { _mousePositionString = value; OnPropertyChanged(nameof(MousePositionString)); } }
        //public string WindowTitle { get => _windowTitle; set { _windowTitle = value; OnPropertyChanged(nameof(WindowTitle)); } }    
        //public string ActiveFilePath { get => _activeFilePath; set { _activeFilePath = value; OnPropertyChanged(nameof(ActiveFilePath)); } }
        //public string CurrentScale { get => _currentScale; set { _currentScale = value; OnPropertyChanged(nameof(CurrentScale)); } }    
        public ICommand OpenFileCommand { get; }
        public MainViewModel()
        {
            _graphics = new ObservableCollection<GraphicModel>();
            SelectedGraphicModel = null;
            OpenFileCommand = new ViewModelCommand(GetExecuteOpenFileCommand);
            Common.MainView.canvas.Loaded += Canvas_Loaded;
            Common.MainView.SizeChanged += MainView_SizeChanged;
            Common.MainView.canvas.MouseMove += Canvas_MouseMove;            
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(Common.MainView.canvas);
            Point canvasPos = Common.MainView.canvas.GetMousePositionOnCanvas(mousePos);
            MousePositionString = $"X: {canvasPos.X} , Y: {canvasPos.Y}";
        }

        private void MainView_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            SetCanvas();
        }
        private void Canvas_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SetCanvas();
        }

        private void GetExecuteOpenFileCommand(object obj)
        {
            string filepath = FileMethods.SelectSourceFile();
            _graphics.Clear();
            Common.MainView.canvas.Children.Clear();
            _graphics = FileMethods.GetGraphics(filepath);
            SetCanvas();
        }
        private void SetCanvas()
        {
            Common.MainView.canvas.Children.Clear();
            if (_graphics.Count > 0) 
            {
                List<CarthesianRangeBoxModel> carthesianRangeBoxModels = new List<CarthesianRangeBoxModel>();
                foreach (GraphicModel graphicsModel in _graphics)
                    carthesianRangeBoxModels.Add(graphicsModel.RangeBox);
                CarthesianRangeBoxModel totalBox = CarthesianMethods.SummerizeRangeBoxes(carthesianRangeBoxModels);
                Common.MainView.canvas.SetCanvasSize(totalBox);                            
                foreach (GraphicModel graphicsModel in _graphics)
                {
                    Common.MainView.canvas.AddCarthesianShape(graphicsModel);
                    Common.MainView.canvas.Children[Common.MainView.canvas.Children.Count - 1].MouseDown += MainViewModel_MouseDown;
                }
                Common.MainView.canvas.UpdateLayout();
                double scale = Common.MainView.canvas.SetCanvasScaleForObjects(totalBox,false);
                Common.MainView.canvas.CreateAxisLines(scale);
                Common.MainView.canvas.ScrollCenter();
            }
        }

        private void MainViewModel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int shapeIndex = Common.MainView.canvas.Children.IndexOf((UIElement)sender);
            if (shapeIndex != -1)
            {
                if (SelectedGraphicModel != _graphics[shapeIndex])
                {
                    if(SelectedGraphicModel!= null)
                    {
                        Shape oldSelectedShape = (Shape)Common.MainView.canvas.Children[_graphics.IndexOf(SelectedGraphicModel)];
                        oldSelectedShape.Stroke = CanvasMethods.GetSolidColorBrush(SelectedGraphicModel.Color);
                    }
                    Shape shape = (Shape)sender;
                    shape.Stroke = Brushes.White;
                    SelectedGraphicModel = _graphics[shapeIndex];
                }
                else
                {
                    Shape shape = (Shape)sender;
                    shape.Stroke = CanvasMethods.GetSolidColorBrush(_graphics[shapeIndex].Color);
                    SelectedGraphicModel = null;
                }
            }
        }
        
    }
}
