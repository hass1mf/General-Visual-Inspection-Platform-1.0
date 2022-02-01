namespace AqCameraFactory
{
	partial class AbstractSetForm
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
		protected void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.buttonCameraNameRefresh = new System.Windows.Forms.Button();
			this.comboBoxCameraName = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxCameraID = new System.Windows.Forms.TextBox();
			this.textBoxMacAddress = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.buttonImagePath = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxCameraIP = new System.Windows.Forms.TextBox();
			this.textBoxImagePath = new System.Windows.Forms.TextBox();
			this.groupBoxParamSet = new System.Windows.Forms.GroupBox();
			this.numericUpDownQueueImageNum = new System.Windows.Forms.NumericUpDown();
			this.buttonCleanQueue = new System.Windows.Forms.Button();
			this.textBoxCurrentQueueNum = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxTriggerMode = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonParamSet = new System.Windows.Forms.Button();
			this.buttonSaveImage = new System.Windows.Forms.Button();
			this.buttonAcquisition = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.panelAcquisitionCtrl = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.aqDisplay = new AqVision.Controls.AqDisplay();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusPickNum = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusShowNum = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox1.SuspendLayout();
			this.groupBoxParamSet.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownQueueImageNum)).BeginInit();
			this.panelAcquisitionCtrl.SuspendLayout();
			this.panel1.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonCameraNameRefresh
			// 
			this.buttonCameraNameRefresh.Location = new System.Drawing.Point(240, 31);
			this.buttonCameraNameRefresh.Name = "buttonCameraNameRefresh";
			this.buttonCameraNameRefresh.Size = new System.Drawing.Size(37, 21);
			this.buttonCameraNameRefresh.TabIndex = 21;
			this.buttonCameraNameRefresh.Text = "刷新";
			this.buttonCameraNameRefresh.UseVisualStyleBackColor = true;
			this.buttonCameraNameRefresh.Click += new System.EventHandler(this.buttonCameraNameRefresh_Click);
			// 
			// comboBoxCameraName
			// 
			this.comboBoxCameraName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCameraName.FormattingEnabled = true;
			this.comboBoxCameraName.Location = new System.Drawing.Point(86, 31);
			this.comboBoxCameraName.Name = "comboBoxCameraName";
			this.comboBoxCameraName.Size = new System.Drawing.Size(154, 20);
			this.comboBoxCameraName.TabIndex = 6;
			this.comboBoxCameraName.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameraName_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label9.Location = new System.Drawing.Point(30, 139);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 12);
			this.label9.TabIndex = 5;
			this.label9.Text = "MAC地址";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label7.Location = new System.Drawing.Point(30, 105);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 12);
			this.label7.TabIndex = 5;
			this.label7.Text = "相机IP";
			// 
			// textBoxCameraID
			// 
			this.textBoxCameraID.Location = new System.Drawing.Point(86, 65);
			this.textBoxCameraID.Name = "textBoxCameraID";
			this.textBoxCameraID.ReadOnly = true;
			this.textBoxCameraID.Size = new System.Drawing.Size(154, 21);
			this.textBoxCameraID.TabIndex = 1;
			// 
			// textBoxMacAddress
			// 
			this.textBoxMacAddress.Location = new System.Drawing.Point(86, 135);
			this.textBoxMacAddress.Name = "textBoxMacAddress";
			this.textBoxMacAddress.ReadOnly = true;
			this.textBoxMacAddress.Size = new System.Drawing.Size(154, 21);
			this.textBoxMacAddress.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(30, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "相机ID";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label14.Location = new System.Drawing.Point(30, 34);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(53, 12);
			this.label14.TabIndex = 0;
			this.label14.Text = "相机名称";
			// 
			// buttonImagePath
			// 
			this.buttonImagePath.Location = new System.Drawing.Point(240, 171);
			this.buttonImagePath.Name = "buttonImagePath";
			this.buttonImagePath.Size = new System.Drawing.Size(37, 21);
			this.buttonImagePath.TabIndex = 20;
			this.buttonImagePath.Text = "...";
			this.buttonImagePath.UseVisualStyleBackColor = true;
			this.buttonImagePath.Click += new System.EventHandler(this.buttonImagePath_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label5.Location = new System.Drawing.Point(26, 173);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 12);
			this.label5.TabIndex = 15;
			this.label5.Text = "存图路径";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonCameraNameRefresh);
			this.groupBox1.Controls.Add(this.comboBoxCameraName);
			this.groupBox1.Controls.Add(this.buttonImagePath);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textBoxCameraID);
			this.groupBox1.Controls.Add(this.textBoxMacAddress);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxCameraIP);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.textBoxImagePath);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(5, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(311, 205);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "相机信息";
			// 
			// textBoxCameraIP
			// 
			this.textBoxCameraIP.Location = new System.Drawing.Point(86, 101);
			this.textBoxCameraIP.Name = "textBoxCameraIP";
			this.textBoxCameraIP.ReadOnly = true;
			this.textBoxCameraIP.Size = new System.Drawing.Size(154, 21);
			this.textBoxCameraIP.TabIndex = 2;
			// 
			// textBoxImagePath
			// 
			this.textBoxImagePath.Location = new System.Drawing.Point(86, 170);
			this.textBoxImagePath.Name = "textBoxImagePath";
			this.textBoxImagePath.Size = new System.Drawing.Size(154, 21);
			this.textBoxImagePath.TabIndex = 17;
			// 
			// groupBoxParamSet
			// 
			this.groupBoxParamSet.Controls.Add(this.numericUpDownQueueImageNum);
			this.groupBoxParamSet.Controls.Add(this.buttonCleanQueue);
			this.groupBoxParamSet.Controls.Add(this.textBoxCurrentQueueNum);
			this.groupBoxParamSet.Controls.Add(this.label1);
			this.groupBoxParamSet.Controls.Add(this.comboBoxTriggerMode);
			this.groupBoxParamSet.Controls.Add(this.label3);
			this.groupBoxParamSet.Controls.Add(this.label4);
			this.groupBoxParamSet.Controls.Add(this.buttonParamSet);
			this.groupBoxParamSet.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxParamSet.Location = new System.Drawing.Point(5, 210);
			this.groupBoxParamSet.Name = "groupBoxParamSet";
			this.groupBoxParamSet.Size = new System.Drawing.Size(311, 225);
			this.groupBoxParamSet.TabIndex = 23;
			this.groupBoxParamSet.TabStop = false;
			this.groupBoxParamSet.Text = "采集控制";
			// 
			// numericUpDownQueueImageNum
			// 
			this.numericUpDownQueueImageNum.Location = new System.Drawing.Point(128, 139);
			this.numericUpDownQueueImageNum.Name = "numericUpDownQueueImageNum";
			this.numericUpDownQueueImageNum.Size = new System.Drawing.Size(100, 21);
			this.numericUpDownQueueImageNum.TabIndex = 42;
			this.numericUpDownQueueImageNum.ValueChanged += new System.EventHandler(this.numericUpDownQueueImageNum_ValueChanged);
			// 
			// buttonCleanQueue
			// 
			this.buttonCleanQueue.Location = new System.Drawing.Point(229, 183);
			this.buttonCleanQueue.Name = "buttonCleanQueue";
			this.buttonCleanQueue.Size = new System.Drawing.Size(42, 23);
			this.buttonCleanQueue.TabIndex = 41;
			this.buttonCleanQueue.Text = "清理";
			this.buttonCleanQueue.UseVisualStyleBackColor = true;
			this.buttonCleanQueue.Click += new System.EventHandler(this.buttonCleanQueue_Click);
			// 
			// textBoxCurrentQueueNum
			// 
			this.textBoxCurrentQueueNum.Location = new System.Drawing.Point(128, 184);
			this.textBoxCurrentQueueNum.Name = "textBoxCurrentQueueNum";
			this.textBoxCurrentQueueNum.ReadOnly = true;
			this.textBoxCurrentQueueNum.Size = new System.Drawing.Size(100, 21);
			this.textBoxCurrentQueueNum.TabIndex = 40;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 188);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 12);
			this.label1.TabIndex = 39;
			this.label1.Text = "当前队列数量:";
			// 
			// comboBoxTriggerMode
			// 
			this.comboBoxTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTriggerMode.FormattingEnabled = true;
			this.comboBoxTriggerMode.Items.AddRange(new object[] {
            "单帧采集",
            "连续采集",
            "硬件触发"});
			this.comboBoxTriggerMode.Location = new System.Drawing.Point(128, 85);
			this.comboBoxTriggerMode.Name = "comboBoxTriggerMode";
			this.comboBoxTriggerMode.Size = new System.Drawing.Size(100, 20);
			this.comboBoxTriggerMode.TabIndex = 26;
			this.comboBoxTriggerMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxTriggerMode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(42, 141);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 12);
			this.label3.TabIndex = 23;
			this.label3.Text = "队列数量设置:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(67, 89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 12);
			this.label4.TabIndex = 23;
			this.label4.Text = "触发模式:";
			// 
			// buttonParamSet
			// 
			this.buttonParamSet.Location = new System.Drawing.Point(69, 30);
			this.buttonParamSet.Name = "buttonParamSet";
			this.buttonParamSet.Size = new System.Drawing.Size(162, 39);
			this.buttonParamSet.TabIndex = 20;
			this.buttonParamSet.Text = "参数设置";
			this.buttonParamSet.UseVisualStyleBackColor = true;
			this.buttonParamSet.Click += new System.EventHandler(this.buttonParamSet_Click);
			// 
			// buttonSaveImage
			// 
			this.buttonSaveImage.Location = new System.Drawing.Point(171, 80);
			this.buttonSaveImage.Name = "buttonSaveImage";
			this.buttonSaveImage.Size = new System.Drawing.Size(95, 45);
			this.buttonSaveImage.TabIndex = 17;
			this.buttonSaveImage.Text = "保存图像";
			this.buttonSaveImage.UseVisualStyleBackColor = true;
			this.buttonSaveImage.Click += new System.EventHandler(this.buttonSaveImage_Click);
			// 
			// buttonAcquisition
			// 
			this.buttonAcquisition.Location = new System.Drawing.Point(45, 80);
			this.buttonAcquisition.Name = "buttonAcquisition";
			this.buttonAcquisition.Size = new System.Drawing.Size(95, 45);
			this.buttonAcquisition.TabIndex = 18;
			this.buttonAcquisition.Text = "采集图像";
			this.buttonAcquisition.UseVisualStyleBackColor = true;
			this.buttonAcquisition.Click += new System.EventHandler(this.buttonAcquisition_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(171, 19);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(95, 45);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "关闭相机";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(43, 19);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(95, 45);
			this.buttonConnect.TabIndex = 3;
			this.buttonConnect.Text = "连接相机";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// panelAcquisitionCtrl
			// 
			this.panelAcquisitionCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelAcquisitionCtrl.Controls.Add(this.buttonSaveImage);
			this.panelAcquisitionCtrl.Controls.Add(this.buttonAcquisition);
			this.panelAcquisitionCtrl.Controls.Add(this.buttonClose);
			this.panelAcquisitionCtrl.Controls.Add(this.buttonConnect);
			this.panelAcquisitionCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelAcquisitionCtrl.Location = new System.Drawing.Point(5, 435);
			this.panelAcquisitionCtrl.Name = "panelAcquisitionCtrl";
			this.panelAcquisitionCtrl.Size = new System.Drawing.Size(311, 160);
			this.panelAcquisitionCtrl.TabIndex = 24;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panelAcquisitionCtrl);
			this.panel1.Controls.Add(this.groupBoxParamSet);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.aqDisplay);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(1044, 600);
			this.panel1.TabIndex = 23;
			// 
			// aqDisplay
			// 
			this.aqDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.aqDisplay.Dock = System.Windows.Forms.DockStyle.Right;
			this.aqDisplay.Image = null;
			this.aqDisplay.IsBeginAddImageMask = false;
			this.aqDisplay.IsSaveResultImage = false;
			this.aqDisplay.IsScrollBar = true;
			this.aqDisplay.IsShowCenterLine = false;
			this.aqDisplay.IsShowStatusBar = false;
			this.aqDisplay.IsUsedEraser = false;
			this.aqDisplay.Location = new System.Drawing.Point(316, 5);
			this.aqDisplay.Margin = new System.Windows.Forms.Padding(2);
			this.aqDisplay.Name = "aqDisplay";
			this.aqDisplay.Radius = 1F;
			this.aqDisplay.Size = new System.Drawing.Size(723, 590);
			this.aqDisplay.TabIndex = 25;
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusPickNum,
            this.toolStripStatusLabel4,
            this.toolStripStatusShowNum,
            this.toolStripStatusLabel2});
			this.statusStrip.Location = new System.Drawing.Point(0, 572);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1044, 28);
			this.statusStrip.TabIndex = 25;
			this.statusStrip.Text = "statusStrip2";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Black;
			this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 23);
			this.toolStripStatusLabel1.Text = "采图数量：";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripStatusPickNum
			// 
			this.toolStripStatusPickNum.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.toolStripStatusPickNum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusPickNum.ForeColor = System.Drawing.Color.Blue;
			this.toolStripStatusPickNum.Name = "toolStripStatusPickNum";
			this.toolStripStatusPickNum.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this.toolStripStatusPickNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.toolStripStatusPickNum.Size = new System.Drawing.Size(284, 23);
			this.toolStripStatusPickNum.Spring = true;
			this.toolStripStatusPickNum.Text = "###";
			this.toolStripStatusPickNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(68, 23);
			this.toolStripStatusLabel4.Text = "显示数量：";
			// 
			// toolStripStatusShowNum
			// 
			this.toolStripStatusShowNum.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.toolStripStatusShowNum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusShowNum.ForeColor = System.Drawing.Color.Blue;
			this.toolStripStatusShowNum.Name = "toolStripStatusShowNum";
			this.toolStripStatusShowNum.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this.toolStripStatusShowNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.toolStripStatusShowNum.Size = new System.Drawing.Size(284, 23);
			this.toolStripStatusShowNum.Spring = true;
			this.toolStripStatusShowNum.Text = "###";
			this.toolStripStatusShowNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(5, 3, 10, 3);
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(284, 23);
			this.toolStripStatusLabel2.Spring = true;
			this.toolStripStatusLabel2.Text = "Designed By Aqrose     ";
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// AbstractSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1044, 600);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.panel1);
			this.Name = "AbstractSetForm";
			this.Text = "AbstractSetForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBoxParamSet.ResumeLayout(false);
			this.groupBoxParamSet.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownQueueImageNum)).EndInit();
			this.panelAcquisitionCtrl.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.Button buttonCameraNameRefresh;
		protected System.Windows.Forms.ComboBox comboBoxCameraName;
		protected System.Windows.Forms.Label label9;
		protected System.Windows.Forms.Label label7;
		protected System.Windows.Forms.TextBox textBoxCameraID;
		protected System.Windows.Forms.TextBox textBoxMacAddress;
		protected System.Windows.Forms.Label label2;
		protected System.Windows.Forms.Label label14;
		protected System.Windows.Forms.Button buttonImagePath;
		protected System.Windows.Forms.Label label5;
		protected System.Windows.Forms.GroupBox groupBox1;
		protected System.Windows.Forms.TextBox textBoxCameraIP;
		protected System.Windows.Forms.TextBox textBoxImagePath;
		protected System.Windows.Forms.GroupBox groupBoxParamSet;
		protected System.Windows.Forms.Button buttonSaveImage;
		protected System.Windows.Forms.Button buttonAcquisition;
		protected System.Windows.Forms.Button buttonClose;
		protected System.Windows.Forms.Button buttonConnect;
		protected System.Windows.Forms.Panel panelAcquisitionCtrl;
		protected System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.Label label4;
		protected System.Windows.Forms.Button buttonParamSet;
		private System.Windows.Forms.ComboBox comboBoxTriggerMode;
		private System.Windows.Forms.Button buttonCleanQueue;
		private System.Windows.Forms.TextBox textBoxCurrentQueueNum;
		private System.Windows.Forms.Label label1;
		protected System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownQueueImageNum;
		protected AqVision.Controls.AqDisplay aqDisplay;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusPickNum;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusShowNum;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
	}
}