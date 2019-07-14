namespace Shipwreck.Phash.V0_3_6.Imaging
{
    internal class ByteImageOperations<T> : IByteImageOperations
        where T : IByteImageWrapper
    {
        public FloatImage Convolve(T image, FloatImage kernel)
        {
            var kw = kernel.Width;
            var kh = kernel.Height;
            var kxs = kw >> 1;
            var kys = kh >> 1;

            var r = new FloatImage(image.Width, image.Height);
            float total = kernel.Sum();

            for (var dy = 0; dy < image.Height; dy++)
            {
                for (var dx = 0; dx < image.Width; dx++)
                {
                    var v = 0f;
                    var sum = 0f;
                    for (var ky = 0; ky < kh; ky++)
                    {
                        var sy = dy + ky - kys;
                        if (sy < 0 || image.Height <= sy)
                        {
                            continue;
                        }

                        for (var kx = 0; kx < kh; kx++)
                        {
                            var sx = dx + kx - kxs;
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

        FloatImage IByteImageOperations.Convolve(IByteImageWrapper image, FloatImage kernel)
            => Convolve((T)image, kernel);
    }
}