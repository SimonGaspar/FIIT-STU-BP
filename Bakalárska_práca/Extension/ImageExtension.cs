using System.Drawing;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bakalárska_práca.Extension
{
    public static class ImageExtension
    {
        public static Image ImageBGR2Image<T>(this T image) where T : IImage
        {
            return image.Bitmap;
        }

        public static Image<Bgr, byte> Image2ImageBGR<T>(this T image) where T : IImage
        {
            return new Image<Bgr, byte>(image.Bitmap);
        }

        public static GpuMat ImageToGpuMat<T>(this T image) where T : IInputArray
        {
            return new GpuMat(image);
        }
    }
}
