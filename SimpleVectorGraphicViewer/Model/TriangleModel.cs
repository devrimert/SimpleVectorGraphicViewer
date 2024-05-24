using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleVectorGraphicViewer.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Model
{
    internal class TriangleModel : GraphicModelShape
    {
        [JsonProperty("a")]
        internal string A { get; set; }
        [JsonProperty("b")]
        internal string B { get; set; }
        [JsonProperty("c")]
        internal string C { get; set; }
        internal Point PointA { get; set; }
        internal Point PointB { get; set; }
        internal Point PointC { get; set; }

        internal TriangleModel()
        {         
        }

        internal override void GenerateValues()
        {
            this.GraphicType = GType.Triangle;
            this.PointA = Common.GetPoint(A);
            this.PointB = Common.GetPoint(B);
            this.PointC = Common.GetPoint(C);
            this.ColorBrush = Common.GetSolidColorBrush(Color);
            this.Thickness = Common.DefaultBorderThickness;
        }

        internal override double GetArea()
        {
            double side1 = Common.CalculateDistance(PointA, PointB);
            double side2 = Common.CalculateDistance(PointB, PointC);
            double side3 = Common.CalculateDistance(PointC, PointA);
            double avgLenth = (side1 + side2 + side3) / 2;
            return Math.Sqrt(avgLenth * (avgLenth - side1) * (avgLenth - side2) * (avgLenth - side3));
        }

        internal override Shape GetShape()
        {
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = PointA;
            pathFigure.Segments.Add(new LineSegment(PointB, true));
            pathFigure.Segments.Add(new LineSegment(PointC, true));
            pathFigure.Segments.Add(new LineSegment(PointA, true));
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            Path path = new Path();
            path.Data = pathGeometry;
            path.Stroke = this.ColorBrush;
            path.StrokeThickness = this.Thickness;
            path.Fill = IsFilled ? ColorBrush : Brushes.Transparent;

            return path;
        }

    
    }
}
