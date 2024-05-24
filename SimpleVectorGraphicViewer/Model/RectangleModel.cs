using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Model
{
    internal class RectangleModel : GraphicModelShape
    {
        internal Point Point1 { get; set; }
        internal Point Point2 { get; set; }
        internal Point Point3 { get; set; }
        internal Point Point4 { get; set; }

        internal RectangleModel() 
        { 

        }

        internal override double GetArea()
        {
            throw new NotImplementedException();
        }

        internal override Shape GetShape()
        {
            throw new NotImplementedException();
        }

        internal override void GenerateValues()
        {
            throw new NotImplementedException();
        }
    }
}
