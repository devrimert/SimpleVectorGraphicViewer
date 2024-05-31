using System;
using System.Xml;
using System.Collections.Generic;
using Newtonsoft.Json;
using SimpleVectorGraphicViewerV2.Statics;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal class RectangleModel : GraphicModelShape
    {
        [JsonProperty("a")]
        internal string CarthesianA { get; set; }
        [JsonProperty("b")]
        internal string CarthesianB { get; set; }
        [JsonProperty("c")]
        internal string CarthesianC { get; set; }
        [JsonProperty("d")]
        internal string CarthesianD { get; set; }

        internal RectangleModel()
        {
        }
        internal RectangleModel(XmlNode XmlNode)
        {
            this.Type = "rectangle";
            this.Color = XmlNode.SelectSingleNode("color")?.InnerText;
            this.CarthesianA = XmlNode.SelectSingleNode("a")?.InnerText;
            this.CarthesianB = XmlNode.SelectSingleNode("b")?.InnerText;
            this.CarthesianC = XmlNode.SelectSingleNode("c")?.InnerText;
            this.CarthesianD = XmlNode.SelectSingleNode("d")?.InnerText;
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
            double side3 = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianC), CarthesianMethods.GetCarthesianPointFromString(CarthesianD));
            double side4 = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianD), CarthesianMethods.GetCarthesianPointFromString(CarthesianA));
            this.Perimeter = side1 + side2 + side3 + side4;
            double diagonal = CarthesianMethods.CalculateDistanceBetweenPoints(CarthesianMethods.GetCarthesianPointFromString(CarthesianA), CarthesianMethods.GetCarthesianPointFromString(CarthesianC));
            double area1 = 0.5 * Math.Abs((CarthesianMethods.GetCarthesianPointFromString(CarthesianA).X * (CarthesianMethods.GetCarthesianPointFromString(CarthesianB).Y - CarthesianMethods.GetCarthesianPointFromString(CarthesianC).Y) +
                                          CarthesianMethods.GetCarthesianPointFromString(CarthesianB).X * (CarthesianMethods.GetCarthesianPointFromString(CarthesianC).Y - CarthesianMethods.GetCarthesianPointFromString(CarthesianA).Y) +
                                          CarthesianMethods.GetCarthesianPointFromString(CarthesianC).X * (CarthesianMethods.GetCarthesianPointFromString(CarthesianA).Y - CarthesianMethods.GetCarthesianPointFromString(CarthesianB).Y)));

            double area2 = 0.5 * Math.Abs((CarthesianMethods.GetCarthesianPointFromString(CarthesianC).X * (CarthesianMethods.GetCarthesianPointFromString(CarthesianD).Y - CarthesianMethods.GetCarthesianPointFromString(CarthesianA).Y) +
                                          CarthesianMethods.GetCarthesianPointFromString(CarthesianD).X * (CarthesianMethods.GetCarthesianPointFromString(CarthesianA).Y - CarthesianMethods.GetCarthesianPointFromString(CarthesianC).Y) +
                                          CarthesianMethods.GetCarthesianPointFromString(CarthesianA).X * (CarthesianMethods.GetCarthesianPointFromString(CarthesianC).Y - CarthesianMethods.GetCarthesianPointFromString(CarthesianD).Y)));

            this.Area = area1 + area2;
        }
        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(new List<string> { CarthesianA, CarthesianB, CarthesianC, CarthesianD });
        }
    }
}