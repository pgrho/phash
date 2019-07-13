using System;
using System.Linq;
using System.Numerics;
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
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void NormalizeDigestTest(int seed)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var r = new Random(seed);
            var a = new float[40];

            for (var i = 0; i < a.Length; i++)
            {
                a[i] = (float)r.NextDouble();
            }

            var max = Enumerable.Max(a);
            var min = Enumerable.Min(a);

            var digest = ImagePhash.NormalizeDigest(a, max, min);

            Assert.Equal(a.Select(e => (byte)(byte.MaxValue * (e - min) / (max - min))), digest.Coefficients);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetCrossCorrelationTest(int seed)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var a = new byte[40];
            var b = new byte[40];

            for (var i = 0; i < 100; i++)
            {
                var r = new Random(seed);

                r.NextBytes(a);
                r.NextBytes(b);

                var expected = GetCrossCorrelationSimple(a, b, 40);
                var actual = ImagePhash.GetCrossCorrelation(a, b);

                Assert.Equal(expected, actual, 5);
            }
        }

        private static double GetCrossCorrelationSimple(byte[] x, byte[] y, int length)
        {
            var sumx = 0.0;
            var sumy = 0.0;
            for (var i = 0; i < length; i++)
            {
                sumx += x[i];
                sumy += y[i];
            }

            var meanx = sumx / length;
            var meany = sumy / length;
            var max = 0.0;
            for (var d = 0; d < length; d++)
            {
                var num = 0.0;
                var denx = 0.0;
                var deny = 0.0;

                for (var i = 0; i < length; i++)
                {
                    var dx = x[i] - meanx;
                    var dy = y[(length + i - d) % length] - meany;
                    num += dx * dy;
                    denx += dx * dx;
                    deny += dy * dy;
                }
                var r = num < 0 || denx == 0 || deny == 0 ? double.NaN : (num * num / (denx * deny));
                if (r > max)
                {
                    max = r;
                }
            }

            return Math.Sqrt(max);
        }
    }
}
