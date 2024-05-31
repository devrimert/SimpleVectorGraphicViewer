using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;
using SimpleVectorGraphicViewerV2.Statics;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal class LineModel : GraphicModel
    {
        [JsonProperty("a")]
        internal string CarthesianA { get; set; }
        [JsonProperty("b")]
        internal string CarthesianB { get; set; }
        internal double Length { get; set; }

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
            GetLength();
        }
        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(new List<string> { CarthesianA, CarthesianB });
        }
        internal void GetLength()
        {
            this.Length = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianA), CarthesianMethods.GetCarthesianPointFromString(CarthesianB)); 
        }
    }
}