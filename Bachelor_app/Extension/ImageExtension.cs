using System.Drawing;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bakalárska_práca.Extension
{
    /// <summary>
    /// Class for conversion of images
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// Conversion from EmguCV Image to Image
        /// </summary>
        /// <typeparam name="T">Input type of image</typeparam>
        /// <param name="image">EmguCV Image</param>
        /// <returns>Image</returns>
        public static Image ImageBGR2Image<T>(this T image) where T : IImage
        {
            return image.Bitmap;
        }

        /// <summary>
        /// Conversion from Image to EmguCV Image
        /// </summary>
        /// <typeparam name="T">Input type of image</typeparam>
        /// <param name="image">Image</param>
        /// <returns>EmguCV Image</returns>
        public static Image<Bgr, byte> Image2ImageBGR<T>(this T image) where T : IImage, IInputArray
        {
            return new Image<Bgr, byte>(image.Bitmap);
        }

        /// <summary>
        /// Conversion Image to GpuMat
        /// </summary>
        /// <typeparam name="T">Input type of image</typeparam>
        /// <param name="image">Image (primary image in Mat)</param>
        /// <returns>GpuMat with image</returns>
        public static GpuMat ImageToGpuMat<T>(this T image) where T : IInputArray
        {
            return new GpuMat(image);
        }
    }
}
