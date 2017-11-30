using System.Drawing;
using System.Numerics;
using System.Drawing.Imaging;
using System;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.Imaging
{
    public static class BitmapExtensions
    {
        public static Bitmap ToRgb24(this Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                return bitmap;
            Bitmap drawingBitmap = null;
            try
            {
                drawingBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                drawingBitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                using (var graphics = Graphics.FromImage(drawingBitmap))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(bitmap, 0, 0);
                }
                return drawingBitmap;
            }
            catch(Exception)
            {
                drawingBitmap?.Dispose();
                throw;
            }
        }

        public static ByteImage ToByteImageOfY(this Bitmap bitmap)
        {
            Bitmap bitmap24Rgb = null;
            bool localOwnedBitmapHandle = false;
            try
            {
                bitmap24Rgb = bitmap.ToRgb24();
                localOwnedBitmapHandle = bitmap != bitmap24Rgb;

                var data = bitmap24Rgb.ToBytes();

                var r = new ByteImage(bitmap24Rgb.Width, bitmap24Rgb.Height);

                int bytesPerPixel = (Image.GetPixelFormatSize(bitmap24Rgb.PixelFormat) + ((sizeof(byte) * 8) - 1)) / (sizeof(byte) * 8);
                int strideDelta = bitmap24Rgb.GetStride() % (bitmap24Rgb.Width * bytesPerPixel);
                var yc = new Vector3(66, 129, 25);
                var i = 0;
                for (var dy = 0; dy < r.Height; dy++)
                {
                    for (var dx = 0; dx < r.Width; dx++)
                    {
                        Vector3 sv;
                        sv.Z = data[i++]; // R
                        sv.Y = data[i++]; // G
                        sv.X = data[i++]; // B

                        r[dx, dy] = (byte)(((int)(Vector3.Dot(yc, sv) + 128) >> 8) + 16);
                    }

                    i += strideDelta;
                }

                return r;
            }
            finally
            {
                if (localOwnedBitmapHandle)
                    bitmap24Rgb?.Dispose();
            }
        }
        
        /// <summary>
        /// Copies the bitmap to its raw bytes format with stride bytes.
        /// </summary>
        /// <param name="bitmap">bitmap to convert</param>
        /// <returns>Raw byte array with stride bytes</returns>
        public static byte[] ToBytes(this Bitmap bitmap)
        {
            BitmapData lockedBits = null;
            try
            {
                lockedBits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int sizeInBytes = lockedBits.Stride * lockedBits.Height;
                byte[] rawPixelByteData = new byte[sizeInBytes];
                Marshal.Copy(lockedBits.Scan0, rawPixelByteData, 0, sizeInBytes);
                
                return rawPixelByteData;
            }
            finally
            {
                if (lockedBits != null)
                    bitmap.UnlockBits(lockedBits);
            }
        }

        public static RawBitmapData ToRawBitmapData(this Bitmap bitmap)
        {
            return RawBitmapData.FromBitmap(bitmap);
        }

        public static int GetStride(this Bitmap bitmap)
        {
            int bitsPerPixel = ((int)bitmap.PixelFormat & 0xff00) >> 8;
            int stride = 4 * ((bitmap.Width * bitsPerPixel + 31) / 32);
            return stride;
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            BitmapSource bs = null;
            IntPtr ip = bitmap.GetHbitmap();
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, System.Windows.Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                if (ip != IntPtr.Zero)
                    DeleteObject(ip);
            }

            return bs;
        }


        public static Bitmap ToBitmap(this Image image)
        {
            var bitmap = new Bitmap(image.Width, image.Height, image.PixelFormat);
            try
            {
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (var g = Graphics.FromImage(bitmap))
                    g.DrawImage(image, 0, 0);
                return bitmap;
            }
            catch (Exception)
            {
                bitmap?.Dispose();
                throw;
            }
        }
    }
}