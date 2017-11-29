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
        public static Bitmap ToRgb24(this Bitmap source)
        {
            if (source.PixelFormat == PixelFormat.Format24bppRgb)
                return source;
            Bitmap drawingBitmap = null;
            try
            {
                drawingBitmap = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb);
                drawingBitmap.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                using (var graphics = Graphics.FromImage(drawingBitmap))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(source, 0, 0);
                }
                return drawingBitmap;
            }
            catch(Exception)
            {
                drawingBitmap?.Dispose();
                throw;
            }
        }

        public static ByteImage ToByteImageOfY(this Bitmap source)
        {
            Bitmap bmp = null;
            bool localOwnedBitmapHandle = false;
            try
            {
                bmp = source.ToRgb24();
                localOwnedBitmapHandle = source != bmp;

                var data = bmp.ToBytes();

                var r = new ByteImage(bmp.Width, bmp.Height);

                int strideDelta = bmp.GetStride() % bmp.Width;
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
                    bmp?.Dispose();
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

        public static int GetStride(this Bitmap bitmap)
        {
            int bitsPerPixel = ((int)bitmap.PixelFormat & 0xff00) >> 8;
            int stride = 4 * ((bitmap.Width * bitsPerPixel + 31) / 32);
            return stride;
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(this Bitmap source)
        {
            BitmapSource bs = null;
            IntPtr ip = source.GetHbitmap();
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