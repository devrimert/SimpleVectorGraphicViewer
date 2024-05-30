using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal class CircleModel : GraphicModelShape
    {
        [JsonProperty("radius")]
        internal string Radius { get; set; }
        [JsonProperty("center")]
        internal string CarthesianCenter { get; set; }
        internal double RadiusValue { get; set; }
        internal Point CanvasCenter { get; set; }
        internal CircleModel() { }
        internal CircleModel(XmlNode XmlNode)
        {
            this.Type = "circle";
            this.Color = XmlNode.SelectSingleNode("color")?.InnerText;
            this.CarthesianCenter = XmlNode.SelectSingleNode("center")?.InnerText;
            this.Radius = XmlNode.SelectSingleNode("radius")?.InnerText;
            this.Filled = XmlNode.SelectSingleNode("filled")?.InnerText;
        }
        internal override void Generate()
        {
            this.RadiusValue = double.Parse(Radius.Replace(',', '.'), CultureInfo.InvariantCulture);
            GetArea();
            GetRectangle();
        }

        internal override double GetArea()
        {
            return Math.PI * this.RadiusValue * this.RadiusValue;
        }

        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(CarthesianCenter, RadiusValue);
        }
    }
}
