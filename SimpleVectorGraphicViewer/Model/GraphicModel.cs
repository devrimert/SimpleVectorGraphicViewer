using Newtonsoft.Json;
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
    public abstract class GraphicModel
    {
        [JsonProperty("type")]
        internal string Type { get; set; }
        [JsonProperty("color")]
        internal string Color { get; set; }
        internal GType GraphicType { get; set; }
        internal SolidColorBrush ColorBrush { get; set; }
        internal double Thickness { get; set; }
        public Shape GraphicShape { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Point BodyMaxPoint { get; set; }
        public Point BodyMinPoint { get; set; }
        public Rect Bounds { get; set; }
        internal abstract void GenerateValues();
        internal abstract void GetRect();
        internal abstract Shape GetShape();
        internal enum GType
        {
            Line =0,
            Spline =1,
            Circle =2,
            Triangle =3,
            Reactangle =4
        }            
    }
    
    public abstract class GraphicModelShape : GraphicModel
    {
        [JsonProperty("filled")]
        internal string Filled { get; set; }
        internal bool IsFilled { get => bool.Parse(Filled); }
        internal abstract double GetArea();
        internal double Area { get => GetArea(); }
    }
}
