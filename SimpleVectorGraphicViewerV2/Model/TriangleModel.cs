using Newtonsoft.Json;
using SimpleVectorGraphicViewerV2.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal class TriangleModel  : GraphicModelShape
    {
        [JsonProperty("a")]
        internal string CarthesianA { get; set; }
        [JsonProperty("b")]
        internal string CarthesianB { get; set; }
        [JsonProperty("c")]
        internal string CarthesianC { get; set; }
        internal Point CanvasA { get; set; }
        internal Point CanvasB { get; set; }
        internal Point CanvasC { get; set; }
        internal TriangleModel()
        {
        }
        internal TriangleModel(XmlNode XmlNode)
        {
            this.Type = "triangle";
            this.Color = XmlNode.SelectSingleNode("color")?.InnerText;
            this.CarthesianA = XmlNode.SelectSingleNode("a")?.InnerText;
            this.CarthesianB = XmlNode.SelectSingleNode("b")?.InnerText;
            this.CarthesianC = XmlNode.SelectSingleNode("c")?.InnerText;
            this.Filled = XmlNode.SelectSingleNode("filled")?.InnerText;
        }
        internal override void Generate()
        {
            this.GetRectangle();
        }

        internal override double GetArea()
        {
            throw new NotImplementedException();
        }

        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(new List<string> { CarthesianA, CarthesianB, CarthesianC });
        }

       
    }
}
