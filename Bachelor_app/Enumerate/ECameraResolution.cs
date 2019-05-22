using System;
using System.Drawing;

namespace Bachelor_app.Enumerate
{
    /// <summary>
    /// Types of camera resolution.
    /// </summary>
    public enum ECameraResolution
    {
        VGA,
        HD,
        FullHD
    }

    /// <summary>
    /// Extension class for enum ECameraResolution.
    /// </summary>
    public static class CameraResolutionExtension
    {
        /// <summary>
        /// Return object Size(width, height) by type.
        /// </summary>
        /// <param name="type">Type of camera resolution.</param>
        /// <returns>Object Size(width, height) by ECameraResolution type.</returns>
        public static Size GetResolution(this ECameraResolution type)
        {
            Size returnValue;

            switch (type)
            {
                case ECameraResolution.VGA:
                    returnValue = new Size(640, 360);
                    break;
                case ECameraResolution.HD:
                    returnValue = new Size(1280, 720);
                    break;
                case ECameraResolution.FullHD:
                    returnValue = new Size(1920, 1080);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return returnValue;
        }
    }
}
