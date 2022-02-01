using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AidiCore.Manger;
using AidiCore.DataType;
using AidiCore.ProjectManger;
namespace AIDI
{
     partial class Form1 : Form
    {

        AqProjectManger _mProject = null;
        public Form1()
        {
            InitializeComponent();
            _mManger = AqModuleManger.Instance();
            foreach (KeyValuePair<string, Type> current in _mManger.TypeList)
            {
                comboBox5.Items.Add(current.Key);
                comboBox6.Items.Add(current.Value.ToString());
            }
            foreach (var item in _mManger.ModuleNameList)
            {
                label1.Text = "ModuleName:" + item.ModuleName;
                label2.Text = "ModuleGroup:" + item.ModuleGroup;
                label3.Text = "InfoMessage:" + item.InfoMessage;
            }
            AqModuleData _mModData = _mManger.CreateModuleData("AqCalibra");
            List<IAqDataObj> aqDataObjs = new List<IAqDataObj>();
            _mModData.GetInputList(out aqDataObjs, new Type[0]);

            aqDataObjs.ForEach((i) =>
            {
                comboBox1.Items.Add(i.DataName);

                comboBox2.Items.Add((i.Value as Type).ToString());

            }
            );

            _mModData.GetOutputList(out aqDataObjs, new Type[0]);
            aqDataObjs.ForEach((i) =>
            {
                comboBox3.Items.Add(i.DataName);

                comboBox4.Items.Add((i.Value as Type).ToString());

            }
          );
         //   AqProjectManger.Instance().AddModule("AqCamera", "Camera1");
            AqProjectManger.Instance().ProjectData.Add("AqCamera", "Camera1");
            _mProject.SetTaskModule("Camera1");

            AqProjectManger.Instance().ProjectData.ModuleInit("", "Camera1");


            _mProject = AqProjectManger.Instance();
            AqProjectDataType aqProjectDataType = GetDefaultProjectData();
            _mProject.ProjectData.CreateModuleInstance("", aqProjectDataType);
            List<AqDataItem> aqData;
            //获取输入与输出连接



            _mProject.GetModuleInputList("SharpMatch1", out aqData);
             aqData.ForEach((i) => { this.listBox1.Items.Add(i.DataName+"  "+i.DataType.ToString() + "  "+i.InputDataName + "  " +i.IndexDataName); });
             List<string> aqlist;
            //当前节点到之后的所有节点的output输出
             _mProject.GetInputNameList("Calibra1", typeof(string), out aqlist);
             aqlist.ForEach((i) => { this.listBox1.Items.Add(i); });
             _mProject.GetIndexNameList("Calibra1", out aqlist);
             aqlist.ForEach((i) => { this.listBox1.Items.Add(i); });
             _mProject.UpdateModuleInputList("SharpMatch1", aqData);
            List<AqDataItem> aqData1 = new List<AqDataItem>();
            AqDataItem aqDataItem = new AqDataItem();            
            _mProject.AddModule("AqShape", "SharpMatch1");
            //被插入的接在插入
            _mProject.SetConnection("Calibra1", "SharpMatch11");
            aqDataItem.DataName = "ImageIn";
            aqDataItem.DataType = typeof(string);
            aqDataItem.InputDataName = "Calibra1.S5";
            aqData1.Add(aqDataItem);
            //更改模块之间的链接问题
            _mProject.UpdateModuleInputList("SharpMatch11", aqData1);
            
            //双击启动
            //_mProject.SetTaskModule
            //修改节点名字
            _mProject.UpdateNodeName("Camera1", "Camera12");
            //  _mProject.SetTaskModule
            //   _mProject.UpdateModuleInputList
            _mProject.SetTaskModule("Camera12");
            _mProject.RunTasks();
            _mProject.TryOpenProject("");
            aqDisplay1.Image = _mProject.taskResult.ModuleResultDictionary["Camera12"].DisplayBitmap;
            aqDisplay1.FitToScreen();
        }

        private AqProjectDataType GetDefaultProjectData()
        {
            AqProjectDataType projectData = new AqProjectDataType();
            List<AqModuleData> ModuleDatas = new List<AqModuleData>();
            ModuleDatas.Add(new AqModuleData
            {
                ModuleName = "AqCamera",
                NodeName = "Camera1"
            });
            List<AqLinkList> link = new List<AqLinkList>
            {
                new AqLinkList("Camera1", "ImageOut", "ImageIn", false, 0)
            };
            ModuleDatas.Add(new AqModuleData
            {
                ModuleName = "AqShape",
                NodeName = "SharpMatch1",
                LinkDatas = link
            });
            List<AqLinkList> link2 = new List<AqLinkList>
            {
                new AqLinkList("SharpMatch1", "S5", "String3", false, 0)
            };
            ModuleDatas.Add(new AqModuleData
            {
                ModuleName = "AqCalibra",
                NodeName = "Calibra1",
                LinkDatas = link2
            });
            foreach (AqModuleData item in ModuleDatas)
            {
                AqModuleData aqModuleDatatemp = _mManger.CreateModuleData(item.ModuleName);
                aqModuleDatatemp.Copy(item);
                projectData.ModuleData.Add(item.NodeName, aqModuleDatatemp);

            }
            projectData.ModuleInput.Add("Camera1", "Start");
            projectData.ModuleInput.Add("SharpMatch1", "Camera1");
            projectData.ModuleInput.Add("Calibra1", "SharpMatch1");

            projectData.ModuleOutput.Add("Camera1", "SharpMatch1");
            projectData.ModuleOutput.Add("SharpMatch1", "Calibra1");
            projectData.ModuleOutput.Add("Calibra1", "End");

            return projectData;
        }



        AqModuleManger _mManger = null;
    }

}
