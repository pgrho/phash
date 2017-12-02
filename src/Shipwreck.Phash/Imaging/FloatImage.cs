using System;

namespace Shipwreck.Phash.Imaging
{
    public sealed partial class FloatImage
    {
        public FloatImage Resize(int w, int h)
        {
            // TODO:bilinearにする

            var r = new FloatImage(w, h);
            var xr = w / (float)_Width;
            var yr = h / (float)_Height;
            for (var sy = 0; sy < _Height; sy++)
            {
                var dy = (int)Math.Max(0, Math.Min(sy * yr, h - 1));
                for (var sx = 0; sx < _Width; sx++)
                {
                    var dx = (int)Math.Max(0, Math.Min(sx * xr, w - 1));

                    r[dx, dy] += this[sx, sy];
                }
            }

            return r;
        }

        public void ApplyGamma(double gamma)
        {
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] = (float)Math.Pow(_Data[i], gamma);
            }
        }

        public static FloatImage operator *(FloatImage image, float coefficient)
        {
            var d = new float[image._Data.Length];
            for (var i = 0; i < d.Length; i++)
            {
                d[i] = image._Data[i] * coefficient;
            }
            return new Imaging.FloatImage(image._Width, image._Height, d);
        }

        public static FloatImage operator *(float coefficient, FloatImage image)
            => image * coefficient;

        public static FloatImage operator /(FloatImage image, float divider)
            => image * (1 / divider);

        public void MultiplyInplace(float coefficient)
        {
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] *= coefficient;
            }
        }

        public void DivideInplace(float divider)
            => MultiplyInplace(1 / divider);

        public FloatImage Multiply(FloatImage other)
        {
            var r = new FloatImage(_Width, _Height);
            for (var sy = 0; sy < _Height; sy++)
            {
                for (var sx = 0; sx < _Width; sx++)
                {
                    r[sx, sy] = this[sx, sy] * other[sx, sy];
                }
            }
            return r;
        }

        public static FloatImage CreateGaussian(int radius, double sigma)
        {
            var r = radius > 0 ? radius : (int)Math.Round(3 * sigma);
            var w = 2 * r + 1;

            var vs = new FloatImage(w, w);
            var s2 = sigma * sigma;
            var i2s2 = 0.5 / s2;
            var i2pis2 = 1 / (2 * Math.PI * s2);
            for (var y = 0; y <= r; y++)
            {
                for (var x = y; x <= r; x++)
                {
                    var d2 = x * x + y * y;
                    var v = (float)(Math.Exp(-d2 * i2s2) * i2pis2);
                    vs[r - y, r - x] = v;
                    vs[r - y, r + x] = v;
                    vs[r + y, r - x] = v;
                    vs[r + y, r + x] = v;
                    if (x != y)
                    {
                        vs[r - x, r - y] = v;
                        vs[r - x, r + y] = v;
                        vs[r + x, r - y] = v;
                        vs[r + x, r + y] = v;
                    }
                }
            }

            return vs;
        }
    }
}