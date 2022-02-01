namespace AqCapture
{
    partial class CaptureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.panelLocalFolder = new System.Windows.Forms.Panel();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.panelCamerapanelLocalFile = new System.Windows.Forms.Panel();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.panelCamera = new System.Windows.Forms.Panel();
            this.textBoxExposureTime = new System.Windows.Forms.TextBox();
            this.trackBarExposureTime = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCamera = new System.Windows.Forms.TextBox();
            this.radioButtonCamera = new System.Windows.Forms.RadioButton();
            this.radioButtonLocalFolder = new System.Windows.Forms.RadioButton();
            this.radioButtonLocalFile = new System.Windows.Forms.RadioButton();
            this.buttonRun = new System.Windows.Forms.Button();
            this.aqDisplay = new AqVision.Controls.AqDisplay();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxIsDisplay = new System.Windows.Forms.CheckBox();
            this.comboBoxWindowName = new System.Windows.Forms.ComboBox();
            this.checkBoxRotate = new System.Windows.Forms.CheckBox();
            this.checkBoxFlipX = new System.Windows.Forms.CheckBox();
            this.checkBoxFlipY = new System.Windows.Forms.CheckBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.panelLocalFolder.SuspendLayout();
            this.panelCamerapanelLocalFile.SuspendLayout();
            this.panelCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposureTime)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.panelLocalFolder);
            this.groupBox.Controls.Add(this.panelCamerapanelLocalFile);
            this.groupBox.Controls.Add(this.panelCamera);
            this.groupBox.Controls.Add(this.radioButtonCamera);
            this.groupBox.Controls.Add(this.radioButtonLocalFolder);
            this.groupBox.Controls.Add(this.radioButtonLocalFile);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(433, 359);
            this.groupBox.TabIndex = 14;
            this.groupBox.TabStop = false;
            // 
            // panelLocalFolder
            // 
            this.panelLocalFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLocalFolder.Controls.Add(this.textBoxFolder);
            this.panelLocalFolder.Controls.Add(this.buttonSelectFolder);
            this.panelLocalFolder.Location = new System.Drawing.Point(13, 298);
            this.panelLocalFolder.Name = "panelLocalFolder";
            this.panelLocalFolder.Size = new System.Drawing.Size(410, 47);
            this.panelLocalFolder.TabIndex = 8;
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Location = new System.Drawing.Point(3, 13);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.ReadOnly = true;
            this.textBoxFolder.Size = new System.Drawing.Size(369, 21);
            this.textBoxFolder.TabIndex = 13;
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Font = new System.Drawing.Font("宋体", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSelectFolder.Location = new System.Drawing.Point(372, 11);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(25, 24);
            this.buttonSelectFolder.TabIndex = 10;
            this.buttonSelectFolder.Text = "...";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // panelCamerapanelLocalFile
            // 
            this.panelCamerapanelLocalFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamerapanelLocalFile.Controls.Add(this.textBoxFile);
            this.panelCamerapanelLocalFile.Controls.Add(this.buttonSelectFile);
            this.panelCamerapanelLocalFile.Location = new System.Drawing.Point(13, 197);
            this.panelCamerapanelLocalFile.Name = "panelCamerapanelLocalFile";
            this.panelCamerapanelLocalFile.Size = new System.Drawing.Size(410, 57);
            this.panelCamerapanelLocalFile.TabIndex = 8;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(3, 17);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(369, 21);
            this.textBoxFile.TabIndex = 12;
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Font = new System.Drawing.Font("宋体", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSelectFile.Location = new System.Drawing.Point(372, 15);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(25, 24);
            this.buttonSelectFile.TabIndex = 10;
            this.buttonSelectFile.Text = "...";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // panelCamera
            // 
            this.panelCamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera.Controls.Add(this.textBoxExposureTime);
            this.panelCamera.Controls.Add(this.trackBarExposureTime);
            this.panelCamera.Controls.Add(this.label3);
            this.panelCamera.Controls.Add(this.textBoxCamera);
            this.panelCamera.Location = new System.Drawing.Point(13, 40);
            this.panelCamera.Name = "panelCamera";
            this.panelCamera.Size = new System.Drawing.Size(409, 120);
            this.panelCamera.TabIndex = 8;
            // 
            // textBoxExposureTime
            // 
            this.textBoxExposureTime.Location = new System.Drawing.Point(333, 68);
            this.textBoxExposureTime.Name = "textBoxExposureTime";
            this.textBoxExposureTime.Size = new System.Drawing.Size(42, 21);
            this.textBoxExposureTime.TabIndex = 37;
            this.textBoxExposureTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxExposureTime_KeyPress);
            this.textBoxExposureTime.Leave += new System.EventHandler(this.textBoxExposureTime_Leave);
            // 
            // trackBarExposureTime
            // 
            this.trackBarExposureTime.Location = new System.Drawing.Point(63, 58);
            this.trackBarExposureTime.Name = "trackBarExposureTime";
            this.trackBarExposureTime.Size = new System.Drawing.Size(264, 45);
            this.trackBarExposureTime.TabIndex = 36;
            this.trackBarExposureTime.TickFrequency = 20;
            this.trackBarExposureTime.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarExposureTime.Scroll += new System.EventHandler(this.trackBarExposureTime_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(11, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "曝光时间";
            // 
            // textBoxCamera
            // 
            this.textBoxCamera.Location = new System.Drawing.Point(3, 13);
            this.textBoxCamera.Name = "textBoxCamera";
            this.textBoxCamera.ReadOnly = true;
            this.textBoxCamera.Size = new System.Drawing.Size(382, 21);
            this.textBoxCamera.TabIndex = 11;
            // 
            // radioButtonCamera
            // 
            this.radioButtonCamera.AutoSize = true;
            this.radioButtonCamera.Location = new System.Drawing.Point(13, 20);
            this.radioButtonCamera.Name = "radioButtonCamera";
            this.radioButtonCamera.Size = new System.Drawing.Size(47, 16);
            this.radioButtonCamera.TabIndex = 8;
            this.radioButtonCamera.Text = "相机";
            this.radioButtonCamera.UseVisualStyleBackColor = true;
            this.radioButtonCamera.CheckedChanged += new System.EventHandler(this.radioButtonCamera_CheckedChanged);
            // 
            // radioButtonLocalFolder
            // 
            this.radioButtonLocalFolder.AutoSize = true;
            this.radioButtonLocalFolder.Location = new System.Drawing.Point(13, 276);
            this.radioButtonLocalFolder.Name = "radioButtonLocalFolder";
            this.radioButtonLocalFolder.Size = new System.Drawing.Size(59, 16);
            this.radioButtonLocalFolder.TabIndex = 8;
            this.radioButtonLocalFolder.Text = "文件夹";
            this.radioButtonLocalFolder.UseVisualStyleBackColor = true;
            this.radioButtonLocalFolder.CheckedChanged += new System.EventHandler(this.radioButtonLocalFolder_CheckedChanged);
            // 
            // radioButtonLocalFile
            // 
            this.radioButtonLocalFile.AutoSize = true;
            this.radioButtonLocalFile.Location = new System.Drawing.Point(13, 177);
            this.radioButtonLocalFile.Name = "radioButtonLocalFile";
            this.radioButtonLocalFile.Size = new System.Drawing.Size(47, 16);
            this.radioButtonLocalFile.TabIndex = 8;
            this.radioButtonLocalFile.Text = "文件";
            this.radioButtonLocalFile.UseVisualStyleBackColor = true;
            this.radioButtonLocalFile.CheckedChanged += new System.EventHandler(this.radioButtonLocalFile_CheckedChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(338, 436);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(96, 23);
            this.buttonRun.TabIndex = 15;
            this.buttonRun.Text = "采集图像";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // aqDisplay
            // 
            this.aqDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.aqDisplay.GroupName = "";
            this.aqDisplay.Image = null;
            this.aqDisplay.IsAddDynamicPoint = false;
            this.aqDisplay.IsBeginAddImageMask = false;
            this.aqDisplay.IsBeginDrawDynamicPolygon = false;
            this.aqDisplay.IsInteractiveFlag = true;
            this.aqDisplay.IsSaveResultImage = false;
            this.aqDisplay.IsScrollBar = true;
            this.aqDisplay.IsShowCenterLine = false;
            this.aqDisplay.IsShowStatusBar = false;
            this.aqDisplay.IsTransformRGB = false;
            this.aqDisplay.IsUsedEraser = false;
            this.aqDisplay.Location = new System.Drawing.Point(450, 17);
            this.aqDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.aqDisplay.Name = "aqDisplay";
            this.aqDisplay.OriginMaskImage = null;
            this.aqDisplay.Radius = 1F;
            this.aqDisplay.Size = new System.Drawing.Size(753, 462);
            this.aqDisplay.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 441);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "显示窗口名";
            // 
            // checkBoxIsDisplay
            // 
            this.checkBoxIsDisplay.AutoSize = true;
            this.checkBoxIsDisplay.Location = new System.Drawing.Point(224, 440);
            this.checkBoxIsDisplay.Name = "checkBoxIsDisplay";
            this.checkBoxIsDisplay.Size = new System.Drawing.Size(72, 16);
            this.checkBoxIsDisplay.TabIndex = 18;
            this.checkBoxIsDisplay.Text = "是否显示";
            this.checkBoxIsDisplay.UseVisualStyleBackColor = true;
            // 
            // comboBoxWindowName
            // 
            this.comboBoxWindowName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWindowName.FormattingEnabled = true;
            this.comboBoxWindowName.Location = new System.Drawing.Point(90, 437);
            this.comboBoxWindowName.Name = "comboBoxWindowName";
            this.comboBoxWindowName.Size = new System.Drawing.Size(108, 20);
            this.comboBoxWindowName.TabIndex = 19;
            this.comboBoxWindowName.SelectedIndexChanged += new System.EventHandler(this.comboBoxWindowName_SelectedIndexChanged);
            // 
            // checkBoxRotate
            // 
            this.checkBoxRotate.AutoSize = true;
            this.checkBoxRotate.Location = new System.Drawing.Point(25, 393);
            this.checkBoxRotate.Name = "checkBoxRotate";
            this.checkBoxRotate.Size = new System.Drawing.Size(96, 16);
            this.checkBoxRotate.TabIndex = 18;
            this.checkBoxRotate.Text = "是否90度旋转";
            this.checkBoxRotate.UseVisualStyleBackColor = true;
            // 
            // checkBoxFlipX
            // 
            this.checkBoxFlipX.AutoSize = true;
            this.checkBoxFlipX.Location = new System.Drawing.Point(127, 393);
            this.checkBoxFlipX.Name = "checkBoxFlipX";
            this.checkBoxFlipX.Size = new System.Drawing.Size(90, 16);
            this.checkBoxFlipX.TabIndex = 18;
            this.checkBoxFlipX.Text = "是否X轴翻转";
            this.checkBoxFlipX.UseVisualStyleBackColor = true;
            // 
            // checkBoxFlipY
            // 
            this.checkBoxFlipY.AutoSize = true;
            this.checkBoxFlipY.Location = new System.Drawing.Point(224, 393);
            this.checkBoxFlipY.Name = "checkBoxFlipY";
            this.checkBoxFlipY.Size = new System.Drawing.Size(90, 16);
            this.checkBoxFlipY.TabIndex = 18;
            this.checkBoxFlipY.Text = "是否Y轴翻转";
            this.checkBoxFlipY.UseVisualStyleBackColor = true;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(339, 386);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(96, 23);
            this.buttonRefresh.TabIndex = 15;
            this.buttonRefresh.Text = "刷新图像";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 490);
            this.Controls.Add(this.comboBoxWindowName);
            this.Controls.Add(this.checkBoxFlipY);
            this.Controls.Add(this.checkBoxFlipX);
            this.Controls.Add(this.checkBoxRotate);
            this.Controls.Add(this.checkBoxIsDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aqDisplay);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.groupBox);
            this.Name = "CaptureForm";
            this.Text = "采集设置";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.panelLocalFolder.ResumeLayout(false);
            this.panelLocalFolder.PerformLayout();
            this.panelCamerapanelLocalFile.ResumeLayout(false);
            this.panelCamerapanelLocalFile.PerformLayout();
            this.panelCamera.ResumeLayout(false);
            this.panelCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposureTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Panel panelLocalFolder;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.Panel panelCamerapanelLocalFile;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.Panel panelCamera;
        private System.Windows.Forms.RadioButton radioButtonCamera;
        private System.Windows.Forms.RadioButton radioButtonLocalFolder;
        private System.Windows.Forms.RadioButton radioButtonLocalFile;
		private System.Windows.Forms.Button buttonRun;
		private AqVision.Controls.AqDisplay aqDisplay;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.TextBox textBoxFile;
		private System.Windows.Forms.TextBox textBoxCamera;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxIsDisplay;
		private System.Windows.Forms.TrackBar trackBarExposureTime;
		protected System.Windows.Forms.Label label3;
		protected System.Windows.Forms.TextBox textBoxExposureTime;
		private System.Windows.Forms.ComboBox comboBoxWindowName;
        private System.Windows.Forms.CheckBox checkBoxRotate;
        private System.Windows.Forms.CheckBox checkBoxFlipX;
        private System.Windows.Forms.CheckBox checkBoxFlipY;
        private System.Windows.Forms.Button buttonRefresh;
    }
}