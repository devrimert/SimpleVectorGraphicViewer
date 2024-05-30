using Newtonsoft.Json;
using SimpleVectorGraphicViewer.Methods;
using System;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Model
{
    public class CircleModel : GraphicModelShape
    {
        internal Point CenterPoint { get; set; }
        [JsonProperty("radius")]
        internal string Radius { get; set; }
        [JsonProperty("center")]
        internal string Center {  get; set; }   
        internal double RadiusValue { get; set; }
        

        internal CircleModel()
        {
            
        }
        internal override void GenerateValues()
        {
            this.GraphicType = GType.Circle;
            this.CenterPoint = Common.GetPoint(Center);
            this.RadiusValue = double.Parse(Radius);
            this.ColorBrush = Common.GetSolidColorBrush(Color);
            this.Thickness = Common.DefaultBorderThickness;
            this.GraphicShape = GetShape();
            this.Height = this.Width = 2 * RadiusValue;
            this.GetRect();
        }
        internal override Shape GetShape()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 2 * RadiusValue;
            ellipse.Height = 2 * RadiusValue;
            ellipse.SetValue(Canvas.LeftProperty, CenterPoint.X - RadiusValue);
            ellipse.SetValue(Canvas.TopProperty, CenterPoint.Y - RadiusValue);
            ellipse.Fill = IsFilled ? ColorBrush : Brushes.Transparent;
            ellipse.Stroke = ColorBrush;
            ellipse.StrokeThickness = Thickness;
            return ellipse;
        }
        internal override double GetArea()
        {
            return Math.PI * this.RadiusValue * this.RadiusValue;
        }
        internal override void GetRect()
        {
            this.BodyMaxPoint = new Point(this.CenterPoint.X + RadiusValue , this.CenterPoint.Y + RadiusValue);
            this.BodyMinPoint = new Point(this.CenterPoint.X - RadiusValue, this.CenterPoint.Y - RadiusValue);
            this.Bounds = new Rect(this.BodyMaxPoint, BodyMinPoint);
        }

       
    }
}
