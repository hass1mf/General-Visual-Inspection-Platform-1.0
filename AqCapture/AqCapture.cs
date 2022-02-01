using AqCameraFactory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AidiCore.Attributes;
using AidiCore.DataType;
using AidiCore.Interface;
using AqVision.Graphic.AqVision.shape;
using Aqrose.Framework.Utility.Tools;

namespace AqCapture
{
	[Module("AqCapture", "相机设备", "Here is for acquire images")]
    public class AqCapture : AqModuleData, IModule, IDisp
    {
        public int FolderFileIndex { get; set; } = 0;
		public FileParam FileParam { get; set; } = new FileParam();
		public double ExposureTime { get; set; } = 0;
		public AcquisitionMode Mode { get; set; } = AcquisitionMode.FromCamera;


        public bool IsRotate { get; set; }
        public bool IsFlipX { get; set; }
        public bool IsFlipY { get; set; }
        
        public Bitmap ImageOrigin { get; set; }

        #region 输入输出参数
        [Input]
	    public AbstractCamera AbstractCamera { get; set; }
        [Output]
        public Bitmap ImageOut { get; set; }
        [Output]
        public int ImageWidth { get; set; }
        [Output]
        public int ImageHeight { get; set; }
		[Output]
		public string Status { get; set; }
		#endregion

		#region IDisplay接口实现

		public Bitmap Bitmap { get; set; }
		public List<AqShap> DisplayShapes { get; set; }
		public string DisplayWindowName { get; set; }
        public bool IsDisplay { get; set; } = true;
		public bool IsUpdate { get; set; }
		#endregion

		#region IModule接口实现
		public void InitModule(string projectDirectory, string nodeName)
        {
			//初始化输出参数
            ImageOrigin = null;
			ImageOut = null;
            ImageHeight = 0;
			ImageWidth = 0;
			Status = "NG";
			//初始化显示参数
			Bitmap = null;
			DisplayShapes = new List<AqShap>();
			DisplayWindowName = "Image0";
			IsDisplay = true;
            IsUpdate = false;

            // 初始化图像操作参数
            IsRotate = false;
            IsFlipX = false;
            IsFlipY = false;
            //读取参数
            string file = projectDirectory + @"\AqCapture-" + nodeName + ".xml";
			if (!File.Exists(file)) return;

			XmlParameter xmlParameter = new XmlParameter();
			xmlParameter.ReadParameter(file);
			string str;
			str = xmlParameter.GetParamData("FilePath");
			if (str != "") FileParam.FilePath = @Convert.ToString(str);
			str = xmlParameter.GetParamData("FolderPath");
			if (str != "") FileParam.FolderPath = @Convert.ToString(str);
			FileParam.UpdateFilesUnderFolder();
			str = xmlParameter.GetParamData("AcquisitionMode");
			if (str != "") Mode = (AcquisitionMode)Convert.ToInt32(str);
            str = xmlParameter.GetParamData("IsRotate");
            if (str != "") IsRotate = Convert.ToBoolean(str);
            str = xmlParameter.GetParamData("IsFlipX");
            if (str != "") IsFlipX = Convert.ToBoolean(str);
            str = xmlParameter.GetParamData("IsFlipY");
            if (str != "") IsFlipY = Convert.ToBoolean(str);
            str = xmlParameter.GetParamData("DisplayWindowName");
			if (str != "") DisplayWindowName = Convert.ToString(str);
			str = xmlParameter.GetParamData("IsDisplay");
			if (str != "") IsDisplay = Convert.ToBoolean(str);
			str = xmlParameter.GetParamData("ExposureTime");
			if (str != "") ExposureTime = Convert.ToDouble(str);
		}

        public void SaveModule(string projectDirectory, string nodeName)
        {
			string file = projectDirectory + @"\AqCapture-" + nodeName + ".xml";
			XmlParameter xmlParameter = new XmlParameter();
			xmlParameter.Add("FilePath", @FileParam.FilePath);
			xmlParameter.Add("FolderPath", @FileParam.FolderPath);
			xmlParameter.Add("AcquisitionMode", (int)Mode);
			xmlParameter.Add("DisplayWindowName", DisplayWindowName);
			xmlParameter.Add("IsDisplay", IsDisplay);
			xmlParameter.Add("ExposureTime", ExposureTime);
            xmlParameter.Add("IsRotate", IsRotate);
            xmlParameter.Add("IsFlipX", IsFlipX);
            xmlParameter.Add("IsFlipY", IsFlipY);
            xmlParameter.WriteParameter(file);
		}

		public void CloseModule() { }

        public void Run()
        {
            ImageOut = null;
            ImageOrigin = null;
            switch (Mode)
            {
                case AcquisitionMode.FromCamera:
					AcquisitionCamera();
					break;
                case AcquisitionMode.FromFile:
					AcquisitionFile();
                    break;
                case AcquisitionMode.FromFolder:
					AcquisitionFolder();
					break;
            }
			if (ImageOrigin != null)
			{
                ImageRotateFlip();
				Status = "OK";
				ImageWidth = ImageOut.Width;
				ImageHeight = ImageOut.Height;

				if (IsDisplay)
				{
					Bitmap = ImageOut;
					IsUpdate = true;
				}
			}
			else
			{
				Status = "NG";
			}
		}

        public bool StartSetForm()
        {
            var form = new CaptureForm(this);
            form.ShowDialog();
			form.Close();
			return true;
        }
        #endregion

        #region 采集函数
        public void AcquisitionCamera()
        {
            try
            {
                ImageOrigin = null;
				if (AbstractCamera == null) 
				{
					//MessageManager.Instance().Warn("AqCapture: 传入相机为空");
				}
				//动态调整曝光
				if (ExposureTime < AbstractCamera.ExposureTimeMax &&
					ExposureTime > AbstractCamera.ExposureTimeMin &&
					AbstractCamera.ExposureTime != ExposureTime) 
				{
					AbstractCamera.ExposureTime = ExposureTime;
					AbstractCamera.SetExposureTime();
				}
				//采集
				if (AbstractCamera.TriggerMode == TriggerModes.HardWare) 
				{
					if (!AbstractCamera.ImageCameraQueue.IsEmpty) 
					{
						bool dequeueSuccesful = false;
						bool peekSuccesful = false;
						Bitmap workItem = null;

						peekSuccesful = AbstractCamera.ImageCameraQueue.TryPeek(out workItem);

						if (peekSuccesful)
						{
							//Get ImageOrigin
							dequeueSuccesful = AbstractCamera.ImageCameraQueue.TryDequeue(out workItem);//出队
							ImageOrigin = workItem;
						}
					}
				}
				else
				{
					if (AbstractCamera.Acquisition() == 0)
					{
						ImageOrigin = AbstractCamera.ImageCameraOut;
					}
				}
			}
            catch (FormatException ex)
            {
			//	MessageManager.Instance().Alarm("AqCapture采图失败" + ex.Message);
			}
            catch (Exception ex)
            {
			//	MessageManager.Instance().Alarm("AqCapture采图失败" + ex.Message);
			}
        }

        public void AcquisitionFile()
        {
			try
			{
				ImageOrigin = ImageOperateTools.GetImageFromFile(FileParam.FilePath);
			}
			catch (Exception ex)
			{
				//MessageManager.Instance().Info("AqCapture:当前路径为 " + Directory.GetCurrentDirectory());
				//MessageManager.Instance().Alarm("AqCapture采图失败" + ex.Message);
			}	
		}

        public void AcquisitionFolder()
        {
			try
			{
				string file = FileParam.FolderFiles[FolderFileIndex];
				ImageOrigin = ImageOperateTools.GetImageFromFile(file);

				if (FolderFileIndex < (FileParam.FolderFiles.Count - 1)) 
				{
					FolderFileIndex++;
				}
				else
				{
					FolderFileIndex = 0;
				}
			}
			catch (Exception ex)
			{
				//MessageManager.Instance().Info("AqCapture:当前路径为 " + Directory.GetCurrentDirectory());
				//MessageManager.Instance().Alarm("AqCapture采图失败" + ex.Message);
			}
		}
		#endregion

        public void ImageRotateFlip()
        {
            if (ImageOrigin==null)
            {
                ImageOut = null;
                return;
            }
            ImageOut = (Bitmap)ImageOrigin.Clone();
            if (IsRotate)
            {
                if (IsFlipX)
                {
                    if (IsFlipY)
                    {
                        ImageOut.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    }
                    else
                    {
                        ImageOut.RotateFlip(RotateFlipType.Rotate90FlipX);
                    }
                }
                else
                {
                    if (IsFlipY)
                    {
                        ImageOut.RotateFlip(RotateFlipType.Rotate90FlipY);
                    }
                    else
                    {
                        ImageOut.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                }
            }
            else
            {
                if (IsFlipX)
                {
                    if (IsFlipY)
                    {
                        ImageOut.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    }
                    else
                    {
                        ImageOut.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                }
                else
                {
                    if (IsFlipY)
                    {
                        ImageOut.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }
                    else
                    {
                        ImageOut.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    }
                }
            }
        }
	}
}
