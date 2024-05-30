using SimpleVectorGraphicViewerV2.Model;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class CanvasMethods
    {
        #region 'Canvas Transitions'
        internal static Point GetCanvasPoint(this Canvas Canvas, string Point)
        {
            try
            {
                CultureInfo culture = CultureInfo.InvariantCulture;
                string[] values = Point.Split(';');

                double x = (Canvas.Width / 2) + double.Parse(values[0].Replace(',', '.'), culture);
                double y = (Canvas.Height / 2) + double.Parse(values[1].Replace(',', '.'), culture);
                return new Point(x, Canvas.Height - y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Canvas point parsing error: " + ex.Message);
                throw ex;
            }
        }
        internal static Point GetCanvasPoint(this Canvas Canvas, Point Point)
        {
            try
            {
                double x = (Canvas.Width / 2) + Point.X;
                double y = (Canvas.Height / 2) + Point.Y;
                return new Point(x, Canvas.Height - y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Canvas point moving error: " + ex.Message);
                throw ex;
            }
        }
        internal static Rect GetCanvasRect(this Canvas Canvas, CarthesianRangeBoxModel RangeBox)
        {
            return new Rect(Canvas.GetCanvasPoint(RangeBox.MinPoint), Canvas.GetCanvasPoint(RangeBox.MaxPoint));
        }
        public static SolidColorBrush GetSolidColorBrush(string argbString)
        {
            try
            {
                string[] values = argbString.Split(';');
                byte a = byte.Parse(values[0]);
                byte r = byte.Parse(values[1]);
                byte g = byte.Parse(values[2]);
                byte b = byte.Parse(values[3]);
                return new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ARGB converting error: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region 'Shape Creation
        public static void CreateAxisLines(this Canvas Canvas, double Scale)
        {
            double thickness = 1 / Scale;
            Line xLine = new Line();
            xLine.X1 = (Canvas.Width / 2) * -1;
            xLine.Y1 = 0;
            xLine.X2 = (Canvas.Width / 2);
            xLine.Y2 = 0;
            xLine.Stroke = Brushes.Blue;
            xLine.StrokeThickness = thickness;
            double left = (Canvas.Width - xLine.RenderSize.Width) / 2;
            double top = (Canvas.Height - xLine.RenderSize.Height) / 2;
            Canvas.SetLeft(xLine, left);
            Canvas.SetTop(xLine, top);
            Canvas.Children.Add(xLine);
            Line yLine = new Line();
            yLine.X1 = 0;
            yLine.Y1 = (Canvas.Height / 2) * -1;
            yLine.X2 = 0;
            yLine.Y2 = (Canvas.Height / 2);
            yLine.Stroke = Brushes.Green;
            yLine.StrokeThickness = thickness;
            left = (Canvas.Width - yLine.RenderSize.Width) / 2;
            top = (Canvas.Height - yLine.RenderSize.Height) / 2;
            Canvas.SetLeft(yLine, left);
            Canvas.SetTop(yLine, top);
            Canvas.Children.Add(yLine);
        }
        public static void AddCarthesianShape(this Canvas Canvas, GraphicModel Model)
        {
            if (Model is LineModel line)
                Canvas.AddLineModel(line);
            else if (Model is CircleModel circle)
                Canvas.AddCircleModel(circle);
            else if (Model is TriangleModel triangle)
                Canvas.AddTriangleModel(triangle);
        }
        private static void AddLineModel(this Canvas Canvas, LineModel Model)
        {
            Line line = new Line();
            line.X1 = Canvas.GetCanvasPoint(Model.CarthesianA).X;
            line.Y1 = Canvas.GetCanvasPoint(Model.CarthesianA).Y;
            line.X2 = Canvas.GetCanvasPoint(Model.CarthesianB).X;
            line.Y2 = Canvas.GetCanvasPoint(Model.CarthesianB).Y;
            line.Stroke = GetSolidColorBrush(Model.Color);
            line.StrokeThickness = Common.DefaultShapeThickness;
            Canvas.Children.Add(line);
        }
        private static void AddCircleModel(this Canvas Canvas, CircleModel Model)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Model.RangeBox.Width;
            ellipse.Height = Model.RangeBox.Height;
            ellipse.SetValue(Canvas.LeftProperty, Canvas.GetCanvasRect(Model.RangeBox).Left);
            ellipse.SetValue(Canvas.TopProperty, Canvas.GetCanvasRect(Model.RangeBox).Top);
            ellipse.Stroke = GetSolidColorBrush(Model.Color);
            ellipse.StrokeThickness = Common.DefaultShapeThickness;
            ellipse.Fill = Model.Filled == "true" ? GetSolidColorBrush(Model.Color) : Brushes.Transparent;
            Canvas.Children.Add(ellipse);
        }
        private static void AddTriangleModel(this Canvas Canvas, TriangleModel Model)
        {
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = Canvas.GetCanvasPoint(Model.CarthesianA);
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianB), true));
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianC), true));
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianA), true));
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            Path path = new Path();
            path.Data = pathGeometry;
            path.Stroke = GetSolidColorBrush(Model.Color);
            path.StrokeThickness = Common.DefaultShapeThickness;
            path.Fill = Model.Filled == "True" ? GetSolidColorBrush(Model.Color) : Brushes.Transparent;
            Canvas.Children.Add(path);
        }
        #endregion

        #region 'Canvas Size and Scale'
        public static double GetPerfectScale(this Canvas Canvas, CarthesianRangeBoxModel ObjectsRangeBox, double tolerance = 10)
        {
            Border border = Canvas.GetParentBorder();
            double scaleX = border.ActualWidth / (ObjectsRangeBox.RequiredViewportWidth + tolerance);
            double scaleY = border.ActualHeight / (ObjectsRangeBox.RequiredViewportHeight + tolerance);
            return Math.Min(scaleX, scaleY);
        }
        public static void SetCanvasSize(this Canvas Canvas, CarthesianRangeBoxModel ObjectsRangeBox)
        {
            Border border = Canvas.GetParentBorder();
            double Height = ObjectsRangeBox.RequiredViewportHeight;
            double Width = ObjectsRangeBox.RequiredViewportWidth;
            bool scaledDown = Width > border.ActualWidth || Height > border.ActualHeight;
            Canvas.Height = scaledDown ? Height : border.ActualHeight;
            Canvas.Width = scaledDown ? Width : border.ActualWidth;
            Canvas.UpdateLayout();
        }
        public static double SetCanvasScaleForObjects(this Canvas Canvas, CarthesianRangeBoxModel ObjectsRangeBox , bool AlsoScaleIn)
        {
            double scale = GetPerfectScale(Canvas, ObjectsRangeBox);
            if(scale > 1)
            {
                if (AlsoScaleIn)
                {
                    Border parentBorder = Canvas.GetParentBorder();
                    ScaleTransform scaleTransform = new ScaleTransform(scale, scale, parentBorder.ActualWidth / 2, parentBorder.ActualHeight / 2);
                    Common.MainView.canvas.RenderTransform = scaleTransform;
                }
                else
                {
                    scale = 1;
                }              
            }
            else
            {
                ScaleTransform scaleTransform = new ScaleTransform(scale, scale, ObjectsRangeBox.RequiredViewportWidth / 2 , ObjectsRangeBox.RequiredViewportHeight / 2);
                Common.MainView.canvas.RenderTransform = scaleTransform;
            }            
            return scale;
        }
        #endregion

        #region 'Border and Scroll'
        public static Border GetParentBorder(this Canvas Canvas)
        {
            return  (Border)((ScrollViewer)Canvas.Parent).Parent;
        }
        public static ScrollViewer GetParentScrollViewer(this Canvas Canvas)
        {
            return (ScrollViewer)Canvas.Parent;
        }
        public static void ScrollCenter(this Canvas Canvas)
        {
            ScrollViewer scrollViewer = GetParentScrollViewer(Canvas);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight / 2);
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.ScrollableWidth / 2);
        }      
        #endregion
        public static Point GetMousePositionOnCanvas(this Canvas Canvas, Point MousePosition)
        {
            return new Point((Canvas.Width/2) - MousePosition.X, (Canvas.Height/2) - MousePosition.Y);
        }
    }
}
