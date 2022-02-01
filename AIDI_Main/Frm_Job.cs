using AIDI_Main.Properties;
using AidiCore.DataType;
using AidiCore.Manger;
using AidiCore.ProjectManger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AIDI_Main
{
    public partial class Frm_Job : DockContent
    {
       // internal DeserializeDockContent deserializeDockContent;

        public Frm_Job()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |    //不闪烁
                    ControlStyles.OptimizedDoubleBuffer    //支持双缓存
                     , true);
            InitializeComponent();
            this.Icon = Resources.View;
        }


        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Job _instance;
        public static Frm_Job Instance()
        {
           
                if (_instance == null)
                    _instance = new Frm_Job();
                return _instance;
            
        }
        /// <summary>
        /// 运行流程线程
        /// </summary>
        internal Thread th_runJob;

        /// <summary>
        /// 作业实时运行
        /// </summary>
        internal void RealTimeRun()
        {
         
        }


        public void pic_deleteJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (tbc_jobs.TabPages.Count < 1)
                    return;
                Frm_ConfirmBox.Instance.lbl_info.Text ="确定要删除当前流程吗？";
                Frm_ConfirmBox.Instance.ShowDialog();
                if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Cancel)
                {
                    return;
                }
                string jobName = tbc_jobs.SelectedTab.Text;
                Job.RemoveJobByName(jobName);
                for (int i = 0; i < tbc_jobs.TabPages.Count; i++)
                {
                    if (tbc_jobs.TabPages[i].Text == jobName)
                    {
                        tbc_jobs.TabPages.RemoveAt(i);
                    }
                }
                if (File.Exists(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job"))
                    File.Delete(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job");
              
            }
            catch (Exception ex)
            {
             //   LogHelper.SaveErrorInfo(ex);
            }
        }
        public void pic_expandJobTree_Click(object sender, EventArgs e)
        {
            if (tbc_jobs.TabPages.Count < 1)
                return;
            string jobName = tbc_jobs.SelectedTab.Text;
            Job job = Job.GetJobByName(jobName);
            Project.GetJobTree(jobName).ExpandAll();
        }


        private void pic_foldJobTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance().tbc_jobs.TabPages.Count < 1)
                    return;
                string jobName = tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                Project.GetJobTree(jobName).CollapseAll();
                job.DrawLine();
            }
            catch (Exception ex)
            {
             //   LogHelper.SaveErrorInfo(ex);
            }
        }
        public void btn_runJob_Click(object sender, EventArgs e)
        {
            try
            {
                Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Clear();
                AqProjectManger.Instance().RunTasks();
                AqProjectDataType a = AqProjectManger.Instance().ProjectData;
               // Thread.Sleep(50 );
                Dictionary<string, AqModuleResult> keyValues = AqProjectManger.Instance().taskResult.ModuleResultDictionary;
                foreach (KeyValuePair<string, AqModuleResult> item in AqProjectManger.Instance().taskResult.ModuleResultDictionary)
                {
                    // Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Clear();
                    if (item.Value.DisplayBitmap != null)
                    {
                        Frm_Disp.Instance().AqDisplay.Image = item.Value.DisplayBitmap;

                    }
                    if (item.Value.DisplayShapes != null)
                    {
                        item.Value.DisplayShapes.ForEach(i1 => { Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Add(i1, "S", true); });
                        // Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                // LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_jobLoopRun_Click(object sender, EventArgs e)
        {
            try
            {
                AqProjectManger.Instance().RunTasks();
                AqProjectDataType a = AqProjectManger.Instance().ProjectData;
                Thread.Sleep(1000);
                Dictionary<string, AqModuleResult> keyValues = AqProjectManger.Instance().taskResult.ModuleResultDictionary;
                foreach (KeyValuePair<string, AqModuleResult> item in AqProjectManger.Instance().taskResult.ModuleResultDictionary)
                {
                   // Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Clear();
                    if (item.Value.DisplayBitmap !=null)
                    {
                        Frm_Disp.Instance().AqDisplay.Image = item.Value.DisplayBitmap;

                    }
                    if (item.Value.DisplayShapes != null)
                    {
                        Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Clear();
                        item.Value.DisplayShapes.ForEach( i1 => { Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Add(i1,"S",true); });
                       // Frm_Disp.Instance().AqDisplay.InteractiveGraphics.Clear();
                    }
                } 

            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
            }
        }
        internal void pic_jobInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (Frm_Job.Instance().tbc_jobs.TabPages.Count == 0)
                {
               //     Frm_Main.Instance.OutputMsg("当前无可用流程，不可打开流程信息页面", Color.Green);
                    return;
                }
             //   Frm_JobInfo.Instance.tbx_jobName.Text = tbc_jobs.SelectedTab.Text;
              //  Frm_JobInfo.Instance.ShowDialog();
            }
            catch (Exception ex)
            {
              //  LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_Job_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void Frm_Job_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void pic_createJob_Click(object sender, EventArgs e)
        {
      
            Frm_Main.Instance().CreateNewJob("sss");
        }
        private void tbc_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
       //     Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
         //   Frm_Monitor.Instance.dgv_monitor.Rows.Clear();
        }

        private void Frm_Job_SizeChanged(object sender, EventArgs e)
        {
            string jobName = tbc_jobs.SelectedTab.Text;
            if (jobName !="")
            {
                Job job = Job.GetJobByName(jobName);
                job.DrawLine();
            }
           
        }

        private void tbc_jobs_MouseClick(object sender, MouseEventArgs e)
        {
            int a = 0;
        }

        private void Frm_Job_Paint(object sender, PaintEventArgs e)
        {
            string jobName = tbc_jobs.SelectedTab.Text;
            if (jobName != "")
            {
                Job job = Job.GetJobByName(jobName);
                job.DrawLine();
            }
        }

        private void Frm_Job_Scroll(object sender, ScrollEventArgs e)
        {
            string jobName = tbc_jobs.SelectedTab.Text;
            if (jobName != "")
            {
                Job job = Job.GetJobByName(jobName);
                job.DrawLine();
            }
        }
    }

   

  
}
