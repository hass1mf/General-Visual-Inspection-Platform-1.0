using AqCameraFactory;
using GxIAPINET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AqDaHengCamera
{
	public class DaHengCamera : AbstractCamera
	{
		static IGXFactory _objIGXFactory = null;
		IGXStream _objIGXStream = null;
		IGXDevice _objIGXDevice = null;                    ///<设备对像
		IGXFeatureControl _objIGXFeatureControl = null;    ///<远端设备属性控制器对像
		byte[] _byMonoBuffer = null;                ///<黑白相机buffer
		byte[] _byColorBuffer = null;                ///<彩色相机buffer
		byte[] _byRawBuffer = null;                ///<用于存储Raw图的Buffer
		int _nPayloadSize = 0;                   ///<图像数据大小
		int _nWidth = 0;                   ///<图像宽度
		int _nHeigh = 0;                   ///<图像高度
		private bool _isSnap = false;
		Bitmap _bitmapForSave = null;                ///<bitmap对象,仅供存储图像使用
		GX_DEVICE_OFFLINE_CALLBACK_HANDLE _eventOnOffline = null; //设备掉线事件

		//set window event
		private event AqCaptureDelegate eventCapture;
		private event AqOffLineDelegate eventOffLine;

		public DaHengCamera()
		{
			BrandName = "DaHeng";
		}

		#region Camera Function
		protected override List<CameraProperty> GetAllCamerasImplement()
		{
			List<CameraProperty> cameras = new List<CameraProperty>();
			_objIGXFactory = IGXFactory.GetInstance();
			_objIGXFactory.Init();

			List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();
			_objIGXFactory.UpdateDeviceList(200, listGXDeviceInfo);

			for (int i = 0; i < listGXDeviceInfo.Count; i++)
			{
				CameraProperty camera = new CameraProperty();
				camera.Id = listGXDeviceInfo[i].GetDeviceID();
				camera.Name = listGXDeviceInfo[i].GetUserID();
				camera.Ip = listGXDeviceInfo[i].GetIP();
				camera.Mac = listGXDeviceInfo[i].GetMAC();
				cameras.Add(camera);
			}

			return cameras;
		}

		protected override int OpenCameraImplement()
		{
			//关闭流
			if (null != _objIGXStream)
			{
				_objIGXStream.Close();
				_objIGXStream = null;
			}

			//关闭设备
			if (null != _objIGXDevice)
			{
				_objIGXDevice.Close();
				_objIGXDevice = null;
			}

			List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();
			_objIGXFactory.UpdateDeviceList(200, listGXDeviceInfo);
			foreach (IGXDeviceInfo tempinfo in listGXDeviceInfo)
			{
				if (tempinfo.GetUserID() == Name)
				{
					_objIGXDevice = _objIGXFactory.OpenDeviceByUserID(this.Name, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
					_objIGXFeatureControl = _objIGXDevice.GetRemoteFeatureControl();
                    _eventOnOffline = _objIGXDevice.RegisterDeviceOfflineCallback(this, CallOffLineFunction);
					_objIGXFeatureControl.GetEnumFeature("AcquisitionMode").SetValue("Continuous");
 
					//打开流
					if (null != _objIGXDevice)
					{
						_objIGXStream = _objIGXDevice.OpenStream(0);
					}
				}

			}
			return 0;
		}

		protected override int CloseCameraImplement()
		{
			if (_isSnap)
			{
				_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
				_objIGXFeatureControl = null;
				_isSnap = false;
			}

			//停止流通道、注销采集回调和关闭流
			if (null != _objIGXStream)
			{
				_objIGXStream.StopGrab();
				//注销采集回调函数
				_objIGXStream.UnregisterCaptureCallback();
				_objIGXStream.Close();
				_objIGXStream = null;
			}

			//关闭设备
			if (null != _objIGXDevice)
			{
				//注销掉线回调函数
				_objIGXDevice.UnregisterDeviceOfflineCallback(_eventOnOffline);
				_objIGXDevice.Close();
				_objIGXDevice = null;
			}

			return 0;
		}

		protected override int OpenStreamImplement()
		{
			//开启采集流通道
			if (null != _objIGXStream)
			{
				//RegisterCaptureCallback第一个参数属于用户自定参数(类型必须为引用
				//类型)，若用户想用这个参数可以在委托函数中进行使用
				_objIGXStream.RegisterCaptureCallback(this, OnFrameCallbackFun);
				_objIGXStream.StartGrab();
			}

			//发送开采命令
			if (null != _objIGXFeatureControl)
			{
				_objIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
			}

			_isSnap = true;

			return 0;
		}

		protected override int CloseStreamImplement()
		{
			//发送停采命令
			if (null != _objIGXFeatureControl)
			{
				_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
			}
			else
			{
				return -1;
			}

			//关闭采集流通道
			if (null != _objIGXStream)
			{
				_objIGXStream.StopGrab();
				//注销采集回调函数
				_objIGXStream.UnregisterCaptureCallback();
				return 0;
			}
			_isSnap = false;

			return 0;
		}

		protected override void RegisterCaptureCallback(AqCaptureDelegate delCaptureFun)
		{
			eventCapture += delCaptureFun;
		}

		public void CallFunction(object obj, Bitmap bmp)
		{
			eventCapture(obj, bmp);
		}

		protected override void RegisterOffLineCallback(AqOffLineDelegate delOffLine)
		{
			eventOffLine += delOffLine;
		}

		public void CallOffLineFunction(object obj)
		{
            //         if (eventOffLine==null)
            //         {
            //             eventOffLine += new AqOffLineDelegate(Unregister);
            //         }
            //eventOffLine(obj);
            Unregister(obj);

        }


        void Unregister(object obj)
        {
            GC.Collect();
            _objIGXFeatureControl = null;

        }

        protected override int GetAllFeaturesImplement()
		{
			//获取是否为彩色相机
			if (_objIGXFeatureControl.IsImplemented("PixelColorFilter"))
			{
				string strValue = _objIGXFeatureControl.GetEnumFeature("PixelColorFilter").GetValue();

				if ("None" != strValue)
				{
					IsColor = true;
				}
			}
			//获得图像原始数据大小、宽度、高度等
			//int Size = (int)m_objIGXFeatureControl.GetIntFeature("PayloadSize").GetValue();
			ImageWidthMax = (Int64)_objIGXFeatureControl.GetIntFeature("Width").GetMax();
			ImageWidthMin = (Int64)_objIGXFeatureControl.GetIntFeature("Width").GetMin();
			ImageHeightMax = (Int64)_objIGXFeatureControl.GetIntFeature("Height").GetMax();
			ImageHeightMin = (Int64)_objIGXFeatureControl.GetIntFeature("Height").GetMin();
			{
				if (ImageWidth > ImageWidthMax || ImageWidth < ImageWidthMin)
					ImageWidth = (Int64)_objIGXFeatureControl.GetIntFeature("Width").GetValue();
				if (ImageHeight > ImageHeightMax || ImageHeight < ImageHeightMin)
					ImageHeight = (Int64)_objIGXFeatureControl.GetIntFeature("Height").GetValue();
			}
			// Get the GainRaw GenICam Node
			_objIGXFeatureControl.GetEnumFeature("GainAuto").SetValue("Off");
			_objIGXFeatureControl.GetFloatFeature("Gain").SetValue(0);
			// Get the ExposureTimeRaw GenICam Node
			ExposureTimeMin = Convert.ToInt64(_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin());
			ExposureTimeMax = Convert.ToInt64(_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax());
			if (ExposureTime < ExposureTimeMin || ExposureTime > ExposureTimeMax)
				ExposureTime = Convert.ToInt64(_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetValue());
			// Get the White Balance Node
			if (null != _objIGXFeatureControl)
			{
				bool bIsImplemented = _objIGXFeatureControl.IsImplemented("BalanceRatioSelector");
				// 如果不支持则直接返回
				if (!bIsImplemented)
				{
					return 0;
				}

				bool bIsReadable = _objIGXFeatureControl.IsReadable("BalanceRatioSelector");

				if (bIsReadable)
				{
					//string strTriggerValue = "";                   //当前选择项
					//List<string> list = new List<string>();   //Combox将要填入的列表

					//list.AddRange(m_objIGXFeatureControl.GetEnumFeature("BalanceRatioSelector").GetEnumEntryList());
					////获取当前功能值
					//strTriggerValue = m_objIGXFeatureControl.GetEnumFeature("BalanceRatioSelector").GetValue();

					BalanceRatioMin = _objIGXFeatureControl.GetFloatFeature("BalanceRatio").GetMin();
					BalanceRatioMax = _objIGXFeatureControl.GetFloatFeature("BalanceRatio").GetMax();
				}

			}

			return 0;

		}

		protected override int TriggerConfigurationImplement()
		{
			if (TriggerMode == TriggerModes.Continuous)
			{
				_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("Off");
			}
			else if (TriggerMode == TriggerModes.Unknow)
			{
				_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");
				_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");
				//m_objIGXFeatureControl.GetEnumFeature("TriggerActivation").SetValue("RisingEdge");
				//m_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(0);

			}
			else if (TriggerMode == TriggerModes.HardWare)
			{   //Line0,Line2,Line3
				_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");
				if (TriggerSource == TriggerSources.Line0)
				{
					_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Line0");
					_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(0);
				}
				else if (TriggerSource == TriggerSources.Line2)
				{
					_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Line2");
					_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(0);
				}
				else if (TriggerSource == TriggerSources.Line3)
				{
					_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Line3");
					_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(0);
				}
				//FallingEdge,RisingEdge
				_objIGXFeatureControl.GetEnumFeature("TriggerActivation").SetValue("RisingEdge");
				//Filter
				_objIGXFeatureControl.GetFloatFeature("TriggerFilterRaisingEdge").SetValue(0);
			}

			return 0;
		}

		protected override int SetExposureTimeImplement()
		{
			try
			{
				_objIGXFeatureControl.GetEnumFeature("ExposureAuto").SetValue("Off");
				_objIGXFeatureControl.GetEnumFeature("ExposureMode").SetValue("Timed");

				Int64 currentTime = Convert.ToInt64(ExposureTime);
				if (currentTime >= ExposureTimeMin && currentTime <= ExposureTimeMax)
					_objIGXFeatureControl.GetFloatFeature("ExposureTime").SetValue(ExposureTime);
			}
			catch(Exception ex)
			{
				throw (ex.InnerException);
			}

			return 0;
		}

		protected override int SetImageROIImplement()
		{
			if (ImageWidth < ImageWidthMin || ImageWidth > ImageWidthMax)
			{
				return -1;
			}
			if (ImageHeight < ImageHeightMin || ImageHeight > ImageHeightMax)
			{
				return -1;
			}
			_objIGXFeatureControl.GetIntFeature("Width").SetValue(ImageWidth);
			_objIGXFeatureControl.GetIntFeature("Height").SetValue(ImageHeight);
			if (ImageOffsetX > ImageWidthMax - ImageWidth)
			{
				ImageOffsetX = 0;
			}
			if (ImageOffsetY > ImageHeightMax - ImageHeight)
			{
				ImageOffsetY = 0;
			}
			_objIGXFeatureControl.GetIntFeature("OffsetX").SetValue(ImageOffsetX);
			_objIGXFeatureControl.GetIntFeature("OffsetY").SetValue(ImageOffsetY);

			return 0;
		}

		protected override int SetGainImplement()
		{
			throw new NotImplementedException();
		}

		protected override int SetWhiteBalanceImplement()
		{
			if (_objIGXFeatureControl == null) return -1;
			if (true) return -2;
			//Red
			_objIGXFeatureControl.GetEnumFeature("BalanceRatioSelector").SetValue("Red");
			if (BalanceRatioRed > BalanceRatioMax || BalanceRatioRed < BalanceRatioMin)
			{
				BalanceRatioRed = _objIGXFeatureControl.GetFloatFeature("BalanceRatio").GetValue();
			}

			_objIGXFeatureControl.GetFloatFeature("BalanceRatio").SetValue(BalanceRatioRed);
			//Blue
			_objIGXFeatureControl.GetEnumFeature("BalanceRatioSelector").SetValue("Blue");
			if (BalanceRatioBlue > BalanceRatioMax || BalanceRatioBlue < BalanceRatioMin)
			{
				BalanceRatioBlue = _objIGXFeatureControl.GetFloatFeature("BalanceRatio").GetValue();
			}

			_objIGXFeatureControl.GetFloatFeature("BalanceRatio").SetValue(BalanceRatioBlue);
			//Green
			_objIGXFeatureControl.GetEnumFeature("BalanceRatioSelector").SetValue("Green");
			if (BalanceRatioGreen > BalanceRatioMax || BalanceRatioGreen < BalanceRatioMin)
			{
				BalanceRatioGreen = _objIGXFeatureControl.GetFloatFeature("BalanceRatio").GetValue();
			}

			_objIGXFeatureControl.GetFloatFeature("BalanceRatio").SetValue(BalanceRatioGreen);

			return 0;
		}

		protected override int TriggerSoftwareImplement()
		{
			try
			{
				//发送软触发命令
				if (null != _objIGXFeatureControl)
				{
                    _objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
				}
			}
			catch (CGalaxyException ex)
			{
				throw (ex.InnerException);
			}
			return 0;
		}
		#endregion

		#region 相机自有函数
		/// <summary>
		/// 用灰度数组新建一个8位灰度图像。
		/// </summary>
		/// <param name="rawValues"> 灰度数组(length = width * height)。 </param>
		/// <param name="width"> 图像宽度。 </param>
		/// <param name="height"> 图像高度。 </param>
		/// <returns> 新建的8位灰度位图。 </returns>
		private static Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
		{
			// 新建一个8位灰度位图，并锁定内存区域操作
			Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
			BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
				 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

			// 计算图像参数
			int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数
			IntPtr ptr = bmpData.Scan0;                         // 获取首地址
			int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度
			byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存

			// 为图像数据赋值
			int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					grayValues[posScan++] = rawValues[posSrc++];
				}
				// 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
				posScan += offset;
			}

			// 内存解锁
			Marshal.Copy(grayValues, 0, ptr, scanBytes);
			bitmap.UnlockBits(bmpData);  // 解锁内存区域

			// 修改生成位图的索引表，从伪彩修改为灰度
			ColorPalette palette;
			// 获取一个Format8bppIndexed格式图像的Palette对象
			using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
			{
				palette = bmp.Palette;
			}
			for (int i = 0; i < 256; i++)
			{
				palette.Entries[i] = Color.FromArgb(i, i, i);
			}
			// 修改生成位图的索引表
			bitmap.Palette = palette;

			return bitmap;
		}

		////////////////////////////////////////////////////////////
		private void OnFrameCallbackFun(object objUserParam, IFrameData objIFrameData)
		{
			try
			{
                if (objIFrameData.GetStatus() == GX_FRAME_STATUS_LIST.GX_FRAME_STATUS_INCOMPLETE)
                {
                    return;
                }
                GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
				UpdateBufferSize(objIFrameData);
				if (objIFrameData != null)
				{
					emValidBits = GetBestValudBit(objIFrameData.GetPixelFormat());

					Bitmap dec = new Bitmap((int)objIFrameData.GetWidth(), (int)objIFrameData.GetHeight(), GetFormat(IsColor));
					ColorPalette palette = dec.Palette;
					if (!IsColor)
					{
						//Mono格式需要使用调色板
						for (int i = 0; i < 256; i++)
						{
							palette.Entries[i] = Color.FromArgb(i, i, i);
						}
						dec.Palette = palette;
					}
					Rectangle rect = new Rectangle(0, 0, dec.Width, dec.Height);
					BitmapData decBmpData = dec.LockBits(rect, ImageLockMode.ReadWrite, dec.PixelFormat);

					IntPtr ptrSrc;
					if (IsColor)
					{
						ptrSrc = objIFrameData.ConvertToRGB24(emValidBits, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, false);
					}
					else
					{
						ptrSrc = objIFrameData.ConvertToRaw8(emValidBits);
					}
					int m_nWidth = (int)objIFrameData.GetWidth();
					int m_nHeigh = (int)objIFrameData.GetHeight();
					int stride = GetStride(m_nWidth, IsColor);
					byte[] p_byteSrc = new byte[stride * m_nHeigh];
					int buffsize = stride * (int)objIFrameData.GetHeight();
					Marshal.Copy(ptrSrc, p_byteSrc, 0, buffsize);
					Marshal.Copy(p_byteSrc, 0, decBmpData.Scan0, buffsize);
					dec.UnlockBits(decBmpData);
					CallFunction(this.Name, dec);
				}
			}
			catch (CGalaxyException ex)
			{
				throw (ex.InnerException);
			}
		}

		/// <summary>
		/// 检查图像是否改变并更新Buffer
		/// </summary>
		/// <param name="objIBaseData">图像数据对象</param>
		private void UpdateBufferSize(IBaseData objIBaseData)
		{
			if (null != objIBaseData)
			{
				if (IsCompatible(_bitmapForSave, _nWidth, _nHeigh, IsColor))
				{
					_nPayloadSize = (int)objIBaseData.GetPayloadSize();
					_nWidth = (int)objIBaseData.GetWidth();
					_nHeigh = (int)objIBaseData.GetHeight();
				}
				else
				{
					_nPayloadSize = (int)objIBaseData.GetPayloadSize();
					_nWidth = (int)objIBaseData.GetWidth();
					_nHeigh = (int)objIBaseData.GetHeight();

					_byRawBuffer = new byte[_nPayloadSize];
					_byMonoBuffer = new byte[GetStride(_nWidth, IsColor) * _nHeigh];
					_byColorBuffer = new byte[GetStride(_nWidth, IsColor) * _nHeigh];

					////更新BitmapInfo
					//m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
					//m_objBitmapInfo.bmiHeader.biHeight = m_nHeigh;
					//Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
				}
			}
		}

		/// <summary>
		/// 判断是否兼容
		/// </summary>
		/// <param name="bitmap">Bitmap对象</param>
		/// <param name="nWidth">图像宽度</param>
		/// <param name="nHeight">图像高度</param>
		/// <param name="bIsColor">是否是彩色相机</param>
		/// <returns>true为一样，false不一样</returns>
		private bool IsCompatible(Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
		{
			if (bitmap == null
				|| bitmap.Height != nHeight
				|| bitmap.Width != nWidth
				|| bitmap.PixelFormat != GetFormat(bIsColor)
			 )
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 通过GX_PIXEL_FORMAT_ENTRY获取最优Bit位
		/// </summary>
		/// <param name="em">图像数据格式</param>
		/// <returns>最优Bit位</returns>
		private GX_VALID_BIT_LIST GetBestValudBit(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
		{
			GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
			switch (emPixelFormatEntry)
			{
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO8:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR8:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG8:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB8:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG8:
					{
						emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
						break;
					}
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO10:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR10:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG10:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB10:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG10:
					{
						emValidBits = GX_VALID_BIT_LIST.GX_BIT_2_9;
						break;
					}
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO12:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR12:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG12:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB12:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG12:
					{
						emValidBits = GX_VALID_BIT_LIST.GX_BIT_4_11;
						break;
					}
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO14:
					{
						//暂时没有这样的数据格式待升级
						break;
					}
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO16:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR16:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG16:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB16:
				case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG16:
					{
						//暂时没有这样的数据格式待升级
						break;
					}
				default:
					break;
			}
			return emValidBits;
		}

		/// <summary>
		/// 获取图像显示格式
		/// </summary>
		/// <param name="isColor">是否为彩色相机</param>
		/// <returns>图像的数据格式</returns>
		private PixelFormat GetFormat(bool isColor)
		{
			return isColor ? PixelFormat.Format24bppRgb : PixelFormat.Format8bppIndexed;
		}

		/// <summary>
		/// 计算宽度所占的字节数
		/// </summary>
		/// <param name="nWidth">图像宽度</param>
		/// <param name="isColor">是否是彩色相机</param>
		/// <returns>图像一行所占的字节数</returns>
		private int GetStride(int nWidth, bool isColor)
		{
			return isColor ? nWidth * 3 : nWidth;
		}
		#endregion
	}
}
