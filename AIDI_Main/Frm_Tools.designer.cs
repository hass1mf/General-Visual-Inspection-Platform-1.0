namespace AIDI_Main
{
    partial class Frm_Tools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Tools));
            this.lbl_info = new System.Windows.Forms.Label();
            this.tvw_jobs = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.展开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.折叠所有节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_info
            // 
            this.lbl_info.BackColor = System.Drawing.Color.White;
            this.lbl_info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_info.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_info.Location = new System.Drawing.Point(0, 490);
            this.lbl_info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_info.MaximumSize = new System.Drawing.Size(0, 50);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(327, 45);
            this.lbl_info.TabIndex = 21;
            this.lbl_info.Text = "注释：此类工具用于图像的获取";
            // 
            // tvw_jobs
            // 
            this.tvw_jobs.AllowDrop = true;
            this.tvw_jobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvw_jobs.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_jobs.Indent = 24;
            this.tvw_jobs.ItemHeight = 25;
            this.tvw_jobs.LineColor = System.Drawing.Color.Green;
            this.tvw_jobs.Location = new System.Drawing.Point(0, 0);
            this.tvw_jobs.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.tvw_jobs.Name = "tvw_jobs";
            this.tvw_jobs.Size = new System.Drawing.Size(327, 490);
            this.tvw_jobs.TabIndex = 23;
            this.tvw_jobs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_job_AfterSelect);
            this.tvw_jobs.DoubleClick += new System.EventHandler(this.tvw_job_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.展开ToolStripMenuItem,
            this.折叠所有节点ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 52);
            // 
            // 展开ToolStripMenuItem
            // 
            this.展开ToolStripMenuItem.Name = "展开ToolStripMenuItem";
            this.展开ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.展开ToolStripMenuItem.Text = "展开所有节点";
            this.展开ToolStripMenuItem.Click += new System.EventHandler(this.展开ToolStripMenuItem_Click);
            // 
            // 折叠所有节点ToolStripMenuItem
            // 
            this.折叠所有节点ToolStripMenuItem.Name = "折叠所有节点ToolStripMenuItem";
            this.折叠所有节点ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.折叠所有节点ToolStripMenuItem.Text = "折叠所有节点";
            this.折叠所有节点ToolStripMenuItem.Click += new System.EventHandler(this.折叠所有节点ToolStripMenuItem_Click);
            // 
            // Frm_Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(327, 535);
            this.Controls.Add(this.tvw_jobs);
            this.Controls.Add(this.lbl_info);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_Tools";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具箱";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Tools_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Tools_Load);
            this.DoubleClick += new System.EventHandler(this.Frm_Tools_DoubleClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.TreeView tvw_jobs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 展开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 折叠所有节点ToolStripMenuItem;
    }
}