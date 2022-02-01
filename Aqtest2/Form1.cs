using AqAidi;
using AqVision.Graphic;
using AqVision.Graphic.AqVision.shape;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aqtest2
{
    public partial class Form1 : Form
    {
        Class3 class1;
        public Form1(Class3 class1)
        {
            InitializeComponent();
            this.class1 = class1;
        }
        int num = 0;
        public void button1_Click(object sender, EventArgs e)
        {
            if (isinit)
            {
                if (this.class1.ImageIn != null)
                {
                    Stopwatch stpwth = new Stopwatch();
                    stpwth.Start();
                    aqDisplay1.InteractiveGraphics.Clear();
                    class1.DisplayShapes.Clear();
                    aqDisplay1.Image = class1.ImageIn;
                    this.class1.Bitmap = new Bitmap(this.class1.ImageIn);
                    // aidiCollect.Init(@".\test", 0, 1);
                    //  Bitmap img = new Bitmap(@".\test\Location_0\source\20.png");
                    List<Bitmap> imgs = new List<Bitmap>();
                    if (class1.ImageIn.PixelFormat.ToString().Contains("32"))
                    {
                        class1.ImageIn = Bitmap32ToBitmap24(class1.ImageIn);
                    }
                    imgs.Add(class1.ImageIn);
                    stpwth.Stop();
                    string time1 = stpwth.Elapsed.TotalMilliseconds.ToString();
                    stpwth.Start();
                    List<List<AIDIShape>> aIDIShapes = this.class1._mRuner.RunModel(imgs);
                    stpwth.Stop();
                    string time2 = stpwth.Elapsed.TotalMilliseconds.ToString();
                    //S 是轮廓的名字，可以通过S找到这个轮廓
                    stpwth.Start();
                    if (aIDIShapes != null)
                    {
                        List<string> types = new List<string>();
                        foreach (var item in aIDIShapes[0])
                        {
                            class1.DisplayShapes = DrawAidiContours(aIDIShapes[0], AqColorEnum.Green, 2);//ForEach(i => { aqDisplay1.InteractiveGraphics.Add(i, "S", true); });
                            class1.DisplayShapes.ForEach(i => { aqDisplay1.InteractiveGraphics.Add(i, "S", true); });
                            stpwth.Stop();
                            string time3 = stpwth.Elapsed.TotalMilliseconds.ToString();
                            
                        }

                    }
          
                }
            }
           
     


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

        //绘制多个
        public List<AqShap> DrawAidiContours(List<AIDIShape> objList, AqColorEnum color, int lineWidth)
        {
            List<AqShap> listaqshpe = new List<AqShap>();
            for (int i = 0; i < objList.Count; i++)
            {

                //    for (int j = 0; j < objList[i].contours.Count - 1; j++)
                //    {

                //        string temp = objList[i].contours[j].x;
                //        float sx = float.Parse(objList[i].contours[j].x);
                //        float sy = float.Parse(objList[i].contours[j].y);
                //        float ex = float.Parse(objList[i].contours[j + 1].x);
                //        float ey = float.Parse(objList[i].contours[j + 1].y);
                //        listaqshpe.Add(DrawLine(sx, sy, ex, ey, color, 1));
                //    }


                //    //再设计一个点，补全多边形
                //    int n = objList[i].contours.Count;
                //    float nx = float.Parse(objList[i].contours[n - 1].x);
                //    float ny = float.Parse(objList[i].contours[n - 1].y);
                //    float zero_x = float.Parse(objList[i].contours[0].x);
                //    float zero_y = float.Parse(objList[i].contours[0].y);
                //    listaqshpe.Add(DrawLine(nx, ny, zero_x, zero_y, color, 1));
                //     if (objList[i].type_name  !="1")
                //    {
                //    listaqshpe.Add(DrawLabel(zero_x, zero_y, objList[i].type_name));
                //}
                //    float cx = float.Parse(objList[i].cx);
                //    float cy = float.Parse(objList[i].cy);
                //    float height = float.Parse(objList[i].height)/2;
                //    float width = float.Parse(objList[i].width)/2;
                //    listaqshpe.Add(DrawLine(cx - width,  cy - height, cx - width, cy + height, AqColorEnum.Red, 1));
                //    listaqshpe.Add(DrawLine(cx - width, cy - height, cx + width, cy - height, AqColorEnum.Red, 1));
                //    listaqshpe.Add(DrawLine(cx - width, cy + height, cx + width, cy + height, AqColorEnum.Red, 1));
                //    listaqshpe.Add(DrawLine(cx + width, cy - height, cx + width, cy + height, AqColorEnum.Red, 1));

                float Cx = float.Parse(objList[i].cx);
                float Cy = float.Parse(objList[i].cy);
                float Cx_LU = Cx - 25;
                float Cy_LU = Cy - 25;
                float Cx_RD = Cx + 25;
                float Cy_RD = Cy + 25;
                listaqshpe.Add(DrawLine(Cx_LU, Cy_LU, Cx_LU, Cy_RD, color, lineWidth));
                listaqshpe.Add(DrawLine(Cx_LU, Cy_RD, Cx_RD, Cy_RD, color, lineWidth));
                listaqshpe.Add(DrawLine(Cx_RD, Cy_LU, Cx_RD, Cy_RD, color, lineWidth));
                listaqshpe.Add(DrawLine(Cx_LU, Cy_LU, Cx_RD, Cy_LU, color, lineWidth));
                HObject Himg, Hregion;
                Bitmap2HObject24(class1.ImageIn, out Himg);
                HOperatorSet.GenRectangle1(out Hregion, Cx_LU, Cy_LU, Cx_RD, Cy_RD);
                HOperatorSet.ReduceDomain(Himg, Hregion, out Himg);
                HOperatorSet.WriteImage(Himg, "bmp", 0, @"C:\Users\aq\Desktop\切图\1.bmp");
            }


            return listaqshpe;
        }
        public static void Bitmap2HObject24(Bitmap srcBitmap, out HObject resHImage)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
                BitmapData srcBmpData = srcBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                HOperatorSet.GenImageInterleaved(out resHImage, srcBmpData.Scan0, "bgr", srcBitmap.Width, srcBitmap.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                srcBitmap.UnlockBits(srcBmpData);

            }
            catch (Exception ex)
            {
                resHImage = null;
                throw ex;
            }

        }
        //绘制单个
        private List<AqShap> DrawAidiSingleCounter(AIDIShape objList, AqColorEnum color, int lineWidth)
        {
            List<AqShap> listaqshpe = new List<AqShap>();
            for (int j = 0; j < objList.contours.Count - 1; j++)
            {
                string temp = objList.contours[j].x;
                float sx = float.Parse(objList.contours[j].x);
                float sy = float.Parse(objList.contours[j].y);
                float ex = float.Parse(objList.contours[j + 1].x);
                float ey = float.Parse(objList.contours[j + 1].y);
                listaqshpe.Add(DrawLine(sx, sy, ex, ey, color, lineWidth) as AqShap);
            }
            //再设计一个点，补全多边形
            int n = objList.contours.Count;
            float nx = float.Parse(objList.contours[n - 1].x);
            float ny = float.Parse(objList.contours[n - 1].y);
            float zero_x = float.Parse(objList.contours[0].x);
            float zero_y = float.Parse(objList.contours[0].y);
            listaqshpe.Add(DrawLine(nx, ny, zero_x, zero_y, color, lineWidth) as AqShap);
            listaqshpe.Add(DrawLabelColor(zero_x, zero_y, objList.type_name, color) as AqShap);
            return listaqshpe;
        }

        private AqShap DrawLabelColor(float displayX, float displayY, string typeName, AqColorEnum color)
        {
            AqCharacter label = new AqCharacter();
            label.LeftTopPointX = displayX;
            label.LeftTopPointY = displayY - 10;
            label.RightBottomPointX = displayX + 10;
            label.RightBottomPointY = displayY;

            if (color == AqColorEnum.Red)
            {
                label.ColorRed = 255;
                label.ColorGreen = 0;
                label.ColorBlue = 0;
            }
            else
            {
                label.ColorRed = 0;
                label.ColorGreen = 255;
                label.ColorBlue = 0;
            }

            label.AqString = typeName;
            label.isDragCharacter = true;
            label.isVisible = true;

            return label;
        }

        public AqShap DrawLabel(float displayX, float displayY, string typeName)
        {
            AqCharacter label = new AqCharacter();
            label.LeftTopPointX = displayX;
            label.LeftTopPointY = displayY - 150;
            label.RightBottomPointX = displayX + 150;
            label.RightBottomPointY = displayY;

            label.ColorRed = 255;
            label.ColorGreen = 0;
            label.ColorBlue = 0;
            label.LineWidthInScreen = 100;
            label.AqString = typeName;
            label.isDragCharacter = true;
            label.isVisible = true;

            return label;
        }

        private AqShap DrawLine(float sx, float sy, float ex, float ey, AqColorEnum color, int lineWidth)
        {
            AqLineSegment lineSegment = new AqLineSegment();
            lineSegment.StartX = sx;
            lineSegment.StartY = sy;
            lineSegment.EndX = ex;
            lineSegment.EndY = ey;

            lineSegment.color = color;
            lineSegment.LineWidthInScreen = lineWidth;
            return lineSegment;
        }
        bool isinit = false;
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择文件存放目录";
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folder.ShowDialog() == DialogResult.OK)
            {
                // todo: xiawuhao2013.
                 string DirPath = folder.SelectedPath;
                if (!isinit)
                {
                    //    class1._mRuner.CheckCode = "eb7f1137-09c6-11ea-ada1-525400162223";

                    isinit = class1._mRuner.Init(DirPath, 0, 1);

                }
            }
           
        }

        private void aqDisplay1_Load(object sender, EventArgs e)
        {

        }
    }
}
