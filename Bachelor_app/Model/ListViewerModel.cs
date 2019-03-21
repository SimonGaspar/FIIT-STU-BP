using System;
using System.Collections.Generic;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.Model
{
    public class ListViewerModel
    {

        public Image<Bgr, Byte> _lastBasicStack { get; set; }
        public Image<Bgr, Byte> _lastCameraStack { get; set; }
        public Image<Bgr, Byte> _lastDepthMapImage { get; set; }
        public Image<Bgr, Byte> _lastDrawnKeypoint { get; set; }
        public Image<Bgr, Byte> _lastDrawnMatches { get; set; }
        public Image<Bgr, Byte> _lastImage { get; set; }



        public List<InputFileModel> BasicStack { get; set; }
        public List<InputFileModel> LeftCameraStack { get; set; }
        public List<InputFileModel> RightCameraStack { get; set; }
        public List<InputFileModel> DrawnKeypoint { get; set; }
        public List<InputFileModel> DrawnMatches { get; set; }
        public List<InputFileModel> DepthMap { get; set; }

        public List<List<InputFileModel>> ListOfListInputFolder { get; set; }

        public ListViewerModel()
        {
            BasicStack = new List<InputFileModel>();
            LeftCameraStack = new List<InputFileModel>();
            RightCameraStack = new List<InputFileModel>();
            DrawnKeypoint = new List<InputFileModel>();
            DrawnMatches = new List<InputFileModel>();
            DepthMap = new List<InputFileModel>();

            ListOfListInputFolder = new List<List<InputFileModel>>();
            ListOfListInputFolder.Add(BasicStack);
            ListOfListInputFolder.Add(LeftCameraStack);
            ListOfListInputFolder.Add(RightCameraStack);
            ListOfListInputFolder.Add(DrawnKeypoint);
            ListOfListInputFolder.Add(DrawnMatches);
            ListOfListInputFolder.Add(DepthMap);
        }
    }
}
