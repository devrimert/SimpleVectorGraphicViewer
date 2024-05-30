using Newtonsoft.Json;
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
    internal class LineModel : GraphicModel
    {
        [JsonProperty("a")]
        internal string CarthesianA { get; set; }
        [JsonProperty("b")]
        internal string CarthesianB { get; set; }
        internal Point CanvasA { get; set; }
        internal Point CanvasB { get; set; }
        internal LineModel() { }
        internal LineModel(XmlNode XmlNode)
        {
            this.Type = "line";
            this.Color = XmlNode.SelectSingleNode("color")?.InnerText;
            this.CarthesianA = XmlNode.SelectSingleNode("a")?.InnerText;
            this.CarthesianB = XmlNode.SelectSingleNode("b")?.InnerText;
        }

        internal override void Generate()
        {
            GetRectangle();
        }

        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(new List<string> { CarthesianA, CarthesianB });
        }
    }
}
