using System;
using System.Windows.Shapes;
using System.Collections.Generic;
using SimpleVectorGraphicViewerV2.Model;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class PropertyViewerMethods
    {
        internal class PropertyDisplay
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public PropertyDisplay(string name, string value)
            {
                Name = name;
                Value = value;
            }   
        }

        internal static List<PropertyDisplay> GetPropertyDisplayOfShape(GraphicModel Model, Shape Shape)
        {
            List<PropertyDisplay> props= new List<PropertyDisplay>();
            if (Model is LineModel line)
                props = GetLineModelProperties(line, Shape);
            else if (Model is EllipseModel circle)
                props = GetCircleModelProperties(circle, Shape);
            else if (Model is TriangleModel triangle)
                props = GetTriangleModelProperties(triangle, Shape);
            else if (Model is RectangleModel rectangle)
                props = GetRectangleModelProperties(rectangle, Shape);
            else if (Model is PolygonModel polygon)
                props = GetPolygonModelProperties(polygon, Shape);
            return props;
        }
        private static List<PropertyDisplay> GetLineModelProperties(LineModel Line, Shape Shape)
        {
            return new List<PropertyDisplay>
            {
                new PropertyDisplay("Name", Shape.Name),
                new PropertyDisplay("Color", Line.Color),
                new PropertyDisplay("A", Line.CarthesianA),
                new PropertyDisplay("B", Line.CarthesianB),
                new PropertyDisplay("Length", Math.Round(Line.Length,2).ToString())
            };
        }
        private static List<PropertyDisplay> GetCircleModelProperties(EllipseModel Circle, Shape Shape)
        {
            return new List<PropertyDisplay>
            {
                new PropertyDisplay("Name", Shape.Name),
                new PropertyDisplay("Color", Circle.Color),
                new PropertyDisplay("Center", Circle.CarthesianCenter),
                new PropertyDisplay("Radius", Circle.Radius),
                new PropertyDisplay("Area", Math.Round(Circle.Area,2).ToString()),
                new PropertyDisplay("Width", Math.Round(Circle.RangeBox.Width,2).ToString()),
                new PropertyDisplay("Height", Math.Round(Circle.RangeBox.Height,2).ToString()),
                new PropertyDisplay("Perimeter", Math.Round(Circle.Perimeter,2).ToString())
            };
        }
        private static List<PropertyDisplay> GetTriangleModelProperties(TriangleModel Triangle, Shape Shape)
        {
            return new List<PropertyDisplay>
            {
                new PropertyDisplay("Name", Shape.Name),
                new PropertyDisplay("Color", Triangle.Color),
                new PropertyDisplay("A", Triangle.CarthesianA),
                new PropertyDisplay("B", Triangle.CarthesianB),
                new PropertyDisplay("C", Triangle.CarthesianC),
                new PropertyDisplay("Area", Math.Round(Triangle.Area,2).ToString()),
                new PropertyDisplay("Width", Math.Round(Triangle.RangeBox.Width,2).ToString()),
                new PropertyDisplay("Height", Math.Round(Triangle.RangeBox.Height,2).ToString()),
                new PropertyDisplay("Perimeter", Math.Round(Triangle.Perimeter,2).ToString())
            };
        }
        private static List<PropertyDisplay> GetRectangleModelProperties(RectangleModel Rectangle, Shape Shape)
        {
            return new List<PropertyDisplay>
            {
                new PropertyDisplay("Name", Shape.Name),
                new PropertyDisplay("Color", Rectangle.Color),
                new PropertyDisplay("A", Rectangle.CarthesianA),
                new PropertyDisplay("B", Rectangle.CarthesianB),
                new PropertyDisplay("C", Rectangle.CarthesianC),
                new PropertyDisplay("D", Rectangle.CarthesianD),
                new PropertyDisplay("Area", Math.Round(Rectangle.Area,2).ToString()),
                new PropertyDisplay("Width", Math.Round(Rectangle.RangeBox.Width,2).ToString()),
                new PropertyDisplay("Height", Math.Round(Rectangle.RangeBox.Height,2).ToString()),
                new PropertyDisplay("Perimeter", Math.Round(Rectangle.Perimeter,2).ToString())
            };
        }
        private static List<PropertyDisplay> GetPolygonModelProperties(PolygonModel Polygon, Shape Shape)
        {
            List<PropertyDisplay> props = new List<PropertyDisplay>
            {
                new PropertyDisplay("Name", Shape.Name),
                new PropertyDisplay("Color", Polygon.Color),
                new PropertyDisplay("Area", Math.Round(Polygon.Area,2).ToString()),
                new PropertyDisplay("Width", Math.Round(Polygon.RangeBox.Width,2).ToString()),
                new PropertyDisplay("Height", Math.Round(Polygon.RangeBox.Height,2).ToString()),
                new PropertyDisplay("Perimeter", Math.Round(Polygon.Perimeter,2).ToString())
            };
            char index = 'a';
            foreach(string Vertice  in Polygon.Vertices)
            {
                props.Add(new PropertyDisplay(index.ToString().ToUpper(), Vertice));
                index++;
            }
            return props;
        }
    }
}