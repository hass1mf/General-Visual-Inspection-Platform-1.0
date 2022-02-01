using AIDI_Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AIDI_Main
{
    public partial class Frm_Message : DockContent
    {
        private Frm_Message()
        {
            InitializeComponent();
            this.Icon = Resources.Output;
        }


        static Frm_Message _Instance = null;
        public static Frm_Message Instance()
        {

                if (_Instance == null)
                {
                    _Instance = new Frm_Message();
                    return _Instance;
                }
                else
                {
                    return _Instance;

                }
            
          
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">信息内容</param>
        /// <param name="color">颜色显示</param>
        public void OutputMsg(string msg, Color color)
        {
            try
            {
                listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - 10;
                ListViewItem item = new ListViewItem();
                item.Text = DateTime.Now.ToString("HH:mm:ss");
                item.SubItems.Add(msg);
                item.ForeColor = color;
                listView1.Items.Insert(0, item);
                if (listView1.Items.Count > 100)
                    listView1.Items.RemoveAt(100);
            }
            catch (Exception ex)
            {

            }
        }


        private void Frm_Output_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instance = null;
        }
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

    }
}
