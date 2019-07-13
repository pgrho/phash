using System;
using System.Diagnostics;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Shipwreck.Phash.Imaging
{
    public class ByteImageOperationsTest
    {
        private readonly ITestOutputHelper _Output;
        public ByteImageOperationsTest(ITestOutputHelper output)
        {
            _Output = output;
        }

        [Theory]
        [InlineData(1, 7)]
        [InlineData(1, 8)]
        [InlineData(1, 9)]
        public void Test(int seed, int width)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var r = new Random(seed);

            var src = new ByteImage(1280, 720);
            r.NextBytes(src.Array);

            var ker = new FloatImage(width, 10);
            var sum = 0f;
            for (var i = 0; i < ker.Array.Length; i++)
            {
                sum += ker.Array[i] = (float)r.NextDouble();
            }
            ker.MultiplyInplace(1 / sum);

            var wrapper = new ByteImageWrapper(src);

            var sw = new Stopwatch();

            sw.Start();
            var expected = ByteImageOperations<ByteImageWrapper>.ConvolveSingle(wrapper, ker, ker.Width, ker.Height);
            sw.Stop();
            _Output.WriteLine("interface: " + sw.ElapsedMilliseconds + "ms");

            sw.Reset();
            sw.Start();
            var actual = new ByteImageOperations<ByteImageWrapper>().Convolve(wrapper, ker);
            sw.Stop();
            _Output.WriteLine("Vector<float>: " + sw.ElapsedMilliseconds + "ms");

            for (var i = 0; i < expected.Array.Length; i++)
            {
                Assert.Equal(
                    0,
                    (expected.Array[i] - actual.Array[i]) / expected.Array[i], 4);
            }
        }
    }
}
