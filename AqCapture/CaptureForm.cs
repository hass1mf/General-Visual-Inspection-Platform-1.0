using Aqrose.Framework.Utility.WindowConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AqCapture
{
	public partial class CaptureForm : Form
    {
        AqCapture _aqCapture;

        public CaptureForm(AqCapture capture)
        {
            _aqCapture = capture;
            InitializeComponent();
            InitializationControlShow();
        }

        private void InitializationControlShow()
        {
			aqDisplay.IsShowStatusBar = true;
			//Window name
			InitializeComboxWndName(comboBoxWindowName, _aqCapture.DisplayWindowName);
			//*Select mode
			switch (_aqCapture.Mode)
            {
                case AcquisitionMode.FromCamera:
                    radioButtonCamera.Checked = true;
                    break;
                case AcquisitionMode.FromFile:
                    radioButtonLocalFile.Checked=true;
                    break;
                case AcquisitionMode.FromFolder:
                    radioButtonLocalFolder.Checked = true;
                    break;
                default:
                    radioButtonCamera.Checked = true;
                    break;
            }
			if (_aqCapture.AbstractCamera != null)
			{
				trackBarExposureTime.Enabled = true;
				textBoxExposureTime.Enabled = true;
				trackBarExposureTime.Maximum = Convert.ToInt32(_aqCapture.AbstractCamera.ExposureTimeMax);
				trackBarExposureTime.Minimum = Convert.ToInt32(_aqCapture.AbstractCamera.ExposureTimeMin);
				if (_aqCapture.ExposureTime < _aqCapture.AbstractCamera.ExposureTimeMax &&
					_aqCapture.ExposureTime > _aqCapture.AbstractCamera.ExposureTimeMin)
				{
					trackBarExposureTime.Value = Convert.ToInt32(_aqCapture.ExposureTime);
					textBoxExposureTime.Text = Convert.ToString(_aqCapture.ExposureTime);
				}
				else if (_aqCapture.AbstractCamera.ExposureTime < _aqCapture.AbstractCamera.ExposureTimeMax &&
					_aqCapture.AbstractCamera.ExposureTime < _aqCapture.AbstractCamera.ExposureTimeMin) 
				{
					trackBarExposureTime.Value = Convert.ToInt32(_aqCapture.AbstractCamera.ExposureTime);
					textBoxExposureTime.Text = Convert.ToString(_aqCapture.AbstractCamera.ExposureTime);
				}
			}
			else
			{
				trackBarExposureTime.Enabled = false;
				textBoxExposureTime.Enabled = false;
			}
			if (_aqCapture != null)
			{
				if (_aqCapture.AbstractCamera != null)
				{
					if (_aqCapture.AbstractCamera != null)
					{
						textBoxCamera.Text = _aqCapture.AbstractCamera.Name;
					}
				}
			}
			textBoxFile.Text = _aqCapture.FileParam.FilePath;
			textBoxFolder.Text = _aqCapture.FileParam.FolderPath;


            var mode = DataSourceUpdateMode.OnPropertyChanged | DataSourceUpdateMode.OnValidation;
            this.checkBoxIsDisplay.DataBindings.Add(new Binding("Checked", _aqCapture, "IsDisplay", true, mode));
            this.checkBoxRotate.DataBindings.Add(new Binding("Checked", _aqCapture, "IsRotate", true, mode));
            this.checkBoxFlipX.DataBindings.Add(new Binding("Checked", _aqCapture, "IsFlipX", true, mode));
            this.checkBoxFlipY.DataBindings.Add(new Binding("Checked", _aqCapture, "IsFlipY", true, mode));

            FormRefresh();
		}

		private void InitializeComboxWndName(ComboBox comboBox, string windowName/*=""*/)
		{
			bool isLessWindow = true;
			List<string> windowNameList;
			WindowNum.Instance().GetWindowNameList(out windowNameList);

			comboBox.Items.Clear();
			foreach (var obj in windowNameList)
			{
				comboBox.Items.Add(obj);
				if (obj == windowName)
				{
					isLessWindow = false;
					comboBox.Text = windowName;
				}
			}
			if (isLessWindow)
			{
				comboBox.SelectedIndex = 0;
			}

			return;
		}

		public void FormRefresh()
		{
			this.aqDisplay.Image = _aqCapture.ImageOut;
			this.aqDisplay.FitToScreen();
			this.aqDisplay.Update(true);
		}

		#region 选择图像采集源
		#region From Camera
		private void radioButtonCamera_CheckedChanged(object sender, EventArgs e)
        {
            _aqCapture.Mode = AcquisitionMode.FromCamera;
            panelCamera.Enabled = true;
            panelCamerapanelLocalFile.Enabled = false;
            panelLocalFolder.Enabled = false;
        }

		private void trackBarExposureTime_Scroll(object sender, EventArgs e)
		{
			if (_aqCapture.AbstractCamera == null) return;
			_aqCapture.ExposureTime = trackBarExposureTime.Value;
			_aqCapture.AbstractCamera.ExposureTime = trackBarExposureTime.Value;
			textBoxExposureTime.Text = Convert.ToString(_aqCapture.AbstractCamera.ExposureTime);
			_aqCapture.AbstractCamera.SetExposureTime();
		}

		private void textBoxExposureTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_aqCapture.AbstractCamera == null) return;
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
					obj.Text = Convert.ToString(_aqCapture.AbstractCamera.ExposureTime);
					return;
				}
				_aqCapture.ExposureTime = Convert.ToInt64(textBoxExposureTime.Text);
				_aqCapture.AbstractCamera.ExposureTime = Convert.ToInt64(textBoxExposureTime.Text);
				trackBarExposureTime.Value = (int)_aqCapture.AbstractCamera.ExposureTime;
				_aqCapture.AbstractCamera.SetExposureTime();
			}
		}

		private void textBoxExposureTime_Leave(object sender, EventArgs e)
		{
			if (_aqCapture.AbstractCamera == null) return;
			TextBox obj = (TextBox)sender;
			//正则表达式限定输入格式
			//string exp = @"^\d+(\.\d+)?$";//非负浮点数
			string exp = @"^\d+$";//非负整数
			Regex regex = new Regex(exp);
			bool matched = regex.IsMatch(obj.Text);
			if (!matched)
			{
				obj.Text = Convert.ToString(_aqCapture.AbstractCamera.ExposureTime);
				return;
			}
			_aqCapture.ExposureTime = Convert.ToInt64(textBoxExposureTime.Text);
			_aqCapture.AbstractCamera.ExposureTime = Convert.ToInt64(textBoxExposureTime.Text);
			trackBarExposureTime.Value = (int)_aqCapture.AbstractCamera.ExposureTime;
			_aqCapture.AbstractCamera.SetExposureTime();
		}
		#endregion

		#region From File
		private void radioButtonLocalFile_CheckedChanged(object sender, EventArgs e)
        {
            _aqCapture.Mode = AcquisitionMode.FromFile;
            panelCamera.Enabled = false;
            panelCamerapanelLocalFile.Enabled = true;
            panelLocalFolder.Enabled = false;
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.InitialDirectory = Application.StartupPath + "\\Images";
            dialog.Title = "选择输入文件";
            dialog.Filter = "图像文件(*jpg *png *bmp)|*.jpg;*.png;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
				string relativePath = GetRelativePath(dialog.FileName, Application.StartupPath);
				_aqCapture.FileParam.FilePath = relativePath;
				textBoxFile.Text = _aqCapture.FileParam.FilePath;
			}
			Directory.SetCurrentDirectory(Application.StartupPath);
		}
		#endregion
		#region 文件夹
		private void radioButtonLocalFolder_CheckedChanged(object sender, EventArgs e)
        {
            _aqCapture.Mode = AcquisitionMode.FromFolder;
            panelCamera.Enabled = false;
            panelCamerapanelLocalFile.Enabled = false;
            panelLocalFolder.Enabled = true;
        }
        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择所有文件存放目录";
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            folder.SelectedPath = Application.StartupPath + "\\Images";
            if (folder.ShowDialog() == DialogResult.OK)
            {
				string relativePath = GetRelativePath(folder.SelectedPath, Application.StartupPath);
				_aqCapture.FileParam.FolderPath = relativePath;
				_aqCapture.FileParam.UpdateFilesUnderFolder();
				_aqCapture.FolderFileIndex = 0;
				textBoxFolder.Text = _aqCapture.FileParam.FolderPath;
			}
		}
        #endregion

        #endregion

        private void comboBoxWindowName_SelectedIndexChanged(object sender, EventArgs e)
		{
			_aqCapture.DisplayWindowName = ((ComboBox)sender).Text;
		}

		private void buttonRun_Click(object sender, EventArgs e)
		{
			_aqCapture.Run();
			FormRefresh();
		}

		#region 绝对路径转相对路径
		//使用中
		public static string GetRelativePath(string filespec, string folder)
		{
			// 可以将"\\"替换为Path.DirectorySeparatorChar
			const string directorySeparatorChar = "\\";
			Uri pathUri = new Uri(filespec);

			if (!folder.EndsWith(directorySeparatorChar))
			{
				folder += directorySeparatorChar;
			}
			Uri folderUri = new Uri(folder);
			return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace("/", directorySeparatorChar));
		}

		//暂未使用
		string RelativePath(string absolutePath, string relativeTo)
		{
			//from - www.cnphp6.com

			string[] absoluteDirectories = absolutePath.Split('\\');
			string[] relativeDirectories = relativeTo.Split('\\');

			//Get the shortest of the two paths
			int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

			//Use to determine where in the loop we exited
			int lastCommonRoot = -1;
			int index;

			//Find common root
			for (index = 0; index < length; index++)
				if (absoluteDirectories[index] == relativeDirectories[index])
					lastCommonRoot = index;
				else
					break;

			//If we didn't find a common prefix then throw
			if (lastCommonRoot == -1)
				throw new ArgumentException("Paths do not have a common base");

			//Build up the relative path
			StringBuilder relativePath = new StringBuilder();

			//Add on the ..
			for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
				if (absoluteDirectories[index].Length > 0)
					relativePath.Append("..\\");

			//Add on the folders
			for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
				relativePath.Append(relativeDirectories[index] + "\\");
			relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

			return relativePath.ToString();
		}
        #endregion

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            _aqCapture.ImageRotateFlip();
            FormRefresh();
        }
    }
}
