using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIDI_Main
{

    public class AqToolInfo
    {
        public AqToolInfo()
        {
            Enable = true;
            ToolType = AqToolType.None;
            ToolName = string.Empty;
            Tag = new object();
            Input = new List<AqToolIO>();
            Output = new List<AqToolIO>();
        }

        /// <summary>
        /// 工具是否启用
        /// </summary>
        public bool Enable;
        /// <summary>
        /// 工具名称
        /// </summary>
        public string ToolName;
        /// <summary>
        /// 工具类型
        /// </summary>
        public AqToolType ToolType;
        /// <summary>
        /// 工具对象
        /// </summary>
        public object Tag;
        /// <summary>
        /// 工具描述信息
        /// </summary>
        public string ToolTipInfo = string.Empty;
        /// <summary>
        /// 工具输入字典集合
        /// </summary>
        public List<AqToolIO> Input;
        /// <summary>
        /// 工具输出字典集合
        /// </summary>
        public List<AqToolIO> Output;


        /// <summary>
        /// 以IO名获取IO对象
        /// </summary>
        /// <param name="IOName"></param>
        /// <returns></returns>
        public AqToolIO GetInput(string IOName)
        {
            for (int i = 0; i < Input.Count; i++)
            {
                if (Input[i].IOName == IOName)
                    return Input[i];
            }
            return new AqToolIO();
        }
        /// <summary>
        /// 以IO名获取IO对象
        /// </summary>
        /// <param name="IOName"></param>
        /// <returns></returns>
        public AqToolIO GetOutput(string IOName)
        {
            for (int i = 0; i < Output.Count; i++)
            {
                if (Output[i].IOName == IOName)
                    return Output[i];
            }
            return new AqToolIO();
        }
        /// <summary>
        /// 移除工具输入项
        /// </summary>
        /// <param name="IOName"></param>
        public void RemoveInputIO(string IOName)
        {
            for (int i = 0; i < Input.Count; i++)
            {
                if (Input[i].IOName == ToolName)
                    Input.RemoveAt(i);
            }
        }
        /// <summary>
        /// 移除工具输出项
        /// </summary>
        /// <param name="IOName"></param>
        public void RemoveOutputIO(string IOName)
        {
            for (int i = 0; i < Output.Count; i++)
            {
                if (Output[i].IOName == ToolName)
                    Output.RemoveAt(i);
            }
        }

    }
}
