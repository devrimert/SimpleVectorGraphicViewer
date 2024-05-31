using System;
using System.Windows;
using System.Globalization;
using System.Collections.Generic;
using SimpleVectorGraphicViewerV2.Model;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class CarthesianMethods
    {
        /// <summary>
        /// Converts point string to a Point object
        /// </summary>
        /// <param name="CoordinateString">Carthesian coordinate string</param>
        /// <returns>Carthesian point object</returns>
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
        /// <summary>
        /// Gets point a point at checks if there is anything to update in min and max point date.
        /// </summary>
        /// <param name="point">Point desired to be checked.</param>
        /// <param name="minX">minX</param>
        /// <param name="maxX">maxX</param>
        /// <param name="minY">minY</param>
        /// <param name="maxY">maxY</param>
        internal static void CheckMinMaxCoordinates(Point point, ref double minX, ref double maxX, ref double minY, ref double maxY)
        {
            minX = Math.Min(minX, point.X);
            maxX = Math.Max(maxX, point.X);
            minY = Math.Min(minY, point.Y);
            maxY = Math.Max(maxY, point.Y);
        }
        /// <summary>
        /// Gets a list of rangeboxes and returns one big rangebox that contains all of the rangeboxes.
        /// </summary>
        /// <param name="RangeBoxes"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Calculates and returns the distance between two points.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        internal static double CalculateDistanceBetweenPoints(Point point1, Point point2)
        {
            double dX = point2.X - point1.X;
            double dY = point2.Y - point1.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
