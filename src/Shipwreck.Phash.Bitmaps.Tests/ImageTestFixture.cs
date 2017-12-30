using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shipwreck.Phash.Bitmaps.Tests
{
    [TestClass]
    public class ImageTestFixture
    {
        public static ImageData Images { get; private set; }
        [ClassInitialize]
        public static void InitImages(TestContext context)
        {
            Images = new ImageData();
        }

        [ClassCleanup] 
        public static void DisposeImages()
        {
            Images?.Dispose();
            Images = null;
        }
    }
}
