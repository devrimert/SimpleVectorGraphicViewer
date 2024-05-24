using Microsoft.Win32;
using SimpleVectorGraphicViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Methods
{
    internal static class FileProcesses
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
            string jsonContent = System.IO.File.ReadAllText(path);
            JArray jsonArray = JArray.Parse(jsonContent);
            foreach (JObject item in jsonArray)
            {
                GraphicModel graphic = GraphicsProcesses.CreateGraphicModel(item);
                graphics.Add(graphic);
            }
            return graphics;
        }
    }
}
