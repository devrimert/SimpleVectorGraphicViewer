using System;
using System.Xml;
using System.Collections.Generic;
using Newtonsoft.Json;
using SimpleVectorGraphicViewerV2.Statics;

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
            this.GetGeo();
        }
        internal override void GetGeo()
        {
            double side1 = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianA), CarthesianMethods.GetCarthesianPointFromString(CarthesianB));
            double side2 = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianB), CarthesianMethods.GetCarthesianPointFromString(CarthesianC));
            double side3 = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianC), CarthesianMethods.GetCarthesianPointFromString(CarthesianA));
            this.Perimeter = side1+side2+side3;
            double avgLenth = (side1 + side2 + side3) / 2;
            this.Area =  Math.Sqrt(avgLenth * (avgLenth - side1) * (avgLenth - side2) * (avgLenth - side3));
        }
        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(new List<string> { CarthesianA, CarthesianB, CarthesianC });
        }       
    }
}