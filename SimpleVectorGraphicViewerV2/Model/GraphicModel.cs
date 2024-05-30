using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace SimpleVectorGraphicViewerV2.Model
{
    [XmlRoot("Root")]
    public abstract class GraphicModel
    {
        [JsonProperty("type")]
        internal string Type { get; set; }
        [JsonProperty("color")]
        internal string Color { get; set; }
        internal SolidColorBrush ColorBrush { get; set; }
        public CarthesianRangeBoxModel RangeBox { get; set; }
        internal abstract void Generate();
        internal abstract void GetRectangle();
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
