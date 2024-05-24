using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        internal abstract Shape GetShape();
        internal abstract void GenerateValues();
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
