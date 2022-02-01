using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.DataType
{
   public class AqLinkList
    {

        public AqLinkList() : this("", "", "", false, 0, "", "")
        {
        }

        public AqLinkList(AqLinkList linkData) : this(linkData.NodeName, linkData.StartName, linkData.EndName, linkData.IsList, linkData.ListNum, linkData.ListNumNodeName, linkData.ListNumValueName)
        {
        }

        public AqLinkList(string nodeName, string startName, string endName, bool isList = false, int listNum = 0, string listNumNodeName = "", string listNumValueName = "")
        {
            this.NodeName = nodeName;
            this.StartName = startName;
            this.EndName = endName;
            this.IsList = isList;
            this.ListNum = listNum;
            this.ListNumNodeName = listNumNodeName;
            this.ListNumValueName = listNumValueName;
        }

        public void Copy(AqLinkList linkData)
        {
            this.NodeName = linkData.NodeName;
            this.StartName = linkData.StartName;
            this.EndName = linkData.EndName;
            this.IsList = linkData.IsList;
            this.ListNum = linkData.ListNum;
            this.ListNumNodeName = linkData.ListNumNodeName;
            this.ListNumValueName = linkData.ListNumValueName;
        }


        
        public string EndName
        {
            get;
            set;
        }

        public bool IsList
        {
            get;
            set;
        }

        public int ListNum
        {
            get;
            set;
        }

        public string ListNumNodeName
        {
            get;
            set;
        }

        public string ListNumValueName
        {
            get;
            set;
        }

        public string NodeName
        {
            get;
            set;
        }

        public string StartName
        {
            get;
            set;
        }


    }
}
