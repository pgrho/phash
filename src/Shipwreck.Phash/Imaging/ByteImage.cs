using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.Imaging
{
    public sealed partial class ByteImage
    {
        public FloatImage Convolve(FloatImage k)
        {
            var kw = k.Width;
            var kh = k.Height;
            var kxs = kw >> 1;
            var kys = kh >> 1;

            var r = new FloatImage(_Width, _Height);
            float total = k.Sum();

            for (var dy = 0; dy < _Height; dy++)
            {
                for (var dx = 0; dx < _Width; dx++)
                {
                    var v = 0f;
                    var sum = 0f;
                    for (var ky = 0; ky < kh; ky++)
                    {
                        var sy = dy + ky - kys;
                        if (sy < 0 || _Height <= sy)
                        {
                            continue;
                        }

                        for (var kx = 0; kx < kh; kx++)
                        {
                            var sx = dx + kx - kxs;
                            if (sx < 0 || _Width <= sx)
                            {
                                continue;
                            }
                            var sv = this[sx, sy];
                            var kv = k[kx, ky];
                            v += sv * kv;
                            sum += kv;
                        }
                    }

                    r[dx, dy] = total == sum ? v : (v * total / sum);
                }
            }

            return r;
        }

        public FloatImage Blur(double sigma)
            => Convolve(FloatImage.CreateGaussian(3, sigma));
    }
}