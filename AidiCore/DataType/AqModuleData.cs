using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AidiCore.Attributes;

namespace AidiCore.DataType
{
    public class AqModuleData
    {
        private List<AqLinkList> _linkDatas;

        public AqModuleData()
        {
            this.ModuleName = "";
            this.NodeName = "";
            this.NextNodeName = "";
            this._linkDatas = new List<AqLinkList>();
        }


        public AqModuleData(AqModuleData moduleData)
        {
            this.ModuleName = moduleData.ModuleName;
            this.NodeName = moduleData.NodeName;
            this.NextNodeName = moduleData.NextNodeName;
            this._linkDatas = new List<AqLinkList>();
            foreach (AqLinkList current in moduleData.LinkDatas)
            {
                AqLinkList item = new AqLinkList(current);
                this.LinkDatas.Add(item);
            }
        }

        public void Copy(AqModuleData moduleData)
        {
            this.ModuleName = moduleData.ModuleName;
            this.NodeName = moduleData.NodeName;
            this.NextNodeName = moduleData.NextNodeName;
            this._linkDatas = new List<AqLinkList>();
            foreach (AqLinkList current in moduleData.LinkDatas)
            {
                AqLinkList item = new AqLinkList(current);
                this.LinkDatas.Add(item);
            }
        }

        /// <summary>
        ///把所有Input添加到集合
        /// </summary>
        /// <param name="inputDataList"></param>
        /// <param name="types"></param>
        public void GetInputList(out List<IAqDataObj> inputDataList, params Type[] types)
        {
            inputDataList = new List<IAqDataObj>();
            Type type = base.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                Attribute customAttribute = propertyInfo.GetCustomAttribute(typeof(InputAttribute), false);
                if (customAttribute != null)
                {
                    if (types.Length == 0 || types.Contains(propertyInfo.PropertyType))
                    {
                        IAqDataObj item;
                        string _type = propertyInfo.PropertyType.ToString();
                        switch (propertyInfo.PropertyType.ToString())
                        {
                            case "System.String":
                                item = new AqOperType<Type>(AqDataTypeEnum.String, propertyInfo.PropertyType, propertyInfo.Name);
                                inputDataList.Add(item);
                                break;
                            case "System.Int32":
                                item = new AqOperType<Type>(AqDataTypeEnum.Int, propertyInfo.PropertyType, propertyInfo.Name);
                                inputDataList.Add(item);
                                break;
                            case "System.Drawing.Bitmap":
                                item = new AqOperType<Type>(AqDataTypeEnum.Bitmap, propertyInfo.PropertyType, propertyInfo.Name);
                                inputDataList.Add(item);
                                break;
                            case "AqCameraFactory.AbstractCamera":
                                item = new AqOperType<Type>(AqDataTypeEnum.AbstractCamera, propertyInfo.PropertyType, propertyInfo.Name);
                                inputDataList.Add(item);
                                break;
                            default:
                                item = new AqOperType<Type>(AqDataTypeEnum.None, propertyInfo.PropertyType, propertyInfo.Name);
                                inputDataList.Add(item);
                                break;

                        }
           
                    }
                }
            }
        }

        /// <summary>
        ///把所有output添加到集合
        /// </summary>
        /// <param name="inputDataList"></param>
        /// <param name="types"></param>
        public void GetOutputList(out List<IAqDataObj> outputDataList, params Type[] types)
        {
            outputDataList = new List<IAqDataObj>();
            Type type = base.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                Attribute customAttribute = propertyInfo.GetCustomAttribute(typeof(OutputAttribute), false);
                if (customAttribute != null)
                {
                    if (types.Length == 0 || types.Contains(propertyInfo.PropertyType))
                    {                       
                        IAqDataObj item;
                        switch (propertyInfo.PropertyType.ToString())
                        {
                            case "System.String":
                                item = new AqOperType<Type>(AqDataTypeEnum.String, propertyInfo.PropertyType, propertyInfo.Name);
                                outputDataList.Add(item);
                                break;
                            case "System.Int32":
                                item = new AqOperType<Type>(AqDataTypeEnum.Int, propertyInfo.PropertyType, propertyInfo.Name);
                                outputDataList.Add(item);
                                break;
                            case "System.Drawing.Bitmap":
                                item = new AqOperType<Type>(AqDataTypeEnum.Bitmap, propertyInfo.PropertyType, propertyInfo.Name);
                                outputDataList.Add(item);
                                break;
                            case "AqCameraFactory.AbstractCamera":
                                item = new AqOperType<Type>(AqDataTypeEnum.AbstractCamera, propertyInfo.PropertyType, propertyInfo.Name);
                                outputDataList.Add(item);
                                break;
                            default:
                                item = new AqOperType<Type>(AqDataTypeEnum.None, propertyInfo.PropertyType, propertyInfo.Name);
                                outputDataList.Add(item);
                                break;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get;
            set;
        }

        public string NextNodeName
        {
            get;
            set;
        }

        /// <summary>
        /// 实例化后的节点名称，节点名称不允许重复
        /// </summary>
        public string NodeName
        {
            get;
            set;
        }

        public List<AqLinkList> LinkDatas
        {
            get
            {
                if (this._linkDatas == null)
                {
                    this._linkDatas = new List<AqLinkList>();
                }
                return this._linkDatas;
            }
            set
            {
                this._linkDatas = value;
            }
        }
    }
}
