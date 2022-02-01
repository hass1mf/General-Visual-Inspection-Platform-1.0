using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDI_Main
{
    public partial class Frm_ConfirmBox : Form
    {
        public Frm_ConfirmBox()
        {
            InitializeComponent();
        }
        private static Frm_ConfirmBox _instance;
        internal ConfirmBoxResult result = ConfirmBoxResult.Confirm;

        public static Frm_ConfirmBox Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ConfirmBox();
                return _instance;
            }
        }
    }
    internal enum ConfirmBoxResult
    {
        Cancel,
        Confirm,
    }
}
