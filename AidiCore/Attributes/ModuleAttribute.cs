using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : Attribute
    {

        public ModuleAttribute(string moduleName, string moduleGroup, string infoMessage = "")
        {
            this.ModuleName = moduleName;
            this.ModuleGroup = moduleGroup;
            //如果没有输入，则默认infoMessage
            this.InfoMessage = ((infoMessage == "") ? moduleName : infoMessage);
        }

        //提示信息
        public string InfoMessage
        {
            get;
            private set;
        }

        //功能组
        public string ModuleGroup
        {
            get;
            private set;
        }

        //模块名字
        public string ModuleName
        {
            get;
            private set;
        }

    }
}
