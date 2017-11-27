using System.Drawing;
using System.Numerics;
using System.Drawing.Imaging;
using System;
using System.Runtime.InteropServices;

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
                using (var graphics = Graphics.FromImage(drawingBitmap))
                {
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

                var data = bmp.SectionAsBytes(new Rectangle(0, 0, bmp.Width, bmp.Height));

                var r = new ByteImage(bmp.Width, bmp.Height);

                var yc = new Vector3(66, 129, 25);
                var i = 0;
                for (var dy = 0; dy < r.Height; dy++)
                {
                    for (var dx = 0; dx < r.Width; dx++)
                    {
                        Vector3 sv;
                        sv.X = data[i++]; // R
                        sv.Y = data[i++]; // G
                        sv.Z = data[i++]; // B

                        r[dx, dy] = (byte)(((int)(Vector3.Dot(yc, sv) + 128) >> 8) + 16);
                    }
                }

                return r;
            }
            finally
            {
                if (localOwnedBitmapHandle)
                    bmp?.Dispose();
            }
        }


        public static byte[] SectionAsBytes(this Bitmap bitmap, Rectangle sectionRect)
        {
            BitmapData lockedBits = null;
            try
            {
                lockedBits = bitmap.LockBits(sectionRect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
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
        
    }
}