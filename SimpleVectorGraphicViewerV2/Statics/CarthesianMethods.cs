using SimpleVectorGraphicViewerV2.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class CarthesianMethods
    {
        internal static Point GetCarthesianPointFromString(string CoordinateString)
        {
            try
            {
                string[] values = CoordinateString.Split(';');
                double x = double.Parse(values[0].Replace(',', '.'), CultureInfo.InvariantCulture);
                double y = + double.Parse(values[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                return new Point(x, y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Carthesian point parsing error: " + ex.Message);
                return new Point();
            }
        }
        internal static void CheckMinMaxCoordinates(Point point, ref double minX, ref double maxX, ref double minY, ref double maxY)
        {
            minX = Math.Min(minX, point.X);
            maxX = Math.Max(maxX, point.X);
            minY = Math.Min(minY, point.Y);
            maxY = Math.Max(maxY, point.Y);
        }
        internal static CarthesianRangeBoxModel SummerizeRangeBoxes(List<CarthesianRangeBoxModel> RangeBoxes)
        {
            double minX, minY, maxX, maxY;
            minX = RangeBoxes[0].MinPoint.X;
            maxX = RangeBoxes[0].MaxPoint.X;
            minY = RangeBoxes[0].MinPoint.Y;
            maxY = RangeBoxes[0].MaxPoint.Y;
            for (int i = 1; i < RangeBoxes.Count; i++)
            {
                CheckMinMaxCoordinates(RangeBoxes[i].MinPoint, ref minX, ref maxX, ref minY,ref maxY);
                CheckMinMaxCoordinates(RangeBoxes[i].MaxPoint, ref minX, ref maxX, ref minY, ref maxY);
            }
            return new CarthesianRangeBoxModel(minX, maxX, minY, maxY);
        }
    }
}
