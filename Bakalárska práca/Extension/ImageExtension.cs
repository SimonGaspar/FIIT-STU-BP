using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.Extension
{
    public static class ImageExtension
    {
        public static Image ImageBGR2Image(this Image<Bgr, byte> image)
        {
            return image.ToBitmap();
        }

        public static Image<Bgr, byte> Image2ImageBGR(this Image image)
        {
            return new Image<Bgr, byte>((Bitmap)image);
        }
    }
}
