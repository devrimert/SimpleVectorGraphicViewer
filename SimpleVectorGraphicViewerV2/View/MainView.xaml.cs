using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleVectorGraphicViewerV2.Model;
using SimpleVectorGraphicViewerV2.Statics;
using static SimpleVectorGraphicViewerV2.Statics.PropertyViewerMethods;

namespace SimpleVectorGraphicViewerV2.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        //Fields
        ObservableCollection<GraphicModel> _graphics;
        GraphicModel _selectedGraphicModel;
        bool _fileActive =false;
        double _activeScale = 1;

        //Builder
        public MainView()
        {
            InitializeComponent();
            _graphics = new ObservableCollection<GraphicModel>();
            _selectedGraphicModel = null;
        }

        #region 'Window Events'
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCanvas();
        }
        #endregion

        #region 'Canvas Events'
        private void canvas_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }
        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.InitializeCanvas();
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_fileActive)
            {
                Point mousePos = e.GetPosition(canvas);
                Point canvasPos = canvas.GetMousePositionOnCanvas(mousePos);
                SetTextBlock(mousePosTB, $"X: {Math.Round(canvasPos.X, 3)} , Y: {Math.Round(canvasPos.Y, 3)}");
            }
            else
            {
                SetTextBlock(mousePosTB, "");
            }
        }
        private void CanvasShape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int shapeIndex = canvas.Children.IndexOf((UIElement)sender);
            if (shapeIndex != -1)
            {
                if (_selectedGraphicModel != _graphics[shapeIndex])
                {
                    if (_selectedGraphicModel != null)
                    {
                        Shape oldSelectedShape = (Shape)canvas.Children[_graphics.IndexOf(_selectedGraphicModel)];
                        oldSelectedShape.Stroke = CanvasMethods.GetSolidColorBrush(_selectedGraphicModel.Color);
                    }
                    Shape shape = (Shape)sender;
                    shape.Stroke = Brushes.White;
                    _selectedGraphicModel = _graphics[shapeIndex];
                    
                }
                else
                {
                    Shape shape = (Shape)sender;
                    shape.Stroke = CanvasMethods.GetSolidColorBrush(_graphics[shapeIndex].Color);
                    _selectedGraphicModel = null;
                }
            }
            SetProperties(shapeIndex);

        }

        #endregion

        #region 'Command Events'

        #region 'File'
        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            string filepath = FileMethods.SelectSourceFile();
            if(string.IsNullOrEmpty(filepath)) { return; }
            ResetWindow();
            SetTextBlock(activeFileNameTB, filepath);
            SetWindowTitle("Simple Vector Graphic Viewer | " + filepath);
            _fileActive = true;           
            canvas.InitializeCanvas();
            _graphics = FileMethods.GetGraphics(filepath);
            SetCanvas();
        }
      
        private void CloseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            canvas.InitializeCanvas();
            ResetWindow();
        }
        //seperator
        private void CloseProgramBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 'View'
        private void ShowAxisCB_Checked(object sender, RoutedEventArgs e)
        {
            if (_fileActive)
            {
                if (ShowAxisCB.IsChecked == true)
                    canvas.CreateAxisLines(_activeScale);
                else
                {
                    canvas.Children.RemoveAt(canvas.Children.Count - 1);
                    canvas.Children.RemoveAt(canvas.Children.Count - 1);
                }
            }
        }
        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        #endregion

        #region 'About'
        private void GitHubPageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenUrlInDefaultBrowser("https://github.com/devrimert");
        }
        private void LinkedInPageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenUrlInDefaultBrowser("https://de.linkedin.com/in/dmyoyen/de");
        }
        private void XingPagEBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenUrlInDefaultBrowser("https://www.xing.com/profile/DevrimMert_Yoeyen");
        }
        #endregion

        #endregion

        #region 'Methods'
        /// <summary>
        /// Sets canvas and puts shapes if it is necessary.
        /// </summary>
        private void SetCanvas()
        {
            canvas.Children.Clear();
            _activeScale = 1;
            if (_graphics.Count > 0)
            {
                List<CarthesianRangeBoxModel> carthesianRangeBoxModels = new List<CarthesianRangeBoxModel>();
                foreach (GraphicModel graphicsModel in _graphics)
                    carthesianRangeBoxModels.Add(graphicsModel.RangeBox);
                CarthesianRangeBoxModel totalBox = CarthesianMethods.SummerizeRangeBoxes(carthesianRangeBoxModels);
                canvas.SetCanvasSize(totalBox);
                foreach (GraphicModel graphicsModel in _graphics)
                {
                    canvas.AddCarthesianShape(graphicsModel);
                    canvas.Children[canvas.Children.Count - 1].MouseDown += CanvasShape_MouseDown; ;
                }
                canvas.UpdateLayout();
                _activeScale = canvas.SetCanvasScaleForObjects(totalBox, ScaleInCB.IsChecked);
                SetTextBlock(currentScaleTB, "%" +  Math.Round(_activeScale * 100).ToString());
                if (ShowAxisCB.IsChecked == true)
                    canvas.CreateAxisLines(_activeScale);
                canvas.ScrollCenter();
            }                       
        }
        /// <summary>
        /// Opens desired URL in current browser
        /// </summary>
        /// <param name="url">Desired URL</param>
        void OpenUrlInDefaultBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open URL. | {ex.Message}");
            }
        }
        /// <summary>
        /// Sets a TextBox's Text property using desired string.
        /// </summary>
        /// <param name="TextBlock">Textbox</param>
        /// <param name="NewValue">New value for TextBox</param>
        void SetTextBlock(TextBlock TextBlock, string NewValue)
        {
            TextBlock.Text = NewValue;
        }
        /// <summary>
        /// Sets Window title.
        /// </summary>
        /// <param name="Title">New window title</param>
        void SetWindowTitle(string Title)
        {
            this.Title = Title;
        }
        /// <summary>
        /// Sets the properties window by information of the given shape.
        /// </summary>
        /// <param name="ShapeIndex">Index of the shape</param>
        void SetProperties(int ShapeIndex)
        {
            if(ShapeIndex != -1&&_selectedGraphicModel!=null)
            {                
                List<PropertyDisplay> props = GetPropertyDisplayOfShape(_graphics[ShapeIndex], (Shape)canvas.Children[ShapeIndex]); 
                PropertyViewDG.ItemsSource = props;
            }
            else
            {
                PropertyViewDG.ItemsSource = null;
            }
        }
        /// <summary>
        /// Resets and prepares the window to next process.
        /// </summary>
        private void ResetWindow()
        {
            _fileActive = false;
            _graphics.Clear();
            _selectedGraphicModel = null;
            _activeScale = 1;
            canvas.Children.Clear();
            SetProperties(-1);
            SetTextBlock(activeFileNameTB, "");
            SetTextBlock(mousePosTB, "");

            SetWindowTitle("Simple Vector Graphic Viewer");
        }

        #endregion
    }
}
