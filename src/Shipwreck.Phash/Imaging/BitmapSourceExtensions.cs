using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.Imaging
{
    public static class BitmapSourceExtensions
    {
        public static BitmapSource ToGray8(this BitmapSource source)
            => source.Format == PixelFormats.Gray8 ? source : new FormatConvertedBitmap(source, PixelFormats.Gray8, null, 0);

        public static BitmapSource ToBgr24(this BitmapSource source)
            => source.Format == PixelFormats.Bgr24 ? source : new FormatConvertedBitmap(source, PixelFormats.Bgr24, null, 0);

        public static ByteImage ToByteImage(this BitmapSource source)
        {
            var bmp = source.ToGray8();

            var data = new byte[bmp.PixelWidth * bmp.PixelHeight];

            bmp.CopyPixels(data, bmp.PixelWidth, 0);

            return new ByteImage(bmp.PixelWidth, bmp.PixelHeight, data);
        }

        public static ByteImage ToByteImageOfY(this BitmapSource source)
        {
            var bmp = source.ToBgr24();

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

        public static ByteImage ToByteImageOfYOrB(this BitmapSource src)
            => src.Format == PixelFormats.BlackWhite
                  || src.Format == PixelFormats.Gray2
                  || src.Format == PixelFormats.Gray4
                  || src.Format == PixelFormats.Gray8
                  || src.Format == PixelFormats.Gray16
                  || src.Format == PixelFormats.Gray32Float ? src.ToByteImage() : src.ToByteImageOfY();

        public static Bitmap ToBitmap(this BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }
    }
}