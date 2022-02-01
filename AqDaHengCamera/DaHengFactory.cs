using AqCameraFactory;
using System;

namespace AqDaHengCamera
{
	public class DaHengFactory : CameraFactory
	{
		DaHengCamera _dahengCamera;
		public AbstractCamera CreateCamera()
		{
			_dahengCamera = new DaHengCamera();
			return _dahengCamera;
		}
	}
}
