using Microsoft.Win32;
using SimpleVectorGraphicViewer.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Methods
{
    internal static class Common
    {
        internal static Window MainWindow;
        internal static Canvas MainCanvas;
        internal static Point CanvasCenter;
        internal static string CurrentFilePath;
        internal static double DefaultBorderThickness = 1;

        public static Point GetPoint(string pointString)
        {
            try
            {
                CultureInfo culture = CultureInfo.InvariantCulture;
                string[] values = pointString.Split(';');
                double x = CanvasCenter.X + double.Parse(values[0].Replace(',', '.'), culture);
                double y = CanvasCenter.Y + double.Parse(values[1].Replace(',', '.'), culture);



                return new Point(x, MainCanvas.ActualHeight - y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Point parsing error: " + ex.Message);
                return new Point();
            }
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
        internal static double CalculateDistance(Point point1, Point point2)
        {           
            double dX = point2.X - point1.X;
            double dY = point2.Y - point1.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

    }
}
