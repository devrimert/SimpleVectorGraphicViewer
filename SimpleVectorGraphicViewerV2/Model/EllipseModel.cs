using System;
using System.Xml;
using System.Globalization;
using Newtonsoft.Json;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal class EllipseModel : GraphicModelShape
    {
        [JsonProperty("width")]
        internal string Width { get; set; }
        [JsonProperty("height")]
        internal string Height { get; set; }
        [JsonProperty("radius")]
        internal string Radius { get; set; }
        [JsonProperty("center")]
        internal string CarthesianCenter { get; set; }
        internal double WidthValue { get; set; }
        internal double HeightValue { get; set; }
        internal double RadiusValue { get; set; }   

        internal EllipseModel() { }
        internal EllipseModel(XmlNode XmlNode)
        {
            this.Color = XmlNode.SelectSingleNode("color")?.InnerText;
            this.CarthesianCenter = XmlNode.SelectSingleNode("center")?.InnerText;
            this.Radius = XmlNode.SelectSingleNode("radius")?.InnerText;
            this.Filled = XmlNode.SelectSingleNode("filled")?.InnerText;
            this.Width = XmlNode.SelectSingleNode("width")?.InnerText;
            this.Height = XmlNode.SelectSingleNode("height")?.InnerText;
        }

        internal override void Generate()
        {
            if (string.IsNullOrEmpty(Radius))
            {
                this.Type = "ellipse";              
            }
            else
            {
                this.Type = "circle";
                this.RadiusValue = double.Parse(Radius.Replace(',', '.'), CultureInfo.InvariantCulture);
                this.Width = (this.RadiusValue*2).ToString();
                this.Height = (this.RadiusValue * 2).ToString();
            }
            this.WidthValue = double.Parse(Width.Replace(',', '.'), CultureInfo.InvariantCulture);
            this.HeightValue = double.Parse(Height.Replace(',', '.'), CultureInfo.InvariantCulture);

            GetGeo();
            GetRectangle();
        }
        internal override void GetGeo()
        {
            if(this.Type == "circle")
            {
                this.Area = Math.PI * this.RadiusValue * this.RadiusValue;
                this.Perimeter = Math.PI * this.RadiusValue * 2;
            }
            else
            {
                double semiMajorAxis = WidthValue / 2;
                double semiMinorAxis = HeightValue / 2;
                this.Area = Math.PI * semiMajorAxis * semiMinorAxis;
                double h = Math.Pow((semiMajorAxis - semiMinorAxis), 2) / Math.Pow((semiMajorAxis + semiMinorAxis), 2);
                this.Perimeter = Math.PI * (semiMajorAxis + semiMinorAxis) * (1 + (3 * h) / (10 + Math.Sqrt(4 - 3 * h)));
            }
        }
        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(CarthesianCenter, WidthValue,HeightValue);            
        }
    }
}