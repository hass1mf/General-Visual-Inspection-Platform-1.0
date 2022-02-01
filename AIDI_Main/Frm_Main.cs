using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using AidiCore;
using AidiCore.ProjectManger;
using AIDI_Main.Properties;

namespace AIDI_Main
{
    public partial class Frm_Main : Form
    {
        // Frm_Disp form2 = new Form2();
        Form3 form3 = new Form3();

        private Frm_Main()
        {
            InitializeComponent();
            this.dockPanel_content.DocumentStyle = DocumentStyle.DockingMdi;
            this.IsMdiContainer = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Frm_Message.Instance().Show(this.dockPanel_content, DockState.DockBottomAutoHide);
            Frm_Message.Instance().Activate();
            //  Frm_Message.Instance().DockTo(this.dockPanel_content, DockStyle.Bottom);
            form3.Show(this.dockPanel_content, DockState.DockBottomAutoHide);
            //form3.DockTo(this.dockPanel_content, DockStyle.Bottom);
            Job job = new Job();
            job.jobName = "SS";
            Project.Instance.L_jobList.Add(job);
            TreeView tvw_job = new TreeView();
            tvw_job.Scrollable = true;
            tvw_job.ItemHeight = 26;
            tvw_job.ShowLines = false;
            tvw_job.AllowDrop = true;
            tvw_job.ImageList = Job.imageList;
            tvw_job.LabelEdit = true;
            tvw_job.AfterSelect += job.tvw_job_AfterSelect;
            tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
            tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
            tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);
            //tvw_job.LabelEdit = true;
            //tvw_job.begin
            //节点间拖拽
            tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
            tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
            tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);
            //以下事件为画线事件
            tvw_job.MouseMove += job.DrawLineWithoutRefresh;
            tvw_job.AfterExpand += job.Draw_Line;
            tvw_job.AfterCollapse += job.Draw_Line;
            Frm_Job.Instance().tbc_jobs.SelectedIndexChanged += job.tbc_jobs_SelectedIndexChanged;

            tvw_job.Dock = DockStyle.Fill;
            tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Frm_Job.Instance().tbc_jobs.TabPages.Add("SS");
            Frm_Job.Instance().tbc_jobs.TabPages[Frm_Job.Instance().tbc_jobs.TabPages.Count - 1].Controls.Add(tvw_job);
            Frm_Job.Instance().tbc_jobs.SelectedIndex = Frm_Job.Instance().tbc_jobs.TabCount - 1;
            tvw_job.ExpandAll();
            Frm_Tools.Instance().Owner = this;
            Frm_Tools.Instance().Show(this.dockPanel_content, DockState.DockLeftAutoHide);
            Frm_Job.Instance().Owner = this;
            Frm_Job.Instance().Show(this.dockPanel_content);
            Frm_Job.Instance().DockTo(this.dockPanel_content, DockStyle.Right);
            Frm_Disp.Instance().Show(this.dockPanel_content);
            Frm_Disp.Instance().DockTo(this.dockPanel_content, DockStyle.Fill);

        }

        static Frm_Main _instance = new Frm_Main();

        public static Frm_Main Instance()
        {

            return _instance;


        }

        static AqProjectDataType _mProjectData = null;
        static public AqProjectDataType ProjectData
        {
            get
            {

                if (_mProjectData == null)
                {
                    _mProjectData = new AqProjectDataType();
                    return _mProjectData;

                }
                else
                {
                    return _mProjectData;
                }
            }

        }
        static public AqProjectManger AqProjectManger
        {
            get
            {
                return AqProjectManger.Instance();
            }

        }


        public void CreateNewJob(string JobName)
        {


            Job job = new Job();
            job.jobName = JobName;
            Project.Instance.L_jobList.Add(job);
            TreeView tvw_job = new TreeView();
            tvw_job.Scrollable = true;
            tvw_job.ItemHeight = 26;
            tvw_job.ShowLines = false;
            tvw_job.AllowDrop = true;
            tvw_job.ImageList = Job.imageList;
            tvw_job.LabelEdit = true;
            tvw_job.AfterSelect += job.tvw_job_AfterSelect;
            tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
            tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
            tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);
            //tvw_job.LabelEdit = true;
            //tvw_job.begin
            //节点间拖拽
            tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
            tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
            tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);

            //以下事件为画线事件
            tvw_job.MouseMove += job.DrawLineWithoutRefresh;
            tvw_job.AfterExpand += job.Draw_Line;
            tvw_job.AfterCollapse += job.Draw_Line;
            Frm_Job.Instance().tbc_jobs.SelectedIndexChanged += job.tbc_jobs_SelectedIndexChanged;

            tvw_job.Dock = DockStyle.Fill;
            tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Frm_Job.Instance().tbc_jobs.TabPages.Add("SS");
            Frm_Job.Instance().tbc_jobs.TabPages[Frm_Job.Instance().tbc_jobs.TabPages.Count - 1].Controls.Add(tvw_job);
            Frm_Job.Instance().tbc_jobs.SelectedIndex = Frm_Job.Instance().tbc_jobs.TabCount - 1;
            tvw_job.ExpandAll();
            Frm_Tools.Instance().Show(this.dockPanel_content);
            Frm_Tools.Instance().DockTo(this.dockPanel_content, DockStyle.Left);
            //  Frm_Job.Instance.Owner = this;
            Frm_Job.Instance().Show(this.dockPanel_content);
            Frm_Job.Instance().DockTo(this.dockPanel_content, DockStyle.Right);

        }

        private void buttonItem73_Click(object sender, EventArgs e)
        {
            try
            {
                buttonItem73.Enabled = false;
                Frm_Job.Instance().btn_runJob_Click(null, null);
                buttonItem73.Enabled = true;
                Frm_Disp.Instance().UpData(sender, e);

            }
            catch (Exception ex)
            {
                buttonItem73.Enabled = true;
                Frm_Message.Instance().OutputMsg(ex.ToString(), Color.Red); ;
            }

            Frm_Message.Instance().OutputMsg("流程执行完毕！", Color.Green);


        }

        private void buttonItem41_Click(object sender, EventArgs e)
        {
            SUIBAIO = !SUIBAIO;
            timer.Tick += new EventHandler(runstart);
             timer.Interval = 150;
            if (SUIBAIO)
            {
                timer.Start();

            }
            else
            {
                timer.Stop();
            }
        }

        Timer timer = new Timer();
        bool SUIBAIO = false;
        void runstart(object sender, EventArgs e)
        {

            try
            {
                buttonItem73.Enabled = false;
                Frm_Job.Instance().btn_runJob_Click(null, null);
                buttonItem73.Enabled = true;
                Frm_Disp.Instance().UpData(sender, e);

            }
            catch (Exception ex)
            {
                buttonItem73.Enabled = true;
                Frm_Message.Instance().OutputMsg(ex.ToString(), Color.Red); ;
            }

            Frm_Message.Instance().OutputMsg("流程执行完毕！", Color.Green);




        }

        private void buttonItem65_Click(object sender, EventArgs e)
        {

        }
    }
}
