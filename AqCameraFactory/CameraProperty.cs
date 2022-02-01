using System;

namespace AqCameraFactory
{
	#region EnumProperty
	public enum ExposureMode
	{
		Unknow = 0,
		Continuous,
		On,
		Off
	}

	public enum TriggerEdges
	{
		Unknow = 0,
		FallingEdge,
		RisingEdge
	}

	public enum TriggerModes
	{
		Unknow = 0,
		Continuous = 1,
		HardWare = 2,
		EventTrigger = 3
	}

	public enum TriggerSources
	{
		Unknow = 0,
		Software = 1,
		Line0 = 2,
		Line1 = 3,
		Line2 = 4,
		Line3 = 5,
		Line4 = 6,
		Line5 = 7,
		Line6 = 8,
		Line7 = 9
	}

	public enum TriggerSwitchs
	{
		Unknow = 0,
		OFF,
		ON
	}

	public enum BalanceSwitchs
	{
		Unknow = 0,
		Red,
		Blue,
		Green
	}
	#endregion

	public class CameraProperty
	{
		#region Camera property
		public string BrandName { get; set; } = "";
		public string ImageSavePath { get; set; } = "";
		public int QueueImageNumMax { get; } = 100;
		public int QueueImageNumMin { get; } = 0;
		private int _queueImageNum = 10;
		public int QueueImageNum
		{
			get
			{
				if (_queueImageNum > QueueImageNumMax || _queueImageNum < QueueImageNumMin)
				{
					return 10;
				}
				else
				{
					return _queueImageNum;
				}
			}
			set
			{
				if (value > QueueImageNumMax || value < QueueImageNumMin)
				{
					_queueImageNum = 10;
				}
				else
				{
					_queueImageNum = value;
				}
			}
		}
		public string Id { get; set; } = "";
		public string Name { get; set; } = "";
		public string Ip { get; set; } = "";
		public string Mac { get; set; } = "";
		public double ExposureTime { get; set; }
		public Int64 ExposureTimeMax { get; set; } = 0;
		public Int64 ExposureTimeMin { get; set; } = 0;
		public double Gain { get; set; } = 0;
		public double AcquisitionFrequency { get; set; } = 0;
		public double TriggerDelay { get; set; } = 0;
		public bool GainAuto { get; set; } = false;

		//set param ROI
		public Int64 ImageWidth { get; set; } = 0;
		public Int64 ImageWidthMax { get; set; } = 0;
		public Int64 ImageWidthMin { get; set; } = 0;
		public Int64 ImageHeight { get; set; } = 0;
		public Int64 ImageHeightMax { get; set; } = 0;
		public Int64 ImageHeightMin { get; set; } = 0;
		public Int64 ImageOffsetX { get; set; } = 0;
		public Int64 ImageOffsetY { get; set; } = 0;

		//set param ROI
		public TriggerSources TriggerSource { get; set; } = TriggerSources.Line0;
		public TriggerModes TriggerMode { get; set; } = TriggerModes.Unknow;
		public TriggerEdges TriggerEdge { get; set; } = TriggerEdges.RisingEdge;
		public TriggerSwitchs TriggerSwitch { get; set; } = TriggerSwitchs.Unknow;

		//Set White Balance
		public BalanceSwitchs BalanceSwitch { get; set; } = BalanceSwitchs.Unknow;
		public double BalanceRatioMax { get; set; } = 0;
		public double BalanceRatioMin { get; set; } = 0;
		public double BalanceRatioRed { get; set; } = 0;
		public double BalanceRatioBlue { get; set; } = 0;
		public double BalanceRatioGreen { get; set; } = 0;
		#endregion Cmaera property

		#region Save Param
		public virtual bool ReadParam(string path)
		{
   //         if (!System.IO.File.Exists(path)) return false;

   //         XmlParameter xmlParameter = new XmlParameter();
			//xmlParameter.ReadParameter(path);
			//string str;
			//str = xmlParameter.GetParamData("ImageSavePath");
			//if (str != "") ImageSavePath = @Convert.ToString(str);
			//str = xmlParameter.GetParamData("Name");
			//if (str != "") Name = Convert.ToString(str);
			//str = xmlParameter.GetParamData("Brand");
			//str = xmlParameter.GetParamData("Id");
			//if (str != "") Id = Convert.ToString(str);
			//str = xmlParameter.GetParamData("Ip");
			//if (str != "") Ip = Convert.ToString(str);
			//str = xmlParameter.GetParamData("Mac");
			//if (str != "") Mac = Convert.ToString(str);
			//str = xmlParameter.GetParamData("TriggerSource");
			//if (str != "") TriggerSource = (TriggerSources)Convert.ToInt32(str);
			//str = xmlParameter.GetParamData("TriggerSwitch");
			//if (str != "") TriggerSwitch = (TriggerSwitchs)Convert.ToInt32(str);
			//str = xmlParameter.GetParamData("TriggerMode");
			//if (str != "") TriggerMode = (TriggerModes)Convert.ToInt32(str);
			//str = xmlParameter.GetParamData("TriggerEdge");
			//if (str != "") TriggerEdge = (TriggerEdges)Convert.ToInt32(str);
			//str = xmlParameter.GetParamData("ExposureTime");
			//if (str != "") ExposureTime = Convert.ToDouble(str);
			//str = xmlParameter.GetParamData("AcquisitionFrequency");
			//if (str != "") AcquisitionFrequency = Convert.ToDouble(str);
			//str = xmlParameter.GetParamData("TriggerDelay");
			//if (str != "") TriggerDelay = Convert.ToDouble(str);
			//str = xmlParameter.GetParamData("Gain");
			//if (str != "") Gain = Convert.ToDouble(str);
			//str = xmlParameter.GetParamData("GainAuto");
			//if (str != "") GainAuto = Convert.ToBoolean(str);
			//str = xmlParameter.GetParamData("ImageWidth");
			//if (str != "") ImageWidth = Convert.ToInt64(str);
			//str = xmlParameter.GetParamData("ImageHeight");
			//if (str != "") ImageHeight = Convert.ToInt64(str);
			//str = xmlParameter.GetParamData("ImageOffsetX");
			//if (str != "") ImageOffsetX = Convert.ToInt64(str);
			//str = xmlParameter.GetParamData("ImageOffsetY");
			//if (str != "") ImageOffsetY = Convert.ToInt64(str);
			//str = xmlParameter.GetParamData("QueueImageNum");
			//if (str != "") QueueImageNum = Convert.ToInt32(str);
			//str = xmlParameter.GetParamData("BalanceRatioRed");
			//if (str != "") BalanceRatioRed = Convert.ToDouble(str);
			//str = xmlParameter.GetParamData("BalanceRatioBlue");
			//if (str != "") BalanceRatioBlue = Convert.ToDouble(str);
			//str = xmlParameter.GetParamData("BalanceRatioGreen");
			//if (str != "") BalanceRatioGreen = Convert.ToDouble(str);

			return true;
		}
		public virtual void SaveParam(string path)
		{
			//XmlParameter xmlParameter = new XmlParameter();
			//xmlParameter.Add("ImageSavePath", @ImageSavePath);
			//xmlParameter.Add("Name", Name);
			//xmlParameter.Add("Id", Id);
			//xmlParameter.Add("Ip", Ip);
			//xmlParameter.Add("Mac", Mac);
			//xmlParameter.Add("TriggerSource", (int)TriggerSource);
			//xmlParameter.Add("TriggerSwitch", (int)TriggerSwitch);
			//xmlParameter.Add("TriggerMode", (int)TriggerMode);
			//xmlParameter.Add("TriggerEdge", (int)TriggerEdge);
			//xmlParameter.Add("ExposureTime", ExposureTime);
			//xmlParameter.Add("AcquisitionFrequency", AcquisitionFrequency);
			//xmlParameter.Add("TriggerDelay", TriggerDelay);
			//xmlParameter.Add("Gain", Gain);
			//xmlParameter.Add("GainAuto", GainAuto);
			//xmlParameter.Add("ImageWidth", (double)ImageWidth);
			//xmlParameter.Add("ImageHeight", (double)ImageHeight);
			//xmlParameter.Add("ImageOffsetX", (double)ImageOffsetX);
			//xmlParameter.Add("ImageOffsetY", (double)ImageOffsetY);
			//xmlParameter.Add("QueueImageNum", QueueImageNum);
			//xmlParameter.Add("BalanceRatioRed", BalanceRatioRed);
			//xmlParameter.Add("BalanceRatioBlue", BalanceRatioBlue);
			//xmlParameter.Add("BalanceRatioGreen", BalanceRatioGreen);

			//xmlParameter.WriteParameter(path);
		}
		#endregion
	}
}
