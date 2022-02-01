using System.Collections.Generic;
using System.Drawing;
using AqVision.Graphic.AqVision.shape;

namespace AidiCore.Interface
{
    public interface IDisp
    {
        Bitmap Bitmap
        {
            get;
            set;
        }

        List<AqShap> DisplayShapes
        {
            get;
            set;
        }
    }
}
