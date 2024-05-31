using System;
using System.Windows;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using SimpleVectorGraphicViewerV2.Model;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class CanvasMethods
    {
        #region 'Canvas Transitions'
        /// <summary>
        /// Converts color string to color brush.
        /// </summary>
        /// <param name="argbString">ARGB string of desired color.</param>
        /// <returns>Retruns SolidColorBrush object of given string.</returns>
        internal static SolidColorBrush GetSolidColorBrush(string argbString)
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
        /// <summary>
        /// Gets the mouse position with canvas transformation.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="MousePosition">The mouse position desired to be converted.</param>
        /// <returns>Converted mouse point.</returns>
        internal static Point GetMousePositionOnCanvas(this Canvas Canvas, Point MousePosition)
        {
            return new Point(((Canvas.Width / 2) - MousePosition.X)*-1, (Canvas.Height / 2) - MousePosition.Y);
        }
        /// <summary>
        /// Converts point string to a Point object and transforms that to canvas coordinate
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Point">Carthesian Point string.</param>
        /// <returns>Canvas Point</returns>
        private static Point GetCanvasPoint(this Canvas Canvas, string Point)
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
        /// <summary>
        /// Converts and transforms a carthesian point to a Canvas point.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Point">Carthesian Point/</param>
        /// <returns>Canvas Point</returns>
        private static Point GetCanvasPoint(this Canvas Canvas, Point Point)
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
        /// <summary>
        /// Converts a Carthesian RangeBox to Canvas Rect.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="RangeBox">Carthesian Range Box</param>
        /// <returns>Canvas Rect</returns>
        private static Rect GetCanvasRect(this Canvas Canvas, CarthesianRangeBoxModel RangeBox)
        {
            return new Rect(Canvas.GetCanvasPoint(RangeBox.MinPoint), Canvas.GetCanvasPoint(RangeBox.MaxPoint));
        }
        #endregion

        #region 'Shape Creation'
        /// <summary>
        /// Creates the axis lines with desired scale. Axis lines always will be 1px thick on the screen.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Scale">Desired Scale</param>
        internal static void CreateAxisLines(this Canvas Canvas, double Scale)
        {
            double thickness = 1 / Scale;
            Line xLine = new Line();
            xLine.Name = "xAxis";
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
            yLine.Name = "yAxis";
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
        /// <summary>
        /// Adds a Shape to the canvas by desired Graphic Model.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Model">Desired Graohic Model</param>
        internal static void AddCarthesianShape(this Canvas Canvas, GraphicModel Model)
        {
            if (Model is LineModel line)
                Canvas.AddLineModel(line);
            else if (Model is EllipseModel circle)
                Canvas.AddCircleModel(circle);
            else if (Model is TriangleModel triangle)
                Canvas.AddTriangleModel(triangle);
            else if (Model is RectangleModel rectangle)
                Canvas.AddRectangleModel(rectangle);
            else if (Model is PolygonModel polygon)
                Canvas.AddPolygonModel(polygon);
        }
        /// <summary>
        /// Adds a line inside of the Canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Model">Desired Line Model.</param>
        private static void AddLineModel(this Canvas Canvas, LineModel Model)
        {
            Line line = new Line();
            line.X1 = Canvas.GetCanvasPoint(Model.CarthesianA).X;
            line.Y1 = Canvas.GetCanvasPoint(Model.CarthesianA).Y;
            line.X2 = Canvas.GetCanvasPoint(Model.CarthesianB).X;
            line.Y2 = Canvas.GetCanvasPoint(Model.CarthesianB).Y;
            line.Stroke = GetSolidColorBrush(Model.Color);
            line.StrokeThickness = Common.DefaultShapeThickness;
            line.Name = Model.Type + GetShapeCount(Canvas,Model.Type);
            Canvas.Children.Add(line);
        }
        /// <summary>
        /// Adds a circle or ellipse inside of the Canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Model">Desired Circle or Ellipse</param>
        private static void AddCircleModel(this Canvas Canvas, EllipseModel Model)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Model.WidthValue;
            ellipse.Height = Model.HeightValue;
            ellipse.SetValue(Canvas.LeftProperty, Canvas.GetCanvasRect(Model.RangeBox).Left);
            ellipse.SetValue(Canvas.TopProperty, Canvas.GetCanvasRect(Model.RangeBox).Top);
            ellipse.Stroke = GetSolidColorBrush(Model.Color);
            ellipse.StrokeThickness = Common.DefaultShapeThickness;
            ellipse.Fill = Model.Filled == "true" ? GetSolidColorBrush(Model.Color) : Brushes.Transparent;
            ellipse.Name = Model.Type + GetShapeCount(Canvas, Model.Type);
            Canvas.Children.Add(ellipse);
        }
        /// <summary>
        /// Adds a triangle inside of the Canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Model">Desired triangle.</param>
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
            path.Fill = Model.Filled.ToLower() == "true" ? GetSolidColorBrush(Model.Color) : Brushes.Transparent;
            path.Name = Model.Type + GetShapeCount(Canvas, Model.Type);
            Canvas.Children.Add(path);
        }
        /// <summary>
        /// Adds a rectangle inside of the Canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Model">Desired Rectangle</param>
        private static void AddRectangleModel(this Canvas Canvas, RectangleModel Model)
        {
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = Canvas.GetCanvasPoint(Model.CarthesianA);
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianB), true));
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianC), true));
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianD), true));
            pathFigure.Segments.Add(new LineSegment(Canvas.GetCanvasPoint(Model.CarthesianA), true));
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            Path path = new Path();
            path.Data = pathGeometry;
            path.Stroke = GetSolidColorBrush(Model.Color);
            path.StrokeThickness = Common.DefaultShapeThickness;
            path.Fill = Model.Filled.ToLower() == "true" ? GetSolidColorBrush(Model.Color) : Brushes.Transparent;
            path.Name = Model.Type + GetShapeCount(Canvas, Model.Type);
            Canvas.Children.Add(path);
        }
        /// <summary>
        /// Adds a polygon inside of the Canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="Model">Desired Polygon</param>
        private static void AddPolygonModel(this Canvas Canvas, PolygonModel Model)
        {
            Polygon polygon = new Polygon
            {
                Stroke = GetSolidColorBrush(Model.Color),
                StrokeThickness = Common.DefaultShapeThickness,
                Fill = Model.Filled.ToLower() == "true" ? GetSolidColorBrush(Model.Color) : Brushes.Transparent,
                Name = Model.Type + GetShapeCount(Canvas, Model.Type)
            };

            foreach(string carthesianPoint in Model.Vertices)
            {
                Point point = Canvas.GetCanvasPoint(carthesianPoint);
                polygon.Points.Add(point);
            }            
            Canvas.Children.Add(polygon);
        }    
        /// <summary>
        /// Gets the total count of the specific kind of shape.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="type">Kind of shape.</param>
        /// <returns></returns>
        private static int GetShapeCount(this Canvas Canvas, string type)
        {
            int count = 1;
            foreach(Shape shape in Canvas.Children)
                if(shape.Name.Contains(type))
                    count++;
            return count;   
        }        
        #endregion

        #region 'Canvas Size and Scale'
        /// <summary>
        /// Initializes the canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        internal static void InitializeCanvas(this Canvas Canvas)
        {
            Border border = Canvas.GetParentBorder();
            Canvas.Width = border.ActualWidth;
            Canvas.Height = border.ActualHeight;
            Canvas.RenderTransform = Transform.Identity;
        }
        /// <summary>
        /// Sets canvases size for objects.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="ObjectsRangeBox">Objects which desired to be in canvas.</param>
        internal static void SetCanvasSize(this Canvas Canvas, CarthesianRangeBoxModel ObjectsRangeBox)
        {
            Border border = Canvas.GetParentBorder();
            double Height = ObjectsRangeBox.RequiredViewportHeight;
            double Width = ObjectsRangeBox.RequiredViewportWidth;            
            bool scaledDown = Width > border.ActualWidth || Height > border.ActualHeight;
            Canvas.Height = scaledDown ? Height : border.ActualHeight;
            Canvas.Width = scaledDown ? Width : border.ActualWidth;
            Canvas.UpdateLayout();
        }
        /// <summary>
        /// Sets the canvas's scale. 
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="ObjectsRangeBox">Objects which desired to be in canvas.</param>
        /// <param name="AlsoScaleIn">If true, even if objects fitting to screen completly program will zoom in.</param>
        /// <returns></returns>
        internal static double SetCanvasScaleForObjects(this Canvas Canvas, CarthesianRangeBoxModel ObjectsRangeBox , bool? AlsoScaleIn)
        {
            double scale = GetPerfectScale(Canvas, ObjectsRangeBox);
            if(scale > 1)
            {
                if (AlsoScaleIn == false) scale = 1;
                Border parentBorder = Canvas.GetParentBorder();
                ScaleTransform scaleTransform = new ScaleTransform(scale, scale, parentBorder.ActualWidth / 2, parentBorder.ActualHeight / 2);
                Canvas.RenderTransform = scaleTransform;
            }
            else
            {
                ScaleTransform scaleTransform = new ScaleTransform(scale, scale, ObjectsRangeBox.RequiredViewportWidth / 2 , ObjectsRangeBox.RequiredViewportHeight / 2);
                Canvas.RenderTransform = scaleTransform;
            }            
            return scale;
        }
        /// <summary>
        /// Gets perfect scale for the canvas to show everything.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <param name="ObjectsRangeBox">RangeBox of the objects desired to be shown.</param>
        /// <param name="tolerance">Tolerance value for RangeBox. It's make Rangebox bigger in every direction.</param>
        /// <returns></returns>
        private static double GetPerfectScale(this Canvas Canvas, CarthesianRangeBoxModel ObjectsRangeBox, double tolerance = 10)
        {
            Border border = Canvas.GetParentBorder();
            double scaleX = border.ActualWidth / (ObjectsRangeBox.RequiredViewportWidth + tolerance);
            double scaleY = border.ActualHeight / (ObjectsRangeBox.RequiredViewportHeight + tolerance);
            return Math.Min(scaleX, scaleY);
        }
        #endregion

        #region 'Border and Scroll'
        /// <summary>
        /// Scrolls the canvas to the center.
        /// </summary>
        /// <param name="Canvas"></param>
        internal static void ScrollCenter(this Canvas Canvas)
        {
            ScrollViewer scrollViewer = GetParentScrollViewer(Canvas);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight / 2);
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.ScrollableWidth / 2);
        }
        /// <summary>
        /// Gets the Border object that parenting to this canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <returns></returns>
        private static Border GetParentBorder(this Canvas Canvas)
        {
            return  (Border)((ScrollViewer)Canvas.Parent).Parent;
        }
        /// <summary>
        /// Gets the ScrollViewer object that parenting to this canvas.
        /// </summary>
        /// <param name="Canvas"></param>
        /// <returns></returns>
        private static ScrollViewer GetParentScrollViewer(this Canvas Canvas)
        {
            return (ScrollViewer)Canvas.Parent;
        }
        #endregion
    }
}