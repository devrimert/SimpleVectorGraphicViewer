using Newtonsoft.Json;
using SimpleVectorGraphicViewer.Methods;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Model
{
    public class LineModel : GraphicModel
    {
        [JsonProperty("a")]
        public string A { get; set; }
        [JsonProperty("b")]
        public string B { get; set; }
        public Point PointA { get; set; }
        public Point PointB { get; set; }
        public double Length { get => GetLenth(); }



        public LineModel()
        {           
        }
        internal override void GenerateValues()
        {
            this.GraphicType = GType.Line;
            this.PointA = Common.GetPoint(this.A);
            this.PointB = Common.GetPoint(this.B);
            this.ColorBrush = Common.GetSolidColorBrush(this.Color);
            this.Thickness = Common.DefaultBorderThickness;
            this.GraphicShape = GetShape();
            GetDimensions();
        }
        private double GetLenth()
        {
            return Common.CalculateDistance(this.PointA, this.PointB);
        }
        internal override Shape GetShape()
        {
            Line line = new Line();
            line.X1 = this.PointA.X;
            line.Y1 = this.PointA.Y;
            line.X2 = this.PointB.X;
            line.Y2 = this.PointB.Y;
            line.Stroke = this.ColorBrush;
            line.StrokeThickness = this.Thickness;
            return line;
        }
        private void GetDimensions()
        {
            this.Height = Math.Abs(PointA.Y - PointB.Y);    
            this.Width = Math.Abs(PointA.X - PointB.X);
        }


    }
}
