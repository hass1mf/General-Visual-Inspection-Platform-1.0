namespace AqCameraFactory
{
	partial class CameraControlForm
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
			this.textBoxExposureTime = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.trackBarExposureTime = new System.Windows.Forms.TrackBar();
			this.groupBoxImageFormat = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxImageOffsetYMax = new System.Windows.Forms.TextBox();
			this.textBoxImageHeightMax = new System.Windows.Forms.TextBox();
			this.textBoxImageOffsetXMax = new System.Windows.Forms.TextBox();
			this.textBoxImageWidthMax = new System.Windows.Forms.TextBox();
			this.textBoxImageOffsetYMin = new System.Windows.Forms.TextBox();
			this.textBoxImageHeightMin = new System.Windows.Forms.TextBox();
			this.textBoxImageOffsetXMin = new System.Windows.Forms.TextBox();
			this.textBoxImageWidthMin = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textBoxImageHeight = new System.Windows.Forms.TextBox();
			this.textBoxImageWidth = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxImageOffsetY = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxImageOffsetX = new System.Windows.Forms.TextBox();
			this.comboBoxBalanceSelector = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.numericUpDownBalanceRatio = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.trackBarExposureTime)).BeginInit();
			this.groupBoxImageFormat.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBalanceRatio)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxExposureTime
			// 
			this.textBoxExposureTime.Location = new System.Drawing.Point(276, 27);
			this.textBoxExposureTime.Name = "textBoxExposureTime";
			this.textBoxExposureTime.Size = new System.Drawing.Size(42, 21);
			this.textBoxExposureTime.TabIndex = 27;
			this.textBoxExposureTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxExposureTime_KeyPress);
			this.textBoxExposureTime.Leave += new System.EventHandler(this.textBoxExposureTime_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 26;
			this.label1.Text = "曝光时间";
			// 
			// trackBarExposureTime
			// 
			this.trackBarExposureTime.Location = new System.Drawing.Point(64, 15);
			this.trackBarExposureTime.Name = "trackBarExposureTime";
			this.trackBarExposureTime.Size = new System.Drawing.Size(206, 45);
			this.trackBarExposureTime.TabIndex = 33;
			this.trackBarExposureTime.TickFrequency = 10;
			this.trackBarExposureTime.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBarExposureTime.Scroll += new System.EventHandler(this.trackBarExposureTime_Scroll);
			// 
			// groupBoxImageFormat
			// 
			this.groupBoxImageFormat.Controls.Add(this.label3);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageOffsetYMax);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageHeightMax);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageOffsetXMax);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageWidthMax);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageOffsetYMin);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageHeightMin);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageOffsetXMin);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageWidthMin);
			this.groupBoxImageFormat.Controls.Add(this.label15);
			this.groupBoxImageFormat.Controls.Add(this.label14);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageHeight);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageWidth);
			this.groupBoxImageFormat.Controls.Add(this.label7);
			this.groupBoxImageFormat.Controls.Add(this.label2);
			this.groupBoxImageFormat.Controls.Add(this.label5);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageOffsetY);
			this.groupBoxImageFormat.Controls.Add(this.label4);
			this.groupBoxImageFormat.Controls.Add(this.textBoxImageOffsetX);
			this.groupBoxImageFormat.Location = new System.Drawing.Point(10, 137);
			this.groupBoxImageFormat.Name = "groupBoxImageFormat";
			this.groupBoxImageFormat.Size = new System.Drawing.Size(308, 154);
			this.groupBoxImageFormat.TabIndex = 34;
			this.groupBoxImageFormat.TabStop = false;
			this.groupBoxImageFormat.Text = "图像格式";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(109, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 51;
			this.label3.Text = "当前值";
			// 
			// textBoxImageOffsetYMax
			// 
			this.textBoxImageOffsetYMax.Location = new System.Drawing.Point(245, 123);
			this.textBoxImageOffsetYMax.Name = "textBoxImageOffsetYMax";
			this.textBoxImageOffsetYMax.ReadOnly = true;
			this.textBoxImageOffsetYMax.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageOffsetYMax.TabIndex = 50;
			// 
			// textBoxImageHeightMax
			// 
			this.textBoxImageHeightMax.Location = new System.Drawing.Point(245, 69);
			this.textBoxImageHeightMax.Name = "textBoxImageHeightMax";
			this.textBoxImageHeightMax.ReadOnly = true;
			this.textBoxImageHeightMax.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageHeightMax.TabIndex = 50;
			// 
			// textBoxImageOffsetXMax
			// 
			this.textBoxImageOffsetXMax.Location = new System.Drawing.Point(245, 96);
			this.textBoxImageOffsetXMax.Name = "textBoxImageOffsetXMax";
			this.textBoxImageOffsetXMax.ReadOnly = true;
			this.textBoxImageOffsetXMax.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageOffsetXMax.TabIndex = 50;
			// 
			// textBoxImageWidthMax
			// 
			this.textBoxImageWidthMax.Location = new System.Drawing.Point(245, 42);
			this.textBoxImageWidthMax.Name = "textBoxImageWidthMax";
			this.textBoxImageWidthMax.ReadOnly = true;
			this.textBoxImageWidthMax.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageWidthMax.TabIndex = 50;
			// 
			// textBoxImageOffsetYMin
			// 
			this.textBoxImageOffsetYMin.Location = new System.Drawing.Point(185, 123);
			this.textBoxImageOffsetYMin.Name = "textBoxImageOffsetYMin";
			this.textBoxImageOffsetYMin.ReadOnly = true;
			this.textBoxImageOffsetYMin.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageOffsetYMin.TabIndex = 50;
			// 
			// textBoxImageHeightMin
			// 
			this.textBoxImageHeightMin.Location = new System.Drawing.Point(185, 69);
			this.textBoxImageHeightMin.Name = "textBoxImageHeightMin";
			this.textBoxImageHeightMin.ReadOnly = true;
			this.textBoxImageHeightMin.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageHeightMin.TabIndex = 50;
			// 
			// textBoxImageOffsetXMin
			// 
			this.textBoxImageOffsetXMin.Location = new System.Drawing.Point(185, 96);
			this.textBoxImageOffsetXMin.Name = "textBoxImageOffsetXMin";
			this.textBoxImageOffsetXMin.ReadOnly = true;
			this.textBoxImageOffsetXMin.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageOffsetXMin.TabIndex = 50;
			// 
			// textBoxImageWidthMin
			// 
			this.textBoxImageWidthMin.Location = new System.Drawing.Point(185, 42);
			this.textBoxImageWidthMin.Name = "textBoxImageWidthMin";
			this.textBoxImageWidthMin.ReadOnly = true;
			this.textBoxImageWidthMin.Size = new System.Drawing.Size(57, 21);
			this.textBoxImageWidthMin.TabIndex = 50;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(253, 17);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(41, 12);
			this.label15.TabIndex = 49;
			this.label15.Text = "最大值";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(193, 17);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(41, 12);
			this.label14.TabIndex = 49;
			this.label14.Text = "最小值";
			// 
			// textBoxImageHeight
			// 
			this.textBoxImageHeight.Location = new System.Drawing.Point(78, 69);
			this.textBoxImageHeight.Name = "textBoxImageHeight";
			this.textBoxImageHeight.Size = new System.Drawing.Size(100, 21);
			this.textBoxImageHeight.TabIndex = 45;
			this.textBoxImageHeight.TextChanged += new System.EventHandler(this.textBoxImageHeight_TextChanged);
			// 
			// textBoxImageWidth
			// 
			this.textBoxImageWidth.Location = new System.Drawing.Point(78, 42);
			this.textBoxImageWidth.Name = "textBoxImageWidth";
			this.textBoxImageWidth.Size = new System.Drawing.Size(100, 21);
			this.textBoxImageWidth.TabIndex = 46;
			this.textBoxImageWidth.TextChanged += new System.EventHandler(this.textBoxImageWidth_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(5, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 12);
			this.label7.TabIndex = 43;
			this.label7.Text = "Y偏移量";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 44;
			this.label2.Text = "图像高度";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 41;
			this.label5.Text = "X偏移量";
			// 
			// textBoxImageOffsetY
			// 
			this.textBoxImageOffsetY.Location = new System.Drawing.Point(78, 122);
			this.textBoxImageOffsetY.Name = "textBoxImageOffsetY";
			this.textBoxImageOffsetY.Size = new System.Drawing.Size(100, 21);
			this.textBoxImageOffsetY.TabIndex = 48;
			this.textBoxImageOffsetY.TextChanged += new System.EventHandler(this.textBoxImageOffsetY_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 42;
			this.label4.Text = "图像宽度";
			// 
			// textBoxImageOffsetX
			// 
			this.textBoxImageOffsetX.Location = new System.Drawing.Point(78, 96);
			this.textBoxImageOffsetX.Name = "textBoxImageOffsetX";
			this.textBoxImageOffsetX.Size = new System.Drawing.Size(100, 21);
			this.textBoxImageOffsetX.TabIndex = 47;
			this.textBoxImageOffsetX.TextChanged += new System.EventHandler(this.textBoxImageOffsetX_TextChanged);
			// 
			// comboBoxBalanceSelector
			// 
			this.comboBoxBalanceSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBalanceSelector.FormattingEnabled = true;
			this.comboBoxBalanceSelector.Items.AddRange(new object[] {
            "红",
            "蓝",
            "绿"});
			this.comboBoxBalanceSelector.Location = new System.Drawing.Point(83, 60);
			this.comboBoxBalanceSelector.Name = "comboBoxBalanceSelector";
			this.comboBoxBalanceSelector.Size = new System.Drawing.Size(121, 20);
			this.comboBoxBalanceSelector.TabIndex = 35;
			this.comboBoxBalanceSelector.SelectedIndexChanged += new System.EventHandler(this.comboBoxBalanceSelector_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 63);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 12);
			this.label6.TabIndex = 36;
			this.label6.Text = "白平衡通道";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 90);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 12);
			this.label8.TabIndex = 36;
			this.label8.Text = "白平衡系数";
			// 
			// numericUpDownBalanceRatio
			// 
			this.numericUpDownBalanceRatio.Location = new System.Drawing.Point(83, 88);
			this.numericUpDownBalanceRatio.Name = "numericUpDownBalanceRatio";
			this.numericUpDownBalanceRatio.Size = new System.Drawing.Size(120, 21);
			this.numericUpDownBalanceRatio.TabIndex = 39;
			this.numericUpDownBalanceRatio.ValueChanged += new System.EventHandler(this.numericUpDownBalanceRatio_ValueChanged);
			// 
			// CameraControlForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 303);
			this.Controls.Add(this.numericUpDownBalanceRatio);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboBoxBalanceSelector);
			this.Controls.Add(this.groupBoxImageFormat);
			this.Controls.Add(this.trackBarExposureTime);
			this.Controls.Add(this.textBoxExposureTime);
			this.Controls.Add(this.label1);
			this.Name = "CameraControlForm";
			this.Text = "相机控制";
			((System.ComponentModel.ISupportInitialize)(this.trackBarExposureTime)).EndInit();
			this.groupBoxImageFormat.ResumeLayout(false);
			this.groupBoxImageFormat.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBalanceRatio)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.TextBox textBoxExposureTime;
		protected System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar trackBarExposureTime;
		private System.Windows.Forms.GroupBox groupBoxImageFormat;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBoxImageHeight;
		private System.Windows.Forms.TextBox textBoxImageWidth;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		protected System.Windows.Forms.TextBox textBoxImageOffsetY;
		private System.Windows.Forms.Label label4;
		protected System.Windows.Forms.TextBox textBoxImageOffsetX;
		private System.Windows.Forms.TextBox textBoxImageOffsetYMax;
		private System.Windows.Forms.TextBox textBoxImageHeightMax;
		private System.Windows.Forms.TextBox textBoxImageOffsetXMax;
		private System.Windows.Forms.TextBox textBoxImageWidthMax;
		private System.Windows.Forms.TextBox textBoxImageOffsetYMin;
		private System.Windows.Forms.TextBox textBoxImageHeightMin;
		private System.Windows.Forms.TextBox textBoxImageOffsetXMin;
		private System.Windows.Forms.TextBox textBoxImageWidthMin;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxBalanceSelector;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numericUpDownBalanceRatio;
	}
}