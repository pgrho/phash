using System;
using System.Numerics;

namespace Shipwreck.Phash.Imaging
{
    internal class ByteImageOperations<T> : IByteImageOperations
        where T : IByteImageWrapper
    {
        public FloatImage Convolve(T image, FloatImage kernel)
        {
            var vc = Vector<float>.Count;
            if (Vector.IsHardwareAccelerated
                && vc > 1
                && kernel.Width > 1)
            {
                return ConvolveVector(image, PackKernel(kernel, vc), kernel.Width, kernel.Height);
            }

            return ConvolveSingle(image, kernel, kernel.Width, kernel.Height);
        }

        private static float[] PackKernel(FloatImage kernel, int vectorSize)
        {
            var rowSize = (kernel.Width + vectorSize - 1) / vectorSize;
            if (rowSize * vectorSize == kernel.Width)
            {
                return kernel.Array;
            }
            else
            {
                var ka = new float[vectorSize * rowSize * kernel.Height];
                for (var y = 0; y < kernel.Height; y++)
                {
                    Array.Copy(kernel.Array, y * kernel.Width, ka, y * vectorSize * rowSize, kernel.Width);
                }
                return ka;
            }
        }

        internal static FloatImage ConvolveSingle(T image, FloatImage kernel, int kernelWidth, int kernelHeight)
        {
            var kernelXRadius = kernel.Width >> 1;
            var kernelYRadius = kernelHeight >> 1;

            var r = new FloatImage(image.Width, image.Height);
            float total = kernel.Sum();

            for (var dy = 0; dy < image.Height; dy++)
            {
                for (var dx = 0; dx < image.Width; dx++)
                {
                    var v = 0f;
                    var sum = 0f;
                    for (var ky = 0; ky < kernelHeight; ky++)
                    {
                        var sy = dy + ky - kernelYRadius;
                        if (sy < 0 || image.Height <= sy)
                        {
                            continue;
                        }

                        for (var kx = 0; kx < kernelWidth; kx++)
                        {
                            var sx = dx + kx - kernelXRadius;
                            if (sx < 0 || image.Width <= sx)
                            {
                                continue;
                            }

                            var sv = image[sx, sy];
                            var kv = kernel[kx, ky];
                            v += sv * kv;
                            sum += kv;
                        }
                    }

                    r[dx, dy] = total == sum ? v : (v * total / sum);
                }
            }

            return r;
        }

        private static unsafe FloatImage ConvolveVector(T image, float[] kernel, int kernelWidth, int kernelHeight)
        {
            var vc = Vector<float>.Count;
            var kernelXRadius = kernelWidth >> 1;
            var kernelYRadius = kernelHeight >> 1;

            var lineSize = image.Width + kernelWidth + vc;
            var lines = new float[lineSize * kernelHeight];

            for (var y = 0; y < kernelHeight - kernelYRadius; y++)
            {
                LoadLine(image, kernelHeight, kernelXRadius, lines, lineSize, y);
            }

            var r = new FloatImage(image.Width, image.Height);

            var total = 0f;
            var one = new Vector<float>(1);
            for (var i = 0; i < kernel.Length; i += vc)
            {
                total += Vector.Dot(one, new Vector<float>(kernel, i));
            }

            var xBatch = (kernelWidth + vc - 1) / vc;
            fixed (float* fp = kernel)
            fixed (float* _ = lines)
            {
                for (var dy = 0; dy < image.Height; dy++)
                {
                    if (dy > 0)
                    {
                        LoadLine(image, kernelHeight, kernelXRadius, lines, lineSize, dy + kernelHeight - kernelYRadius - 1);
                    }

                    var isYEdge = dy - kernelYRadius < 0 || image.Height <= dy + kernelHeight - kernelYRadius - 1;

                    for (var dx = 0; dx < image.Width; dx++)
                    {
                        var isEdge = isYEdge || dx - kernelXRadius < 0 || image.Width <= dx + kernelWidth - kernelXRadius - 1;

                        var result = isEdge ? ConvolveVectorPixel(image, lines, lineSize, fp, kernelWidth, kernelHeight, kernelXRadius, kernelYRadius, total, xBatch, dx, dy)
                                    : ConvolveVectorPixel(image, lines, lineSize, fp, kernelHeight, kernelYRadius, xBatch, dx, dy);
                        r[dx, dy] = result;
                    }
                }
            }

            return r;
        }

        private static unsafe float ConvolveVectorPixel(T image, float[] lines, int lineSize, float* kernel, int kernelHeight, int kernelYRadius, int xBatch, int x, int y)
        {
            var vc = Vector<float>.Count;
            var v = 0f;
            for (var ky = 0; ky < kernelHeight; ky++)
            {
                var sy = y + ky - kernelYRadius;

                var ly = (sy % kernelHeight) * lineSize;
                for (var ri = 0; ri < xBatch; ri++)
                {
                    var sx = x + ri * vc;
                    var kv = ((Vector<float>*)kernel)[ri + ky * xBatch];
                    var vv = new Vector<float>(lines, ly + sx);

                    var dv = Vector.Dot(vv, kv);
                    v += dv;
                }
            }

            return v;
        }

        private static unsafe float ConvolveVectorPixel(T image, float[] lines, int lineSize, float* kernel, int kernelWidth, int kernelHeight, int kernelXRadius, int kernelYRadius, float kernelSum, int xBatch, int x, int y)
        {
            var vc = Vector<float>.Count;
            var v = 0f;
            var sum = 0f;
            var bases = stackalloc float[Vector<float>.Count];
            for (var ky = 0; ky < kernelHeight; ky++)
            {
                var sy = y + ky - kernelYRadius;
                if (sy < 0 || image.Height <= sy)
                {
                    continue;
                }

                var ly = (sy % kernelHeight) * lineSize;
                for (var ri = 0; ri < xBatch; ri++)
                {
                    var sx = x + ri * vc - kernelXRadius;
                    var kv = ((Vector<float>*)kernel)[ri + ky * xBatch];
                    var vv = new Vector<float>(lines, ly + sx + kernelXRadius);
                    Vector<float> bv;
                    if (kernelXRadius <= x && x < kernelWidth - kernelXRadius)
                    {
                        bv = new Vector<float>(1);
                    }
                    else
                    {
                        for (var i = 0; i < vc; i++)
                        {
                            bases[i] = sx + i < 0 || image.Width <= sx + i ? 0 : 1;
                        }
                        bv = *(Vector<float>*)bases;
                    }

                    var dv = Vector.Dot(vv, kv);
                    var ds = Vector.Dot(bv, kv);
                    v += dv;
                    sum += ds;
                }
            }

            var result = kernelSum == sum ? v : (v * kernelSum / sum);
            return result;
        }

        private static void LoadLine(T image, int kernelHeight, int kernelXRadius, float[] lines, int lineSize, int y)
        {
            var i = ((y + kernelHeight) % kernelHeight) * lineSize;

            if (0 <= y && y < image.Height)
            {
                for (var x = 0; x < lineSize; x++)
                {
                    var sx = x - kernelXRadius;
                    lines[i++] = 0 <= sx && sx < image.Width ? image[sx, y] : 0;
                }
            }
        }

        FloatImage IByteImageOperations.Convolve(IByteImageWrapper image, FloatImage kernel)
            => Convolve((T)image, kernel);
    }
}
