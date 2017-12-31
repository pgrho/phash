using Shipwreck.Phash.PresentationCore;
using System;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Shipwreck.Phash.Bitmaps.Tests
{
    
    public class RawBitmapDataSpecs : IClassFixture<ImageData>
    {
        public RawBitmapDataSpecs(ImageData images)
        {
            Images = images;
        }

        public ImageData Images { get; private set; }

        const double ExactThreshhold = 0.000000000000000000000000001;

        // TODO: Categories
        [Fact]
        public void Digest_Should_Have_Non_Zero_Hash_For_Small_Images()
        {
            var rawBitmapDataDigest = ImagePhash.ComputeDigest(Images.StainedGlassSmallBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            Assert.Contains(rawBitmapDataDigest.Coefficents, coeff => coeff > 0);
        }

        [Fact]
        public void Digest_Should_Match_BitmapSource_Exactly()
        {
            var rawBitmapDataDigest = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapSourceDigest = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.BitmapSource.ToLuminanceImage());
            Digest_Should_Match_Exactly(rawBitmapDataDigest, bitmapSourceDigest);
        }

        void Digest_Should_Match_Exactly(Digest one, Digest two)
        {
            Assert.Equal(one.ToString(), two.ToString());
        }


        [Fact]
        public void Digest_Should_Match_Bitmap_Exactly()
        {
            var rawBitmapDataDigest = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapDigest = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.Bitmap.ToLuminanceImage());
            Digest_Should_Match_Exactly(rawBitmapDataDigest, bitmapDigest);
        }


        [Fact]
        public void CCR_Should_Match_BitmapSource_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var rawBitmapDataDigestCompressed = ImagePhash.ComputeDigest(Images.StainedGlassCompressed.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapSourceDigestBlurred = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.BitmapSource.ToLuminanceImage());
            var bitmapSourceDigestCompressed = ImagePhash.ComputeDigest(Images.StainedGlassCompressed.BitmapSource.ToLuminanceImage());

            CCR_Should_Match_Exactly(
                new Tuple<Digest, Digest>(rawBitmapDataDigestBlurred, rawBitmapDataDigestCompressed),
                new Tuple<Digest, Digest>(bitmapSourceDigestBlurred, bitmapSourceDigestCompressed));
        }
        
        void CCR_Should_Match_Exactly(Tuple<Digest, Digest> ccrComparisonOne, Tuple<Digest, Digest> ccrComparisonTwo)
        {
            var ccrOne = ImagePhash.GetCrossCorrelation(ccrComparisonOne.Item1, ccrComparisonOne.Item2);
            var ccrTwo = ImagePhash.GetCrossCorrelation(ccrComparisonTwo.Item1, ccrComparisonTwo.Item2);

            Assert.True(Math.Abs(ccrOne - ccrTwo) < ExactThreshhold);
        }


        [Fact]
        public void CCR_Should_Match_Bitmap_Exactly()
        {
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var rawBitmapDataDigestCompressed = ImagePhash.ComputeDigest(Images.StainedGlassCompressed.Bitmap.ToRawBitmapData().ToLuminanceImage());
            var bitmapDigestBlurred = ImagePhash.ComputeDigest(Images.StainedGlassBlurred.Bitmap.ToLuminanceImage());
            var bitmapDigestCompressed = ImagePhash.ComputeDigest(Images.StainedGlassCompressed.Bitmap.ToLuminanceImage());

            CCR_Should_Match_Exactly(
                new Tuple<Digest, Digest>(rawBitmapDataDigestBlurred, rawBitmapDataDigestCompressed),
                new Tuple<Digest, Digest>(bitmapDigestBlurred, bitmapDigestCompressed));
        }


        [Fact]
        public void Left_Bottom_Area_Should_Match_Original_Digest_Exactly()
        {
            var originalImage = Images.StainedGlassBlurred.Bitmap.ToRawBitmapData();
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(originalImage.ToLuminanceImage());

            var trimArea = new Rectangle(60, 0, originalImage.PixelWidth, originalImage.PixelHeight);
            var rawBitmapDataDigestLeftBottom = ImagePhash.ComputeDigest(Images.StainedGlassLeftBottom.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestLeftBottom);
        }

        void Area_Should_Match_Original_Digest_Exactly(Digest original, Digest subsection)
        {
            Assert.Equal(original.ToString(), subsection.ToString());
        }

        [Fact]
        public void Left_Top_Area_Should_Match_Original_Digest_Exactly()
        {
            var originalImage = Images.StainedGlassBlurred.Bitmap.ToRawBitmapData();
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(originalImage.ToLuminanceImage());

            var trimArea = new Rectangle(60, 80, originalImage.PixelWidth, originalImage.PixelHeight);
            var rawBitmapDataDigestLeftTop = ImagePhash.ComputeDigest(Images.StainedGlassLeftTop.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestLeftTop);
        }

        [Fact]
        public void Right_Bottom_Area_Should_Match_Original_Digest_Exactly()
        {
            var originalImage = Images.StainedGlassBlurred.Bitmap.ToRawBitmapData();
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(originalImage.ToLuminanceImage());

            var trimArea = new Rectangle(0, 0, originalImage.PixelWidth, originalImage.PixelHeight);
            var rawBitmapDataDigestRightBottom = ImagePhash.ComputeDigest(Images.StainedGlassRightBottom.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestRightBottom);
        }

        [Fact]
        public void Right_Top_Area_Should_Match_Original_Digest_Exactly()
        {
            var originalImage = Images.StainedGlassBlurred.Bitmap.ToRawBitmapData();
            var rawBitmapDataDigestBlurred = ImagePhash.ComputeDigest(originalImage.ToLuminanceImage());

            var trimArea = new Rectangle(0, 80, originalImage.PixelWidth, originalImage.PixelHeight);
            var rawBitmapDataDigestRightTop = ImagePhash.ComputeDigest(Images.StainedGlassRightTop.Bitmap.ToRawBitmapData().ToLuminanceImage(trimArea));

            Area_Should_Match_Original_Digest_Exactly(rawBitmapDataDigestBlurred, rawBitmapDataDigestRightTop);
        }
    }
}
