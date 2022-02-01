namespace AIDI_Main
{
    partial class Frm_Job
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Job));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_createJob = new System.Windows.Forms.PictureBox();
            this.btn_runOnce = new System.Windows.Forms.Button();
            this.btn_runLoop = new System.Windows.Forms.Button();
            this.pic_jobProperty = new System.Windows.Forms.PictureBox();
            this.pic_foldJobTree = new System.Windows.Forms.PictureBox();
            this.pic_deleteJob = new System.Windows.Forms.PictureBox();
            this.pic_expandJobTree = new System.Windows.Forms.PictureBox();
            this.tbc_jobs = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_createJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_jobProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_foldJobTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_deleteJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_expandJobTree)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pic_createJob);
            this.panel1.Controls.Add(this.btn_runOnce);
            this.panel1.Controls.Add(this.btn_runLoop);
            this.panel1.Controls.Add(this.pic_jobProperty);
            this.panel1.Controls.Add(this.pic_foldJobTree);
            this.panel1.Controls.Add(this.pic_deleteJob);
            this.panel1.Controls.Add(this.pic_expandJobTree);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 603);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 47);
            this.panel1.TabIndex = 0;
            // 
            // pic_createJob
            // 
            this.pic_createJob.Image = ((System.Drawing.Image)(resources.GetObject("pic_createJob.Image")));
            this.pic_createJob.Location = new System.Drawing.Point(8, -1);
            this.pic_createJob.Margin = new System.Windows.Forms.Padding(5);
            this.pic_createJob.Name = "pic_createJob";
            this.pic_createJob.Size = new System.Drawing.Size(36, 42);
            this.pic_createJob.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_createJob.TabIndex = 60;
            this.pic_createJob.TabStop = false;
            this.pic_createJob.Tag = "lock";
            this.pic_createJob.Click += new System.EventHandler(this.pic_createJob_Click);
            // 
            // btn_runOnce
            // 
            this.btn_runOnce.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_runOnce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_runOnce.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_runOnce.Location = new System.Drawing.Point(326, 3);
            this.btn_runOnce.Margin = new System.Windows.Forms.Padding(5);
            this.btn_runOnce.Name = "btn_runOnce";
            this.btn_runOnce.Size = new System.Drawing.Size(105, 41);
            this.btn_runOnce.TabIndex = 10;
            this.btn_runOnce.Text = "单次运行";
            this.btn_runOnce.UseVisualStyleBackColor = true;
            this.btn_runOnce.Click += new System.EventHandler(this.btn_runJob_Click);
            // 
            // btn_runLoop
            // 
            this.btn_runLoop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_runLoop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_runLoop.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_runLoop.Location = new System.Drawing.Point(218, 3);
            this.btn_runLoop.Margin = new System.Windows.Forms.Padding(5);
            this.btn_runLoop.Name = "btn_runLoop";
            this.btn_runLoop.Size = new System.Drawing.Size(105, 41);
            this.btn_runLoop.TabIndex = 55;
            this.btn_runLoop.TabStop = false;
            this.btn_runLoop.Text = "连续运行";
            this.btn_runLoop.UseVisualStyleBackColor = true;
            this.btn_runLoop.Click += new System.EventHandler(this.btn_jobLoopRun_Click);
            // 
            // pic_jobProperty
            // 
            this.pic_jobProperty.Image = ((System.Drawing.Image)(resources.GetObject("pic_jobProperty.Image")));
            this.pic_jobProperty.Location = new System.Drawing.Point(131, -1);
            this.pic_jobProperty.Margin = new System.Windows.Forms.Padding(5);
            this.pic_jobProperty.Name = "pic_jobProperty";
            this.pic_jobProperty.Size = new System.Drawing.Size(41, 46);
            this.pic_jobProperty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_jobProperty.TabIndex = 59;
            this.pic_jobProperty.TabStop = false;
            this.pic_jobProperty.Tag = "lock";
            this.pic_jobProperty.Click += new System.EventHandler(this.pic_jobInfo_Click);
            // 
            // pic_foldJobTree
            // 
            this.pic_foldJobTree.Image = ((System.Drawing.Image)(resources.GetObject("pic_foldJobTree.Image")));
            this.pic_foldJobTree.Location = new System.Drawing.Point(87, -5);
            this.pic_foldJobTree.Margin = new System.Windows.Forms.Padding(5);
            this.pic_foldJobTree.Name = "pic_foldJobTree";
            this.pic_foldJobTree.Size = new System.Drawing.Size(45, 52);
            this.pic_foldJobTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_foldJobTree.TabIndex = 57;
            this.pic_foldJobTree.TabStop = false;
            this.pic_foldJobTree.Tag = "lock";
            this.pic_foldJobTree.Click += new System.EventHandler(this.pic_foldJobTree_Click);
            // 
            // pic_deleteJob
            // 
            this.pic_deleteJob.Image = ((System.Drawing.Image)(resources.GetObject("pic_deleteJob.Image")));
            this.pic_deleteJob.Location = new System.Drawing.Point(164, -9);
            this.pic_deleteJob.Margin = new System.Windows.Forms.Padding(5);
            this.pic_deleteJob.Name = "pic_deleteJob";
            this.pic_deleteJob.Size = new System.Drawing.Size(56, 60);
            this.pic_deleteJob.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_deleteJob.TabIndex = 56;
            this.pic_deleteJob.TabStop = false;
            this.pic_deleteJob.Tag = "lock";
            this.pic_deleteJob.Click += new System.EventHandler(this.pic_deleteJob_Click);
            // 
            // pic_expandJobTree
            // 
            this.pic_expandJobTree.Image = ((System.Drawing.Image)(resources.GetObject("pic_expandJobTree.Image")));
            this.pic_expandJobTree.Location = new System.Drawing.Point(48, -1);
            this.pic_expandJobTree.Margin = new System.Windows.Forms.Padding(5);
            this.pic_expandJobTree.Name = "pic_expandJobTree";
            this.pic_expandJobTree.Size = new System.Drawing.Size(38, 44);
            this.pic_expandJobTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_expandJobTree.TabIndex = 58;
            this.pic_expandJobTree.TabStop = false;
            this.pic_expandJobTree.Tag = "lock";
            this.pic_expandJobTree.Click += new System.EventHandler(this.pic_expandJobTree_Click);
            // 
            // tbc_jobs
            // 
            this.tbc_jobs.AllowDrop = true;
            this.tbc_jobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_jobs.ItemSize = new System.Drawing.Size(0, 20);
            this.tbc_jobs.Location = new System.Drawing.Point(0, 2);
            this.tbc_jobs.Name = "tbc_jobs";
            this.tbc_jobs.SelectedIndex = 0;
            this.tbc_jobs.Size = new System.Drawing.Size(436, 601);
            this.tbc_jobs.TabIndex = 10;
            this.tbc_jobs.TabStop = false;
            this.tbc_jobs.SelectedIndexChanged += new System.EventHandler(this.tbc_jobs_SelectedIndexChanged);
            this.tbc_jobs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbc_jobs_MouseClick);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 2);
            this.label1.TabIndex = 11;
            // 
            // Frm_Job
            // 
            this.AcceptButton = this.btn_runOnce;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(436, 650);
            this.Controls.Add(this.tbc_jobs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Frm_Job";
            this.Text = "流程编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Job_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Job_FormClosed);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Frm_Job_Scroll);
            this.SizeChanged += new System.EventHandler(this.Frm_Job_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Frm_Job_Paint);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_createJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_jobProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_foldJobTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_deleteJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_expandJobTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.PictureBox pic_createJob;
        internal System.Windows.Forms.Button btn_runOnce;
        internal System.Windows.Forms.Button btn_runLoop;
        internal System.Windows.Forms.PictureBox pic_jobProperty;
        internal System.Windows.Forms.PictureBox pic_foldJobTree;
        internal System.Windows.Forms.PictureBox pic_deleteJob;
        internal System.Windows.Forms.PictureBox pic_expandJobTree;
        internal System.Windows.Forms.TabControl tbc_jobs;
    }
}