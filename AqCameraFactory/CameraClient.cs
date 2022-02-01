using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AqCameraFactory
{
	public interface CameraClient
	{
		bool IsSDKLoad { get; set; }
		AbstractCamera AbstractCamera { get; set; }
	}
}
