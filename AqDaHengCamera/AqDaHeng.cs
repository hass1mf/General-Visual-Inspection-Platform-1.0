using AqCameraFactory;
using System;
using System.IO;
using AidiCore.Interface;
using AidiCore.Attributes;
using AqVision.Graphic.AqVision.shape;
using System.Collections.Generic;
using System.Drawing;
using AidiCore.DataType;

namespace AqDaHengCamera
{
	[Module("AqDaHeng", "相机设备", "Here is for DaHeng Camera parameters set.")]
	public class AqDaHeng : AqModuleData, IDisp, IModule, CameraClient
	{
		private static object _myLock = new object();
		private DaHengCamera _dahengCamera;
		public bool IsSDKLoad { get; set; } = true;
		[Output]
		public AbstractCamera AbstractCamera
		{
			get => _dahengCamera;
			set => _dahengCamera = (DaHengCamera)value;
		}
        public Bitmap Bitmap { get; set; }
        public List<AqShap> DisplayShapes { get; set; } = new List<AqShap>();

        public AqDaHeng()
	    {
	        try
	        {
	            CameraFactory dahengFactory = new DaHengFactory();
	            AbstractCamera = dahengFactory.CreateCamera() as DaHengCamera;
	        }
	        catch (Exception ex)
	        {
	            IsSDKLoad = false;
	        }
        }

		#region IModule接口实现
		public void InitModule(string projectDirectory, string nodeName)
		{
			try
			{
				string file = projectDirectory + @"\AqDaHeng-" + nodeName + ".xml";
				AbstractCamera.ReadParam(file);
			}
			catch(Exception ex)
			{
				IsSDKLoad = false;
			}
		}

		public void SaveModule(string projectDirectory, string nodeName)
		{
			if (!IsSDKLoad) return;

			try
			{
				string file = projectDirectory + @"\AqDaHeng-" + nodeName + ".xml";
				if (!Directory.Exists(projectDirectory))
					Directory.CreateDirectory(projectDirectory);
				AbstractCamera.SaveParam(file);
			}
			catch (Exception ex)
			{
				IsSDKLoad = false;
			}
		}

		public void CloseModule()
		{
			if (!IsSDKLoad) return;

			try
			{
				AbstractCamera.CloseStream();
				AbstractCamera.CloseCamera();
			}
			catch (Exception ex)
			{
				IsSDKLoad = false;
			}
		}

		public void Run()
		{
			lock (_myLock)
			{
				if (!IsSDKLoad) return;

				try
				{
					if (AbstractCamera.CamerasList.Count == 0)
					{
						AbstractCamera.InitCameraProperty();
					}
					AbstractCamera.OpenCamera();
					AbstractCamera.OpenStream();
				}
				catch (Exception ex)
				{
					IsSDKLoad = false;
				}
			}
		}

		public bool StartSetForm()
		{
			if (!IsSDKLoad) return false;

			try
			{
				var form = new DaHengSetForm(this);
				form.ShowDialog();
				form.Close();
				return true;
			}
			catch (Exception ex)
			{
				IsSDKLoad = false;
				return false;
			}
		}
		#endregion
	}
}
