using System;
using System.Linq;
using System.Numerics;
using Shipwreck.Phash.Imaging;
using Xunit;
using Xunit.Abstractions;

namespace Shipwreck.Phash
{
    public class ImagePhashTest
    {
        private readonly ITestOutputHelper _Output;
        public ImagePhashTest(ITestOutputHelper output)
        {
            _Output = output;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ComputeDctHashTest(int seed)
        {
            const int W = 40;
            const int H = 30;

            var d = new byte[W * H];
            var r = new Random(seed);
            r.NextBytes(d);

            var head = ImagePhash.ComputeDctHash(new ByteImage(W, H, d));
            var v0_3_6 = V0_3_6.ImagePhash.ComputeDctHash(new V0_3_6.Imaging.ByteImage(W, H, d));

            _Output.WriteLine($"{head:x16} {v0_3_6:x16} {ImagePhash.GetHammingDistance(head, v0_3_6)}");
            Assert.Equal(v0_3_6, head);

            /*
                        var img = image.Convolve(new FloatImage(7, 7, 1));

            var resized = img.Resize(32, 32);
            var C = CreateDctMatrix(32);
            var Ctransp = C.Transpose();
            var dctImage = C.Multiply(resized).Multiply(Ctransp);
             
             */
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ComputeDigest(int seed)
        {
            const int W = 128;
            const int H = 128;

            var d = new byte[W * H];
            var r = new Random(seed);
            r.NextBytes(d);

            var head = ImagePhash.ComputeDigest(new ByteImage(W, H, d));
            var v0_3_6 = V0_3_6.ImagePhash.ComputeDigest(new V0_3_6.Imaging.ByteImage(W, H, d));

            var cc = ImagePhash.GetCrossCorrelation(head.Coefficients, v0_3_6.Coefficients);
            _Output.WriteLine($"ImagePhash.GetCrossCorrelation: {cc}");
            Assert.Equal(1, cc, 3);
        }

        [Theory]
        [InlineData(32)]
        public void CreateDctMatrixTest(int size)
        {
            var head = ImagePhash.CreateDctMatrix(size);
            var v0_3_6 = V0_3_6.ImagePhash.CreateDctMatrix(size);

            _Output.WriteLine($"{head.Width}x{head.Height}");
            _Output.WriteLine(string.Join(" ", head.Array.Select(e => e.ToString("f3"))));
            _Output.WriteLine($"{v0_3_6.Width}x{v0_3_6.Height}");
            _Output.WriteLine(string.Join(" ", v0_3_6.Array.Select(e => e.ToString("f3"))));

            Assert.Equal(v0_3_6.Width, head.Width);
            Assert.Equal(v0_3_6.Height, head.Height);

            for (var y = 0; y < head.Height; y++)
            {
                for (var x = 0; x < head.Width; x++)
                {
                    Assert.Equal(0, head[x, y] - v0_3_6[x, y], 5);
                }
            }
        }
    }
}
