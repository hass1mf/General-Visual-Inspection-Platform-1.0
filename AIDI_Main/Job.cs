using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AIDI_Main.Properties;
using AidiCore.DataType;
using AidiCore.ProjectManger;

namespace AIDI_Main
{
    public class Job
    {
        public Job()
        {
            if (rightClickMenuAtBlank.Items.Count == 0)
            {
                ToolStripItem toolStripItem_展开流程树 = rightClickMenuAtBlank.Items.Add("展开流程树");
                toolStripItem_展开流程树.Click += toolStripItem_展开流程树_Click;
                ToolStripItem toolStripItem_折叠流程树 = rightClickMenuAtBlank.Items.Add("折叠流程树");
                toolStripItem_折叠流程树.Click += toolStripItem_折叠流程树_Click;
                ToolStripItem toolStripItem_删除当前流程 = rightClickMenuAtBlank.Items.Add("删除当前流程");
                toolStripItem_删除当前流程.Click += toolStripItem_删除当前流程_Click;
                ToolStripItem toolStripItem_流程属性 = rightClickMenuAtBlank.Items.Add("流程属性");
                toolStripItem_流程属性.Click += toolStripItem_流程属性_Click;
            }
           Init_Icon_List();
        }

        /// <summary>
        /// 当前流程树是否处于折叠状态
        /// </summary>
        private static bool jobTreeFold = true;
        /// <summary>
        /// 当前流程此次运行结果
        /// </summary>
        private JobRunStatu jobRunStatu = JobRunStatu.Fail;
        /// <summary>
        /// 工具输入项个数
        /// </summary>
        private int inputItemNum = 0;
        /// <summary>
        /// 工具输出项个数
        /// </summary>
        private int outputItemNum = 0;
        /// <summary>
        /// 指示图像窗口是否为第一次显示窗体，第一次显示时要初始化
        /// </summary>
        internal bool firstDisplayImage = true;
        /// <summary>
        /// 需要连线的节点对，不停的画连线，注意键值对中第一个为连线的结束节点，第二个为起始节点，一个输出可能连接多个输入，而键值对中的键不能重复，所以把源作为值，输入作为键
        /// </summary>
         public Dictionary<TreeNode, TreeNode> D_itemAndSource = new Dictionary<TreeNode, TreeNode>();

        //是否重新绘制
        /// <summary>
        /// 本流程所绑定的生产窗口的名称
        /// </summary>
        internal string imageWindowName = "无";
        /// <summary>
        /// 流程结果图像所绑定的窗体
        /// </summary>
        internal string debugImageWindow = "图像";
        /// <summary>
        /// 流程运行结果图像
        /// </summary>
        /// <summary>
        /// 编辑节点前节点文本，用于修改工具名称
        /// </summary>
        private string nodeTextBeforeEdit = string.Empty;
        /// <summary>
        /// 流程编辑时的右击菜单
        /// </summary>
        private static ContextMenuStrip rightClickMenu = new ContextMenuStrip();
        /// <summary>
        /// 在空白除右击菜单
        /// </summary>
        private static ContextMenuStrip rightClickMenuAtBlank = new ContextMenuStrip();
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 流程树中节点的最大长度
        /// </summary>
        static private int maxLength = 130;
        /// <summary>
        /// 工具对象集合
        /// </summary>
        public List<AqToolInfo> L_toolList = new List<AqToolInfo>();
        /// <summary>
        /// 正在绘制输入输出指向线
        /// </summary>
        internal static bool isDrawing = false;
        /// <summary>
        /// 记录本工具执行完的耗时，用于计算各工具耗时
        /// </summary>
        private double recordElapseTime = 0;
        /// <summary>
        /// 标准图像字典，用于存储标准图像路径和图像对象
        /// </summary>
        /// <summary>
        /// 工具图标列表
        /// </summary>
        internal static ImageList imageList = new ImageList();
        /// <summary>
        /// 记录起始节点和此节点的列坐标值
        /// </summary>
        private static Dictionary<TreeNode, Color> startNodeAndColor = new Dictionary<TreeNode, Color>();
        /// <summary>
        /// 记录前面的划线所跨越的列段，
        /// </summary>
        private static Dictionary<int, Dictionary<TreeNode, TreeNode>> list = new Dictionary<int, Dictionary<TreeNode, TreeNode>>();
        /// <summary>
        /// 每一个列坐标值对应一种颜色
        /// </summary>
        private Dictionary<int, Color> colValueAndColor = new Dictionary<int, Color>();
        /// <summary>
        /// 输入输出指向线的颜色数组
        /// </summary>
        private static Color[] color = new Color[] { Color.Blue, Color.Orange, Color.Black, Color.Red, Color.Green, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown };


        #region 绘制输入输出指向线
        internal void tvw_job_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nodeTextBeforeEdit = Project.GetJobTree(jobName).SelectedNode.Text;
            //  Project.GetJobTree(jobName).EndUpdate();
            DrawLine();

        }
        internal void Draw_Line(object sender, TreeViewEventArgs e)
        {
            Project.GetJobTree(jobName).Refresh();
            DrawLine();
        }
        internal void tbc_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project.GetJobTree(jobName).Refresh();
            DrawLine();
        }
        public void DrawLineWithoutRefresh(object sender, MouseEventArgs e)
        {
            Project.GetJobTree(jobName).Update();
          //  DrawLine();
        }
        #endregion
        void toolStripItem_折叠流程树_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance().tbc_jobs.TabCount < 1)
                    return;
                string jobName = Frm_Job.Instance().tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                Project.GetJobTree(jobName).CollapseAll();
             //   IsUpUI = true;
                job.DrawLine();
            }
            catch (Exception ex)
            {
                //LogHelper.SaveErrorInfo(ex);
            }
        }
        void toolStripItem_展开流程树_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance().tbc_jobs.TabCount < 1)
                    return;
                string jobName = Frm_Job.Instance().tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                Project.GetJobTree(jobName).ExpandAll();

            }
            catch (Exception ex)
            {
                //LogHelper.SaveErrorInfo(ex);
            }
        }
        void toolStripItem_删除当前流程_Click(object sender, EventArgs e)
        {
            Frm_Job.Instance().pic_deleteJob_Click(null, null);
        }
        void toolStripItem_流程属性_Click(object sender, EventArgs e)
        {
            Frm_Job.Instance().pic_jobInfo_Click(null, null);
        }
        /// <summary>
        /// 拖动工具节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_ItemDrag(object sender, ItemDragEventArgs e)//左键拖动  
        {
            try
            {
                if (((TreeView)sender).SelectedNode != null)
                {
                    if (((TreeView)sender).SelectedNode.Level == 1)          //输入输出不允许拖动
                    {
                        Project.GetJobTree(jobName).DoDragDrop(e.Item, DragDropEffects.Move);
                    }

                    else if (e.Button == MouseButtons.Left)
                    {
                        Project.GetJobTree(jobName).DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }
            }
            catch (Exception ex)
            {
                // LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 克隆当前流程
        /// </summary>
        internal static void CloneCurJob()
        {
           
        }
        /// <summary>
        /// 节点拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
             //   LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 释放被拖动的节点,脱完
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_DragDrop(object sender, DragEventArgs e)//拖动  
        {
            try
            {
                //获得拖放中的节点  
                TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                //根据鼠标坐标确定要移动到的目标节点  
                System.Drawing.Point pt;
                TreeNode targeNode;
                pt = ((TreeView)(sender)).PointToClient(new System.Drawing.Point(e.X, e.Y));
                targeNode = Project.GetJobTree(jobName).GetNodeAt(pt);
                //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端  

                if (moveNode == targeNode)       //若是把自己拖放到自己，不可，返回
                    return;

                if (targeNode == null)       //目标节点为null，就是把节点拖到了空白区域，不可，直接返回
                    return;

                if (moveNode.Level == 1 && targeNode.Level == 1 && moveNode.Parent == targeNode.Parent)          //都是输入输出节点，内部拖动排序
                {
                    moveNode.Remove();
                    targeNode.Parent.Nodes.Insert(targeNode.Index, moveNode);
                    return;
                }

                if (moveNode.Level == 0)        //被拖动的是子节点，也就是工具节点
                {
                    if (targeNode.Level == 0)
                    {
                        moveNode.Remove();
                        Project.GetJobTree(jobName).Nodes.Insert(targeNode.Index, moveNode);

                        AqToolInfo temp = new AqToolInfo();
                        for (int i = 0; i < L_toolList.Count; i++)
                        {
                            if (L_toolList[i].ToolName == moveNode.Text)
                            {
                                temp = L_toolList[i];
                                L_toolList.RemoveAt(i);
                                L_toolList.Insert(targeNode.Index - 2, temp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        moveNode.Remove();
                        Project.GetJobTree(jobName).Nodes.Insert(targeNode.Parent.Index + 1, moveNode);

                        AqToolInfo temp = new AqToolInfo();
                        for (int i = 0; i < L_toolList.Count; i++)
                        {
                            if (L_toolList[i].ToolName == moveNode.Text)
                            {
                                temp = L_toolList[i];
                                L_toolList.RemoveAt(i);
                                L_toolList.Insert(targeNode.Parent.Index, temp);
                                break;
                            }
                        }
                    }
                }
                else        //被拖动的是输入输出节点
                {
                    if (targeNode.Level == 0 && GetToolInfoByToolName(jobName, targeNode.Text).ToolType == AqToolType.Output)
                    {
                        string result = moveNode.Parent.Text + " . -->" + moveNode.Text.Substring(3);
                        //添加检测值
                        //if (!((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Contains(result))
                        //    ((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Add(result);

                        GetToolInfoByToolName(jobName, targeNode.Text).Input.Add(new AqToolIO("<--" + result, "", DataType.String));
                        TreeNode node = targeNode.Nodes.Add("", "<--" + result, 26, 26);
                        node.ForeColor = Color.DarkMagenta;
                        D_itemAndSource.Add(node, moveNode);
                        targeNode.Expand();
                        DrawLine();
                        return;
                    }
                    else if (targeNode.Level == 0)
                        return;

                    //连线前首先要判断被拖动节点是否为输出项，目标节点是否为输入项
                    if (moveNode.Text.Substring(0, 3) != "-->" || targeNode.Text.Substring(0, 3) != "<--")
                    {
                     //   Frm_Main.Instance.OutputMsg("被拖动节点和目标节点输入输出不匹配，不可关联", Color.Red);
                        return;
                    }

                    //连线前要判断被拖动节点和目标节点的数据类型是否一致
                    if ((DataType)moveNode.Tag != (DataType)targeNode.Tag)
                    {
                       // Frm_Main.Instance.OutputMsg("被拖动节点和目标节点数据类型不一致，不可关联", Color.Red);
                        return;
                    }

                    string input = targeNode.Text;
                    if (input.Contains("《"))       //表示已经连接了源
                        input = Regex.Split(input, "《")[0];
                    else            //第一次连接源就需要添加到输入输出集合
                        D_itemAndSource.Add(targeNode, moveNode);
                    //GetToolInfoByToolName(jobName, targeNode.Parent.Text).GetInput(input.Substring(3)).Value = "《- " + moveNode.Parent.Text + " . " + moveNode.Text.Substring(3);
                    GetToolInfoByToolName(jobName, targeNode.Parent.Text).GetInput(input.Substring(3)).Value = "《- " + moveNode.Text.Substring(3);
                    // targeNode.Text = input + "《- " + moveNode.Parent.Text + " . " + moveNode.Text.Substring(3);
                    targeNode.Text = input + "《- "  + moveNode.Text.Substring(3);

                    DrawLine();

                    //移除拖放的节点  
                    if (moveNode.Level == 0)
                        moveNode.Remove();
                }
                //更新当前拖动的节点选择  
                Project.GetJobTree(jobName).SelectedNode = moveNode;
                //展开目标节点,便于显示拖放效果  
                targeNode.Expand();
            }
            catch (Exception ex)
            {
                //LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 初始化图标集合
        /// </summary>
        internal  void Init_Icon_List()
        {
            try
            {
                //工具图标
                imageList.Images.Add(Resources.Folder);
                imageList.Images.Add(Resources.ImageAcquistionTool);
                imageList.Images.Add(Resources.ShapeMatchTool);
                imageList.Images.Add(Resources.EyeHandCalibrationTool);
                imageList.Images.Add(Resources.CoorTransTool);
                imageList.Images.Add(Resources.SubImageTool);
                imageList.Images.Add(Resources.BlobAnalyseTool);
                imageList.Images.Add(Resources.DownCameraAlignTool);
                imageList.Images.Add(Resources.DistanceLLTool);
                imageList.Images.Add(Resources.FindLineTool);
                imageList.Images.Add(Resources.FindCircleTool);         //10
                imageList.Images.Add(Resources.CodeEditTool);
                imageList.Images.Add(Resources.LabelTool);
                imageList.Images.Add(Resources.OutputTool);

                //非工具图标
                imageList.Images.Add(Resources.Image);
                imageList.Images.Add(Resources.GrayMatchTool);
                imageList.Images.Add(Resources.UnknownTool);
                imageList.Images.Add(Resources.DistancePPTool);
                imageList.Images.Add(Resources.DistancePLTool);
                imageList.Images.Add(Resources.AngleLLTool);           //20
                imageList.Images.Add(Resources.FitLineTool);
                imageList.Images.Add(Resources.FitCircleTool);
                imageList.Images.Add(Resources.OCRTool);
                imageList.Images.Add(Resources.BarCodeTool);
                imageList.Images.Add(Resources.Empty);
                imageList.Images.Add(Resources.ColorToRGBTool);
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 判断流程是否已经存在输出工具，一个流程只能含有一个输出工具
        /// </summary>
        /// <returns></returns>
        internal bool Exist_Output()
        {
            try
            {
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].ToolType == AqToolType.Output)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
                return false;
            }
        }
        /// <summary>
        /// 绘制输入输出指向线
        /// </summary>
        /// <param name="obj"></param>
        public void DrawLine()
        {
            try
            {
     
                    if (!isDrawing)
                    {
                        isDrawing = true;
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        //Thread th = new Thread(Drawlinefunc);
                        //  th.IsBackground = true;
                        //  th.ApartmentState = ApartmentState.STA;             //此处要加一行，否则画线时会报错
                        //  th.Start();
                        timer.Tick += new EventHandler(Drawlinefunc);
                        timer.Interval = 10;
                        timer.Start();
                    }
                
             
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
            }
        }
        static object obj = new object();

        void Drawlinefunc(object sender, EventArgs e)
        {

            System.Windows.Forms.Timer time= sender as System.Windows.Forms.Timer;
                Project.GetJobTree(jobName).MouseWheel += new MouseEventHandler(numericUpDown1_MouseWheel);          //划线的时候不能滚动，否则画好了线，结果已经滚到其它地方了
                maxLength = 150;
                colValueAndColor.Clear();
                startNodeAndColor.Clear();
                list.Clear();
                TreeView tree = Project.GetJobTree(jobName);
                g = tree.CreateGraphics();
                tree.CreateGraphics().Dispose();

                foreach (KeyValuePair<TreeNode, TreeNode> item in D_itemAndSource)
                {
                    CreateLine(tree, item.Key, item.Value);
                }
                Application.DoEvents();
                Project.GetJobTree(jobName).MouseWheel -= new MouseEventHandler(numericUpDown1_MouseWheel);
                isDrawing = false;
                time.Stop();
                time.Dispose();




        }
        //取消滚轮事件
        void numericUpDown1_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }        
        }
        /// <summary>
        /// 通过流程名获取流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程</returns>
        public static Job GetJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                        return (Job)Project.Instance.L_jobList[i];
                }
                //Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Can not find job named：" + jobName + "（Error code：0001）" : "未找到名为" + jobName + "的流程（错误代码：0001）");
                return null;
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        private static Graphics g;
        /// <summary>
        /// 画Treeview控件两个节点之间的连线
        /// </summary>
        /// <param name="treeview">要画连线的Treeview</param>
        /// <param name="startNode">结束节点</param>
        /// <param name="endNode">开始节点</param>
        private static void CreateLine(TreeView treeview, TreeNode endNode, TreeNode startNode)
        {
            lock (obj)
            {
                try
                {
                    //得到起始与结束节点之间所有节点的最大长度  ，保证画线不穿过节点
                    int startNodeParantIndex = startNode.Parent.Index;
                    int endNodeParantIndex = endNode.Parent.Index;
                    int startNodeIndex = startNode.Index;
                    int endNodeIndex = endNode.Index;
                    int max = 0;                    
                    if (!startNode.Parent.IsExpanded)
                    {
                        max = startNode.Parent.Bounds.X + startNode.Parent.Bounds.Width;
                    }
                    else
                    {
                        for (int i = startNodeIndex; i < startNode.Parent.Nodes.Count - 1; i++)
                        {
                            if (max < treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.Width)
                                max = treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.Width;
                        }
                    }
                    for (int i = startNodeParantIndex + 1; i < endNodeParantIndex; i++)
                    {
                        if (!treeview.Nodes[i].IsExpanded)
                        {
                            if (max < treeview.Nodes[i].Bounds.X + treeview.Nodes[i].Bounds.Width)
                                max = treeview.Nodes[i].Bounds.X + treeview.Nodes[i].Bounds.Width;
                        }
                        else
                        {
                            for (int j = 0; j < treeview.Nodes[i].Nodes.Count; j++)
                            {
                                if (max < treeview.Nodes[i].Nodes[j].Bounds.X + treeview.Nodes[i].Nodes[j].Bounds.Width)
                                    max = treeview.Nodes[i].Nodes[j].Bounds.X + treeview.Nodes[i].Nodes[j].Bounds.Width;
                            }
                        }
                    }
                    if (!endNode.Parent.IsExpanded)
                    {
                        if (max < endNode.Parent.Bounds.X + endNode.Parent.Bounds.Width)
                            max = endNode.Parent.Bounds.X + endNode.Parent.Bounds.Width;
                    }
                    else
                    {
                        for (int i = 0; i < endNode.Index; i++)
                        {
                            if (max < treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.Width)
                                max = treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.Width;
                        }
                    }
                    max += 20;        //箭头不能连着 节点，

                    if (!startNode.Parent.IsExpanded)
                        startNode = startNode.Parent;
                    if (!endNode.Parent.IsExpanded)
                        endNode = endNode.Parent;

                    if (endNode.Bounds.X + endNode.Bounds.Width + 20 > max)
                        max = endNode.Bounds.X + endNode.Bounds.Width + 20;
                    if (startNode.Bounds.X + startNode.Bounds.Width + 20 > max)
                        max = startNode.Bounds.X + startNode.Bounds.Width + 20;

                    //判断是否可以在当前处划线
                    foreach (KeyValuePair<int, Dictionary<TreeNode, TreeNode>> item in list)
                    {
                        if (Math.Abs(max - item.Key) < 15)
                        {
                            foreach (KeyValuePair<TreeNode, TreeNode> item1 in item.Value)
                            {
                                if (startNode != item1.Value)
                                {
                                    if ((item1.Value.Bounds.X <maxLength && item1.Key.Bounds.X < maxLength) || (item1.Value.Bounds.X < maxLength && item1.Key.Bounds.X < maxLength))
                                    {
                                        max += (15 - Math.Abs(max - item.Key));
                                    }
                                }
                            }
                        }
                    }

                    Dictionary<TreeNode, TreeNode> temp = new Dictionary<TreeNode, TreeNode>();
                    temp.Add(endNode, startNode);
                    if (!list.ContainsKey(max))
                        list.Add(max, temp);
                    else
                        list[max].Add(endNode, startNode);

                    if (!startNodeAndColor.ContainsKey(startNode))
                        startNodeAndColor.Add(startNode, color[startNodeAndColor.Count]);

                    Pen pen = new Pen(startNodeAndColor[startNode], 1);
                    Brush brush = new SolidBrush(startNodeAndColor[startNode]);

                    g.DrawLine(pen, startNode.Bounds.X + startNode.Bounds.Width,
                        startNode.Bounds.Y + startNode.Bounds.Height / 2,
                    max,
                      startNode.Bounds.Y + startNode.Bounds.Height / 2);
                    g.DrawLine(pen, max,
                       startNode.Bounds.Y + startNode.Bounds.Height / 2,
                       max,
                      endNode.Bounds.Y + endNode.Bounds.Height / 2);
                    g.DrawLine(pen, max,
                       endNode.Bounds.Y + endNode.Bounds.Height / 2,
                       endNode.Bounds.X + endNode.Bounds.Width,
                         endNode.Bounds.Y + endNode.Bounds.Height / 2);
                    g.DrawString("<", new Font("微软雅黑", 12F), brush, endNode.Bounds.X + endNode.Bounds.Width - 5,
                         endNode.Bounds.Y + endNode.Bounds.Height / 2 - 12);
                    Application.DoEvents();
                }
                catch { }
            }
           
         
        }
        /// <summary>
        /// 通过作业名删除作业
        /// </summary>
        /// <param name="jobName">流程名</param>
        internal static void RemoveJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                    {
                        Project.Instance.L_jobList.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 判断是否已经存在此名称的流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>是否已存在</returns>
        internal static bool Job_Exist(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                return true;
            }
        }
        /// <summary>
        /// 判断TreeView是否已经包含某节点
        /// </summary>
        /// <param name="key">节点文本</param>
        /// <returns>是否包含</returns>
        private bool TreeView_Contains_Key(string key)
        {
            try
            {
                foreach (TreeNode node in Project.GetJobTree(jobName).Nodes)
                {
                    if (node.Text == key)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 放弃重命名
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void ResetName(object obj)
        {
            try
            {
                // Thread.Sleep(20);
    
                Project.GetJobTree(jobName).SelectedNode.Text = nodeTextBeforeEdit;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 修改工具名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EditNodeText(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                string newToolName = e.Label;
                if (newToolName == "" || newToolName == null)
                {
                    ThreadPool.QueueUserWorkItem(ResetName);
                    return;
                }

                //检查是否已经存在此名称的工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].ToolName == newToolName)
                    {
                        ((TreeView)sender).SelectedNode.Text = nodeTextBeforeEdit;
                        Application.DoEvents();
                        ((TreeView)sender).SelectedNode.BeginEdit();
                        return;
                    }
                }


                for (int i = 0; i < L_toolList.Count; i++)
                {
                    //对OutputBox特殊处理
                    if (L_toolList[i].ToolType == AqToolType.Output)
                    {
                        for (int j = 0; j < L_toolList[i].Input.Count; j++)
                        {
                            string sourceFromItem = L_toolList[i].Input[j].IOName;
                            string sourceFromToolName = Regex.Split(sourceFromItem.Substring(3), " . ")[0];
                            if (sourceFromToolName == nodeTextBeforeEdit)
                            {
                                string oldKey = L_toolList[i].Input[j].IOName;
                                string value = L_toolList[i].Input[j].Value.ToString();
                                L_toolList[i].RemoveInputIO(oldKey);
                                string newKey = "<--" + newToolName + " . " + Regex.Split(sourceFromItem.Substring(3), " . ")[1];
                                L_toolList[i].Input.Add(new AqToolIO(newKey, value, DataType.String));
                                //修改节点文本
                                TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].ToolName);
                                string nodeText = oldKey;
                                foreach (TreeNode item in toolNode.Nodes)
                                {
                                    if (((TreeNode)item).Text == nodeText)
                                    {
                                        ((TreeNode)item).Text = L_toolList[i].Input[j].IOName;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < L_toolList[i].Input.Count; j++)
                        {
                            string sourceFromItem = L_toolList[i].Input[j].Value.ToString();
                            string string1 = sourceFromItem.Substring(3);
                            string[] nums =  string1.Split('.');
                            string[] sourceName = nums[0].Split(' ');
                            string sourceFromToolName = sourceName[1];
                            if (sourceFromToolName == nodeTextBeforeEdit)
                            {
                                //修改节点文本
                                string items = L_toolList[i].Input[j].Value.ToString().Replace(sourceFromToolName, newToolName);
                                TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].ToolName);
                                foreach (TreeNode item in toolNode.Nodes)
                                {
                                   if (item.Text == sourceFromItem)
                                    {
                                        string tempstring = item.Text.Replace(sourceFromToolName, newToolName);
                                        item.Text = tempstring;
                                    }                              
                                }
                                L_toolList[i].Input[j].Value = items;
                            }
                        }
                        for (int j = 0; j < L_toolList[i].Output.Count; j++)
                        {
                            string sourceFromItem = L_toolList[i].Output[j].Value.ToString();
                            string string1 = sourceFromItem.Substring(3);
                            string[] nums = string1.Split('.');
                            string[] sourceName = nums[0].Split(' ');
                            string sourceFromToolName = sourceName[1];
                            if (sourceFromToolName == nodeTextBeforeEdit)
                            {
                                //修改节点文本
                                string items = L_toolList[i].Output[j].Value.ToString().Replace(sourceFromToolName, newToolName);
                                TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].ToolName);
                                //    string nodeText = L_toolList[i].Input[j].Value + sourceFromItem;
                                foreach (TreeNode item in toolNode.Nodes)
                                {
                                    if (item.Text == sourceFromItem)
                                    {
                                        string tempstring = item.Text.Replace(sourceFromToolName, newToolName);
                                        item.Text = tempstring;
                                    }

                                }
                                L_toolList[i].Output[j].Value = items;
                                foreach (KeyValuePair<TreeNode, TreeNode> item6 in D_itemAndSource)
                                {
                                    if (L_toolList[i].Output[j].Value.ToString() == item6.Value.Text)
                                    {
                                        string NewName =item6.Key.Text.Replace(string1, L_toolList[i].Output[j].Value.ToString().Substring(3));
                                        item6.Key.Text = NewName;
                                    }
                                }
                            }
                        }
 
                    }
                }

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].ToolName == nodeTextBeforeEdit)
                    {
                        L_toolList[i].ToolName = newToolName;
                    }
                }
                //Project.GetJobTree(jobName).Show();
                if (Project.AqModuleTab[jobName].ContainsKey(Project.GetJobTree(jobName).SelectedNode.Text))
                {
                    Project.AqModuleTab[jobName].Exchange(Project.GetJobTree(jobName).SelectedNode.Text, newToolName);
                }
        
                nodeTextBeforeEdit = newToolName;
                DrawLine();
                Project.GetJobTree(jobName).LabelEdit = false;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 从本地加载作业到程序中
        /// </summary>
        /// <param name="path">流程文件路径</param>
        public static Job LoadJob(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                Job job = (Job)formatter.Deserialize(stream);
                stream.Close();
                foreach (TabPage item in Frm_Job.Instance().tbc_jobs.TabPages)
                {
                    if (item.Text == job.jobName)
                    {
                        return new Job();
                    }
                }
                Project.Instance.L_jobList.Add(job);

                TreeView tvw_job = new TreeView();
                tvw_job.Scrollable = true;
                tvw_job.ItemHeight = 26;
                tvw_job.ShowLines = false;
                tvw_job.AllowDrop = true;
                tvw_job.ImageList = Job.imageList;
                tvw_job.TabStop = false;

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);

                //以下事件为画线事件

                //   tvw_job.DrawMode = TreeViewDrawMode.OwnerDrawText;

                Frm_Job.Instance().Paint += job.Instance_Paint;

                tvw_job.MouseMove += job.DrawLineWithoutRefresh;
                tvw_job.MouseWheel += job.DrawLineWithoutRefresh;

                tvw_job.AfterExpand += job.Draw_Line;
                tvw_job.AfterCollapse += job.Draw_Line;
                Frm_Job.Instance().tbc_jobs.SelectedIndexChanged += job.tbc_jobs_SelectedIndexChanged;

                Frm_Job.Instance().tbc_jobs.TabPages.Add(job.jobName);
                Frm_Job.Instance().tbc_jobs.TabPages[Frm_Job.Instance().tbc_jobs.TabPages.Count - 1].Controls.Add(tvw_job);
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.ShowNodeToolTips = true;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //反序列化各工具
                job.D_itemAndSource.Clear();
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    TreeNode node = Project.GetJobTree(job.jobName).Nodes.Add(job.L_toolList[i].ToolName);
                    //  string[] inputKeys = job.L_toolList[i].input.Keys.ToArray();
                    for (int j = 0; j < job.L_toolList[i].Input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].ToolType != AqToolType.Output)
                            treeNode = node.Nodes.Add("<--" + job.L_toolList[i].Input[j].IOName + job.L_toolList[i].Input[j].Value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].Input[j].IOName);

                        ////if (inputKeys[j].Contains("Image"))       //图像变量类型
                        ////    treeNode.Tag = DataType.Image;
                        ////else if (inputKeys[j].Contains("Region"))       //区域变量类型
                        ////    treeNode.Tag = "Region";
                        ////else
                        treeNode.Tag = job.L_toolList[i].Input[j].IoType;       //字符串变量类型
                        treeNode.ForeColor = Color.DarkMagenta;

                        //解析需要连线的节点对

                        if (treeNode.ToString().Contains("《-"))
                        {
                            string toolNodeText = Regex.Split(job.L_toolList[i].Input[j].Value.ToString(), " . ")[0].Substring(3);
                            string toolIONodeText = "-->" + Regex.Split(job.L_toolList[i].Input[j].Value.ToString(), " . ")[1];
                            job.D_itemAndSource.Add(treeNode, job.GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                        }
                        if (job.L_toolList[i].ToolType == AqToolType.Output)
                        {
                            string toolNodeText = Regex.Split(treeNode.Text, " . ")[0].Substring(3);
                            string toolIONodeText = Regex.Split(treeNode.Text, " . ")[1];
                            job.D_itemAndSource.Add(treeNode, job.GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                        }
                    }
                    // string[] outputKeys = job.L_toolList[i].output.Keys.ToArray();
                    for (int k = 0; k < job.L_toolList[i].Output.Count; k++)
                    {
                        TreeNode treeNode = node.Nodes.Add("-->" + job.L_toolList[i].Output[k].IOName);
                        //if (outputKeys[k].Contains("Image") || outputKeys[k].Contains("图像"))
                        //    treeNode.Tag = DataType.Image;
                        //else if (outputKeys[k].Contains("Region"))
                        //    treeNode.Tag = "Region";
                        //else
                        treeNode.Tag = job.L_toolList[i].Output[k].IoType;
                        treeNode.ForeColor = Color.Blue;
                    }
                }

                //更新工具树图标
                for (int j = 0; j < Project.GetJobTree(job.jobName).Nodes.Count; j++)
                {
                    switch (Job.GetToolInfoByToolName(job.jobName, Project.GetJobTree(job.jobName).Nodes[j].Text).ToolType)
                    {                    
                        case AqToolType.Output:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;
                            break;

                        default:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
                            break;

                    }
                    for (int k = 0; k < Project.GetJobTree(job.jobName).Nodes[j].Nodes.Count; k++)
                    {
                        Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].SelectedImageIndex = 26;
                    }
                }
                //默认选中第一个节点
                if (tvw_job.Nodes.Count > 0)
                    tvw_job.SelectedNode = tvw_job.Nodes[0];
                return job;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        void Instance_Paint(object sender, PaintEventArgs e)
        {
            DrawLineWithoutRefresh(null, null);
        }

        void tvw_job_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Draw_Line(null, null);
        }
        /// <summary>
        /// 生成新工具的名称
        /// </summary>
        /// <param name="toolName">工具类型</param>
        /// <returns>工具名称</returns>
        internal string GetNewToolName(string toolType)
        {
            try
            {
                if (!TreeView_Contains_Key(toolType))
                {
                    return toolType;
                }
                for (int i = 1; i < 101; i++)
                {
                    if (!TreeView_Contains_Key(toolType + "_" + i))
                    {
                        return toolType + "_" + i;
                    }
                }
                return "Error";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
        /// <summary>
        /// 加载流程
        /// </summary>
        /// <param name="job">流程对象</param>
        internal static void InportJob(Job job)
        {
            try
            {
                TreeView tvw_job = new TreeView();
                tvw_job.Scrollable = true;
                tvw_job.ItemHeight = 26;
                tvw_job.ShowLines = false;
                tvw_job.AllowDrop = true;
                tvw_job.ImageList = Job.imageList;

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);

                //以下事件为画线事件
                tvw_job.MouseMove += job.DrawLineWithoutRefresh;
                tvw_job.AfterExpand += job.Draw_Line;
                tvw_job.AfterCollapse += job.Draw_Line;
                Frm_Job.Instance().tbc_jobs.SelectedIndexChanged += job.tbc_jobs_SelectedIndexChanged;

                Frm_Job.Instance().tbc_jobs.TabPages.Add(job.jobName);
                Frm_Job.Instance().tbc_jobs.TabPages[Frm_Job.Instance().tbc_jobs.TabPages.Count - 1].Controls.Add(tvw_job);
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.ShowNodeToolTips = true;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //反序列化各工具
                job.D_itemAndSource.Clear();
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    TreeNode node = Project.GetJobTree(job.jobName).Nodes.Add(job.L_toolList[i].ToolName);
                    //string[] inputKeys = job.L_toolList[i].input.Keys.ToArray();
                    for (int j = 0; j < job.L_toolList[i].Input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].ToolType != AqToolType.Output)
                            treeNode = node.Nodes.Add(job.L_toolList[i].Input[j].IOName + job.L_toolList[i].Input[j].Value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].Input[j].IOName);

                        //if (inputKeys[j].Contains("Image"))       //图像变量类型
                        //    treeNode.Tag = DataType.Image;
                        //else if (inputKeys[j].Contains("Region"))       //区域变量类型
                        //    treeNode.Tag = "Region";
                        //else
                        treeNode.Tag = job.L_toolList[i].Input[j].IoType;       //字符串变量类型
                        treeNode.ForeColor = Color.DarkMagenta;

                        //解析需要连线的节点对

                        if (treeNode.ToString().Contains("《-"))
                        {
                            string toolNodeText = Regex.Split(job.L_toolList[i].Input[j].Value.ToString(), " . ")[0].Substring(3);
                            string toolIONodeText = "-->" + Regex.Split(job.L_toolList[i].Input[j].Value.ToString(), " . ")[1];
                            job.D_itemAndSource.Add(treeNode, job.GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                        }
                        if (job.L_toolList[i].ToolType == AqToolType.Output)
                        {
                            string toolNodeText = Regex.Split(treeNode.Text, " . ")[0].Substring(3);
                            string toolIONodeText = Regex.Split(treeNode.Text, " . ")[1];
                            job.D_itemAndSource.Add(treeNode, job.GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                        }
                    }
                    // string[] outputKeys = job.L_toolList[i].output.Keys.ToArray();
                    for (int k = 0; k < job.L_toolList[i].Output.Count; k++)
                    {
                        TreeNode treeNode = node.Nodes.Add("-->" + job.L_toolList[i].Output[k].IOName);
                        ////if (outputKeys[k].Contains("Image") || outputKeys[k].Contains("图像"))
                        ////    treeNode.Tag = DataType.Image;
                        ////else if (outputKeys[k].Contains("Region"))
                        ////    treeNode.Tag = "Region";
                        ////else
                        treeNode.Tag = job.L_toolList[i].Output[k].IoType;
                        treeNode.ForeColor = Color.Blue;
                    }

                    //更新工具树图标
                    for (int j = 0; j < Project.GetJobTree(job.jobName).Nodes.Count; j++)
                    {
                        switch (Job.GetToolInfoByToolName(job.jobName, Project.GetJobTree(job.jobName).Nodes[j].Text).ToolType)
                        {
                            case AqToolType.Output:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;
                                break;

                            default:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
                                break;

                        }
                        for (int k = 0; k < Project.GetJobTree(job.jobName).Nodes[j].Nodes.Count; k++)
                        {
                            Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].SelectedImageIndex = 26;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 运行当前作业
        /// </summary>
        internal static void RunCurJob()
        {
            try
            {
                if (Frm_Job.Instance().tbc_jobs.TabPages.Count == 0)
                {
                    return;
                }
                Frm_Job.Instance().btn_runOnce.Enabled = false;
                Job job = Job.GetJobByName(Frm_Job.Instance().tbc_jobs.SelectedTab.Text);
                job.Run();
                Frm_Job.Instance().btn_runOnce.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 通过工具名获取工具信息
        /// </summary>
        /// <param name="toolName">工具名</param>
        /// <returns>工具信息</returns>
        internal static AqToolInfo GetToolInfoByToolName(string jobName, string toolName)
        {
            try
            {
                Job job = new Job();
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (Project.Instance.L_jobList[i].jobName == jobName)
                    {
                        job = Project.Instance.L_jobList[i];
                        break;
                    }
                }
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    if (job.L_toolList[i].ToolName == toolName)
                    {
                        return job.L_toolList[i];
                    }
                }
                return new AqToolInfo();
            }
            catch (Exception ex)
            {
                return new AqToolInfo();
            }
        }
        /// <summary>
        /// 通过流程名和工具名获取工具
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <param name="toolName">工具名</param>
        /// <returns></returns>
        internal static object GetToolByToolName(string jobName, string toolName)
        {
            try
            {
                Job job = new Job();
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (Project.Instance.L_jobList[i].jobName == jobName)
                    {
                        job = Project.Instance.L_jobList[i];
                        break;
                    }
                }
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    if (job.L_toolList[i].ToolName == toolName)
                    {
                        return job.L_toolList[i].Tag;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 通过输出项字符串获取输出项的值
        /// </summary>
        /// <param name="outputItem">输出项字符创</param>
        /// <returns>输出项的值</returns>
        public string GetOutputItemValue(string outputItem)
        {
            try
            {
                //寻找输出盒工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].ToolType == AqToolType.Output)
                    {
                        for (int j = 0; j < L_toolList[i].Input.Count; j++)
                        {
                            if (L_toolList[i].Input[j].Value.ToString() == outputItem)
                            {
                                return L_toolList[i].GetInput(outputItem).Value.ToString();
                            }
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }
        /// <summary>
        /// 通过TreeNode节点文本获取节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <returns>节点对象</returns>
        internal TreeNode GetToolNodeByNodeText(string nodeText)
        {
            try
            {
                foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                {
                    if (((TreeNode)toolNode).Text != nodeText)
                    {
                        foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                        {
                            if (((TreeNode)itemNode).Text.Substring(3) == nodeText)
                            {
                                return itemNode;
                            }
                        }
                    }
                    else
                    {
                        return toolNode;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 通过TreeNode节点文本获取输入输出节点
        /// </summary>
        /// <param name="toolName">工具名称</param>
        /// <returns>IO名称</returns>
        internal TreeNode GetToolIONodeByNodeText(string toolName, string toolIOName)
        {
            try
            {
                foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                {
                    if (toolNode.Text == toolName)
                    {
                        foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                        {
                            if (((TreeNode)itemNode).Text.Contains(toolIOName))
                            {
                                return itemNode;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 指定源事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceFrom(object sender, EventArgs e)
        {
            try
            {
                string nodeText;
                if (Project.GetJobTree(jobName).SelectedNode.Level == 1)
                {
                    nodeText = Project.GetJobTree(jobName).SelectedNode.Parent.Text;
                }
                else
                {
                    nodeText = Project.GetJobTree(jobName).SelectedNode.Text;
                }
                string input = Project.GetJobTree(jobName).SelectedNode.Text;
                if (Project.GetJobTree(jobName).SelectedNode.Text.Contains("《"))       //表示已经连接了源
                {
                    input = Regex.Split(Project.GetJobTree(jobName).SelectedNode.Text, "《")[0];
                    MessageBox.Show("已连接，请先断开连接！");
                    return;
                }
                GetToolInfoByToolName(jobName, nodeText).GetInput(input).Value = sender.ToString();
                Project.GetJobTree(jobName).SelectedNode.Text = input + sender.ToString();
                GetToolInfoByToolName(jobName, nodeText).GetInput(input).Value = sender.ToString();

                string toolNodeText = Regex.Split(sender.ToString(), "  ")[2].Split('.')[0];

                string toolIONodeText = "<--"+sender.ToString().Substring(4);
                TreeNode nodenametree = Project.GetJobTree(jobName).SelectedNode;
                TreeNode newnode = GetToolIONodeByNodeText(toolNodeText, toolIONodeText);
                if (AqProjectManger.Instance().ContainsKey(toolNodeText))
                {
                    if (nodenametree.Text.Contains(nodeText) && nodenametree.Text.Contains("-->") && nodenametree.Text.Contains("《-"))
                    {
                        string[] tmps = newnode.Text.Split('.');
                        string[] tmps1 = tmps[0].Split(' ');
                        string[] tmps2 = tmps[1].Split(' ');
                        string[] tmps3 = nodenametree.Text.Split('.');
                        string[] tmps4 = tmps3[0].Split(' ');
                        string[] tmps5 = tmps3[1].Split(' ');
                        if (tmps4[2] == nodeText)
                        {
                            AqLinkList aqLinkList = new AqLinkList(tmps1[2], tmps2[0], tmps5[0]);
                            
                            AqProjectManger.Instance().ProjectData.ModuleData[nodeText].LinkDatas.Add(aqLinkList);
                            AqProjectDataType aqProjectDataType = AqProjectManger.Instance().ProjectData;
                            if (AqProjectManger.Instance().ProjectData.ModuleInput.ContainsKey(tmps4[2]) && AqProjectManger.Instance().ProjectData.ModuleInput.ContainsKey(tmps1[2]))
                            {

                                string linkname = tmps1[2];
                                while (linkname != "End" && AqProjectManger.Instance().ProjectData.ModuleOutput[linkname] != "End")
                                {
                                    linkname = AqProjectManger.Instance().ProjectData.ModuleOutput[linkname];
                                }
                                AqProjectManger.Instance().SetConnection(linkname, tmps4[2]);
                            }

                                        
                        }
                    }
                }     
                D_itemAndSource.Add(nodenametree, newnode);
                //IsUpUI = true;
                DrawLine();
            }
            catch (Exception ex)
            {
            }
        }
        //运行runtool 执行流程
        private void RunTool(object sender, EventArgs e)
        {
            //try
            //{
            //    ((ToolBase)(GetToolByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text))).Run(jobName, true, true);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.SaveErrorInfo(ex);
            //}
        }
        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem(object sender, EventArgs e)
        {
            try
            {
                if (Project.GetJobTree(jobName).SelectedNode == null)
                    return;

                string nodeText = Project.GetJobTree(jobName).SelectedNode.Text.ToString();
                int level = Project.GetJobTree(jobName).SelectedNode.Level;
                string fatherNodeText = string.Empty;

                //如果是子节点
                if (level == 1)
                {
                    fatherNodeText = Project.GetJobTree(jobName).SelectedNode.Parent.Text;
                }
                foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                {
                    if (level == 1)
                    {
                        if (toolNode.Text == fatherNodeText)
                        {
                            foreach (var itemNode in ((TreeNode)toolNode).Nodes)
                            {
                                if (itemNode != null)
                                {
                                    if (((TreeNode)itemNode).Text == nodeText)
                                    {
                                        //移除连线集合中的这条连线
                                        for (int i = 0; i < D_itemAndSource.Count; i++)
                                        {
                                            if ((((TreeNode)itemNode) == D_itemAndSource.Keys.ToArray()[i]) || (((TreeNode)itemNode) == D_itemAndSource[D_itemAndSource.Keys.ToArray()[i]]))
                                            {

                                                TreeNode treeNodeed = D_itemAndSource.Keys.ToArray()[i];
                                                TreeNode treeNodeing = D_itemAndSource[treeNodeed];
                                              
                                                string[] tmps = treeNodeed.Text.Split('.');
                                                string[] tmps1 = tmps[0].Split(' ');
                                                string[] tmps3 = treeNodeing.Text.Split('.');
                                                string[] tmps4 = tmps3[0].Split(' ');
                                                string nodeneameed = tmps1[1];
                                                string nodeneameing = tmps4[1];
                                               // AqProjectManger.Instance().RemoveConnection(nodeneameed, nodeneameing);
                                                D_itemAndSource.Remove(treeNodeed);
                                                List<AqDataItem> aqDatas = new List<AqDataItem>(); 
                                                foreach (KeyValuePair<TreeNode, TreeNode> item3 in D_itemAndSource)
                                                {
                                                    if (item3.Key.Parent == Project.GetJobTree(jobName).SelectedNode.Parent)
                                                    {
                                                        if (item3.Key.Text.Contains("-->") && item3.Key.Text.Contains("《-"))
                                                        {
                                                            string[] D_tmps = item3.Value.Text.Split('.');
                                                            string[] D_tmps1 = tmps[0].Split(' ');
                                                            string[] D_tmps2 = tmps[1].Split(' ');
                                                            string[] D_tmps3 = item3.Key.Text.Split('.');
                                                            string[] D_tmps4 = tmps3[0].Split(' ');
                                                            string[] D_tmps5 = tmps3[1].Split(' ');

                                                            //aqcamra imagein imageout
                                                            string InputDataName = D_tmps1[1] + '.' + D_tmps5[0];
                                                            string DataName = D_tmps2[0];
                                                            AqDataItem aqData = new AqDataItem()
                                                            {
                                                                DataName = DataName,
                                                                InputDataName = InputDataName
                                                            };
                                                            // AqLinkList aqLinkList = new AqLinkList(D_tmps1[1], D_tmps2[0], D_tmps5[0]);
                                                            aqDatas.Add(aqData);                             
                                                        }

                                                    }
                                                }

                                                if (aqDatas.Count == 0)
                                                {
                                                    AqProjectManger.Instance().RemoveConnection(nodeneameed, nodeneameing);
                                                    AqProjectDataType aqProjectDataType = AqProjectManger.Instance().ProjectData;
                                                    AqProjectManger.Instance().UpdateModuleInputList(Project.GetJobTree(jobName).SelectedNode.Parent.Text, aqDatas);
                                                }
                                                else
                                                {
                                                    AqProjectManger.Instance().UpdateModuleInputList(Project.GetJobTree(jobName).SelectedNode.Parent.Text, aqDatas);
                                                }
                                                Project.GetJobTree(jobName).SelectedNode.Text = Regex.Split(nodeText, "《")[0];
                                                DrawLine();
                                            }

                                        }

                                      //  ((TreeNode)itemNode).Remove();
                                       // Project.GetJobTree(jobName).SelectedNode = null;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (((TreeNode)toolNode).Text == nodeText)
                        {
                            ((TreeNode)toolNode).Remove();
                            break;
                        }
                    }
                }

                //如果是父节点
                if (level == 0)
                {
                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        if (L_toolList[i].ToolName == nodeText)
                        {
                            try
                            {
                                //移除连线集合中的这条连线
                                for (int j = D_itemAndSource.Count - 1; j >= 0; j--)
                                {
                                    
                                    string[] tmps = D_itemAndSource.Keys.ToArray()[j].Text.Split('.');
                                    string[] tmps1 = tmps[0].Split(' ');                    
                                    string nodeneameed = tmps1[1];
                                    AqProjectManger.Instance().Remove(nodeneameed);
                                    AqProjectDataType dataType = AqProjectManger.Instance().ProjectData;
                                    if (nodeText == D_itemAndSource.Keys.ToArray()[j].Parent.Text || nodeText == D_itemAndSource[D_itemAndSource.Keys.ToArray()[j]].Parent.Text)
                                        D_itemAndSource.Remove(D_itemAndSource.Keys.ToArray()[j]);
                                }
                            }
                            catch { }

                            L_toolList.RemoveAt(i);
                            AqProjectDataType aqProjectDataType1 = AqProjectManger.Instance().ProjectData;

                            string lastname =  AqProjectManger.Instance().ProjectData.ModuleInput[nodeText];

                            string nextname = AqProjectManger.Instance().ProjectData.ModuleOutput[nodeText];
                            AqProjectManger.Instance().Remove(nodeText);

                            if (lastname != "Start" && nextname !="End")
                            {
                                AqProjectManger.Instance().SetConnection(lastname, nextname);


                            }
                                    
                                    
                            AqProjectDataType aqProjectDataType = AqProjectManger.Instance().ProjectData;

                        }
                    }
                }
                else
                {
                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        if (L_toolList[i].ToolName == fatherNodeText)
                        {
                            for (int j = 0; j < L_toolList[i].Input.Count; j++)
                            {
                                if (L_toolList[i].Input[j].Value.ToString() == Regex.Split(nodeText, "《")[0])
                                    L_toolList[i].RemoveInputIO(Regex.Split(nodeText, "《")[0]);
                            }
                            for (int j = 0; j < L_toolList[i].Output.Count; j++)
                            {
                                if (L_toolList[i].Output[j].IOName == nodeText.Substring(3))
                                    L_toolList[i].RemoveOutputIO(nodeText.Substring(3));
                            }
                        }
                    }
                }

                Project.AqModuleTab[jobName].Remove(nodeText); ;

            }
            catch (Exception ex)
            {
              //  LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 插入工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTool(object sender, EventArgs e)
        {
         //   Frm_Tools.Instance.Add_Tool(((ToolStripItem)sender).Text, Project.GetJobTree(jobName).SelectedNode.Index);
        }
        /// <summary>
        /// 启用工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableOrDisenableTool(object sender, EventArgs e)
        {
            string jobName = Frm_Job.Instance().tbc_jobs.SelectedTab.Text;
            Job.GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).Enable = !Job.GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).Enable;
        }
        /// <summary>
        /// 获取工具输入项的个数
        /// </summary>
        private int GetInputItemNum(TreeNode toolNode)
        {
            try
            {
                int num = 0;
                foreach (TreeNode item in toolNode.Nodes)
                {
                    if (item.Text.Substring(0, 3) == "<--")
                    {
                        num++;
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                //LogHelper.SaveErrorInfo(ex);
                return 0;
            }
        }
        /// <summary>
        /// 重命名工具                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameTool(object sender, EventArgs e)
        {

            Project.GetJobTree(jobName).LabelEdit = true;
            Project.GetJobTree(jobName).SelectedNode.BeginEdit();
           // Project.GetJobTree(jobName).LabelEdit = false;

        }
        /// <summary>
        /// 修改工具备注                
        /// </summary>
        /// <param name="sender"></param>5
        /// <param name="e"></param>
        private void ModifyTipInfo(object sender, EventArgs e)
        {
          
        }
        /// <summary>
        /// 添加输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_input(object sender, EventArgs e)
        {
            try
            {
                string result = sender.ToString();

                //首先检查是否已经有此输入项,若已添加，则返回
                foreach (var item in Project.GetJobTree(jobName).SelectedNode.Nodes)
                {
                    string text;
                    if (((TreeNode)item).Text.Contains("《"))
                    {
                        text = Regex.Split(((TreeNode)item).Text, "《")[0];
                    }
                    else
                    {
                        text = ((TreeNode)item).Text;
                    }
                    if (text == "<--" + result)
                    {
                        return;
                    }
                }

                int insertPos = GetInputItemNum(Project.GetJobTree(jobName).SelectedNode);        //获取插入位置，要保证输入项在前，输出项在后
                TreeNode node = Project.GetJobTree(jobName).SelectedNode.Nodes.Insert(insertPos, "", "<--" + result, 26, 26);
                node.ForeColor = Color.DarkMagenta;
                Project.GetJobTree(jobName).SelectedNode.Expand();
                DataType ioType = (DataType)((ToolStripItem)sender).Tag;

                //指定输入变量的类型
                //if (result == (Configuration.language == Language.English ? "InputImage" : "输入图像"))
                //    node.Tag = DataType.Image;
                //else if (result == "BlobResult")
                //    node.Tag = "BlobResult";
                //else
                node.Tag = ioType;
                node.Name = "<--" + result;
                GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).Input.Add(new AqToolIO(result, "", ioType));

                //如果是给输出工具添加输入，则需要连线
                if (GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).ToolType == AqToolType.Output)
                {
                    //添加检测值
                    //if (!((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Contains(result))
                    //    ((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Add(result);
                    string toolNodeText = Regex.Split(sender.ToString(), " . ")[0];
                    string toolIONodeText = Regex.Split(sender.ToString(), " . ")[1];
                    D_itemAndSource.Add(GetToolIONodeByNodeText(Project.GetJobTree(jobName).SelectedNode.Text, "<--" + sender.ToString()), GetToolIONodeByNodeText(toolNodeText, toolIONodeText));

                    Draw_Line(null, null);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 添加输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_output(object sender, EventArgs e)
        {
            try
            {
                string result = sender.ToString();
                foreach (var item in Project.GetJobTree(jobName).SelectedNode.Nodes)
                {
                    if (((TreeNode)item).Text == "-->" + result)
                    {
                        return;
                    }
                }
                TreeNode node = Project.GetJobTree(jobName).SelectedNode.Nodes.Add("", "-->" + result, 26, 26);
                node.ForeColor = Color.Blue;
                Project.GetJobTree(jobName).SelectedNode.Expand();
                DataType ioType = (DataType)((ToolStripItem)sender).Tag;

                //指定输出变量的类型
                if (result == "输出图像")
                {
                    //  node.Tag = DataType.Image;
                    node.ToolTipText = "图形变量不支持显示";
                }
                //else if (result == "BlobResult")
                //    node.Tag = "BlobResult";
                //else
                node.Tag = ioType;

                node.Name = "-->" + result;
                GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).Output.Add(new AqToolIO(result, "", ioType));
                node.ToolTipText ="未运行";
                Project.GetJobTree(jobName).ShowNodeToolTips = true;
            }
            catch (Exception ex)
            {
                //LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 工具上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUp(object sender, EventArgs e)
        {
            try
            {
                if (Project.GetJobTree(jobName).SelectedNode.Index == 0)
                    return;

                TreeNode Node = Project.GetJobTree(jobName).SelectedNode;
                TreeNode PrevNode = Node.PrevNode;
                if (PrevNode != null)
                {
                    //  TreeNode NewNode = (TreeNode)Node.Clone();

                    int Prev =  PrevNode.Index;
                    int current = Node.Index;
                    PrevNode.Remove();
                    Node.Remove();
                    if (Node.Parent == null)
                    {
                        Project.GetJobTree(jobName).Nodes.Insert(Prev, Node);
                        Project.GetJobTree(jobName).Nodes.Insert(current, PrevNode);

                    }
                    else
                    {
                        Project.GetJobTree(jobName).Nodes.Insert(Prev, Node);
                        Project.GetJobTree(jobName).Nodes.Insert(current, PrevNode);
                    }
                    Project.GetJobTree(jobName).SelectedNode = Node;
                }

                AqToolInfo temp = new AqToolInfo();
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].ToolName == Project.GetJobTree(jobName).SelectedNode.Text)
                    {
                        temp = L_toolList[i];
                        L_toolList[i] = L_toolList[i - 1];
                        L_toolList[i - 1] = temp;
                        break;
                    }
                }
                DrawLine();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 工具下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDown(object sender, EventArgs e)
        {
            try
            {
                if (Project.GetJobTree(jobName).SelectedNode.Index == Project.GetJobTree(jobName).Nodes.Count - 1)
                    return;
                TreeNode Node = Project.GetJobTree(jobName).SelectedNode;
                TreeNode NextNode = Node.NextNode;
                if (NextNode != null)
                {

                    int Next = NextNode.Index;
                    int current = Node.Index;
                    NextNode.Remove();
                    Node.Remove();
                    if (Node.Parent == null)
                    {
                        Project.GetJobTree(jobName).Nodes.Insert(Next + 1, Node);
                        Project.GetJobTree(jobName).Nodes.Insert(current, NextNode);

                    }
                    else
                    {
                        Project.GetJobTree(jobName).Nodes.Insert(Next + 1, Node);
                        Project.GetJobTree(jobName).Nodes.Insert(current, NextNode);
                    }
                    Project.GetJobTree(jobName).SelectedNode = Node;
                }

                AqToolInfo temp = new AqToolInfo();
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].ToolName == Project.GetJobTree(jobName).SelectedNode.Text)
                    {
                        temp = L_toolList[i];
                        L_toolList[i] = L_toolList[i + 1];
                        L_toolList[i + 1] = temp;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 把Double数值格式化成标准的9位的格式
        /// </summary>
        /// <param name="value">需要格式化的数值</param>
        /// <returns>结果字符串</returns>
        private string FormatValueTo9Bit(double value)
        {
            return value >= 0 ? "+" + value.ToString("0000.000") : value.ToString("0000.000");
        }
        /// <summary>
        /// 把节点文本添加到剪切板，用于复制粘贴输出项文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyNodeText(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Project.GetJobTree(jobName).SelectedNode.Text);
        }
        /// <summary>
        /// 流程树右击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TVW_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //判断是否在节点单击
                DrawLine();             
                TreeViewHitTestInfo test = Project.GetJobTree(jobName).HitTest(e.X, e.Y);
                if (Project.GetJobTree(jobName).ContextMenuStrip == null)
                {
                    Project.GetJobTree(jobName).ContextMenuStrip = new ContextMenuStrip();
                }
                else
                {
                    Project.GetJobTree(jobName).ContextMenuStrip.Items.Clear();
                }
                ContextMenuStrip rightClickMenu1 = Project.GetJobTree(jobName).ContextMenuStrip;
                if (Project.GetJobTree(jobName).GetNodeAt(e.X, e.Y).Parent != null)
                {
                     Project.GetJobTree(jobName).SelectedNode = Project.GetJobTree(jobName).GetNodeAt(e.X, e.Y);
             
                    string nodeText = Project.GetJobTree(jobName).SelectedNode.Text;
                    string fatherNodeText = Project.GetJobTree(jobName).SelectedNode.Parent.Text;
                    string curNodeType = Project.GetJobTree(jobName).SelectedNode.Tag.ToString();
                    foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                    {
                        if (((TreeNode)toolNode).Text == fatherNodeText)
                        {
                            foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                            {
                                if (((TreeNode)itemNode).Text == nodeText)
                                {

                                    if (!nodeText.Contains("输出"))
                                    {
                                        ToolStripItem sourceFrom = rightClickMenu1.Items.Add("源于");
                                    }
                                    ToolStripItem deleteItem = rightClickMenu1.Items.Add("删除连接");
                                    deleteItem.Click += new EventHandler(DeleteItem);
                                    ToolStripItem copyNodeText = rightClickMenu1.Items.Add("复制节点文本");
                                    copyNodeText.Click += new EventHandler(CopyNodeText);
                                    deleteItem.Click += new EventHandler(DeleteItem);
                                    if (!nodeText.Contains("输出"))
                                    {
                                        ToolStripMenuItem item = rightClickMenu1.Items[0] as ToolStripMenuItem;
                                        ((ToolStripMenuItem)rightClickMenu1.Items[0]).DropDownItems.Clear();
                                        foreach (TreeNode toolNode1 in Project.GetJobTree(jobName).Nodes)
                                        {
                                            if (toolNode1.Text == fatherNodeText)        //不能指定自己的输出项为源
                                                continue;
                                            if (toolNode1.Text != "输出")
                                            {
                                                foreach (TreeNode itemNode1 in toolNode1.Nodes)
                                                {
                                                    string  type = itemNode1.Tag.ToString();
                                                    if (type == curNodeType)
                                                    {
                                                        if (((TreeNode)itemNode1).Text.Substring(0, 3) == "<--")
                                                        {
                                                            string resultStr = "《- "  + " " + itemNode1.Text.Substring(3);
                                                            ToolStripItem item1 = item.DropDownItems.Add(resultStr);
                                                            item1.Name = resultStr;
                                                            item1.Click += new EventHandler(SourceFrom);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                    DrawLine();
                    return;

                }
                if (test.Node == null || test.Location != TreeViewHitTestLocations.Label && e.Button == MouseButtons.Right)       //单击空白
                {
                    Project.GetJobTree(jobName).ContextMenuStrip = rightClickMenuAtBlank;
                    rightClickMenuAtBlank.Show(e.X, e.Y);
                    DrawLine();
                    return;
                }
                else
                {
                  //  Project.GetJobTree(jobName).ContextMenuStrip = rightClickMenu;
                }

                rightClickMenu1.Items.Clear();
                rightClickMenu1.Items.Add("运行");
                rightClickMenu1.Items[0].Click += new EventHandler(RunTool);
                rightClickMenu1.Items.Add("添加输入项");
                rightClickMenu1.Items.Add("添加输出项");
                rightClickMenu1.Items.Add("插入工具");
                rightClickMenu1.Items.Add("启用/忽略切换");
                rightClickMenu1.Items[4].Click += new EventHandler(EnableOrDisenableTool);
                rightClickMenu1.Items.Add("删除项");
                // rightClickMenu.Items[5].Image = Resources.DeleteItem;
                rightClickMenu1.Items[5].Click += new EventHandler(DeleteItem);
                rightClickMenu1.Items.Add("重命名");
                rightClickMenu1.Items[6].Click += new EventHandler(RenameTool);
                rightClickMenu1.Items.Add("修改备注");
                rightClickMenu1.Items[7].Click += new EventHandler(ModifyTipInfo);

                //如果不是第一个则添加上移选项
                TreeNode tree = Project.GetJobTree(jobName).GetNodeAt(e.X, e.Y);
                Project.GetJobTree(jobName).SelectedNode = tree;
                if (tree == null)
                    return;
                if (tree.Index != 0)
                {
                    rightClickMenu1.Items.Add("上移");
                    rightClickMenu1.Items[8].Click += new EventHandler(MoveUp);
                    if (tree.Index != Project.GetJobTree(jobName).Nodes.Count - 1)
                    {
                        rightClickMenu1.Items.Add("下移");
                        rightClickMenu1.Items[9].Click += new EventHandler(MoveDown);
                    }
                }
                else
                {
                    rightClickMenu1.Items.Add("下移");
                    rightClickMenu1.Items[8].Click += new EventHandler(MoveDown);
                }
               // rightClickMenu1.Show();
                //if (e.Button == MouseButtons.Right && e.Clicks == 1)        //如果右击
                //{
                //    AqToolType toolType = GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).ToolType;

                //    //清空输入，输出下拉选项
                //    ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Clear();
                //    ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Clear();
                //    Application.DoEvents();
                //    switch (toolType)
                //    {
                //        case AqToolType.HalconInterface:
                //            ToolStripItem toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("输出图像");
                //            // ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                //            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                //            toolStripItem.Click += new EventHandler(Add_output);
                //            break;
                //        default:
                //            break;
                //    }
                //    Project.GetJobTree(jobName).ContextMenuStrip = rightClickMenu;
                //    rightClickMenu.Show();
                //    DrawLine();
                //    //  Application.DoEvents();
                //}
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 流程树的双击事件
        /// </summary>
        internal void TVW_DoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TreeViewHitTestInfo test = Project.GetJobTree(jobName).HitTest(e.X, e.Y);
                if (test.Node == null || test.Location != TreeViewHitTestLocations.Label)       //双击节点
                {
                    if (jobTreeFold)
                    {
                        Project.GetJobTree(jobName).ExpandAll();
                        jobTreeFold = false;
                    }
                    else
                    {
                        Project.GetJobTree(jobName).CollapseAll();
                        jobTreeFold = true;
                    }

                    return;
                }
                AqProjectManger.Instance().SetTaskModule(test.Node.Text);

            }
            catch (Exception ex)
            {
             //   LogHelper.SaveErrorInfo(ex);
            }
        }

       
        /// <summary>
        /// 运行此流程
        /// </summary>
        public List<string> Run()
        {
            try
            {
                jobRunStatu = JobRunStatu.Fail;
                Project.GetJobTree(jobName).ShowNodeToolTips = true;
                Stopwatch jobElapsedTime = new Stopwatch();
                jobElapsedTime.Restart();
                recordElapseTime = 0;
                //////foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                //////{
                //////    toolNode.ForeColor = Color.Black;
                //////}

                //开始逐个执行各工具
                jobRunStatu = JobRunStatu.Fail;
                List<string> L_result = new List<string>();
                int toolIndex = -1;

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    toolIndex++;
                    TreeNode treeNode = GetToolNodeByNodeText(L_toolList[i].ToolName);
                    inputItemNum = (L_toolList[i]).Input.Count;
                    outputItemNum = (L_toolList[i]).Output.Count;

            
                    double elapseTime = jobElapsedTime.ElapsedMilliseconds - recordElapseTime;
                    recordElapseTime = jobElapsedTime.ElapsedMilliseconds;
                    treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n备注：{2}", "aaa", elapseTime, L_toolList[i].ToolTipInfo);
                    Application.DoEvents();
                }
                for (int i = toolIndex + 1; i < L_toolList.Count; i++)
                {
                    GetToolNodeByNodeText(L_toolList[i].ToolName).ForeColor = Color.Black;
                }

            

                Project.GetJobTree(jobName).SelectedNode = null;
                jobElapsedTime.Stop();
                double time = jobElapsedTime.ElapsedMilliseconds;
                //自动运行状态下结果不显示

                jobRunStatu = JobRunStatu.Succeed;
                Application.DoEvents();
                return L_result;
            }
            catch (Exception ex)
            {
               // LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }

    }
    public enum JobRunStatu
    {
        Succeed,
        Fail,
    }
}




