using Newtonsoft.Json.Linq;
using SimpleVectorGraphicViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace SimpleVectorGraphicViewer.Methods
{
    internal static class GraphicsProcesses
    {
        internal static GraphicModel CreateGraphicModel(JObject item)
        {
            GraphicModel model = null;
            switch (item["type"].ToString())
            {
                case "line":
                    model = item.ToObject<LineModel>();
                     break;
                case "circle":
                    model = item.ToObject<CircleModel>();
                    break;
                case "triangle":
                    model = item.ToObject<TriangleModel>();
                    break;
            }
            //model.GenerateValues();
            return model;
        }
        public static Canvas SetCoordinateSystem(this Canvas canvas, Double xMin, Double xMax, Double yMin, Double yMax)
        {
            var width = xMax - xMin;
            var height = yMax - yMin;

            var translateX = -xMin;
            var translateY = height + yMin;

            var group = new TransformGroup();

            group.Children.Add(new TranslateTransform(translateX, -translateY));
            group.Children.Add(new ScaleTransform(canvas.ActualWidth / width, canvas.ActualHeight / -height));

            canvas.RenderTransform = group;

            return canvas;
        }
    }


}
