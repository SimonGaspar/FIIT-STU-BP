using Bachelor_app.Model;
using Bakalárska_práca;
using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.Manager
{
    public class CameraManager
    {
        public List<KeyValuePair<int, string>> ListCamerasData;
        private MainForm _winForm;
        public CameraModel LeftCamera = new CameraModel();
        public CameraModel RightCamera = new CameraModel();

        public CameraManager(MainForm WinForm)
        {
            this._winForm = WinForm;
            GetListOfWebCam();
        }

        public void GetListOfWebCam() {
            ListCamerasData = new List<KeyValuePair<int, string>>();

            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }

            ////-> clear the combobox
            //ComboBoxCameraList.DataSource = null;
            //ComboBoxCameraList.Items.Clear();

            ////-> bind the combobox
            //ComboBoxCameraList.DataSource = new BindingSource(ListCamerasData, null);
            //ComboBoxCameraList.DisplayMember = "Value";
            //ComboBoxCameraList.ValueMember = "Key";
        }

        public void SetCameraID(CameraModel cameraModel, int ID, string name)
        {
            cameraModel.ID = ID;
            cameraModel.Name = name;
            cameraModel.CreateCameraInstance();
            //cameraModel.camera.QueryFrame().Save($@"D:\Downloads\{ID}.JPG");
        }
    }
}
