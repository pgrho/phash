using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shipwreck.Phash.PresentationCore;
using System;
using System.Drawing;
using System.Linq;

namespace Shipwreck.Phash.Bitmaps.Tests
{

    [TestClass]
    public class RawBitmapDataSpecs : ImageTestFixture
    {
        // Really Microsoft? No inheritance of attributes?
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            InitImages(context);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            DisposeImages();
        }

        const double ExactThreshhold = 0.000000000000000000000000001;

        // TODO: Categories
        [TestMethod]
        public void Digest_Should_Have_Non_Zero_Hash_For_Small_Images()
        {
            var rawBitmapDataDigest = ImagePhash.ComputeDigest(Images.ArchitectureSmallBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            Assert.IsTrue(rawBitmapDataDigest.Coefficents.Any(coeff => coeff > 0));
        }

        [TestMethod]
        public void Digest_Should_Match_BitmapSource_Exactly()
        {
            var rawBitmapDataDigest = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapSourceDigest = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.BitmapSource.ToLuminanceImage());
            Digest_Should_Match_Exactly(rawBitmapDataDigest, bitmapSourceDigest);
        }

        void Digest_Should_Match_Exactly(Digest one, Digest two)
        {
            Assert.AreEqual(one.ToString(), two.ToString());
        }


        [TestMethod]
        public void Digest_Should_Match_Bitmap_Exactly()
        {
            var rawBitmapDataDigest = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapDigest = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToLuminanceImage());
            Digest_Should_Match_Exactly(rawBitmapDataDigest, bitmapDigest);
        }


        [TestMethod]
        public void CCR_Should_Match_BitmapSource_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var rawBitmapDataDigestCompressed = ImagePhash.ComputeDigest(Images.ArchitectureCompressed.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapSourceDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.BitmapSource.ToLuminanceImage());
            var bitmapSourceDigestCompressed = ImagePhash.ComputeDigest(Images.ArchitectureCompressed.BitmapSource.ToLuminanceImage());

            CCR_Should_Match_Exactly(
                new Tuple<Digest, Digest>(rawBitmapDataDigestBlurred, rawBitmapDataDigestCompressed),
                new Tuple<Digest, Digest>(bitmapSourceDigestBlurred, bitmapSourceDigestCompressed));
        }
        
        void CCR_Should_Match_Exactly(Tuple<Digest, Digest> ccrComparisonOne, Tuple<Digest, Digest> ccrComparisonTwo)
        {
            var ccrOne = ImagePhash.GetCrossCorrelation(ccrComparisonOne.Item1, ccrComparisonOne.Item2);
            var ccrTwo = ImagePhash.GetCrossCorrelation(ccrComparisonTwo.Item1, ccrComparisonTwo.Item2);

            Assert.IsTrue(Math.Abs(ccrOne - ccrTwo) < ExactThreshhold);
        }


        [TestMethod]
        public void CCR_Should_Match_Bitmap_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var rawBitmapDataDigestCompressed = ImagePhash.ComputeDigest(Images.ArchitectureCompressed.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToLuminanceImage());
            var bitmapDigestCompressed = ImagePhash.ComputeDigest(Images.ArchitectureCompressed.Bitmap.ToLuminanceImage());

            CCR_Should_Match_Exactly(
                new Tuple<Digest, Digest>(rawBitmapDataDigestBlurred, rawBitmapDataDigestCompressed),
                new Tuple<Digest, Digest>(bitmapDigestBlurred, bitmapDigestCompressed));
        }


        [TestMethod]
        public void Left_Bottom_Area_Should_Match_Original_Digest_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());

            var trimArea = new Rectangle(60, 0, 300, 400);
            var rawBitmapDataDigestLeftBottom = ImagePhash.ComputeDigest(Images.ArchitectureSubsectionLeftBottom.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestLeftBottom);
        }

        void Area_Should_Match_Original_Digest_Exactly(Digest original, Digest subsection)
        {
            Assert.AreEqual(original.ToString(), subsection.ToString());
        }

        [TestMethod]
        public void Left_Top_Area_Should_Match_Original_Digest_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());

            var trimArea = new Rectangle(60, 80, 300, 400);
            var rawBitmapDataDigestLeftTop = ImagePhash.ComputeDigest(Images.ArchitectureSubsectionLeftTop.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestLeftTop);
        }

        [TestMethod]
        public void Right_Bottom_Area_Should_Match_Original_Digest_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());

            var trimArea = new Rectangle(0, 0, 300, 400);
            var rawBitmapDataDigestRightBottom = ImagePhash.ComputeDigest(Images.ArchitectureSubsectionRightBottom.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestRightBottom);
        }

        [TestMethod]
        public void Right_Top_Area_Should_Match_Original_Digest_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.ArchitectureBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());

            var trimArea = new Rectangle(0, 80, 300, 400);
            var rawBitmapDataDigestRightTop = ImagePhash.ComputeDigest(Images.ArchitectureSubsectionRightTop.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestRightTop);
        }
    }
}
