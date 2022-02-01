using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;
using AidiCore.Manger;
using AidiCore.DataType;
using AidiCore.ProjectManger;
using AIDI_Main.Properties;

namespace AIDI_Main
{
    internal partial class Frm_Tools : DockContent
    {
        internal Frm_Tools()
        {
            InitializeComponent();
            this.tvw_jobs.ImageList = Job.imageList;
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Tools _instance;
        Dictionary<string, string> _mDicTip = new Dictionary<string, string>();
       static Dictionary<string, string> ModuleTab = new Dictionary<string, string>();
        internal static Frm_Tools Instance()
        {
       
                if (_instance == null)
                    _instance = new Frm_Tools();
                return _instance;
            
        }


        /// <summary>
        /// 向流程中添加工具
        /// </summary>
        /// <param name="tool">工具类型</param>
        /// <param name="isInsert">插入位置，当为-1时，表示在末尾插入，当不为-1时，表示被插入的工具索引</param>
        internal void Add_Tool(string tool, int insertPos = -1)
        {
           
            try
            {
                string jobName = Frm_Job.Instance().tbc_jobs.SelectedTab.Text;
                if (Project.AqModuleTab.ContainsKey(jobName))
                {
                    Project.AqModuleTab[jobName].Add(Job.GetJobByName(jobName).GetNewToolName(tool), tool);
                }
                else
                {
                    Project.AqModuleTab.Add(jobName, new AqModuleTab(Job.GetJobByName(jobName).GetNewToolName(tool), tool));
                }
                AqToolInfo toolInfo = new AqToolInfo();
                TreeNode toolNode = new TreeNode();
                AqModuleData aqModuleData = AqModuleManger.Instance().CreateModuleData(tool);
                AqProjectManger.Instance().ProjectData.Add(tool,Job.GetJobByName(jobName).GetNewToolName(tool));
                AqProjectManger.Instance().ProjectData.ModuleInit("", Job.GetJobByName(jobName).GetNewToolName(tool));
                toolInfo.ToolName = Job.GetJobByName(jobName).GetNewToolName(tool);
                if (toolInfo.ToolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                {
                    return;
                }
                if (insertPos == -1)
                {
                    toolNode = new TreeNode(toolInfo.ToolName);
                    toolNode.ImageIndex = 0;
                    toolNode.SelectedImageIndex = 1;
                    Project.GetJobTree(jobName).Nodes.Add(toolNode);
                    Job.GetJobByName(jobName).L_toolList.Add(toolInfo);
                }
                else
                {
                    toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.ToolName, 1, 1);
                    Job.GetJobByName(jobName).L_toolList.Insert(insertPos, toolInfo);
                }

                List<IAqDataObj> aqDataObjs = new List<IAqDataObj>();
                aqModuleData.GetInputList(out aqDataObjs, new Type[0]);
                foreach (var item in aqDataObjs)
                {
                    int typelength = (item.Value as Type).ToString().Split('.').Length - 1;

                    TreeNode itemNode = toolNode.Nodes.Add("", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[typelength] + ")", 26, 26);
                    itemNode.ForeColor = Color.Blue;
                    toolNode.ExpandAll();
                    //不可行
                    switch (item.Type)
                    {

                        case AqDataTypeEnum.String:
                            itemNode.Tag = AqDataTypeEnum.String;
                            toolNode.ToolTipText = "输入字符串";
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Input.Add(new AqToolIO("输入字符串", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[1] + ")", DataType.String));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;
                        case AqDataTypeEnum.Int:
                            itemNode.Tag = AqDataTypeEnum.Int;
                            toolNode.ToolTipText = "输入整型数据";
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Input.Add(new AqToolIO("输入整型数据", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[1] + ")", DataType.Int));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;
                        case AqDataTypeEnum.Bitmap:
                            itemNode.Tag = AqDataTypeEnum.Bitmap;
                            toolNode.ToolTipText = "输入Bitmap格式图片";
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Input.Add(new AqToolIO("输入Bitmap数据", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[1] + ")", DataType.Image));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;
                        case AqDataTypeEnum.AbstractCamera:
                            itemNode.Tag = AqDataTypeEnum.AbstractCamera;
                            toolNode.ToolTipText = "输入Bitmap格式图片";
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Input.Add(new AqToolIO("输入Bitmap数据", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[1] + ")", DataType.Camrea));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;                      
                        default:
                            break;
                    }

                }
                aqModuleData.GetOutputList(out aqDataObjs, new Type[0]);
                foreach (var item in aqDataObjs)
                {
                    // string typename = (item.Value as Type).ToString().Split('.')[1];
                    int typelength = (item.Value as Type).ToString().Split('.').Length - 1;
                    TreeNode itemNode = toolNode.Nodes.Add("", "<--输出：  " + toolInfo.ToolName + "." + item.DataName + "   类型：(" + (item.Value as Type).ToString().Split('.')[typelength] + ")", 26, 26);
                    itemNode.ForeColor = Color.DarkMagenta;
                    toolNode.ExpandAll();
                    switch (item.Type)
                    {

                        case AqDataTypeEnum.String:
                            itemNode.Tag = DataType.String;
                            toolNode.ToolTipText = "输出字符串";
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Output.Add(new AqToolIO("输出字符串", "<--输出：   " + toolInfo.ToolName + "." + item.DataName + "   类型：(" + (item.Value as Type).ToString().Split('.')[typelength] + ")", DataType.String));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;
                        case AqDataTypeEnum.Int:
                            itemNode.Tag = AqDataTypeEnum.Int;
                            toolNode.ToolTipText = "输出整型数据";
                            typelength = (item.Value as Type).ToString().Split('.').Length-1;
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Output.Add(new AqToolIO("输出整型数据", "<--输出：   " + toolInfo.ToolName + "." + item.DataName + "   类型：(" + (item.Value as Type).ToString().Split('.')[typelength] + ")", DataType.Int));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;
                      
                        case AqDataTypeEnum.Bitmap:
                            itemNode.Tag = AqDataTypeEnum.Bitmap;
                            toolNode.ToolTipText = "输出Bitmap格式图片";
                            typelength = (item.Value as Type).ToString().Split('.').Length-1;
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Output.Add(new AqToolIO("输出Bitmap数据", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[typelength] + ")", DataType.Image));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;

                        case AqDataTypeEnum.AbstractCamera:
                            itemNode.Tag = AqDataTypeEnum.AbstractCamera;
                            toolNode.ToolTipText = "输出相机类";
                            typelength = (item.Value as Type).ToString().Split('.').Length - 1;
                            Job.GetToolInfoByToolName(jobName, toolInfo.ToolName).Output.Add(new AqToolIO("输出相机类", "-->输入：  " + toolInfo.ToolName + "." + item.DataName + " 类型：(" + (item.Value as Type).ToString().Split('.')[typelength] + ")", DataType.Camrea));
                            Project.GetJobTree(jobName).ShowNodeToolTips = true;
                            break;
                        default:                          
                            break;
                    }

                    Project.GetJobTree(jobName).Nodes[Project.GetJobTree(jobName).Nodes.Count - 1].EnsureVisible();
                }
                toolNode.ToolTipText = "未运行";
                Project.GetJobTree(jobName).ShowNodeToolTips = true;
                Application.DoEvents();
                Job.GetJobByName(Frm_Job.Instance().tbc_jobs.SelectedTab.Text).DrawLine();


            }
            catch
            {

            }
           
        }


        private void Frm_Tools_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> _mRecord = new List<string>();
                foreach (var item in AqModuleManger.Instance().ModuleNameList)
                {

                        bool isexit = _mRecord.Contains(item.ModuleGroup);
                        if (!isexit)
                        {
                            TreeNode imageAcaquisitionNode1 = tvw_jobs.Nodes.Add("", item.ModuleGroup, 0, 0);
                            {
                                imageAcaquisitionNode1.Nodes.Add("", item.ModuleName, 1, 1);
                                _mDicTip.Add(item.ModuleName, item.InfoMessage);
                               _mRecord.Add(item.ModuleGroup);
                            }
                        }
                        else
                        {
                                foreach (TreeNode item2 in tvw_jobs.Nodes)
                                {
                                      if (item2.Text == item.ModuleGroup)
                                      {
                                            item2.Nodes.Add("", item.ModuleName, 1, 1);
                                             _mDicTip.Add(item.ModuleName, item.InfoMessage);
                                             continue;
                                      }
                        }
                            

                }
                    
                    
                }
                this.tvw_jobs.GetNodeAt(0, 0).Expand();
            }
            catch (Exception ex)
            {

            }
        }
        private void tvw_job_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (_mDicTip.ContainsKey(tvw_jobs.SelectedNode.Text))
            {
                lbl_info.Text = "注释："+ _mDicTip[tvw_jobs.SelectedNode.Text];
            }
            else
            {
                lbl_info.Text = "注释：未知";

            }      
        }

        private void tvw_job_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (tvw_jobs.SelectedNode.SelectedImageIndex == 0)         //如果双击的是文件夹节点，返回
                    return;
                if (Frm_Job.Instance().tbc_jobs.TabPages.Count > 0)        //如果已存在流程
                {
                    Add_Tool(tvw_jobs.SelectedNode.Text);
                }
                else
                {
                    //如果当前不存在可用流程，先创建流程，在添加工具
                  //  Frm_Main.Instance.Create_New_Job();
                }
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 展开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvw_jobs.ExpandAll();
            tvw_jobs.SelectedNode = tvw_jobs.Nodes[0].Nodes[0];
            tvw_jobs.AutoScrollOffset = new System.Drawing.Point(0, 0);
        }
        private void 折叠所有节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvw_jobs.CollapseAll();
        }
        private void Frm_Tools_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void Frm_Tools_DoubleClick(object sender, EventArgs e)
        {

        }

    }
}
