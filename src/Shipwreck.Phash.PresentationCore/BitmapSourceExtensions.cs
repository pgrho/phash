using Shipwreck.Phash.Imaging;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.PresentationCore
{
    public static class BitmapSourceExtensions
    {
        private static BitmapSource AsGray8(this BitmapSource source)
            => source.Format == PixelFormats.Gray8 ? source : new FormatConvertedBitmap(source, PixelFormats.Gray8, null, 0);

        private static BitmapSource AsBgr24(this BitmapSource source)
            => source.Format == PixelFormats.Bgr24 ? source : new FormatConvertedBitmap(source, PixelFormats.Bgr24, null, 0);

        /// <summary>
        /// Returns a gray scale image that contains intensity of the specified image.
        /// </summary>
        /// <param name="source">A <see cref="BitmapSource"/> to compute luminance.</param>
        /// <returns>The new <see cref="ByteImage"/>.</returns>
        /// <remarks>The coefficient is (1/3. 1/3. 1/3).</remarks>
        public static ByteImage ToIntensityImage(this BitmapSource source)
        {
            var bmp = source.AsGray8();

            var data = new byte[bmp.PixelWidth * bmp.PixelHeight];

            bmp.CopyPixels(data, bmp.PixelWidth, 0);

            return new ByteImage(bmp.PixelWidth, bmp.PixelHeight, data);
        }

        /// <summary>
        /// Returns a gray scale image that contains the luminance defined in MPEG of the specified image.
        /// </summary>
        /// <param name="source">A <see cref="BitmapSource"/> to compute luminance.</param>
        /// <returns>The new <see cref="ByteImage"/>.</returns>
        public static ByteImage ToLuminanceImage(this BitmapSource source)
        {
            var bmp = source.AsBgr24();

            var data = new byte[bmp.PixelWidth * bmp.PixelHeight * 3];
            bmp.CopyPixels(data, bmp.PixelWidth * 3, 0);

            var r = new ByteImage(bmp.PixelWidth, bmp.PixelHeight);

            var yc = new Vector3(66, 129, 25);
            var i = 0;
            for (var dy = 0; dy < r.Height; dy++)
            {
                for (var dx = 0; dx < r.Width; dx++)
                {
                    Vector3 sv;
                    sv.Z = data[i++];
                    sv.Y = data[i++];
                    sv.X = data[i++];

                    r[dx, dy] = (byte)(((int)(Vector3.Dot(yc, sv) + 128) >> 8) + 16);
                }
            }

            return r;
        }

        public static ByteImage ToByteImage(this BitmapSource src)
            => src.Format == PixelFormats.BlackWhite
                  || src.Format == PixelFormats.Gray2
                  || src.Format == PixelFormats.Gray4
                  || src.Format == PixelFormats.Gray8
                  || src.Format == PixelFormats.Gray16
                  || src.Format == PixelFormats.Gray32Float ? src.ToIntensityImage() : src.ToLuminanceImage();

        public static ByteImage ToRedImage(this BitmapSource src)
            => src.ToChannelImage(2);

        public static ByteImage ToGreenImage(this BitmapSource src)
            => src.ToChannelImage(1);

        public static ByteImage ToBlueImage(this BitmapSource src)
            => src.ToChannelImage(0);

        private static ByteImage ToChannelImage(this BitmapSource src, int offset)
        {
            if (src.Format == PixelFormats.BlackWhite
                  || src.Format == PixelFormats.Gray2
                  || src.Format == PixelFormats.Gray4
                  || src.Format == PixelFormats.Gray8
                  || src.Format == PixelFormats.Gray16
                  || src.Format == PixelFormats.Gray32Float)
            {
                return src.ToIntensityImage();
            }

            var bmp = src.AsBgr24();

            var data = new byte[bmp.PixelWidth * bmp.PixelHeight * 3];
            bmp.CopyPixels(data, bmp.PixelWidth * 3, 0);

            var r = new ByteImage(bmp.PixelWidth, bmp.PixelHeight);

            for (var dy = 0; dy < r.Height; dy++)
            {
                for (var dx = 0; dx < r.Width; dx++)
                {
                    r[dx, dy] = data[(dx + dy * r.Width) * 3 + offset];
                }
            }

            return r;
        }
    }
}