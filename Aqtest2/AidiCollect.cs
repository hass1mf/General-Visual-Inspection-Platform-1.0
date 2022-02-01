using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using aqrose.aidi_vision_client;
using System.Drawing;
using System.Runtime.InteropServices;
using AqAidi;
using System.Diagnostics;

namespace AqAidi
{


    public class ModelPathType
    {
        public ModelPathType() { }
        public String ModuleType { get; set; } = "";
        public String TypeNumber { get; set; } = "";
    }

    public class AidiCollect
    {
        private AidiFactoryRunner _aidi;
        private string _checkCode = "f71d8025-424b-11e9-ae35-525400396520";

        public string CheckCode
        {
            set { _checkCode = value; }
        }



        public AidiCollect(string code = "") { }
        /// <summary>
        /// 从选定的文件夹读取模型
        /// </summary>
        /// <param name="selectPath"></param>
        /// <returns></returns>
        public  List<ModelPathType> GetAllModuleFromPath(string selectPath)
        {
            DirectoryInfo root = new DirectoryInfo(selectPath);
            DirectoryInfo[] rootSubDirectory = root.GetDirectories();
            List<ModelPathType> modelPathTypeList = new List<ModelPathType>();

            if (rootSubDirectory.Length == 0)
            {
                Console.WriteLine("当前目录无模型查找，请正确选择目录！");
                return null;
            }

            for (int index = 0; index < rootSubDirectory.Length; index++)
            {
                string[] infoTempName = rootSubDirectory[index].Name.Split('_');
                if (infoTempName[0]== "Factory") continue;
                modelPathTypeList.Add(new ModelPathType());
            }

            for(int index = 0; index < rootSubDirectory.Length; index++)
            {
                var childDirectory = rootSubDirectory[index];
                string tempName = childDirectory.Name;
                string[] infoTempName = tempName.Split('_');
                if (infoTempName.Length < 2)
                {
                    Console.WriteLine("模型名称非法，应该类似{module_name}_{number}(Detect_0)，当前路径：" + selectPath + "， 当前类型" + infoTempName);
                    return null;
                }
                if (infoTempName[0] == "Factory") continue;
                if (infoTempName[0] != "Location" && infoTempName[0] != "Detect" && infoTempName[0] != "Detection" && infoTempName[0] != "Classify" && infoTempName[0] != "FeatureLocation")
                {
                    Console.WriteLine("模型名称非法，应为(Location/Detect/Classify/FeatureLocation)中的一种，当前路径：" + selectPath + "， 当前类型" + infoTempName[0]);
                    return null;
                }
                if (int.Parse(infoTempName[1]) >= rootSubDirectory.Length)
                {
                    Console.WriteLine("可能缺少名称形如***_0的文件夹，当前路径：" + selectPath);
                    return null;
                }
                //按照aidi的组合顺序将模型载入
                modelPathTypeList[Convert.ToInt32(infoTempName[1])].ModuleType = infoTempName[0];
                modelPathTypeList[Convert.ToInt32(infoTempName[1])].TypeNumber = infoTempName[1];
            }
            return modelPathTypeList;
        }

        /// <summary>
        /// 配置aidi参数，并初始化
        /// </summary>
        /// <param name="selectPath"></param>
        /// <param name="gpuNumber"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public bool Init(string selectPath, int gpuNumber, int batch_Size)
        {
            IntVector batchSize = new IntVector();
            batchSize.Add(batch_Size);
            List<ModelPathType> moduleTypeList = GetAllModuleFromPath(selectPath);
            StringVector modelPath = new StringVector();
            StringVector modelType = new StringVector();
            try
            {
                for (int index = 0; index < moduleTypeList.Count; index++)
                {
                    string currentModelPath = selectPath + "/" + moduleTypeList[index].ModuleType + "_" + moduleTypeList[index].TypeNumber + "/model/";
                    string currentModelAqbinPath = selectPath + "/" + moduleTypeList[index].ModuleType + "_" +
                                                    moduleTypeList[index].TypeNumber + "/model/model.aqbin";
                    string currentModelJsonPath = selectPath + "/" + moduleTypeList[index].ModuleType + "_" + moduleTypeList[index].TypeNumber
                                                    + "/model/param.json";

                    if (!Directory.Exists(currentModelPath) || !File.Exists(currentModelJsonPath) || !File.Exists(currentModelAqbinPath))
                    {
                        Console.WriteLine("请注意，路径: " + selectPath + "模型文件不完整！");
                        return false;
                    }
                    modelPath.Add(currentModelPath);
                    modelType.Add(moduleTypeList[index].ModuleType);
                }
                _aidi = new AidiFactoryRunner(_checkCode);
                FactoryClientParamWrapper param = new FactoryClientParamWrapper();
                param.device_number = gpuNumber;
                param.save_model_path_list = modelPath;
                param.operator_type_list = modelType;
                param.use_runtime = true;
                _aidi.set_param(param);
                _aidi.set_batch_size(batchSize);
                _aidi.start();
                return true;
            }
            catch
            {
                return false;
            }   
        }

        /// <summary>
        /// 拷贝完整aidi模型到json下，方便下一次加载
        /// </summary>
        /// <param name="srcpath"></param>
        /// <param name="resultPath"></param>
        /// <param name="batch_Size"></param>
        /// <returns></returns>
        public bool CopyAidiModule(string srcpath, string resultPath, int batch_Size = 1)
        {
            if (srcpath == null) return false;
            if (resultPath == null) return false;
            
            IntVector batchSize = new IntVector();
            batchSize.Add(batch_Size);
            List<ModelPathType> moduleTypeList = GetAllModuleFromPath(srcpath);
            //判断Aidi模型是否完整
            for (int index = 0; index < moduleTypeList.Count; index++)
            {
                string currentModelPath = srcpath + "/" + moduleTypeList[index].ModuleType + "_" + moduleTypeList[index].TypeNumber + "/model/";
                string currentModelAqbinPath = srcpath + "/" + moduleTypeList[index].ModuleType + "_" +
                                                moduleTypeList[index].TypeNumber + "/model/model.aqbin";
                string currentModelJsonPath = srcpath + "/" + moduleTypeList[index].ModuleType + "_" + moduleTypeList[index].TypeNumber
                                                + "/model/param.json";

                if (!Directory.Exists(currentModelPath) || !File.Exists(currentModelJsonPath) || !File.Exists(currentModelAqbinPath))
                {
                    return false;
                }
            }

            if (srcpath == resultPath) return true;

            string[] dirName = srcpath.Split('\\');
            if (CopyDir(srcpath, resultPath + @"\"+ dirName[dirName.Length - 1])) return true;
            return false;
        }

        public bool DeleteAidiModule(string path)
        {
            if (DeleteDir(path)) return true;
            return false;
        }

        private bool CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，里面是包含文件以及目录路径的一个数组
                // 如果只想copy目标文件下面的文件而不包含目录请使用下面方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    if (file.Contains("source") ||
                      file.Contains("copys") ||
                      file.Contains("thumbnail") ||
                      file.Contains("test_result") ||
                      file.Contains("label"))
                    continue;
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DeleteDir(string file)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }
                    //删除空文件夹
                    Directory.Delete(file);
                    return true;
                }
                else
                {
                    return false;
                }        
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// 实时运行aidi，输出aidi的系列化参数
        /// </summary>
        /// <param name="listBitmap"></param>
        /// <returns></returns>
        private List<string> RealTimeRun(List<Bitmap> listBitmap)
        {
            //Stopwatch stpwth = new Stopwatch();
            //stpwth.Start();
            List<string> result_list = new List<string>();
            BatchAidiImage aidi_images = new BatchAidiImage();
            for (int i = 0; i < listBitmap.Count; i++)
            {
                Bitmap current_bitmap = listBitmap[i];
                AidiImage image = new AidiImage();
                string bit_number = current_bitmap.PixelFormat.ToString();
                int channel_number = 1;
                if (bit_number.Contains("24")) //24位即为3通道，8位为1通道
                    channel_number = 3;
                else
                    channel_number = 1;
                int stride;
                byte[] transform_image = GetBGRValues(listBitmap[i], out  stride);
                image.char_ptr_to_mat(transform_image, current_bitmap.Height, current_bitmap.Width, channel_number);
                aidi_images.add_image(image);
            }
            _aidi.set_test_batch_image(aidi_images);
            StringVector results = _aidi.get_detect_result();
            for (int i = 0; i < results.Count; i++)
            {
                result_list.Add(results[i]);
            }
            //stpwth.Stop();
            //string time3 = stpwth.Elapsed.TotalMilliseconds.ToString();

            return result_list;
          
        }
     
        /// <summary>
        /// 输入图像序列，运行模型，获取结果
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public List<List<AIDIShape>> RunModel(List<Bitmap> images)
        {
            try
            {
                List<string> _mResults = RealTimeRun(images);
                List<List<AIDIShape>> aIDIShapes = new List<List<AIDIShape>>();
               _mResults.ForEach( i=> { aIDIShapes.Add(GetobjList(i)); });

                //foreach (string item in _mResults)
                //{
                //    List<AIDIShape> GetobjList1 = GetobjList(item);
                //    aIDIShapes.Add(GetobjList1);
                //}
                return aIDIShapes;
            }
            catch(Exception ex)
            {
                Console.WriteLine("RunModel实时运行aidi函数错误！");
                return null;
            }
        }
        
        /// <summary>
        /// 解析aidi输出的list<string>数据，反序列化成数值
        /// </summary>
        /// <param name="jsonText"></param>
        /// <param name="minArea"></param>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        /// <param name="cxStart"></param>
        /// <param name="cxEnd"></param>
        /// <param name="cyStart"></param>
        /// <param name="cyEnd"></param>
        /// <returns></returns>
        private List<AIDIShape> GetobjList(string jsonText,double minArea = 0, double minWidth = 0, double minHeight = 0,
                                   double cxStart = 0, double cxEnd = 9999999, double cyStart = 0, double cyEnd = 9999999)
        {
            List<AIDIShape> objList = new List<AIDIShape>();
            if (jsonText.Length > 20)
            {
                //Json数据解析
                string str = jsonText;
                var arrdata = Newtonsoft.Json.Linq.JArray.Parse(jsonText);
                objList = arrdata.ToObject<List<AIDIShape>>();
            }
            else  //数据长度<20的肯定是无效数据
            {
                objList = new List<AIDIShape>();
            }
            objList = GetNeededObjectList(objList, minArea, minWidth, minHeight, cxStart, cxEnd, cyStart, cyEnd);   
            return objList;
        }

        /// <summary>
        /// 解析aidi输出的list<string>数据
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="minArea"></param>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        /// <param name="cxStart"></param>
        /// <param name="cxEnd"></param>
        /// <param name="cyStart"></param>
        /// <param name="cyEnd"></param>
        /// <returns></returns>
        private List<AIDIShape> GetNeededObjectList(List<AIDIShape> objList,double minArea = 0, double minWidth = 0, double minHeight = 0,
                                                    double cxStart = 0, double cxEnd = 9999999, double cyStart = 0, double cyEnd = 9999999)
        {
            List<AIDIShape> NeededObjectList = new List<AIDIShape>();
            for (int i = 0; i < objList.Count; i++)
            {
                if (Convert.ToDouble(objList[i].area) >= minArea &&
                    Convert.ToDouble(objList[i].width) >= minWidth &&
                    Convert.ToDouble(objList[i].height) >= minHeight &&
                    (Convert.ToDouble(objList[i].cx) >= cxStart && Convert.ToDouble(objList[i].cx) <= cxEnd) &&
                    (Convert.ToDouble(objList[i].cy) >= cyStart && Convert.ToDouble(objList[i].cy) <= cyEnd))
                {
                    NeededObjectList.Add(objList[i]);
                }
            }
            return NeededObjectList;
        }

      
       



        /// <summary>
        /// 取出像素的灰度值【aidi监测是矩阵传值，然后自行根据长宽及通道，重新构建图像】
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="stride"></param>
        /// <returns></returns>
        private static byte[] GetBGRValues(Bitmap bmp, out int stride)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            stride = bmpData.Stride;
            //int channel = bmpData.Stride / bmp.Width; 
            var rowBytes = bmpData.Width * Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            var imgBytes = bmp.Height * rowBytes;
            byte[] rgbValues = new byte[imgBytes];
            IntPtr ptr = bmpData.Scan0;
            for (var i = 0; i < bmp.Height; i++)
            {
                Marshal.Copy(ptr, rgbValues, i * rowBytes, rowBytes);   // 对齐
                ptr += bmpData.Stride; // next row
            }
            bmp.UnlockBits(bmpData);
            return rgbValues;
        }

       

        public static Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
 
            int stride = bmpData.Stride;  // 扫描线的宽度  
            IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置  
            int scanBytes = stride * height;// 用stride宽度，表示这是内存区域的大小  

            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组  
            int posScan = 0, posReal = 0;// 分别设置两个位置指针，指向源数组和目标数组  
            byte[] pixelValues = new byte[scanBytes];  //为目标数组分配内存  

            for (int x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描  
                for (int y = 0; y < width * 3; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
            }
            //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // 解锁内存区域  
            return bmp;
        }
    }
}
