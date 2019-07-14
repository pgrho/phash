using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Shipwreck.Phash.Imaging
{
    public class FloatImageTest
    {
        private readonly ITestOutputHelper _Output;
        public FloatImageTest(ITestOutputHelper output)
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
        public void ResizeTest(int seed)
        {
            const int W = 40;
            const int H = 30;

            var d = new byte[W * H];
            var r = new Random(seed);
            r.NextBytes(d);

            var head = new ByteImage(W, H, d).Convolve(new FloatImage(7, 7, 1)).Resize(32, 32);
            var v0_3_6 = V0_3_6.Imaging.ByteImageHelper.Convolve(
                new V0_3_6.Imaging.ByteImage(W, H, d),
                new V0_3_6.Imaging.FloatImage(7, 7, 1)).Resize(32, 32);

            for (var y = 0; y < 32; y++)
            {
                for (var x = 0; x < 32; x++)
                {
                    Assert.Equal(0, head[x, y] - v0_3_6[x, y], 1);
                }
            }
        }
    }
    public class ByteImageHelperTest
    {
        private readonly ITestOutputHelper _Output;
        public ByteImageHelperTest(ITestOutputHelper output)
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
        public void ConvolveTest(int seed)
        {
            const int W = 40;
            const int H = 30;

            var d = new byte[W * H];
            var r = new Random(seed);
            r.NextBytes(d);

            var head = new ByteImage(W, H, d).Convolve(new FloatImage(7, 7, 1));
            var v0_3_6 = V0_3_6.Imaging.ByteImageHelper.Convolve(
                new V0_3_6.Imaging.ByteImage(W, H, d),
                new V0_3_6.Imaging.FloatImage(7, 7, 1));

            for (var y = 0; y < H; y++)
            {
                for (var x = 0; x < W; x++)
                {
                    Assert.Equal(0, head[x, y] - v0_3_6[x, y], 1);
                }
            }
        }

    }
}
