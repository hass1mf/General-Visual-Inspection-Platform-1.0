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
using AqAidi;
using System.Drawing.Imaging;

namespace Aqtest2
{
   
        [Module("AIDI检测模块", "图像处理", "用于调用AIDI接口的模块")]
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
        } = new List<AqShap>();

       public   AidiCollect _mRuner= new AidiCollect();

            private string STR1 = "";
             Bitmap bit;
            [Input]
            public Bitmap ImageIn
            {
                get
                {
                    return bit;
                }

                set
                {
                bit = value;
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
            Form1 form1;
            public void InitModule(string projectDirectory, string nodeName)
            {
               form1 = new Form1(this);
            }

            public void Run()
            {
            //STR1 = String3;
            // STR1 = ImageIn;
                if (form1 == null)
                {
                     form1 = new Form1(this);
                }
             form1.button1_Click(null,null);

             }


        public static Bitmap Bitmap32ToBitmap24(Bitmap bmp32)
        {
            BitmapData data32 = bmp32.LockBits(new Rectangle(0, 0, bmp32.Width, bmp32.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Bitmap bmp24 = new Bitmap(bmp32.Width, bmp32.Height, PixelFormat.Format24bppRgb);
            BitmapData data24 = bmp24.LockBits(new Rectangle(0, 0, bmp24.Width, bmp24.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr32 = (byte*)data32.Scan0.ToPointer();
                byte* ptr24 = (byte*)data24.Scan0.ToPointer();
                for (int i = 0; i < bmp24.Height; i++)
                {
                    for (int j = 0; j < bmp24.Width; j++)
                    {
                        //将32位位图的RGB值赋值给24位位图的RGB值 
                        *ptr24++ = *ptr32++;
                        *ptr24++ = *ptr32++;
                        *ptr24++ = *ptr32++;
                        ptr32++;//跳过透明度字节 
                    }
                    ptr24 += data24.Stride - bmp24.Width * 3;
                    ptr32 += data32.Stride - bmp32.Width * 4;
                }
            }
            bmp32.UnlockBits(data32);
            bmp24.UnlockBits(data24);
            return bmp24;
        }



        public void SaveModule(string projectDirectory, string nodeName)
            {
            }

            public bool StartSetForm()
            {
                form1.ShowDialog();
                return true;

            }
        }
    
}
