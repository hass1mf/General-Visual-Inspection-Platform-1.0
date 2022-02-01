using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AidiCore.Attributes;
using AidiCore.DataType;
using AidiCore.Interface;
using AidiCore.Manger;

namespace AidiCore.ProjectManger
{


    /// <summary>
    /// 一个流程一个ProjectData
    /// </summary>
    public class AqProjectDataType
    {

        public AqProjectDataType()
        {
            this.ModuleData = new Dictionary<string, AqModuleData>();
            this.ModuleInput = new Dictionary<string, string>();
            this.ModuleOutput = new Dictionary<string, string>();
            this.ImageSave = "";
        }


        
        public string AddModule(string moduleName, string displayName)
        {
            int num = 1;
            string text = displayName + num.ToString();
            while (this.ModuleData.ContainsKey(text))
            {
                num++;
                text = displayName + num.ToString();
            }
            AqModuleManger moduleManage = AqModuleManger.Instance();
            AqModuleData moduleData = moduleManage.CreateModuleData(moduleName);
            moduleData.ModuleName = moduleName;
            moduleData.NodeName = text;
            moduleData.NextNodeName = "";
            moduleData.LinkDatas.Clear();
            this.ModuleData.Add(text, moduleData);
            this.ModuleInput.Add(text, "Start");
            this.ModuleOutput.Add(text, "End");
            return text;
        }


        public void Add(string ModuleName, string NodeName)
        {
        
            AqModuleManger moduleManage = AqModuleManger.Instance();
            AqModuleData moduleData = moduleManage.CreateModuleData(ModuleName);
            moduleData.ModuleName = ModuleName;
            moduleData.NodeName = NodeName;
            moduleData.NextNodeName = "";
            moduleData.LinkDatas.Clear();
            this.ModuleData.Add(NodeName, moduleData);
            this.ModuleInput.Add(NodeName, "Start");
            this.ModuleOutput.Add(NodeName, "End");
        }


        /// <summary>
        /// 检测节点
        /// </summary>
        /// <returns></returns>
        public bool CheckProjectDataLink()
        {
            List<string> list;
            this.GetStartList(out list);
            bool result;
            foreach (string current in list)
            {
                List<string> list2;
                //在end中查是不是所有的都对应上
                this.GetTaskNodeList(current, out list2);
                AqModuleData moduleData = this.ModuleData[current];
                string text = moduleData.NodeName;
                while (moduleData != null)
                {
                    Type type = moduleData.GetType();
                    foreach (AqLinkList current2 in moduleData.LinkDatas)
                    {
                        bool flag = !list2.Contains(current2.NodeName);
                        if (flag)
                        {
                            MessageBox.Show("节点: " + text + " 的模块所在Task中不存在输入数据节点: " + current2.NodeName);
                            result = false;
                            return result;
                        }
                        bool flag2 = current2.ListNumNodeName == "";
                        if (!flag2)
                        {
                            bool flag3 = !list2.Contains(current2.ListNumNodeName);
                            if (flag3)
                            {
                                MessageBox.Show("节点: " + text + " 的模块所在Task中不存在数据索引节点: " + current2.ListNumNodeName);
                                result = false;
                                return result;
                            }
                        }
                    }
                    text = this.ModuleOutput[text];
                    bool flag4 = text == "End";
                    if (flag4)
                    {
                        break;
                    }
                    moduleData = this.ModuleData[text];
                }
            }
            return  true;
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="nodeNameList"></param>
        /// <returns></returns>
        public bool GetTaskNodeList(string startNode, out List<string> nodeNameList)
        {
            nodeNameList = new List<string>();
            bool flag = this.ModuleInput[startNode] != "Start";
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                string text = startNode;
                while (text != "End")
                {
                    nodeNameList.Add(text);
                    text = this.ModuleOutput[text];
                }
                result = true;
            }
            return result;
        }
       
        //判断几个task
        public void GetStartList(out List<string> startList)
        {
            startList = new List<string>();
            foreach (KeyValuePair<string, string> current in this.ModuleInput)
            {
                bool flag = current.Value == "Start";
                if (flag)
                {
                    startList.Add(current.Key);
                }
            }
        }


        //初始化 路径和数据
        public void CreateModuleInstance(string projectDir, AqProjectDataType projectData)
        {
            AqModuleManger moduleManage = AqModuleManger.Instance();
            foreach (KeyValuePair<string, AqModuleData> current in projectData.ModuleData)
            {
                string key = current.Key;
                //这个参数是只有nodename moudulname link 几个参数，类型不确定
                AqModuleData value = current.Value;
                //创建来了模块的类型
                if (value.ModuleName =="")
                {
                    MessageBox.Show("方案中节点名重复: " + projectData.ModuleData["SharpMatch1"].NodeName);

                }
                else
                {
                    AqModuleData moduleData = moduleManage.CreateModuleData(value.ModuleName);
                    //这个赋值参数

                    moduleData.Copy(value);
                    //装箱操作可优化
                    (moduleData as IModule).InitModule(projectDir, value.NodeName);
                    bool flag = this.ModuleData.ContainsKey(value.NodeName);
                    if (flag)
                    {
                        MessageBox.Show("方案中节点名重复: " + value.NodeName);
                    }
                    this.ModuleData.Add(key, moduleData);
                    this.ModuleInput.Add(key, projectData.ModuleInput[key]);
                    this.ModuleOutput.Add(key, projectData.ModuleOutput[key]);
                }
             
            }
        }


        public void ModuleInit(string projectDir, string ModuleName)
        {
            AqModuleManger moduleManage = AqModuleManger.Instance();
            foreach (KeyValuePair<string, AqModuleData> current in this.ModuleData)
            {
                if (current.Key == ModuleName)
                {
                    string key = current.Key;
                    //这个参数是只有nodename moudulname link 几个参数，类型不确定
                    //装箱操作可优化
                    (current.Value as IModule).InitModule(projectDir, current.Value.NodeName);
                }
                                 
            }
        }



        //
        private void GenInputName(string nodeName, Type currentType, Type listCurrenType, List<IAqDataObj> dataList, ref List<string> inputNameList)
        {
            foreach (IAqDataObj current in dataList)
            {

                if ((current.Value as Type) == listCurrenType)
                {
                    inputNameList.Add(nodeName + "." + current.DataName + "[i]");
                }
                else
                {
                    inputNameList.Add(nodeName + "." + current.DataName);
                }
            }
        }

        public void GetModuleInputList(string nodeName, out List<AqDataItem> dataConfigList)
        {
            dataConfigList = new List<AqDataItem>();
            //判断模块是否还在
            bool flag = this.ModuleData.ContainsKey(nodeName);
            if (flag)
            {
                AqModuleData moduleData = this.ModuleData[nodeName];
                List<IAqDataObj> list;
                moduleData.GetInputList(out list, new Type[0]);
                foreach (IAqDataObj current in list)
                {
                    AqDataItem dataItem = new AqDataItem();
                    dataItem.DataName = current.DataName;
                    dataItem.DataType = current.Value as Type;
                    foreach (AqLinkList current2 in moduleData.LinkDatas)
                    {
                        //判断输入是否存在
                        bool flag2 = current2.EndName == current.DataName;
                        if (flag2)
                        {
                            //判断是不是数组类型
                            bool isList = current2.IsList;
                            if (isList)
                            {

                                dataItem.InputDataName = current2.NodeName + "." + current2.StartName + "[i]";
                                dataItem.IndexNum = current2.ListNum;
                                bool flag3 = current2.ListNumNodeName != "";
                                if (flag3)
                                {
                                    dataItem.IndexDataName = current2.ListNumNodeName + "." + current2.ListNumValueName;
                                }
                            }
                            else
                            {
                                dataItem.InputDataName = current2.NodeName + "." + current2.StartName;
                            }
                            break;
                        }
                    }
                    dataConfigList.Add(dataItem);
                }
            }
        }

        //获取所有的input list
        public void GetInputNameList(string nodeName, Type currentType, out List<string> inputNameList, bool isExpandList = true, bool isSingleTask = true)
        {
            inputNameList = new List<string>();
            inputNameList.Add("");
         //   List<string> inputNameList1 = new List<string>();

            if (isSingleTask)
            {
                //读取泛型类型
                Type typeFromHandle = typeof(List<>);
                Type type = typeFromHandle.MakeGenericType(new Type[]
                {
                    currentType
                });
                string text = this.ModuleInput[nodeName];
                while (text != "Start")
                {
                    List<IAqDataObj> dataList;
                    //获取当前类型的集合， T list<T>
                    this.ModuleData[text].GetOutputList(out dataList, new Type[]
                    {
                        currentType,
                        type
                    });
                    this.GenInputName(text, currentType, type, dataList, ref inputNameList);
                    text = this.ModuleInput[text];
                }
                //foreach (var item in inputNameList1)
                //{
                //    inputNameList.Add(item);
                //}
                text = this.ModuleOutput[nodeName];
                while (text != "End")
                {
                    List<IAqDataObj> dataList2;
                    this.ModuleData[text].GetOutputList(out dataList2, new Type[]
                    {
                        currentType,
                        type
                    });
                    this.GenInputName(text, currentType, type, dataList2, ref inputNameList);
                    text = this.ModuleOutput[text];
                }
                //foreach (var item in inputNameList1)
                //{
                //    inputNameList.Add(item);
                //}
            }
        }

        private bool IsBringLoop(string sourceNodeName, string sinkNodeName)
        {
            string text = sinkNodeName;
            bool result;
            while (text != "End")
            {
                bool flag = text == sourceNodeName;
                if (flag)
                {
                    result = true;
                    return result;
                }
                text = this.ModuleOutput[text];
            }
            result = false;
            return result;
        }

        public bool Remove(string nodeName)
        {
            bool flag = this.ModuleData.ContainsKey(nodeName);
            bool result;
            if (flag)
            {
                AqModuleData moduleData = this.ModuleData[nodeName];
                Type type = moduleData.GetType();
                MethodInfo method = type.GetMethod("CloseModule");
                bool flag2 = method == null;
                if (flag2)
                {
                    MessageBox.Show("节点：" + moduleData.NodeName + "没有实现 CloseModule 接口,请添加该接口!!!");
                }
                else
                {
                    method.Invoke(moduleData, null);
                }
                this.ModuleData.Remove(nodeName);
                string text = this.ModuleInput[nodeName];
                string text2 = this.ModuleOutput[nodeName];
                this.ModuleInput.Remove(nodeName);
                this.ModuleOutput.Remove(nodeName);
                bool flag3 = text != "End" && text != "Start";
                if (flag3)
                {
                    this.ModuleOutput[text] = "End";
                }
                bool flag4 = text2 != "End" && text2 != "Start";
                if (flag4)
                {
                    this.ModuleInput[text2] = "Start";
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }


        //断成两半
        public bool RemoveConnection(string sourceNodeName, string sinkNodeName)
        {
            bool flag = this.ModuleData.ContainsKey(sourceNodeName) && this.ModuleData.ContainsKey(sinkNodeName);
            bool result;
            if (flag)
            {
                this.ModuleOutput[sourceNodeName] = "End";
                this.ModuleInput[sinkNodeName] = "Start";
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool SetConnection(string sourceNodeName, string sinkNodeName)
        {
            bool flag = this.ModuleData.ContainsKey(sourceNodeName) && this.ModuleData.ContainsKey(sinkNodeName);
            bool result;
            if (flag)
            {
                bool flag2 = this.IsBringLoop(sourceNodeName, sinkNodeName);
                if (flag2)
                {
                    MessageBox.Show("不允许连接，或造成死循环");
                    result = false;
                }
                else
                {
                    string text = this.ModuleInput[sinkNodeName];
                    string text2 = this.ModuleOutput[sourceNodeName];
                    bool flag3 = text != "End" && text != "Start";
                    if (flag3)
                    {
                        this.ModuleOutput[text] = "End";
                    }
                    bool flag4 = text2 != "End" && text2 != "Start";
                    if (flag4)
                    {
                        this.ModuleInput[text2] = "Start";
                    }
                    this.ModuleOutput[sourceNodeName] = sinkNodeName;
                    this.ModuleInput[sinkNodeName] = sourceNodeName;
                    result = true;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }


        //是否进行更改节点
        public bool UpdateNodeName(string oldName, string newName)
        {
            bool flag = this.ModuleData.ContainsKey(oldName) && !this.ModuleData.ContainsKey(newName);
            bool result;
            if (flag)
            {
                string text = this.ModuleInput[oldName];
                string text2 = this.ModuleOutput[oldName];
                AqModuleData moduleData = this.ModuleData[oldName];
                bool flag2 = text != "End" && text != "Start";
                if (flag2)
                {
                    this.ModuleOutput[text] = newName;
                }
                bool flag3 = text2 != "End" && text2 != "Start";
                if (flag3)
                {
                    this.ModuleInput[text2] = newName;
                }
              
                foreach (KeyValuePair<string, AqModuleData> item in this.ModuleData)
                {
                    foreach (var item1 in item.Value.LinkDatas)
                    {
                        if (item1.NodeName == oldName)
                        {
                            item1.NodeName = newName;
                        }
                        if (item1.StartName == oldName)
                        {
                            item1.StartName = newName;
                        }
                        if (item1.EndName == oldName)
                        {
                            item1.EndName = newName;
                        }
                    }                  
                }
                
                this.ModuleInput.Remove(oldName);
                this.ModuleInput.Add(newName, text);
                this.ModuleOutput.Remove(oldName);
                this.ModuleOutput.Add(newName, text2);
                this.ModuleData.Remove(oldName);
                this.ModuleData.Add(newName, moduleData);
                moduleData.NodeName = newName;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }



        public string ImageSave
        {
            get;
            set;
        }

 

        public Dictionary<string, AqModuleData> ModuleData
        {
            get;
            set;
        }

        public Dictionary<string, string> ModuleInput
        {
            get;
            set;    
        }

        public Dictionary<string, string> ModuleOutput
        {
            get;
            set;
        }
    }
}
