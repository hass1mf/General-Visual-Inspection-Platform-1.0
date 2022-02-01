using AqCameraFactory;
using System;

namespace AqDaHengCamera
{
    public partial class DaHengSetForm : AbstractSetForm
    {
        public DaHengSetForm(CameraClient aqDaHeng)
        {
            _module = aqDaHeng;
            _abstractCamera = aqDaHeng.AbstractCamera;

            InitializeComponent();
            buttonCameraNameRefresh_Click(null, null);
            InitializeSet();
        }

    }
}