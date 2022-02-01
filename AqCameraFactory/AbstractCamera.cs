using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AqCameraFactory
{
	public delegate void AqCaptureDelegate(object objUserParam, Bitmap bitmap);
	public delegate void AqOffLineDelegate(object obj);
	public delegate void DelegateOnError(string message);
	public delegate void DelegateOnWarnning(string message);
	public delegate void DelegateOnInformation(string message);
	public delegate void DelegateOnGetImage(Bitmap bitmap);

	public abstract class AbstractCamera : CameraProperty 
	{
		//Debug
		private int _frameCount = 0;
		private bool _isCaptureSuccess = false;

		public ConcurrentQueue<Bitmap> ImageCameraQueue { get; set; } = new ConcurrentQueue<Bitmap>();
		public List<CameraProperty> CamerasList { get; set; } = new List<CameraProperty>();
		public Bitmap ImageCameraOut { get; set; } = null;

		#region Camera status
		public bool IsCameraOpened { get; set; } = false;
		public bool IsStreamOpened { get; set; } = false;
		public bool IsAcqiring { get; set; } = false;
		public bool IsColor { get; set; } = true;
		#endregion


		protected event DelegateOnError EventOnError;
		protected event DelegateOnWarnning EventOnWarn;
		protected event DelegateOnInformation EventOnInfo;
		public event DelegateOnGetImage EventOnGetImage;

		public AbstractCamera()
		{
			EventOnError += OnError;
			EventOnWarn += OnWarn;
			EventOnInfo += OnMessage;
		}

		#region 报错机制
		private void OnError(string message)
		{
			//MessageManager.Instance().Alarm("AbstrctCamera OnError: " + BrandName + ": " + message);
		}

		private void OnWarn(string message)
		{
			//MessageManager.Instance().Warn("AbstrctCamera OnWarn: " + BrandName + ": " + message);
		}

		private void OnMessage(string message)
		{
			//MessageManager.Instance().Info("AbstrctCamera OnMessage: " + BrandName + ": " + message);
		}
		#endregion

		#region Child class implement 
		protected abstract List<CameraProperty> GetAllCamerasImplement();
		protected abstract void RegisterCaptureCallback(AqCaptureDelegate delCaptureFun);
		protected abstract void RegisterOffLineCallback(AqOffLineDelegate delOffLine);
		protected abstract int OpenCameraImplement();
		protected abstract int CloseCameraImplement();
		protected abstract int OpenStreamImplement();
		protected abstract int CloseStreamImplement();
		protected abstract int GetAllFeaturesImplement();
		protected abstract int TriggerConfigurationImplement();
		protected abstract int SetExposureTimeImplement();
		protected abstract int SetGainImplement();
		protected abstract int SetWhiteBalanceImplement();
		protected abstract int SetImageROIImplement();
		protected abstract int TriggerSoftwareImplement();
		#endregion

		public virtual void InitCameraProperty()
		{
			CamerasList = GetAllCameras();
			if (CamerasList != null)
			{
				//遍历相机列表，获取参数记录的相机
				for (int i = CamerasList.Count - 1; i >= 0; i--)
				{
					if (CamerasList[i].Name == Name)
					{
						Ip = CamerasList[i].Ip;
						Id = CamerasList[i].Id;
						Mac = CamerasList[i].Mac;
						return;
					}
				}
			}
			Name = null;
			Gain = 0;
			ExposureTime = 0;
			TriggerMode = TriggerModes.Unknow;
			TriggerSource = TriggerSources.Software;
			ImageWidth = 0;
			ImageHeight = 0;
			ImageOffsetX = 0;
			ImageOffsetY = 0;
		}

		public virtual List<CameraProperty> GetAllCameras()
		{
			List<CameraProperty> cameras = GetAllCamerasImplement();
			return cameras;
		}

		public virtual void OpenCamera()
		{
			if (Name == null) return;
			if (IsCameraOpened) return;
			try
			{
				RegisterCaptureCallback(new AqCaptureDelegate(RecCapture));
				RegisterOffLineCallback(new AqOffLineDelegate(ReConnect));
				OpenCameraImplement();
				GetAllFeaturesImplement();
				TriggerConfigurationImplement();
				SetImageROIImplement();
				EventOnInfo("打开相机成功");
				IsCameraOpened = true;
			}
			catch (Exception ex)
			{
				EventOnError("打开相机错误," + ex.Message);
			}
		}

		public virtual void CloseCamera()
		{
			if (Name == null) return;
			if (!IsCameraOpened) return;

			CloseCameraImplement();
			IsCameraOpened = false;
		}

		public virtual void OpenStream()
		{
			if (Name == null) return;
			if (IsStreamOpened) return;
			try
			{
				SetExposureTimeImplement();
				SetWhiteBalanceImplement();
				OpenStreamImplement();
				EventOnInfo("打开图像流成功");
				IsStreamOpened = true;
				if (TriggerMode == TriggerModes.Continuous || TriggerMode == TriggerModes.HardWare) 
				{
					IsAcqiring = true;
				}
			}
			catch (Exception ex)
			{
				EventOnError("打开图像流错误 " + ex.Message);
			}
		}

		public virtual void CloseStream()
		{
			if (Name == null) return;
			if (!IsStreamOpened) return;
			CloseStreamImplement();
			IsStreamOpened = false;
			if (TriggerMode == TriggerModes.Continuous || TriggerMode == TriggerModes.HardWare)
			{
				IsAcqiring = false;
			}
		}

		protected virtual void RecCapture(object objUserparam, Bitmap bitmap)
		{
			ImageCameraOut = bitmap;
			_isCaptureSuccess = true;
			//硬触发队列
			if (TriggerMode == TriggerModes.HardWare)
			{
				bool peekSuccesful = false;
				Bitmap workItem = null;

				peekSuccesful = ImageCameraQueue.TryPeek(out workItem);
				//Clean image queue
				if (ImageCameraQueue.Count >= QueueImageNum)
				{
					int num = ImageCameraQueue.Count - QueueImageNum + 1;
					for (int i = 0; i < num; i++)
					{
						ImageCameraQueue.TryDequeue(out workItem);
					}
				}
				ImageCameraQueue.Enqueue(bitmap);
			}

			EventOnGetImage?.Invoke(bitmap);
			//相当于if(EventOnGetImage!=null) EventOnGetImage();
		}

		protected virtual void ReConnect(object obj)
		{
			EventOnInfo("相机掉线重连");
			CloseStream();
			CloseCamera();
			Thread.Sleep(20);
			OpenCamera();
			OpenStream();
		}

		public void GetAllFeatures()
		{
			GetAllFeaturesImplement();
		}

		public void TriggerConfiguration()
		{
			TriggerConfigurationImplement();
		}

		public void SetExposureTime()
		{
			SetExposureTimeImplement();
		}

		public void SetWhiteBalance()
		{
			SetWhiteBalanceImplement();
		}

		public void SetGain()
		{
			SetGainImplement();
		}

		public void SetImageROI()
		{
			SetImageROIImplement();
		}

		public virtual int Acquisition()
		{
			try
			{
				OpenCamera();
				if (!IsCameraOpened) return -2;
				OpenStream();
				if (!IsStreamOpened) return -3;

				_isCaptureSuccess = false;
				TriggerSoftwareImplement();
				DateTime t1 = DateTime.Now;
				DateTime t2;
				TimeSpan ts;
				while (!_isCaptureSuccess)
				{
					System.Threading.Thread.Sleep(20);
					t2 = DateTime.Now;
					ts = t2.Subtract(t1);
					if (ts.TotalSeconds >= 3)
					{
						EventOnWarn("采图超时");
						return -1;
					}
				}
				return 0;
			}
			catch (FormatException ex)
			{
				EventOnError("采图失败 " + ex.Message);
				return -4;
			}
			catch (Exception ex)
			{
				EventOnError("采图失败 " + ex.Message);
				return -5;
			}
		}

		public virtual void AcquisitionContinous(bool command)
		{
			if (command)
			{
				OpenStream();
			}
			else
			{
				CloseStream();
			}
		}
	}
}
