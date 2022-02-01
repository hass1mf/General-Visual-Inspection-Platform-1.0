using AqVision.Controls;
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
    public partial class Frm_Disp : DockContent
    {
        public Frm_Disp()
        {
            InitializeComponent();
            _mtimer.Interval = 100;
            _mtimer.Tick += new EventHandler(UpData);
            //_mtimer.Start();
        }
        static Frm_Disp _disp = new Frm_Disp();

        public static Frm_Disp Instance()
        {

            return _disp;
        }



        Timer _mtimer = new Timer();

     public   void UpData(object sender, EventArgs e)
        {
            aqDisplay1.Update();


        }


        public AqDisplay AqDisplay
        {
            get 
            {
                return aqDisplay1;
            }
        }
    }
}
