using AqCameraFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AqCapture
{

	public class FileParam
    {
		#region 属性定义
		public string FilePath { get; set; }
		public string FolderPath { get; set; }
		public List<string> FolderFiles { get; set; }
		#endregion 属性定义

		public FileParam()
		{
			FilePath = "";
			FolderPath = "";
			FolderFiles = new List<string>();
		}

        //方法：遍历添加文件夹中的文件路径到FolderFiles
        public void UpdateFilesUnderFolder()
        {
			Directory.SetCurrentDirectory(Application.StartupPath);
			FolderFiles.Clear();
			if (FolderPath == "") return;//无路径
			if (!Directory.Exists(FolderPath)) return;//空路径
			if (Directory.GetFiles(FolderPath).Length == 0) return;//空文件夹

			//文件格式过滤
			string[] files = Directory.GetFiles(FolderPath);
			foreach (string str in files)
			{
				if (IsPicture(str))
				{
					FolderFiles.Add(str);
				}
			}
		}

        private bool IsPicture(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                byte[] b = new byte[2];
                buffer = reader.ReadByte();
                b[0] = buffer;
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                b[1] = buffer;
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();

                FileExtension fileExtension = (FileExtension)(Convert.ToInt32(fileClass));
                bool A = fileExtension == FileExtension.JPG;
                bool B = fileExtension == FileExtension.BMP;
                bool C = fileExtension == FileExtension.PNG;
                if (A || B || C) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        private enum FileExtension
        {
            JPG = 255216,
            GIF = 7173,
            BMP = 6677,
            PNG = 13780,
            COM = 7790,
            EXE = 7790,
            DLL = 7790,
            RAR = 8297,
            ZIP = 8075,
            XML = 6063,
            HTML = 6033,
            ASPX = 239187,
            CS = 117115,
            JS = 119105,
            TXT = 210187,
            SQL = 255254,
            BAT = 64101,
            BTSEED = 10056,
            RDP = 255254,
            PSD = 5666,
            PDF = 3780,
            CHM = 7384,
            LOG = 70105,
            REG = 8269,
            HLP = 6395,
            DOC = 208207,
            XLS = 208207,
            DOCX = 208207,
            XLSX = 208207,
        }
    }
}
