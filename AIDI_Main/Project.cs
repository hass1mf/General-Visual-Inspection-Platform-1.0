using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AIDI_Main
{
    [Serializable]
    internal class Project
    {

        /// <summary>
        /// 项目名称
        /// </summary>
        private string projectName = string.Empty;
        /// <summary>
        /// 流程集合
        /// </summary>
        internal List<Job> L_jobList = new List<Job>();
        /// <summary>
        /// 项目实例
        /// </summary>
        private static Project _instance;
        public static Project Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Project();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        static public Dictionary<string, AqModuleTab> AqModuleTab = new Dictionary<string,AqModuleTab>();
        /// <summary>
        /// 导入项目
        /// </summary>
        internal static void InportProject()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openFileDialog = new System.Windows.Forms.OpenFileDialog();
                dig_openFileDialog.Title ="请选择项目文件";
                dig_openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openFileDialog.Filter = "项目文件(*.pjt)|*.pjt";
                if (dig_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(dig_openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    Project.Instance = (Project)formatter.Deserialize(stream);
                    stream.Close();

                    Frm_Job.Instance().tbc_jobs.TabPages.Clear();
                    for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                    {
                        Job.InportJob(Project.Instance.L_jobList[i]);
                    }
                }
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 导出项目
        /// </summary>
        internal static void ExportProject()
        {
            try
            {
                if (Frm_Job.Instance().tbc_jobs.TabCount > 0)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    System.Windows.Forms.SaveFileDialog dig_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    dig_saveFileDialog.Title ="请选择项目保存路径";
                    dig_saveFileDialog.Filter = "项目文件|*.pjt";
                    dig_saveFileDialog.InitialDirectory = path;
                    if (dig_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(dig_saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                        formatter.Serialize(stream, Project.Instance);
                        stream.Close();

                        //更新结果下拉框
                      //  Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                       // Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Project exported successfully" : "项目导出成功", Color.Green);
                    }
                }
                else
                {
                   // Frm_Main.Instance.OutputMsg("当前项目尚未添加流程，不可导出", Color.Red);
                }
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 删除当前项目
        /// </summary>
        internal static void DeleteProject()
        {
            try
            {
                Frm_Job.Instance().tbc_jobs.TabPages.Clear();
                Project.Instance.L_jobList.Clear();
            }
            catch (Exception ex)
            {
             //   LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 获取当前流程所对应的流程树对象
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程树控件对象</returns>
        internal static TreeView GetJobTree(string jobName)
        {
            try
            {
                for (int i = 0; i < Frm_Job.Instance().tbc_jobs.TabCount; i++)
                {
                    if (Frm_Job.Instance().tbc_jobs.TabPages[i].Text == jobName)
                    {
                        return (TreeView)(Frm_Job.Instance().tbc_jobs.TabPages[i].Controls[0]);
                    }
                }
                return new TreeView();
            }
            catch (Exception ex)
            {
                //LogHelper.SaveErrorInfo(ex);
                return new TreeView();
            }
        }

    }
}
