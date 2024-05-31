using System;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SimpleVectorGraphicViewerV2.Model;

namespace SimpleVectorGraphicViewerV2.Statics
{
    internal static class FileMethods
    {
        #region 'File Selection'
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
        #endregion

        #region 'Graphic Creation
        internal static ObservableCollection<GraphicModel> GetGraphics(string path)
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

        #region 'JSON'
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
                case "circle" :
                    model = item.ToObject<EllipseModel>();
                    break;
                case "triangle":
                    model = item.ToObject<TriangleModel>();
                    break;
                case "ellipse":
                    model = item.ToObject<EllipseModel>();
                    break;
                case "rectangle":
                    model = item.ToObject<RectangleModel>();
                    break;
                case "polygon":
                    model = new PolygonModel(item) ;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type: {type}");
            }
            return model;
        }
        #endregion

        #region 'XML'
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
                    model = new EllipseModel(item);
                    break;
                case "ellipse":
                    model = new EllipseModel(item);
                    break;
                case "triangle":
                    model = new TriangleModel(item);
                    break;
                case "rectangle":
                    model = new RectangleModel(item);
                    break;
                case "polygon":
                    model = new PolygonModel(item);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type: {type}");
            }
            return model;
        }
        #endregion
        #endregion
    }
}