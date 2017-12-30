using System;
using System.Drawing;
using System.IO;

namespace Shipwreck.Phash.Bitmaps.Tests
{

    public class ImageData : IDisposable
    {
        static Uri blurredUri = new Uri(Path.GetFullPath("./data/blur/"));
        static Uri compressedUri = new Uri(Path.GetFullPath("./data/compr/"));
        static Uri subsectionUri = new Uri(Path.GetFullPath("./data/subsection/"));
        
        public LazyBitmap ArchitectureBlurred { get; private set; }
        public LazyBitmap ArchitectureSmallBlurred { get; private set; }

        public LazyBitmap ArchitectureCompressed { get; private set; }

        public LazyBitmap ArchitectureSubsectionLeftBottom { get; private set; }
        public LazyBitmap ArchitectureSubsectionLeftTop { get; private set; }
        public LazyBitmap ArchitectureSubsectionRightBottom { get; private set; }
        public LazyBitmap ArchitectureSubsectionRightTop { get; private set; }

        public ImageData()
        {
            InitBlurred();
            InitCompressed();
            InitSubsection();
        }

        void InitBlurred()
        {
            ArchitectureBlurred = new LazyBitmap(new Uri(blurredUri, "architecture_2.bmp"));
            ArchitectureSmallBlurred = new LazyBitmap(new Uri(blurredUri, "architecture_2.small.bmp"));
        }

        void InitCompressed()
        {
            ArchitectureCompressed = new LazyBitmap(new Uri(compressedUri, "architecture_2.jpg"));
        }

        void InitSubsection()
        {
            ArchitectureSubsectionLeftBottom = new LazyBitmap(new Uri(subsectionUri, "architecture_2.left60bottom80.bmp"));
            ArchitectureSubsectionLeftTop = new LazyBitmap(new Uri(subsectionUri, "architecture_2.left60top80.bmp"));
            ArchitectureSubsectionRightBottom = new LazyBitmap(new Uri(subsectionUri, "architecture_2.right60bottom80.bmp"));
            ArchitectureSubsectionRightTop = new LazyBitmap(new Uri(subsectionUri, "architecture_2.right60top80.bmp"));
        }

        public void Dispose()
        {
            ArchitectureBlurred.Dispose();
            ArchitectureSmallBlurred.Dispose();

            ArchitectureCompressed.Dispose();

            ArchitectureSubsectionLeftBottom.Dispose();
            ArchitectureSubsectionLeftTop.Dispose();
            ArchitectureSubsectionRightBottom.Dispose();
            ArchitectureSubsectionRightTop.Dispose();
        }
    }
}
