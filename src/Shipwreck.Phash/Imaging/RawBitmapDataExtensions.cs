using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Shipwreck.Phash.Imaging
{
    public static class RawBitmapDataExtensions
    {
        static bool Is1BytePerComponentFormat(PixelFormat format)
        {
            return format == PixelFormat.Format24bppRgb ||
                format == PixelFormat.Format32bppArgb ||
                format == PixelFormat.Format32bppPArgb ||
                format == PixelFormat.Format32bppRgb;
        }

        public static ByteImage ToByteImageOfY(this RawBitmapData rawBitmapData)
        {
            return rawBitmapData.ToByteImageOfY(new Rectangle(0, 0, rawBitmapData.PixelWidth, rawBitmapData.PixelHeight);
        }
        public static ByteImage ToByteImageOfY(this RawBitmapData rawBitmapData, Rectangle area)
        {
            RawBitmapData rawBitmapData1ByteComponent = rawBitmapData;
            if (!Is1BytePerComponentFormat(rawBitmapData.PixelFormat))
            {
                using (var bitmap = rawBitmapData.ToBitmap())
                {
                    if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                        rawBitmapData1ByteComponent = bitmap.ToRawBitmapData();
                    else
                    {
                        using (var bitmap24Rgb = bitmap.ToRgb24())
                            rawBitmapData1ByteComponent = bitmap24Rgb.ToRawBitmapData();
                    }
                }
            }

            var data = rawBitmapData1ByteComponent;

            var r = new ByteImage(rawBitmapData1ByteComponent.PixelWidth, rawBitmapData1ByteComponent.PixelHeight);
            var yc = new Vector3(66, 129, 25);

            rawBitmapData1ByteComponent.PerPixelInArea(area, (dx, dy, A, R, G, B) =>
            {
                Vector3 sv;
                sv.Z = R; 
                sv.Y = G; 
                sv.X = B; 

                r[dx, dy] = (byte)(((int)(Vector3.Dot(yc, sv) + 128) >> 8) + 16);
            });
            
            return r;
        }

        public static Bitmap ToBitmap(this RawBitmapData rawBitmapData)
        {
            Bitmap bitmap = new Bitmap(rawBitmapData.PixelWidth, rawBitmapData.PixelHeight, rawBitmapData.PixelFormat);
            BitmapData lockedBits = null;
            try
            {
                lockedBits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                Marshal.Copy(rawBitmapData.RawPixelBytes, 0, lockedBits.Scan0, rawBitmapData.RawPixelBytes.Length);

                return bitmap;
            }
            catch (Exception)
            {
                bitmap?.Dispose();
                throw;
            }
            finally
            {
                if (lockedBits != null)
                    bitmap.UnlockBits(lockedBits);
            }
        }
    }
}