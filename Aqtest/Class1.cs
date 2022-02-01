using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AidiCore.Attributes;
using AidiCore.DataType;
using AidiCore.Interface;
using AqVision.Graphic.AqVision.shape;

namespace Aqtest2
{
    public class Class1
    {
        [Module("没有任何实际功能的模块", "图像处理", "用于测试的模块！")]
        public class Class3 : AqModuleData, IDisp, IModule
        {
            public Bitmap Bitmap
            {
                get;
                set;
            }
            public List<AqShap> DisplayShapes
            {
                get;
                set;
            }


            private string STR1 = "";

            [Input]
            public string String3
            {
                get
                {
                    return STR1;
                }

                set
                {
                    STR1 = value;
                }
            }

            [Input]
            public string ImageIn
            {
                get
                {
                    return STR1;
                }

                set
                {
                    STR1 = value;
                }
            }

            [Output]
            public string S5
            {
                get
                {
                    return STR1;
                }

                set
                {
                    STR1 = value;
                }
            }
            public void CloseModule()
            {
            }

            public void InitModule(string projectDirectory, string nodeName)
            {
            }

            public void Run()
            {
                STR1 = String3;
                STR1 = ImageIn;

            }

            public void SaveModule(string projectDirectory, string nodeName)
            {
            }

            public bool StartSetForm()
            {
                return true;

            }
        }
    }
}
