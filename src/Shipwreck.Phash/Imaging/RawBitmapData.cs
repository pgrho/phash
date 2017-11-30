using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Shipwreck.Phash.Imaging
{
    public class RawBitmapData
    {

        public delegate void PixelHandler(int A, int R, int G, int B);

        public int Width { get; }
        public int Stride { get; }
        public int Height { get; }
        public PixelFormat PixelFormat { get; }
        public byte[] RawPixelBytes { get; }
        public int BytesPerPixel { get; }

        private RawBitmapData(int width, int stride, int height, PixelFormat pixelFormat, byte[] rawPixelBytes)
        {
            Width = width;
            Stride = stride;
            Height = height;
            PixelFormat = pixelFormat;
            RawPixelBytes = rawPixelBytes;
            int bitsPerPixel = Image.GetPixelFormatSize(pixelFormat);
            BytesPerPixel = (bitsPerPixel + (sizeof(byte) - 1)) / sizeof(byte);
    }

        int PixelXYToByteIndex(int pixelX, int pixelY)
        {
            return (pixelY * BytesPerPixel * Stride) + (pixelX * BytesPerPixel);
        }

        public void PerPixel(PixelHandler pixelHandler)
        {
            PerPixelInArea(new Rectangle(0, 0, Width, Height), pixelHandler);
        }

        public void PerPixelInArea(Rectangle area, PixelHandler pixelHandler)
        {
            if (area.X + area.Width > Width)
                throw new ArgumentException($"Requested Rectangle {area} does not fit into the bounds of the bitmap width {Width}");
            if (area.Y + area.Height > Height)
                throw new ArgumentException($"Requested Rectangle {area} does not fit into the bounds of the bitmap height {Height}");

            var pixelExtractor = PixelColorExtractor.Create(PixelFormat);

            int pixelWidth = area.X;
            int pixelHeight = area.Y;
            int pixelFinalWidth = (area.X + area.Width);

            int byteIndex = PixelXYToByteIndex(area.X, area.Y);
            int byteFinalIndex = PixelXYToByteIndex(area.X + area.Width, area.Y + area.Height);

            int A = 255;
            int R = 255;
            int G = 255;
            int B = 255;
            while (byteIndex < byteFinalIndex)
            {
                pixelExtractor.ExtractPixelBytesToColor(RawPixelBytes, byteIndex, ref A, ref R, ref G, ref B);
                pixelHandler?.Invoke(A, R, G, B);
                pixelWidth++;
                if (pixelWidth >= pixelFinalWidth)
                {
                    pixelWidth = area.X;
                    pixelHeight++;
                }
                byteIndex = PixelXYToByteIndex(pixelWidth, pixelHeight);

                A = 255;
                R = 255;
                G = 255;
                B = 255;
            }
        }

        public static RawBitmapData FromBitmap(Bitmap bitmap)
        {
            var lockedBits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            try
            {
                int sizeInBytes = lockedBits.Stride * lockedBits.Height;
                byte[] rawPixelByteData = new byte[sizeInBytes];
                Marshal.Copy(lockedBits.Scan0, rawPixelByteData, 0, sizeInBytes);
                
                return new RawBitmapData(lockedBits.Width, lockedBits.Stride, lockedBits.Height, lockedBits.PixelFormat, rawPixelByteData);
            }
            finally
            {
                if (lockedBits != null)
                    bitmap.UnlockBits(lockedBits);
            }
            
        }
        

        public abstract class PixelColorExtractor
        {
            protected PixelColorExtractor(PixelFormat pixelFormat)
            {
                PixelFormat = pixelFormat;
                BitsPerPixel = Image.GetPixelFormatSize(pixelFormat);
            }

            public PixelFormat PixelFormat { get; }
            public int BitsPerPixel { get; }
            public int BytesPerPixel => (BitsPerPixel + (sizeof(byte) - 1)) / sizeof(byte);

            /// <summary>
            /// Extracts ARGB components from the raw bytes of a pixel.
            /// </summary>
            /// <param name="rawBytes">raw pixel bytes</param>
            /// <param name="startIndex">start index in the raw byte array</param>
            /// <param name="A">Output alpha component, optional</param>
            /// <param name="R">Output red component, required</param>
            /// <param name="G">Output green component, required</param>
            /// <param name="B">Output blue component, required</param>
            public abstract void ExtractPixelBytesToColor(byte[] rawBytes, int startIndex, ref int A, ref int R, ref int G, ref int B);

            static Dictionary<PixelFormat, PixelColorExtractor> RegisteredColorExtractors = new Dictionary<PixelFormat, PixelColorExtractor>()
            {
                { PixelFormat.Format24bppRgb, new Rgb24PixelScanner() },
                { PixelFormat.Format32bppArgb, new Argb32PixelScanner() },
                { PixelFormat.Format32bppPArgb, new PArgb32PixelScanner() },
            };

            public static void RegisterPixelColorExtractor(PixelColorExtractor extractor)
            {
                RegisteredColorExtractors.Remove(extractor.PixelFormat);
                RegisteredColorExtractors.Add(extractor.PixelFormat, extractor);
            }
            
            public static PixelColorExtractor Create(PixelFormat pixelFormat)
            {
                if (!RegisteredColorExtractors.ContainsKey(pixelFormat))
                    throw new FormatException($"PixelScanner is not supported for {pixelFormat} pixel format");

                return RegisteredColorExtractors[pixelFormat];
            }
        }

        class Rgb24PixelScanner : PixelColorExtractor
        {
            public Rgb24PixelScanner() : base(PixelFormat.Format24bppRgb)
            { }

            public override void ExtractPixelBytesToColor(byte[] rawBytes, int startIndex, ref int A, ref int R, ref int G, ref int B)
            {
                R = rawBytes[startIndex];
                G = rawBytes[startIndex + 1];
                B = rawBytes[startIndex + 2];
            }
        }
        class Argb32PixelScanner : PixelColorExtractor
        {
            public Argb32PixelScanner() : base(PixelFormat.Format32bppArgb)
            { }

            public override void ExtractPixelBytesToColor(byte[] rawBytes, int startIndex, ref int A, ref int R, ref int G, ref int B)
            {
                A = rawBytes[startIndex];
                R = rawBytes[startIndex + 1];
                G = rawBytes[startIndex + 2];
                B = rawBytes[startIndex + 3];
            }
        }

        class PArgb32PixelScanner : Argb32PixelScanner { }
    }
}