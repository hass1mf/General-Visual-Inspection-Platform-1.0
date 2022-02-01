using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.DataType
{
   public class AqDataItem
    {
        public string DataName
        {
            get
            {
                return this._dataName;
            }
            set
            {
                this._dataName = value;
            }
        }

        public Type DataType
        {
            get
            {
                return this._dataType;
            }
            set
            {
                this._dataType = value;
            }
        }

        public string IndexDataName
        {
            get
            {
                return this._indexDataName;
            }
            set
            {
                this._indexDataName = value;
            }
        }

        public int IndexNum
        {
            get
            {
                return this._indexNum;
            }
            set
            {
                this._indexNum = value;
            }
        }

        public string InputDataName
        {
            get
            {
                return this._inputDataName;
            }
            set
            {
                this._inputDataName = value;
            }
        }

        //对应在变量名字
        private string _dataName = "";

        //类型
        private Type _dataType;

        //
        private string _indexDataName = "";

        //索引
        private int _indexNum = 0;

        //输入变量
        private string _inputDataName = "";
    }
}
