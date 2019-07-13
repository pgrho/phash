using System;
using System.Linq;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Shipwreck.Phash.Imaging
{
    public class ArrayHelperTest
    {
        private readonly ITestOutputHelper _Output;
        public ArrayHelperTest(ITestOutputHelper output)
        {
            _Output = output;
        }

        [Theory]
        [InlineData(0, 7)]
        [InlineData(0, 8)]
        [InlineData(0, 9)]
        [InlineData(0, 15)]
        [InlineData(0, 16)]
        [InlineData(0, 17)]
        public void MaxTest(int seed, int length)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var a = new float[length];

            for (var d = 0; d < length; d++)
            {
                var r = new Random(seed);
                for (var i = 0; i < a.Length; i++)
                {
                    a[(i + d) % length] = (float)r.NextDouble();
                }

                Assert.Equal(Enumerable.Max(a), ArrayHelper.Max(a));
            }
        }

        [Theory]
        [InlineData(0, 7)]
        [InlineData(0, 8)]
        [InlineData(0, 9)]
        [InlineData(0, 15)]
        [InlineData(0, 16)]
        [InlineData(0, 17)]
        public void SumTest(int seed, int length)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var a = new float[length];
            var r = new Random(seed);

            for (var i = 0; i < a.Length; i++)
            {
                a[i] = (float)r.NextDouble();
            }

            Assert.Equal(Enumerable.Sum(a), ArrayHelper.Sum(a), 3);
        }
    }
}
