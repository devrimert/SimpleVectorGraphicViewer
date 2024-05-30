using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace SimpleVectorGraphicViewer.Model
{
    internal class SplineModel : GraphicModel
    {
        internal string StartPoint { get; set; }    
        internal string EndPoint { get; set; }
        internal string MidPoint { get; set; }

        internal override void GenerateValues()
        {
            throw new NotImplementedException();
        }

        internal override void GetRect()
        {
            throw new NotImplementedException();
        }

        internal override Shape GetShape()
        {
            throw new NotImplementedException();
        }
    }
}
