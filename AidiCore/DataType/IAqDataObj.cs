using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.DataType
{
    public interface IAqDataObj
    {
        AqDataTypeEnum Type { get; }
        Object Value { get; }
        string DataName { get; }
    }


 

    public struct AqOperType<T> : IAqDataObj
    {
        public AqOperType(AqDataTypeEnum type, T value,string dataname)
        {
            _type = type;
            _value = value;
            _dataname = dataname;
        }



        private AqDataTypeEnum _type;
        private T _value;
        private string _dataname;


        /// <summary>
        /// 获取操作名字
        /// </summary>
        public string DataName
        {
            get { return _dataname; }

        }

        /// <summary>
        /// 获取操作数值
        /// </summary>
        public T TValue
        {
            get { return _value; }
        }

        #region IOperand 成员

        /// <summary>
        /// 获取操作数类型
        /// </summary>
        public AqDataTypeEnum Type
        {
            get
            {
                return _type;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }

     
        #endregion

        public override string ToString()
        {


            if (Type != AqDataTypeEnum.Bitmap)
            {
                return _value.ToString();
            }
            else
            {
                return "bitmap";
            }
        }
    }


  
}
