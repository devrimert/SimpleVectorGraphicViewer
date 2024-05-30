using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SimpleVectorGraphicViewerV2.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class FileMethods
    {
        public static string SelectSourceFile()
        {
            string selectedFilePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Please Select A Source File",
                Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
                selectedFilePath = openFileDialog.FileName;
            return selectedFilePath;
        }
        public static ObservableCollection<GraphicModel> GetGraphics(string path)
        {
            ObservableCollection<GraphicModel> graphics = new ObservableCollection<GraphicModel>();
            if (path.EndsWith(".json"))
            {
                foreach (JObject item in GetJsonArray(path))
                    graphics.Add(CreateGraphicModel(item));
            }
            else if(path.EndsWith(".xml"))
            {              
                foreach (XmlNode rootNode in GetXmlNodes(path))
                    graphics.Add(CreateGraphicModel(rootNode));
            }

            foreach (GraphicModel graphicsModel in graphics)
                graphicsModel.Generate();
          
            return graphics;
        }

        private static JArray GetJsonArray(string path)
        {
            string fileContent = File.ReadAllText(path);
            return JArray.Parse(fileContent);
        }
        private static GraphicModel CreateGraphicModel(JObject item)
        {
            GraphicModel model = null;
            string type = item["type"].ToString();
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
                default:
                    throw new InvalidOperationException($"Unknown type: {type}");
            }
            return model;
        }     
        private static XmlNodeList GetXmlNodes(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            return xmlDoc.SelectNodes("//Root");
        }
        private static GraphicModel CreateGraphicModel(XmlNode item)
        {
            GraphicModel model = null;
            string type = item.SelectSingleNode("type")?.InnerText;
            switch (type)
            {
                case "line":
                    model = new LineModel(item);
                    break;
                case "circle":
                    model = new CircleModel(item);
                    break;
                case "triangle":
                    model = new TriangleModel(item);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type: {type}");
            }
            return model;
        }          
    }
}
