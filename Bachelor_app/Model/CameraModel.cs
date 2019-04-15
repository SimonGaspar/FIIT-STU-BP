﻿using Emgu.CV;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for camera
    /// </summary>
    public class CameraModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public VideoCapture camera { get; set; } = null;

        public void CreateCameraInstance()
        {
            camera = new VideoCapture(ID);
        }
    }
}
