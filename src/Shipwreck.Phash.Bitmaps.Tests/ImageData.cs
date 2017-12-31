using System;
using System.Drawing;
using System.IO;

namespace Shipwreck.Phash.Bitmaps.Tests
{

    public class ImageData : IDisposable
    {
        static string GetDataDirectory(string directoryName)
        {
            return Path.GetFullPath($"./{directoryName}/");
        }

        static Uri blurredUri = new Uri(GetDataDirectory("blur"));
        static Uri compressedUri = new Uri(GetDataDirectory("compr"));
        static Uri subsectionUri = new Uri(GetDataDirectory("subsection"));
        
        public LazyBitmap StainedGlassBlurred { get; private set; }
        public LazyBitmap StainedGlassSmallBlurred { get; private set; }

        public LazyBitmap StainedGlassCompressed { get; private set; }

        public LazyBitmap StainedGlassLeftBottom { get; private set; }
        public LazyBitmap StainedGlassLeftTop { get; private set; }
        public LazyBitmap StainedGlassRightBottom { get; private set; }
        public LazyBitmap StainedGlassRightTop { get; private set; }

        public ImageData()
        {
            InitBlurred();
            InitCompressed();
            InitSubsection();
        }

        void InitBlurred()
        {
            StainedGlassBlurred = new LazyBitmap(new Uri(blurredUri, "stain_glass.bmp"));
            StainedGlassSmallBlurred = new LazyBitmap(new Uri(blurredUri, "stain_glass.small.bmp"));
        }

        void InitCompressed()
        {
            StainedGlassCompressed = new LazyBitmap(new Uri(compressedUri, "stain_glass.jpg"));
        }

        void InitSubsection()
        {
            StainedGlassLeftBottom = new LazyBitmap(new Uri(subsectionUri, "stain_glass.left60bottom80.bmp"));
            StainedGlassLeftTop = new LazyBitmap(new Uri(subsectionUri, "stain_glass.left60top80.bmp"));
            StainedGlassRightBottom = new LazyBitmap(new Uri(subsectionUri, "stain_glass.right60bottom80.bmp"));
            StainedGlassRightTop = new LazyBitmap(new Uri(subsectionUri, "stain_glass.right60top80.bmp"));
        }

        public void Dispose()
        {
            StainedGlassBlurred.Dispose();
            StainedGlassSmallBlurred.Dispose();

            StainedGlassCompressed.Dispose();

            StainedGlassLeftBottom.Dispose();
            StainedGlassLeftTop.Dispose();
            StainedGlassRightBottom.Dispose();
            StainedGlassRightTop.Dispose();
        }
    }
}
