using Newtonsoft.Json.Linq;
using SimpleVectorGraphicViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            model.GenerateValues();
            return model;
        }
    
    

    }
}
