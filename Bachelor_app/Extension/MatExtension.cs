using System.Drawing;
using System.Runtime.InteropServices;
using Bachelor_app.StereoVision.Calibration;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bachelor_app.Extension
{
    /// <summary>
    /// Extension methods for images and mat.
    /// </summary>
    public static class MatExtension
    {
        /// <summary>
        /// Metod used in stereo vision to modify image for computing depth map.
        /// </summary>
        /// <param name="mat">Image in Mat to modify</param>
        /// <param name="leftImage">It's left image?</param>
        /// <param name="useRemap">Use remap?</param>
        /// <returns></returns>
        public static Mat RemapMat(this Mat mat, bool leftImage, bool useRemap = false)
        {
            if (CalibrationModel.IsCalibrated && useRemap)
            {
                if (leftImage)
                    CvInvoke.Remap(mat, mat, CalibrationModel.UndistortCam1.MapX, CalibrationModel.UndistortCam1.MapY, Inter.Linear);
                else
                    CvInvoke.Remap(mat, mat, CalibrationModel.UndistortCam2.MapX, CalibrationModel.UndistortCam2.MapY, Inter.Linear);
            }

            return mat;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">Input type of Image/Mat.</typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image ToImage<T>(this T image)
            where T : IImage, IInputArray
        {
            return image.Bitmap;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">Input type of Image/Mat.</typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image<Bgr, byte> ToImageBGR<T>(this T image)
            where T : IImage, IInputArray
        {
            return (image == null || image.Bitmap == null) ? null : new Image<Bgr, byte>(image.Bitmap);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">Input type of Image/Mat.</typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        public static GpuMat ToGpuMat<T>(this T image)
            where T : IInputArray
        {
            return image == null ? null : new GpuMat(image);
        }

        public static Mat ConvertMatForMatching(this Mat mat)
        {
            Mat result = new Mat(mat.Size, DepthType.Cv8U, 1);
            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    string value = mat.GetValue(i, j).ToString();
                    result.SetValue(i, j, byte.Parse(value));
                }
            }

            return result;
        }

        /// <summary>
        /// Method to get value from Mat.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="row">t</param>
        /// <param name="col"></param>
        /// <returns>Value from Mat</returns>
        public static dynamic GetValue(this Mat mat, int row, int col)
        {
            var value = CreateElement(mat.Depth);
            Marshal.Copy(mat.DataPointer + (((row * mat.Cols) + col) * mat.ElementSize), value, 0, 1);
            return value[0];
        }

        /// <summary>
        /// Method to set value in Mat.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public static void SetValue(this Mat mat, int row, int col, dynamic value)
        {
            var target = CreateElement(mat.Depth, value);
            Marshal.Copy(target, 0, mat.DataPointer + (((row * mat.Cols) + col) * mat.ElementSize), 1);
        }

        /// <summary>
        /// Get type of value.
        /// </summary>
        /// <param name="depthType">Depth type of Mat.</param>
        /// <param name="value">Primary nothing.</param>
        /// <returns>Array of C# type from depth type.</returns>
        private static dynamic CreateElement(DepthType depthType, dynamic value)
        {
            var element = CreateElement(depthType);
            element[0] = value;
            return element;
        }

        /// <summary>
        /// Types of value in Mat.
        /// </summary>
        /// <param name="depthType">Depth of Mat.</param>
        /// <returns>C# object of deoth type.</returns>
        private static dynamic CreateElement(DepthType depthType)
        {
            switch (depthType)
            {
                case DepthType.Cv8S: return new sbyte[1];
                case DepthType.Cv8U: return new byte[1];
                case DepthType.Cv16S: return new short[1];
                case DepthType.Cv16U: return new ushort[1];
                case DepthType.Cv32S: return new int[1];
                case DepthType.Cv32F: return new float[1];
                case DepthType.Cv64F: return new double[1];
                default: return new float[1];
            }
        }
    }
}
