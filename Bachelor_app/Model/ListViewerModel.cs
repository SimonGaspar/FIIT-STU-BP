using System;
using System.Collections.Generic;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.Model
{
    /// <summary>
    /// ListView model for all needed list to show
    /// </summary>
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

        public SortedList<int, List<InputFileModel>> ListOfListInputFolder { get; set; }

        public ListViewerModel()
        {
            BasicStack = new List<InputFileModel>();
            LeftCameraStack = new List<InputFileModel>();
            RightCameraStack = new List<InputFileModel>();
            DrawnKeypoint = new List<InputFileModel>();
            DrawnMatches = new List<InputFileModel>();
            DepthMap = new List<InputFileModel>();

            ListOfListInputFolder = new SortedList<int, List<InputFileModel>>();
            ListOfListInputFolder.Add((int)EListViewGroup.BasicStack, BasicStack);
            ListOfListInputFolder.Add((int)EListViewGroup.LeftCameraStack, LeftCameraStack);
            ListOfListInputFolder.Add((int)EListViewGroup.RightCameraStack, RightCameraStack);
            ListOfListInputFolder.Add((int)EListViewGroup.DrawnKeyPoint, DrawnKeypoint);
            ListOfListInputFolder.Add((int)EListViewGroup.DrawnMatches, DrawnMatches);
            ListOfListInputFolder.Add((int)EListViewGroup.DepthMap, DepthMap);
        }
    }
}
