using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AqCameraFactory
{
	public partial class CameraControlForm : Form
	{
		CameraClient _module;
		AbstractCamera _abstractCamera;

		public CameraControlForm(CameraClient cameraClient)
		{
			_module = cameraClient;
			_abstractCamera = _module.AbstractCamera;
			InitializeComponent();
			InitializeSet();
		}

		private void InitializeSet()
		{
			trackBarExposureTime.Maximum = Convert.ToInt32(_abstractCamera.ExposureTimeMax);
			trackBarExposureTime.Minimum = Convert.ToInt32(_abstractCamera.ExposureTimeMin);
			trackBarExposureTime.Value = Convert.ToInt32(_abstractCamera.ExposureTime);
			textBoxExposureTime.Text = Convert.ToString(_abstractCamera.ExposureTime);

			textBoxImageWidthMax.Text = Convert.ToString(_abstractCamera.ImageWidthMax);
			textBoxImageWidthMin.Text = Convert.ToString(_abstractCamera.ImageWidthMin);
			textBoxImageWidth.Text = Convert.ToString(_abstractCamera.ImageWidth);

			textBoxImageHeightMax.Text = Convert.ToString(_abstractCamera.ImageHeightMax);
			textBoxImageHeightMin.Text = Convert.ToString(_abstractCamera.ImageHeightMin);
			textBoxImageHeight.Text = Convert.ToString(_abstractCamera.ImageHeight);

			textBoxImageOffsetXMax.Text = Convert.ToString(_abstractCamera.ImageWidthMax - _abstractCamera.ImageWidth);
			textBoxImageOffsetXMin.Text = Convert.ToString(0);
			textBoxImageOffsetX.Text = Convert.ToString(_abstractCamera.ImageOffsetX);

			textBoxImageOffsetYMax.Text = Convert.ToString(_abstractCamera.ImageHeightMax - _abstractCamera.ImageHeight);
			textBoxImageOffsetYMin.Text = Convert.ToString(0);
			textBoxImageOffsetY.Text = Convert.ToString(_abstractCamera.ImageOffsetY);

			numericUpDownBalanceRatio.DecimalPlaces = 2;
			numericUpDownBalanceRatio.Increment = 0.1M;
			if (_abstractCamera.IsColor)
			{
				comboBoxBalanceSelector.Enabled = true;
				numericUpDownBalanceRatio.Enabled = true;
				numericUpDownBalanceRatio.Maximum = (decimal)_abstractCamera.BalanceRatioMax;
				numericUpDownBalanceRatio.Minimum = (decimal)_abstractCamera.BalanceRatioMin;
				comboBoxBalanceSelector.SelectedIndex = 0;
			}
			else
			{
				comboBoxBalanceSelector.Enabled = false;
				numericUpDownBalanceRatio.Enabled = false;
			}
		}

		private void trackBarExposureTime_Scroll(object sender, EventArgs e)
		{
			_abstractCamera.ExposureTime = trackBarExposureTime.Value;
			textBoxExposureTime.Text = Convert.ToString(_abstractCamera.ExposureTime);
			_abstractCamera.SetExposureTime();
		}

		private void textBoxExposureTime_Leave(object sender, EventArgs e)
		{
			TextBox obj = (TextBox)sender;
			//正则表达式限定输入格式
			//string exp = @"^\d+(\.\d+)?$";//非负浮点数
			string exp = @"^\d+$";//非负整数
			Regex regex = new Regex(exp);
			bool matched = regex.IsMatch(obj.Text);
			if (!matched)
			{
				obj.Text = Convert.ToString(_abstractCamera.ExposureTime);
				return;
			}
			if (Convert.ToDouble(obj.Text) < _abstractCamera.ExposureTimeMin || Convert.ToDouble(obj.Text) > _abstractCamera.ExposureTimeMax)
			{
				obj.Text = Convert.ToString(_abstractCamera.ExposureTime);
				return;
			}
			_abstractCamera.ExposureTime = Convert.ToInt64(textBoxExposureTime.Text);
			trackBarExposureTime.Value = (int)_abstractCamera.ExposureTime;
			_abstractCamera.SetExposureTime();
		}

		private void textBoxExposureTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) //回车
			{
				TextBox obj = (TextBox)sender;
				//正则表达式限定输入格式
				//string exp = @"^\d+(\.\d+)?$";//非负浮点数
				string exp = @"^\d+$";//非负整数
				Regex regex = new Regex(exp);
				bool matched = regex.IsMatch(obj.Text);
				if (!matched)
				{
					obj.Text = Convert.ToString(_abstractCamera.ExposureTime);
					return;
				}
				if (Convert.ToDouble(obj.Text) < _abstractCamera.ExposureTimeMin || Convert.ToDouble(obj.Text) > _abstractCamera.ExposureTimeMax)
				{
					obj.Text = Convert.ToString(_abstractCamera.ExposureTime);
					return;
				}
				_abstractCamera.ExposureTime = Convert.ToInt64(textBoxExposureTime.Text);
				trackBarExposureTime.Value = (int)_abstractCamera.ExposureTime;
				_abstractCamera.SetExposureTime();
			}
		}

		private void comboBoxBalanceSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			switch(comboBox.SelectedIndex)
			{
				case 0:
					numericUpDownBalanceRatio.Value = (decimal)_abstractCamera.BalanceRatioRed;
					break;
				case 1:
					numericUpDownBalanceRatio.Value = (decimal)_abstractCamera.BalanceRatioBlue;
					break;
				case 2:
					numericUpDownBalanceRatio.Value = (decimal)_abstractCamera.BalanceRatioGreen;
					break;
			}
		}

		private void numericUpDownBalanceRatio_ValueChanged(object sender, EventArgs e)
		{
			switch (comboBoxBalanceSelector.SelectedIndex)
			{
				case 0:
					_abstractCamera.BalanceRatioRed = Convert.ToDouble(numericUpDownBalanceRatio.Value);
					break;
				case 1:
					_abstractCamera.BalanceRatioBlue = Convert.ToDouble(numericUpDownBalanceRatio.Value);
					break;
				case 2:
					_abstractCamera.BalanceRatioGreen = Convert.ToDouble(numericUpDownBalanceRatio.Value);
					break;
			}
			_abstractCamera.SetWhiteBalance();
		}

		private void textBoxImageWidth_TextChanged(object sender, EventArgs e)
		{
			TextBox obj = (TextBox)sender;
			//正则表达式限定输入格式
			//string exp = @"^\d+(\.\d+)?$";//非负浮点数
			string exp = @"^\d+$";//非负整数
			Regex regex = new Regex(exp);
			bool matched = regex.IsMatch(obj.Text);
			if (!matched)
			{
				obj.Text = Convert.ToString(_abstractCamera.ImageWidth);
				return;
			}
			if (Convert.ToDouble(obj.Text) < _abstractCamera.ImageWidthMin || Convert.ToDouble(obj.Text) > _abstractCamera.ImageWidthMax)
			{
				obj.Text = Convert.ToString(_abstractCamera.ImageWidth);
				return;
			}
			//Set param
			_abstractCamera.ImageWidth = Convert.ToInt32(textBoxImageWidth.Text);
		}

		private void textBoxImageHeight_TextChanged(object sender, EventArgs e)
		{
			TextBox obj = (TextBox)sender;
			//正则表达式限定输入格式
			//string exp = @"^\d+(\.\d+)?$";//非负浮点数
			string exp = @"^\d+$";//非负整数
			Regex regex = new Regex(exp);
			bool matched = regex.IsMatch(obj.Text);
			if (!matched)
			{
				obj.Text = Convert.ToString(_abstractCamera.ImageHeight);
				return;
			}
			if (Convert.ToDouble(obj.Text) < _abstractCamera.ImageHeightMin || Convert.ToDouble(obj.Text) > _abstractCamera.ImageHeightMax)
			{
				obj.Text = Convert.ToString(_abstractCamera.ImageHeight);
				return;
			}
			//Set param
			_abstractCamera.ImageHeight = Convert.ToInt32(textBoxImageHeight.Text);
		}

		private void textBoxImageOffsetX_TextChanged(object sender, EventArgs e)
		{
			TextBox obj = (TextBox)sender;
			//正则表达式限定输入格式
			//string exp = @"^\d+(\.\d+)?$";//非负浮点数
			string exp = @"^\d+$";//非负整数
			Regex regex = new Regex(exp);
			bool matched = regex.IsMatch(obj.Text);
			if (!matched)
			{
				obj.Text = Convert.ToString(_abstractCamera.ImageOffsetX);
				return;
			}
			//Set param
			_abstractCamera.ImageOffsetX = Convert.ToInt32(textBoxImageOffsetX.Text);
		}

		private void textBoxImageOffsetY_TextChanged(object sender, EventArgs e)
		{
			TextBox obj = (TextBox)sender;
			//正则表达式限定输入格式
			//string exp = @"^\d+(\.\d+)?$";//非负浮点数
			string exp = @"^\d+$";//非负整数
			Regex regex = new Regex(exp);
			bool matched = regex.IsMatch(obj.Text);
			if (!matched)
			{
				obj.Text = Convert.ToString(_abstractCamera.ImageOffsetY);
				return;
			}
			//Set param
			_abstractCamera.ImageOffsetY = Convert.ToInt32(textBoxImageOffsetY.Text);
		}
	}
}
