using System.Linq;
using System.Numerics;
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
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void MultiplyTest(int width)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var src = new FloatImage(width, 1);
            for (var i = 0; i < src.Array.Length; i++)
            {
                src.Array[i] = i + 1;
            }

            var dest = src * 2;

            for (var i = 0; i < src.Array.Length; i++)
            {
                Assert.Equal(src.Array[i] * 2, dest.Array[i], 7);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(16)]
        [InlineData(24)]
        [InlineData(32)]
        [InlineData(80)]
        [InlineData(128)]
        [InlineData(256)]
        public void MultiplyInplaceTest(int width)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var src = new FloatImage(width, 1);
            for (var i = 0; i < src.Array.Length; i++)
            {
                src.Array[i] = i + 1;
            }

            src.MultiplyInplace(2);

            for (var i = 0; i < src.Array.Length; i++)
            {
                Assert.Equal(src.Array[i], 2 * (i + 1), 7);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void MultiplyImageTest(int width)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var src1 = new FloatImage(width, 5);
            var src2 = new FloatImage(10, 5);

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 10; x++)
                {
                    if (x < width)
                    {
                        src1[x, y] = y * 10 + x + 1;
                    }
                    src2[x, y] = y * 10 + x;
                }
            }

            var res = src1.Multiply(src2);

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Assert.Equal(src1[x, y] * src2[x, y], res[x, y], 7);
                }
            }
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(3, false)]
        [InlineData(4, false)]
        [InlineData(5, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(9, false)]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(9, true)]
        public void MatrixMultiplyTest(int width, bool transpose)
        {
            _Output.WriteLine("Vector<float>.Count: {0}", Vector<float>.Count);
            _Output.WriteLine("Vector.IsHardwareAccelerated: {0}", Vector.IsHardwareAccelerated);

            var src = new FloatImage(width, 1);
            var src2 = transpose ? new FloatImage(width, 1) : new FloatImage(1, width);
            for (var i = 0; i < src.Array.Length; i++)
            {
                src.Array[i] = i + 1;
                src2.Array[i] = 2 * i;
            }

            var dest = src.MatrixMultiply(src2, isTransposed: transpose);

            Assert.Equal(Enumerable.Range(0, width).Sum(i => (i + 1) * 2 * i), dest[0, 0]);
        }
    }
}
