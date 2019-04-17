using System;
using System.Collections.Generic;
using Bachelor_app.Enumerate;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.Model
{
    /// <summary>
    /// ListView model for all needed list to show.
    /// </summary>
    public class ListViewModel
    {

        public Image<Bgr, Byte> _lastBasicStack { get; set; }
        public Image<Bgr, Byte> _lastCameraStack { get; set; }
        public Image<Bgr, Byte> _lastDepthMapImage { get; set; }
        public Image<Bgr, Byte> _lastDrawnKeypoint { get; set; }
        public Image<Bgr, Byte> _lastDrawnMatches { get; set; }
        public Image<Bgr, Byte> _lastImage { get; set; }



        public List<InputFileModel> BasicStack { get; private set; }
        public List<InputFileModel> LeftCameraStack { get; private set; }
        public List<InputFileModel> RightCameraStack { get; private set; }
        public List<InputFileModel> DrawnKeypoint { get; private set; }
        public List<InputFileModel> DrawnMatches { get; private set; }
        public List<InputFileModel> DepthMap { get; private set; }

        public SortedList<int, List<InputFileModel>> ListOfListInputFolder { get; private set; }

        public ListViewModel()
        {
            BasicStack = new List<InputFileModel>();
            LeftCameraStack = new List<InputFileModel>();
            RightCameraStack = new List<InputFileModel>();
            DrawnKeypoint = new List<InputFileModel>();
            DrawnMatches = new List<InputFileModel>();
            DepthMap = new List<InputFileModel>();

            ListOfListInputFolder = new SortedList<int, List<InputFileModel>>
            {
                { (int)EListViewGroup.BasicStack, BasicStack },
                { (int)EListViewGroup.LeftCameraStack, LeftCameraStack },
                { (int)EListViewGroup.RightCameraStack, RightCameraStack },
                { (int)EListViewGroup.DrawnKeyPoint, DrawnKeypoint },
                { (int)EListViewGroup.DrawnMatches, DrawnMatches },
                { (int)EListViewGroup.DepthMap, DepthMap }
            };
        }
    }
}
