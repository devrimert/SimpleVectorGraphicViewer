using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SimpleVectorGraphicViewerV2.Statics;

namespace SimpleVectorGraphicViewerV2.Model
{
    public class CarthesianRangeBoxModel
    {
        public Point MinPoint { get; set; } 
        public Point MaxPoint { get; set; }
        public Point CenterPoint { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double RequiredViewportWidth {  get; set; }
        public double RequiredViewportHeight { get; set; }
        public CarthesianRangeBoxModel(List<string> Points) 
        {
            double minX, minY, maxX, maxY;
            Point startPoint = CarthesianMethods.GetCarthesianPointFromString(Points[0]);
            minX = maxX = startPoint.X;
            minY = maxY = startPoint.Y;
            for (int i = 1; i < Points.Count; i++)
            {
                Point point = CarthesianMethods.GetCarthesianPointFromString(Points[i]);
                CarthesianMethods.CheckMinMaxCoordinates(point,ref minX, ref maxX, ref minY, ref maxY);
            }

            Generate(minX, maxX, minY, maxY);
        }
        public CarthesianRangeBoxModel(string Point, double Radius)
        {
            double minX, minY, maxX, maxY;
            Point centerPoint = CarthesianMethods.GetCarthesianPointFromString(Point);
            maxX = centerPoint.X +Radius;
            maxY = centerPoint.Y + Radius;
            minX = centerPoint.X - Radius;
            minY = centerPoint.Y - Radius;

            Generate(minX, maxX, minY, maxY);
        }
        public CarthesianRangeBoxModel(double minX,  double maxX, double minY, double maxY)
        {
            Generate(minX, maxX, minY, maxY);   
        }       
        private void Generate(double minX, double maxX, double minY, double maxY)
        {
            MinPoint = new Point(minX, minY);
            MaxPoint = new Point(maxX, maxY);
            CenterPoint = new Point((minX + maxX)/2, (minY + maxY)/2);
            Width = Math.Abs(maxX - minX);
            Height = Math.Abs(maxY - minY);
            RequiredViewportWidth = Math.Max(Math.Abs(MaxPoint.X), Math.Abs(MinPoint.X)) * 2;
            RequiredViewportHeight = Math.Max(Math.Abs(MaxPoint.Y), Math.Abs(MinPoint.Y)) * 2;
        }
    }
}
