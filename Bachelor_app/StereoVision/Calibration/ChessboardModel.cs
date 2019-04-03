﻿using System;
using System.Drawing;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.Calibration
{
    public class ChessboardModel
    {
        public const int width = 9;
        public const int height = 6;
        public Size patternSize = new Size(width, height); //size of chess board to be detected
        public Bgr[] line_colour_array = new Bgr[width * height]; // just for displaying coloured lines of detected chessboard
        public PointF[] corners_Left;
        public PointF[] corners_Right;
        public bool start_Flag = true; //start straight away

        public ChessboardModel()
        {
            Random R = new Random();
            for (int i = 0; i < line_colour_array.Length; i++)
            {
                line_colour_array[i] = new Bgr(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255));
            }
        }
    }
}