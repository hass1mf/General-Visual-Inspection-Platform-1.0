using System.Collections.Generic;
using System.Drawing;
using AqVision.Graphic.AqVision.shape;

namespace AidiCore.DataType
{
    public class AqModuleResult
    {

        public AqModuleResult()
        {
            this.ModuleName = "";
            this.NodeName = "";
            this.RunNum = 0;
            this.DisplayBitmap = null;
            this.DisplayShapes = new List<AqShap>();
        }

        public bool MergeResult(AqModuleResult moduleResult)
        {
           
            bool flag2 = this.DisplayShapes == null;
            if (flag2)
            {
                this.DisplayShapes = new List<AqShap>();
            }
            bool flag3 = moduleResult == null || moduleResult.ModuleName != this.ModuleName || moduleResult.NodeName != this.NodeName || moduleResult.DisplayShapes == null;
            bool result;
            if (flag3)
            {
                result = false;
            }
            else
            {
                bool flag4 = this.DisplayWindowName == "";
                if (flag4)
                {
                    this.DisplayWindowName = moduleResult.DisplayWindowName;
                }
                else
                {
                    bool flag5 = moduleResult.DisplayWindowName != "" && this.DisplayWindowName != moduleResult.DisplayWindowName;
                    if (flag5)
                    {
                        result = false;
                        return result;
                    }
                }
                this.IsResultOK &= moduleResult.IsResultOK;
                this.RunNum += moduleResult.RunNum;
                bool flag6 = moduleResult.DisplayBitmap != null;
                if (flag6)
                {
                    this.DisplayBitmap = moduleResult.DisplayBitmap;
                }
                foreach (AqShap current2 in moduleResult.DisplayShapes)
                {
                    this.DisplayShapes.Add(current2);
                }
                result = true;
            }
            return result;
        }

        public Bitmap DisplayBitmap
        {
            get;
            set;
        }

        public List<AqShap> DisplayShapes
        {
            get;
            set;
        }

        public string DisplayWindowName
        {
            get;
            set;
        }
     

        public bool IsResultOK
        {
            get;
            set;
        }

        public string ModuleName
        {
            get;
            set;
        }


        public string NodeName
        {
            get;
            set;
        }

        public int RunNum
        {
            get;
            set;
        }

    }
}
