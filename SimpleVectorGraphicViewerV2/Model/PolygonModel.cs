using System;
using System.Xml;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using SimpleVectorGraphicViewerV2.Statics;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal class PolygonModel : GraphicModelShape
    {
        internal List<string> Vertices { get; set; }

        internal PolygonModel(XmlNode xmlNode)
        {
            Vertices = new List<string>();
            char currentChar = 'a';
            while (true)
            {
                string propertyName = currentChar.ToString();
                XmlNode node = xmlNode.SelectSingleNode(propertyName);
                if (node == null)
                    break;
                Vertices.Add(node.InnerText);
                currentChar++;
            }
            Color = xmlNode.SelectSingleNode("color").InnerText;
            Filled = xmlNode.SelectSingleNode("filled").InnerText;
            Type = "polygon";
        }
        internal PolygonModel(JObject item)
        {
            Vertices = new List<string>();
            char currentChar = 'a';
            while (true)
            {
                JToken token = item[currentChar.ToString()];
                if (token == null)
                    break;
                Vertices.Add(token.ToString());
                currentChar++;
            }
            Color = item["color"].ToString();
            Filled = item["filled"].ToString();
            Type = "polygon";
        }

        internal override void Generate()
        {
            GetRectangle();
            GetGeo();
        }
        internal override void GetGeo()
        {
            double perimeter = 0;
            for (int i = 0; i < Vertices.Count; i++)
            {
                Point p1 = CarthesianMethods.GetCarthesianPointFromString(Vertices[i]);
                Point p2 = CarthesianMethods.GetCarthesianPointFromString(Vertices[(i + 1) % Vertices.Count]);
                perimeter += CarthesianMethods.CalculateDistanceBetweenPoints(p1, p2);
            }
            this.Perimeter = perimeter;
            double area = 0;
            for (int i = 0; i < Vertices.Count; i++)
            {
                Point p1 = CarthesianMethods.GetCarthesianPointFromString(Vertices[i]);
                Point p2 = CarthesianMethods.GetCarthesianPointFromString(Vertices[(i + 1) % Vertices.Count]);
                area += p1.X * p2.Y - p2.X * p1.Y;
            }
            this.Area = Math.Abs(area) / 2;
        }
        internal override void GetRectangle()
        {
            this.RangeBox = new CarthesianRangeBoxModel(Vertices);
        }
    }
}