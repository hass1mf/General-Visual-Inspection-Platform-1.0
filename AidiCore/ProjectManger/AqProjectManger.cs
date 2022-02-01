using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AidiCore.DataType;
using AidiCore.Interface;
using AqVision.Graphic.AqVision.shape;
using Newtonsoft.Json;

namespace AidiCore.ProjectManger
{
    public class AqProjectManger
    {

        public AqProjectManger(AqProjectDataType aqProjectManger)
        {

            ProjectData = aqProjectManger;

        }
        private AqProjectManger()
         {
            //工程名字
            this.ProjectDirection = "";
            //工程存储的地址
            this.ProjectSaveDirection = "";
            //工程的名字
            this.ProjectName = "";
            //这个工程是否修改
            this.IsModified = false;
            //
            this._startList = new List<string>();
            //运行状态
            this._runState = RunState.stopState;        
            this._fileName = "";
            //任务队列
            this.ProjectData = new AqProjectDataType();


        }
       //清除所有方案
        private void ClearProjectName()
        {
            this.ProjectDirection = "";
            this.ProjectName = "";
            this.ProjectSaveDirection = "";
            this._fileName = "";
        }
        public void Add(string ModuleName, string NodeName)
        {
             this.ProjectData.Add(ModuleName, NodeName);
        }

        public bool ContainsKey(string key)
        {
          return  this.ProjectData.ModuleData.ContainsKey(key);
        }
        //关闭工程
        public void CloseProject()
        {
            foreach (AqModuleData current in this.ProjectData.ModuleData.Values)
            {
                Type type = current.GetType();
                MethodInfo method = type.GetMethod("CloseModule");
                bool flag = method == null;
                if (flag)
                {
                    MessageBox.Show("节点：" + current.NodeName + "没有实现接口CloseModule，请添加该接口!!!");
                }
                else
                {
                    method.Invoke(current, null);
                }
            }
            this.ProjectData.ModuleData.Clear();
            this.ProjectData.ModuleInput.Clear();
            this.ProjectData.ModuleOutput.Clear();
            this.ProjectDirection = "";
            this.ProjectName = "";
            this.ProjectSaveDirection = "";
            this.IsModified = false;
        }
        //单例模式
        public static AqProjectManger Instance()
        {
            bool flag = AqProjectManger._instance == null;
            if (flag)
            {
                AqProjectManger._instance = new AqProjectManger();
            }
            bool flag2 = AqProjectManger._instance == null;
            if (flag2)
            {
                throw new Exception("不能创建ProjectManage对象！");
            }
            return AqProjectManger._instance;
        }
        //添加project
        public string AddModule(string moduleName, string displayName)
        {
            this.IsModified = true;
            return this.ProjectData.AddModule(moduleName, displayName);
        }
  
        //获取输入列表
        public void GetIndexNameList(string nodeName, out List<string> indexNameList)
        {
            this.GetInputNameList(nodeName, typeof(int), out indexNameList, false, true);
        }
        //得到所有的输入
        public void GetInputNameList(string nodeName, Type currentType, out List<string> inputNameList, bool isExpandList = true, bool isSingleTask = true)
        {
            this.ProjectData.GetInputNameList(nodeName, currentType, out inputNameList, isExpandList, isSingleTask);
        }
        //更改节点名字
        public bool UpdateNodeName(string oldName, string newName)
        {
            bool flag = this.ProjectData.UpdateNodeName(oldName, newName);
            this.IsModified |= flag;
            return flag;
        }
        //获取输入输出列表用于显示
        public void GetModuleInputList(string nodeName, out List<AqDataItem> dataConfigList)
        {
            this.ProjectData.GetModuleInputList(nodeName, out dataConfigList);
        }
        //单一模块获取当前模块结果
        private void GetModuleResult(AqModuleData module, out AqModuleResult moduleResult)
        {
            moduleResult = new AqModuleResult();
            moduleResult.ModuleName = module.ModuleName;
            moduleResult.NodeName = module.NodeName;
            moduleResult.RunNum = 1;
            Type type = module.GetType();
            PropertyInfo property = type.GetProperty("Bitmap");
            bool flag = property == null || property.PropertyType != typeof(Bitmap);
            if (!flag)
            {               
                    Bitmap bitmap;
                    try
                    {
                        bitmap = (property.GetValue(module) as Bitmap);
                    }
                    catch
                    {
                        return;
                    }
                    bool flag4 = bitmap != null;
                    if (flag4)
                    {
                        moduleResult.DisplayBitmap = (bitmap.Clone() as Bitmap);
                    }
              }           
            PropertyInfo property1 = type.GetProperty("DisplayShapes");
            bool flag5 = property1 == null || property.PropertyType != typeof(Bitmap);
            if (!flag5)
            {
                List<AqShap> list = (List<AqShap>)property1.GetValue(module);
                bool flag13 = list != null && list.Count != 0;
                if (flag13)
                {
                    foreach (AqShap current in list)
                    {
                        moduleResult.DisplayShapes.Add(current);
                    }
                }
            }
        }
        //设置路径
        private void GetProjectName(string fileName)
        {
            this.ProjectDirection = Path.GetDirectoryName(fileName);
            this.ProjectName = Path.GetFileNameWithoutExtension(fileName);
            this.ProjectSaveDirection = this.ProjectDirection + "\\" + this.ProjectName;
            this._fileName = fileName;
        }
        //初始化task任务
        private void InitTaskResult( string startNodeName, out AqTaskResult taskResult)
        {
            taskResult = new AqTaskResult();
            List<string> list;
            this.ProjectData.GetTaskNodeList(startNodeName, out list);
            foreach (string current in list)
            {
                bool flag = !taskResult.ModuleResultDictionary.ContainsKey(current);
                if (flag)
                {
                    //单一模块对应一个结果
                    AqModuleResult moduleResult = new AqModuleResult();
                    moduleResult.NodeName = current;
                    moduleResult.ModuleName = this.ProjectData.ModuleData[current].ModuleName;
                    taskResult.ModuleResultDictionary.Add(current, moduleResult);
                }
            }
        }

        public bool IsRunning()
        {
            return this._runState > RunState.stopState;
        }

        public bool Remove(string nodeName)
        {
            bool flag = this.ProjectData.Remove(nodeName);
            this.IsModified |= flag;
            return flag;
        }

        public bool RemoveConnection(string sourceNodeName, string sinkNodeName)
        {
            bool flag = this.ProjectData.RemoveConnection(sourceNodeName, sinkNodeName);
            this.IsModified |= flag;
            return flag;
        }

        public void RunCircleTasks()
        {
            bool flag = this._runState == RunState.stopState;
            if (flag)
            {
                this._runState = RunState.triggerCircle;
            }
        }
        public AqTaskResult taskResult;

        private void RunOnce(string startNodeName)
        {
            //AqTaskResult taskResult;
            this.InitTaskResult(startNodeName, out taskResult);
            AqModuleData moduleData = this.ProjectData.ModuleData[startNodeName];
            string text = moduleData.NodeName;
            try
            {
                while (moduleData != null)
                {
                    Type type = moduleData.GetType();
                    bool flag = !this.UpdateLinkData(text);
                    if (flag)
                    {
                        //return;
                    }
                    MethodInfo method = type.GetMethod("Run");
                    method.Invoke(moduleData, null);
                    AqModuleResult moduleResult;
                    this.GetModuleResult(moduleData, out moduleResult);
                    bool flag2 = taskResult.ModuleResultDictionary.ContainsKey(moduleResult.NodeName);
                    if (flag2)
                    {
                        taskResult.ModuleResultDictionary[moduleResult.NodeName].MergeResult(moduleResult);
                    }
                    else
                    {
                        taskResult.ModuleResultDictionary.Add(moduleResult.NodeName, moduleResult);
                    }
                    string text2 = moduleData.NextNodeName;
                    bool flag3 = text2 == "";
                    if (flag3)
                    {
                        text2 = this.ProjectData.ModuleOutput[text];
                    }
                    bool flag4 = text2 == "End";
                    if (flag4)
                    {
                        break;
                    }
                    text = text2;
                    moduleData = this.ProjectData.ModuleData[text];
                }
               // this._taskResultQueue.Enqueue(taskResult);
            }
            catch (Exception ex)
            {
               MessageBox.Show("任务运行时异常 --->>>  节点：" + text + "\n异常信息：" + ex.ToString());
                throw new Exception("Exception: RunOnce");
            }
        }

        private bool UpdateLinkData(string nodeName)
        {
            //图像
            bool flag = this.ProjectData.ModuleData.ContainsKey(nodeName);
            bool result;
            if (!flag)
            {
                result = false;
            }
            else
            {
                //
                AqModuleData current_moduleData = this.ProjectData.ModuleData[nodeName];
                Type current_type = current_moduleData.GetType();
                //下个节点
                foreach (AqLinkList current_nextcon in current_moduleData.LinkDatas)
                {
                    AqModuleData nextcon_moduleData = this.ProjectData.ModuleData[current_nextcon.NodeName];
                    Type nextcon_type = nextcon_moduleData.GetType();
                    //找到节点是输入端
                    PropertyInfo property = current_type.GetProperty(current_nextcon.EndName);
                    bool flag2 = property == null;
                    if (flag2)
                    {
                        MessageBox.Show("节点: " + nodeName + " 的模块中无该属性: " + current_nextcon.EndName);
                        result = false;
                        return result;
                    }
                    //获取需要输入类型
                    Type propertyType = property.PropertyType;
                    property = nextcon_type.GetProperty(current_nextcon.StartName);
                    bool flag3 = property == null;
                    if (flag3)
                    {
                        MessageBox.Show("节点: " + current_nextcon.NodeName + " 的模块中无该属性: " + current_nextcon.StartName);
                        result = false;
                        return result;
                    }
                    //获取需要输出的类型
                    Type propertyType2 = nextcon_type.GetProperty(current_nextcon.StartName).PropertyType;
                    object value = nextcon_type.GetProperty(current_nextcon.StartName).GetValue(nextcon_moduleData);
                    bool isList = current_nextcon.IsList;
                    //判断是否是集合
                    if (isList)
                    {
                        bool flag4 = current_nextcon.ListNumNodeName == "";
                        int num;
                        if (flag4)
                        {
                            num = current_nextcon.ListNum;
                        }
                        else
                        {
                            bool flag5 = !this.ProjectData.ModuleData.ContainsKey(current_nextcon.ListNumNodeName);
                            if (flag5)
                            {
                                MessageBox.Show("节点: " + current_nextcon.NodeName + " 的模块所需的List节点不存在: " + current_nextcon.ListNumNodeName);
                                result = false;
                                return result;
                            }
                            AqModuleData moduleData3 = this.ProjectData.ModuleData[current_nextcon.ListNumNodeName];
                            Type type3 = moduleData3.GetType();
                            Type propertyType3 = type3.GetProperty(current_nextcon.ListNumValueName).PropertyType;
                            bool flag6 = propertyType3 != typeof(int);
                            if (flag6)
                            {
                                MessageBox.Show("节点: " + nodeName + " 的List Num不是整型数据");
                                result = false;
                                return result;
                            }
                            object value2 = type3.GetProperty(current_nextcon.ListNumValueName).GetValue(moduleData3);
                            num = ((value2 is int) ? ((int)value2) : 0);
                        }
                        //获取输出类型
                        Type right = propertyType2.GetGenericArguments()[0];
                        //如果输入等于null
                        bool flag7 = value != null;
                        if (flag7)
                        {
                            
                            int num2 = (int)propertyType2.GetProperty("Count").GetValue(value, null);
                            bool flag8 = num >= 0 && num < num2;
                            if (flag8)
                            {
                                //集合则获取item
                                object value3 = propertyType2.GetProperty("Item").GetValue(value, new object[]
                                {
                                    num
                                });
                                //判断输入输出是否一致
                                bool flag9 = propertyType != right;
                                if (flag9)
                                {
                                    MessageBox.Show("节点: " + nodeName + " 的List赋值的属性类型不一致");
                                    result = false;
                                    return result;
                                }
                                //设置集合
                                current_type.GetProperty(current_nextcon.EndName).SetValue(current_moduleData, value3);
                            }
                        }
                    }
                    else
                    {
                        bool flag10 = propertyType != propertyType2;
                        if (flag10)
                        {
                            MessageBox.Show("节点: " + nodeName + " 的赋值的属性类型不一致");
                            result = false;
                            return result;
                        }
                        current_type.GetProperty(current_nextcon.EndName).SetValue(current_moduleData, value);
                    }
                }
                result = true;
            }
            return result;
        }

        private void RunOneTask(object obj)
        {
            try
            {
                string text = (string)obj;
                this.RunOnce(text);
            }
            catch
            {
                
                
                throw new Exception("Exception: RunOneTask");
            }
        }

        public void RunTasks()
        {
            this._startList.Clear();
            this.ProjectData.GetStartList(out this._startList);
            System.Threading.RegisteredWaitHandle rhw = null;
            foreach (string item in _startList)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunOneTask), item);
            }
            rhw = System.Threading.ThreadPool.RegisterWaitForSingleObject(new System.Threading.AutoResetEvent(false), new System.Threading.WaitOrTimerCallback((obj, b) =>
            {
                int workerThreads = 0;
                int maxWordThreads = 0;
                //int  
                int compleThreads = 0;
                System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out compleThreads);
                System.Threading.ThreadPool.GetMaxThreads(out maxWordThreads, out compleThreads);
                //Console.WriteLine(workerThreads);
                //Console.WriteLine(maxWordThreads);
                //当可用的线数与池程池最大的线程相等时表示线程池中所有的线程已经完成 
                if (workerThreads == maxWordThreads)
                {
                    //当执行此方法后CheckThreadPool将不再执行 
                    rhw.Unregister(null);
                    //此处加入所有线程完成后的处理代码
                    rhw = null;
                }
            }), null, 100, false);
            while (rhw != null) ;
        }

        public bool IsModified
        {
            get;
            private set;
        }

        public bool SetConnection(string sourceNodeName, string sinkNodeName)
        {
            bool flag = this.IsRunning();
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.ProjectData.SetConnection(sourceNodeName, sinkNodeName);
                this.IsModified |= flag2;
                result = flag2;
            }
            return result;
        }

        public void SetTaskModule(string nodeName)
        {
    
                bool flag2 = this.ProjectData.ModuleData.ContainsKey(nodeName);
                if (flag2)
                {
                    bool flag3 = this.ProjectData.CheckProjectDataLink();
                    if (flag3)
                    {
                        //更新数据
                        this.UpdateLinkData(nodeName);
                    }
                    AqModuleData moduleData = this.ProjectData.ModuleData[nodeName];
                    bool flag4 = moduleData is IModule;
                    if (flag4)
                    {
                        (moduleData as IModule).StartSetForm();
                        this.IsModified = true;
                    }
                }
            
        }

        private bool TryLoadProjectMsg(string fileName, out AqProjectDataType projectData)
        {
            using (StreamReader streamReader = File.OpenText(fileName))
            {
                try
                {
                    JsonSerializer jsonSerializer = JsonSerializer.Create(null);
                    projectData = (jsonSerializer.Deserialize(streamReader, typeof(AqProjectDataType)) as AqProjectDataType);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开方案时异常，方案文件存在问题：" + ex.Message);
                    projectData = null;
                }
            }
            bool flag = projectData == null;
            return !flag;
        }

        public bool TryOpenProject(string fileName)
        {
            bool flag = this._runState > RunState.stopState;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = !File.Exists(fileName);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    AqProjectDataType projectDataType;
                    bool flag3 = !this.TryLoadProjectMsg(fileName, out projectDataType);
                    if (flag3)
                    {
                        result = false;
                    }
                    else
                    {
                        this.GetProjectName(fileName);
                        this.ProjectData.CreateModuleInstance(this.ProjectSaveDirection, projectDataType);
                        this.IsModified = false;
                        result = true;
                    }
                }
            }
            return result;
        }

 
        public bool TryStopTasks()
        {
            bool flag = this._runState == RunState.singleRunState || this._runState == RunState.circleRunState;
            if (flag)
            {
                this._runState = RunState.tryStopState;
            }
            return this._runState == RunState.stopState;
        }


        public void UpdateModuleInputList(string nodeName, List<AqDataItem> dataConfigList)
        {
            bool flag = this.ProjectData.ModuleData.ContainsKey(nodeName);
            if (flag)
            {
                this.IsModified = true;
                AqModuleData moduleData = this.ProjectData.ModuleData[nodeName];
                moduleData.LinkDatas.Clear();
                foreach (AqDataItem current in dataConfigList)
                {
                    //如果等于“”则没有对应关系，变量未被使用
                    bool flag2 = current.InputDataName == "";
                    if (!flag2)
                    {
                        AqLinkList linkData = new AqLinkList();
                        linkData.EndName = current.DataName;
                        string text = current.InputDataName;
                        bool flag3 = text.Contains("[i]");
                        //判断是否是集合类型
                        if (flag3)
                        {
                            text = text.Substring(0, text.Length - 3);
                            linkData.IsList = true;
                        }
                        else
                        {
                            linkData.IsList = false;
                        }
                        //把模块和输入类型分开，分类赋值
                        string[] array = text.Split(new char[]
                        {
                            '.'
                        });
                        bool flag4 = array.Length == 2;
                        if (flag4)
                        {
                            linkData.NodeName = array[0];
                            linkData.StartName = array[1];
                            bool isList = linkData.IsList;
                            //如果不是集合则不再继续处理
                            if (isList)
                            {
                                //是集合的话，索引是多少
                                linkData.ListNum = current.IndexNum;
                                string indexDataName = current.IndexDataName;
                                bool flag5 = indexDataName != "";
                                if (flag5)
                                {
                                    string[] array2 = indexDataName.Split(new char[]
                                    {
                                        '.'
                                    });
                                    bool flag6 = array2.Length == 2;
                                    if (!flag6)
                                    {
                                        continue;
                                    }
                                    linkData.ListNumNodeName = array2[0];
                                    linkData.ListNumValueName = array2[1];
                                }
                            }
                            moduleData.LinkDatas.Add(linkData);
                        }
                    }
                }
            }
        }

        public AqProjectDataType ProjectData
        {
            get
            {

                return _mprojectData;
            }
            set
            {
                _mprojectData = value;
            }
        }


        AqProjectDataType _mprojectData = null;
        

        
        //工程的地址
        public string ProjectDirection
        {
            get;
            set;
        }

        //工程的名字
        public string ProjectName
        {
            get;
            set;
        }


        //工程存储的地址
        public string ProjectSaveDirection
        {
            get;
            set;
        }

        private string _fileName;

        private static AqProjectManger _instance = null;



        private RunState _runState;

        private List<string> _startList;

        private enum RunState
        {
            stopState,
            triggerCircle,
            triggerSingle,
            circleRunState,
            singleRunState,
            tryStopState
        }





    }
}
