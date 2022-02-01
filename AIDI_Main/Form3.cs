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
    public partial class Form3 : DockContent
    {
        public Form3()
        {
            InitializeComponent();
            this.Icon = Resources.Monitor;
        }
    }
}
