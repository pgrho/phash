using Shipwreck.Phash.Imaging;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.Bitmaps
{
    public class BitmapPhash : ImagePhash
    {
        protected BitmapPhash()
        { }

        public static Digest ComputeBitmapSourceDigest(BitmapSource bitmapSource, double sigma = DEFAULT_SIGMA, double gamma = DEFAULT_GAMMA, int numberOfAngles = DEFAULT_NUMBER_OF_ANGLES)
        {
            return ComputeDigest(bitmapSource.ToByteImageOfYOrB(), sigma, gamma, numberOfAngles: numberOfAngles);
        }

        /// <summary>
        /// Computes a Digest of a bitmap. 24bit RGB color format is recommended to avoid unneccessary conversions.
        /// </summary>
        /// <param name="bitmap">bitmap image to compute digest against</param>
        /// <param name="sigma">double value for the deviation for a gaussian filter function</param>
        /// <param name="gamma">double value for gamma correction on the input image</param>
        /// <param name="numberOfAngles">int value for the number of angles to consider.</param>
        /// <returns></returns>
        public static Digest ComputeBitmapDigest(Bitmap bitmap, double sigma = DEFAULT_SIGMA, double gamma = DEFAULT_GAMMA, int numberOfAngles = DEFAULT_NUMBER_OF_ANGLES)
        {
            return ComputeDigest(bitmap.ToByteImageOfY(), sigma, gamma, numberOfAngles: numberOfAngles);
        }

        /// <summary>
        /// Computes a Digest of raw bitmap data. 8bit per color component format is recommended to avoid unneccessary conversions.
        /// </summary>
        /// <param name="rawBitmapData">bitmap image to compute digest against</param>
        /// <param name="sigma">double value for the deviation for a gaussian filter function</param>
        /// <param name="gamma">double value for gamma correction on the input image</param>
        /// <param name="numberOfAngles">int value for the number of angles to consider.</param>
        /// <returns></returns>
        public static Digest ComputeRawBitmapDigest(RawBitmapData rawBitmapData, double sigma = DEFAULT_SIGMA, double gamma = DEFAULT_GAMMA, int numberOfAngles = DEFAULT_NUMBER_OF_ANGLES)
        {
            return ComputeDigest(rawBitmapData.ToByteImageOfY(), sigma, gamma, numberOfAngles: numberOfAngles);
        }

        /// <summary>
        /// Computes a Digest of raw bitmap data. 8bit per color component format is recommended to avoid unneccessary conversions.
        /// </summary>
        /// <param name="rawBitmapData">bitmap image to compute digest against</param>
        /// <param name="computeArea">section of the image to consider for digest</param>
        /// <param name="sigma">double value for the deviation for a gaussian filter function</param>
        /// <param name="gamma">double value for gamma correction on the input image</param>
        /// <param name="numberOfAngles">int value for the number of angles to consider.</param>
        /// <returns></returns>
        public static Digest ComputeRawBitmapDigest(RawBitmapData rawBitmapData, Rectangle computeArea, double sigma = DEFAULT_SIGMA, double gamma = DEFAULT_GAMMA, int numberOfAngles = DEFAULT_NUMBER_OF_ANGLES)
        {
            return ComputeDigest(rawBitmapData.ToByteImageOfY(computeArea), sigma, gamma, numberOfAngles: numberOfAngles);
        }

    }
}
