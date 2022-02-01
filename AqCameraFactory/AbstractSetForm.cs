using AidiCore.Interface;
using AidiCore.ProjectManger;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AqCameraFactory
{
	public partial class AbstractSetForm : Form
    {
		private bool _isFirsShow = true;
		protected CameraClient _module;
		protected AbstractCamera _abstractCamera;
		private System.Windows.Forms.Timer _formTimer = new System.Windows.Forms.Timer();
		private ConcurrentQueue<Bitmap> _dataQueue = new ConcurrentQueue<Bitmap>();
		private Bitmap _image;
		private Int64 _pickNum = 0;
		private Int64 _showNum = 0;

		public AbstractSetForm()
		{
			InitializeComponent();
			_formTimer.Interval = 10;
			_formTimer.Tick += ExecuteTask;
			_formTimer.Start();
		}

		protected virtual void InitializeSet()
		{
			textBoxCameraID.Text = _abstractCamera.Id;
			textBoxCameraIP.Text = _abstractCamera.Ip;
			textBoxMacAddress.Text = _abstractCamera.Mac;
			textBoxImagePath.Text = _abstractCamera.ImageSavePath;
			textBoxCurrentQueueNum.Text = _abstractCamera.ImageCameraQueue.Count.ToString();
			numericUpDownQueueImageNum.Maximum = _abstractCamera.QueueImageNumMax;
			numericUpDownQueueImageNum.Minimum = _abstractCamera.QueueImageNumMin;
			numericUpDownQueueImageNum.Value = _abstractCamera.QueueImageNum;

			_abstractCamera.EventOnGetImage += new DelegateOnGetImage(ImageDataEnqueue);
			//Set trigger mode show
			switch (_abstractCamera.TriggerMode)
			{
				case TriggerModes.Unknow:
					comboBoxTriggerMode.SelectedIndex = 0;
					break;
				case TriggerModes.Continuous:
					comboBoxTriggerMode.SelectedIndex = 1;
					break;
				case TriggerModes.HardWare:
					comboBoxTriggerMode.SelectedIndex = 2;
					break;
				default:
					break;
			}
			//Set acquisition button show
			if (_abstractCamera.IsAcqiring)
			{
				buttonAcquisition.Text = "停止采集";
			}
			else
			{
				buttonAcquisition.Text = "采集图像";
			}

			SetParamsControlShow();
			FormRefresh();
		}

		public void FormRefresh()
		{
			try
			{
				if (_isFirsShow && _image != null)
				{
					aqDisplay.Image = _image;
					aqDisplay.FitToScreen();
					_isFirsShow = false;
					_showNum++;
				}
				else if (_isFirsShow && _image == null)
				{
					if (_abstractCamera.ImageCameraOut != null)
					{
						aqDisplay.Image = _abstractCamera.ImageCameraOut;
						aqDisplay.FitToScreen();
						_showNum++;
					}
				}
				else if (_image != null)
				{
					aqDisplay.Image = _image;
					_showNum++;
				}
				textBoxCurrentQueueNum.Text = _abstractCamera.ImageCameraQueue.Count.ToString();
				//Status bar
				toolStripStatusPickNum.Text = _pickNum.ToString();
				toolStripStatusShowNum.Text = _showNum.ToString();
			}
			catch (Exception ex)
			{
			}
		}

		private void SetParamsControlShow()
		{
			if (_abstractCamera == null)
			{
				foreach (Control ct in groupBoxParamSet.Controls)
				{
					ct.Enabled = false;
				}
			}
			else
			{
				foreach (Control ct in groupBoxParamSet.Controls)
				{
					ct.Enabled = _abstractCamera.IsCameraOpened;
				}
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (_abstractCamera.TriggerMode == TriggerModes.Continuous)   
			{
				_abstractCamera.TriggerMode = TriggerModes.Unknow;
				_abstractCamera.TriggerConfiguration();
				if (_abstractCamera.IsAcqiring)
				{
					_abstractCamera.IsAcqiring = false;
				}
			}
			_abstractCamera.EventOnGetImage -= new DelegateOnGetImage(ImageDataEnqueue);
			_formTimer.Stop();
			base.OnClosing(e);
		}

        public void ImageDataEnqueue(Bitmap bitmap)
        {
			try
            {
				DataEnqueue(bitmap);
				_pickNum++;
			}
            catch(Exception ex)
            {
            }
        }

		#region 队列操作
		/// <summary>
		/// 入队
		/// </summary>
		public void DataEnqueue(Bitmap image)
		{
			_dataQueue.Enqueue(image);
		}

		/// <summary>
		/// 定时执行出队操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ExecuteTask(object sender, EventArgs e)
		{
			DataDequeue();
		}

		/// <summary>
		/// 出队
		/// </summary>
		public void DataDequeue()
		{
			if (_dataQueue.Count > 0)
			{
				bool dequeueSuccesful = false;
				bool peekSuccesful = false;
				Bitmap workItem;

				peekSuccesful = _dataQueue.TryPeek(out workItem);

				if (peekSuccesful)
				{
					for (int i = 0; i < _dataQueue.Count; i++)
					{
						dequeueSuccesful = _dataQueue.TryDequeue(out workItem);//出队
					}
					_image = (Bitmap)workItem.Clone();
					FormRefresh();
				}
			}
			else
			{
				//Show wait status
			}
		}
		#endregion

		protected void comboBoxCameraName_SelectedIndexChanged(object sender, EventArgs e)
		{
			_abstractCamera.Name = comboBoxCameraName.Text;
			_abstractCamera.InitCameraProperty();
			textBoxCameraID.Text = _abstractCamera.Id;
			textBoxCameraIP.Text = _abstractCamera.Ip;
			textBoxMacAddress.Text = _abstractCamera.Mac;
		}

		protected void buttonCameraNameRefresh_Click(object sender, EventArgs e)
		{
			_abstractCamera.InitCameraProperty();
			comboBoxCameraName.Items.Clear();
			if (_abstractCamera != null)
			{
				if (_abstractCamera.Name != null)
				{
					comboBoxCameraName.Items.Add(_abstractCamera.Name);
					comboBoxCameraName.SelectedIndex = 0;
				}
			}

			for (int i = 0; i < _abstractCamera.CamerasList.Count; i++)
			{
				if (!comboBoxCameraName.Items.Contains(_abstractCamera.CamerasList[i].Name))
					comboBoxCameraName.Items.Add(_abstractCamera.CamerasList[i].Name);
			}
		}

		protected void buttonConnect_Click(object sender, EventArgs e)
		{
			_abstractCamera.OpenCamera();
			_abstractCamera.OpenStream();
			SetParamsControlShow();
		}

		protected void buttonClose_Click(object sender, EventArgs e)
		{
			if (_abstractCamera.IsAcqiring)
			{
				buttonAcquisition_Click(null, null);
				Thread.Sleep(20);
			}
			_abstractCamera.CloseStream();
			_abstractCamera.CloseCamera();
			Thread.Sleep(20);
			SetParamsControlShow();
		}

		protected void buttonAcquisition_Click(object sender, EventArgs e)
		{
			try
			{
				if (_abstractCamera.TriggerMode == TriggerModes.Continuous || _abstractCamera.TriggerMode == TriggerModes.HardWare)
				{
					if (!_abstractCamera.IsAcqiring)
					{
						_abstractCamera.AcquisitionContinous(true);
						_abstractCamera.IsAcqiring = true;
						buttonAcquisition.Text = "停止采集";
						comboBoxTriggerMode.Enabled = false;
					}
					else
					{
						_abstractCamera.AcquisitionContinous(false);
						_abstractCamera.IsAcqiring = false;
						buttonAcquisition.Text = "采集图像";
						comboBoxTriggerMode.Enabled = true;
					}
				}
				else if (_abstractCamera.TriggerMode == TriggerModes.Unknow) 
				{
					if (_abstractCamera.IsAcqiring)
					{
						_abstractCamera.AcquisitionContinous(false);
						_abstractCamera.IsAcqiring = false;
						buttonAcquisition.Text = "采集图像";
						comboBoxTriggerMode.Enabled = true;
					}
					else
					{
						_abstractCamera.Acquisition();
					}
				}
				SetParamsControlShow();
			}
			catch(Exception exception)
			{
			}
		}

		protected void buttonSaveImage_Click(object sender, EventArgs e)
		{
			string date = DateTime.Now.ToString("yyyy-MM-dd");
			string time = DateTime.Now.ToLongTimeString().ToString();
			time = time.Replace(":", "-");
			string name = @"\" + date + "-" + time + ".bmp";
			if (_abstractCamera.ImageCameraOut != null)
			{
				if (Directory.Exists(_abstractCamera.ImageSavePath))
				{
					_abstractCamera.ImageCameraOut.Save(_abstractCamera.ImageSavePath + name);
				}
				else
				{
				}
			}
		}

		protected void buttonParamSet_Click(object sender, EventArgs e)
		{
			try
			{
				var form = new CameraControlForm(_module);
				form.Show();
			}
			catch(Exception exception)
			{
				MessageBox.Show("打开设置失败，请关闭相机重新打开 " + exception.Message);
				return;
			}
		}

		private void comboBoxTriggerMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (null == _abstractCamera) return;
			try
			{
				ComboBox comboBox = (ComboBox)sender;
				switch (comboBox.SelectedIndex)
				{
					case 0:
						_abstractCamera.TriggerMode = TriggerModes.Unknow;
						_abstractCamera.TriggerSource = TriggerSources.Software;
						break;
					case 1:
						_abstractCamera.TriggerMode = TriggerModes.Continuous;
						_abstractCamera.TriggerSource = TriggerSources.Software;
						break;
					case 2:
						_abstractCamera.TriggerMode = TriggerModes.HardWare;
						_abstractCamera.TriggerSource = TriggerSources.Line0;
						break;
					default:
						break;
				}
				if (_abstractCamera.IsCameraOpened) 
				{
					if (_abstractCamera.TriggerMode == TriggerModes.Continuous)
					{
						_abstractCamera.CloseStream();
					}
					_abstractCamera.TriggerConfiguration();
				}
			}
			catch(Exception ex)
			{
			}
		}

		protected void buttonImagePath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folder = new FolderBrowserDialog();
			folder.Description = "选择图像存放目录";
			folder.RootFolder = Environment.SpecialFolder.MyComputer;
			folder.SelectedPath = Application.StartupPath + "\\Images";
			if (folder.ShowDialog() == DialogResult.OK)
			{
				_abstractCamera.ImageSavePath = folder.SelectedPath;
				textBoxImagePath.Text = _abstractCamera.ImageSavePath;
			}
		}

		private void numericUpDownQueueImageNum_ValueChanged(object sender, EventArgs e)
		{
			_abstractCamera.QueueImageNum = (int)((NumericUpDown)sender).Value;
		}

		private void buttonCleanQueue_Click(object sender, EventArgs e)
		{
			if (!_abstractCamera.ImageCameraQueue.IsEmpty)
			{
				Bitmap bitmap;
				for (int i = 0; i < _abstractCamera.ImageCameraQueue.Count; i++)
				{
					_abstractCamera.ImageCameraQueue.TryDequeue(out bitmap);
				}
			}
			if (!_abstractCamera.ImageCameraQueue.IsEmpty)
			{
				buttonCleanQueue_Click(null, null);
			}
			FormRefresh();
		}
	}
}
