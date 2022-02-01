using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDI_Main
{
    /// <summary>
    /// 工具类型
    /// </summary>
    public enum AqToolType
    {
        None,
        HalconInterface,
        AIDI,
        Output
    }
    /// <summary>
    /// 工具的输入输出类
    /// </summary>
    public class AqToolIO
    {
        public AqToolIO() { }
        public AqToolIO(string IOName1, object value1, DataType ioType1)
        {
            this.IOName = IOName1;
            this.Value = value1;
            this.IoType = ioType1;
        }

        public string IOName;
        public object Value;
        public DataType IoType;
    }

    public enum DataType
    {
        String,
        Image,
        Point,
        Line,
        Circle,
        Pose,
        Int,
        Camrea

    }
}
