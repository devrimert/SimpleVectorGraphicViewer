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
        public static Rect GetBoundingRect(List<Rect> rects)
        {
            // İlk rect'in koordinatlarını başlangıç noktası olarak alın
            double minX = rects[0].Left;
            double minY = rects[0].Top;
            double maxX = rects[0].Right;
            double maxY = rects[0].Bottom;

            // Tüm rect'ler üzerinden geçerek min ve max koordinatları bulun
            foreach (var rect in rects)
            {
                if (rect.Left < minX) minX = rect.Left;
                if (rect.Top < minY) minY = rect.Top;
                if (rect.Right > maxX) maxX = rect.Right;
                if (rect.Bottom > maxY) maxY = rect.Bottom;
            }

            // Yeni rect'in genişlik ve yüksekliğini hesaplayın
            double width = maxX - minX;
            double height = maxY - minY;

            // Kapsayıcı rect'i oluşturun
            return new Rect(minX, minY, width, height);
        }
        public static double GetScaleFactorToContain(Rect outer, Rect inner)
        {
            // Outer rectangle iç rectangleyi zaten kapsıyorsa -1 döndür
            if (outer.Contains(inner))
            {
                return -1;
            }

            // Gerekli scale faktörlerini hesapla
            double scaleX = inner.Width / outer.Width;
            double scaleY = inner.Height / outer.Height;

            // İki eksendeki scale faktörlerinin maksimumunu döndür
            // Böylece outer rectangle, inner rectangle'ı tamamen kapsayacak şekilde ölçeklenir
            return Math.Max(scaleX, scaleY);
        }
        public static double CalculateOptimalCanvas(Rect outer, Rect inner, out double Height,  out double Width)
        {
            if (outer.Contains(inner))
            {
                Height = outer.Height;
                Width = outer.Width;
                return 1;
            }
            else
            {
                double currentHeight = MainCanvas.Height;
                double currentWidth = MainCanvas.Width;
                Height = inner.X + inner.Width;
                Width = inner.Y + inner.Height;
                double scaleX = currentWidth / Width;
                double scaleY = currentHeight / Height;

                // İki eksendeki scale faktörlerinin maksimumunu döndür
                // Böylece outer rectangle, inner rectangle'ı tamamen kapsayacak şekilde ölçeklenir
                return Math.Min(scaleX, scaleY);

            }
                
        }



    }
}
