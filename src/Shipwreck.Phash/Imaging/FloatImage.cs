using System;
using System.Numerics;
using System.Runtime;

namespace Shipwreck.Phash.Imaging
{
    public sealed partial class FloatImage : IArrayImage<float>
    {
        public FloatImage(int width, int height)
        {
            Width = width;
            Height = height;
            Array = new float[width * height];
        }

        public FloatImage(int width, int height, float value)
        {
            Width = width;
            Height = height;
            Array = new float[width * height];
            for (var i = 0; i < Array.Length; i++)
            {
                Array[i] = value;
            }
        }

        public FloatImage(int width, int height, float[] data)
        {
            Width = width;
            Height = height;
            Array = data;
        }

        public int Width
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get;
        }

        public int Height
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get;
        }

        public float[] Array
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get;
        }

        public float this[int x, int y]
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get => Array[x + y * Width];
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            set => Array[x + y * Width] = value;
        }

        public FloatImage Resize(int w, int h)
        {
            // TODO:bilinearにする

            var r = new FloatImage(w, h);
            var xr = w / (float)Width;
            var yr = h / (float)Height;
            for (var sy = 0; sy < Height; sy++)
            {
                var dy = (int)Math.Max(0, Math.Min(sy * yr, h - 1));
                for (var sx = 0; sx < Width; sx++)
                {
                    var dx = (int)Math.Max(0, Math.Min(sx * xr, w - 1));

                    r[dx, dy] += this[sx, sy];
                }
            }

            return r;
        }

        public void ApplyGamma(float gamma)
        {
            for (var i = 0; i < Array.Length; i++)
            {
                Array[i] = MathF.Pow(Array[i], gamma);
            }
        }

        public static FloatImage operator *(FloatImage image, float coefficient)
        {
            var d = new float[image.Array.Length];
            Multiply(image.Array, d, coefficient);
            return new FloatImage(image.Width, image.Height, d);
        }

        public static FloatImage operator *(float coefficient, FloatImage image)
            => image * coefficient;

        public static FloatImage operator /(FloatImage image, float divider)
            => image * (1 / divider);

        public void MultiplyInplace(float coefficient)
        {
            Multiply(Array, Array, coefficient);
        }

        private static void Multiply(float[] source, float[] dest, float coefficient)
        {
            var vc = Vector<float>.Count;
            if (vc > 1 && Vector.IsHardwareAccelerated)
            {
                for (var i = 0; i < source.Length;)
                {
                    var ni = i + vc;
                    if (ni <= source.Length)
                    {
                        (new Vector<float>(source, i) * coefficient).CopyTo(dest, i);
                        i = ni;
                    }
                    else
                    {
                        dest[i] = source[i] * coefficient;
                        i++;
                    }
                }
            }
            else
            {
                for (var i = 0; i < source.Length; i++)
                {
                    dest[i] = source[i] * coefficient;
                }
            }
        }

        public void DivideInplace(float divider)
            => MultiplyInplace(1 / divider);

        public FloatImage Multiply(FloatImage other)
        {
            if (other.Width < Width || other.Height < Height)
            {
                throw new InvalidOperationException();
            }

            var vc = Vector<float>.Count;
            if (vc > 1 && Vector.IsHardwareAccelerated)
            {
                var oa = other.Array;
                var d = new float[Width * Height];
                if (oa.Length == d.Length)
                {
                    for (var i = 0; i < d.Length;)
                    {
                        var ni = i + vc;
                        if (ni <= Array.Length)
                        {
                            (new Vector<float>(Array, i) * new Vector<float>(oa, i)).CopyTo(d, i);
                            i = ni;
                        }
                        else
                        {
                            d[i] = Array[i] * oa[i];
                            i++;
                        }
                    }
                }
                else
                {
                    var i = 0;
                    for (var y = 0; y < Height; y++)
                    {
                        var j = y * other.Width;
                        for (var x = 0; x < Width;)
                        {
                            var ni = x + vc;
                            if (ni <= Width)
                            {
                                (new Vector<float>(Array, i) * new Vector<float>(oa, j)).CopyTo(d, i);
                                i += vc;
                                j += vc;
                                x = ni;
                            }
                            else
                            {
                                d[i] = Array[i] * oa[j];
                                i++;
                                j++;
                                x++;
                            }
                        }
                    }
                }
                return new FloatImage(Width, Height, d);
            }

            var r = new FloatImage(Width, Height);

            for (var sy = 0; sy < Height; sy++)
            {
                for (var sx = 0; sx < Width; sx++)
                {
                    r[sx, sy] = this[sx, sy] * other[sx, sy];
                }
            }
            return r;
        }

        public unsafe FloatImage MatrixMultiply(FloatImage other, bool isTransposed = false)
        {
            if (Width != (isTransposed ? other.Width : other.Height))
            {
                throw new InvalidOperationException();
            }

            var r = new FloatImage(isTransposed ? other.Height : other.Width, Height);
            var vc = Vector<float>.Count;
            if (Width >= vc
                && vc > 1
                && Vector.IsHardwareAccelerated)
            {
                var transp = isTransposed ? other : other.Transpose();

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < transp.Height; x++)
                    {
                        var v = 0f;
                        for (var i = 0; i < Width;)
                        {
                            var ni = i + vc;
                            if (ni <= Width)
                            {
                                v += Vector.Dot(
                                        new Vector<float>(Array, y * Width + i),
                                        new Vector<float>(transp.Array, x * Width + i));

                                i = ni;
                            }
                            else
                            {
                                v += this[i, y] * transp[i, x];
                                i++;
                            }
                        }

                        r[x, y] = v;
                    }
                }
            }
            else if (isTransposed)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < other.Height; x++)
                    {
                        var v = 0f;
                        for (var i = 0; i < Width; i++)
                        {
                            v += this[i, y] * other[i, x];
                        }
                        r[x, y] = v;
                    }
                }
            }
            else
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < other.Width; x++)
                    {
                        var v = 0f;
                        for (var i = 0; i < Width; i++)
                        {
                            v += this[i, y] * other[x, i];
                        }
                        r[x, y] = v;
                    }
                }
            }

            return r;
        }

        public static FloatImage CreateGaussian(int radius, float sigma)
        {
            var r = radius > 0 ? radius : (int)MathF.Round(3 * sigma);
            var w = 2 * r + 1;

            var vs = new FloatImage(w, w);
            var s2 = sigma * sigma;
            var i2s2 = 0.5f / s2;
            var i2pis2 = 1 / (2 * MathF.PI * s2);
            for (var y = 0; y <= r; y++)
            {
                for (var x = y; x <= r; x++)
                {
                    var d2 = x * x + y * y;
                    var v = MathF.Exp(-d2 * i2s2) * i2pis2;
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

        public float Max()
            => Array.Max();

        public float Min()
        {
            float r = 0;
            for (var i = 0; i < Array.Length; i++)
            {
                r = Math.Min(Array[i], r);
            }
            return r;
        }

        public float Sum()
            => Array.Sum();

        public FloatImage Transpose()
        {
            var r = new FloatImage(Height, Width);
            this.TransposeTo(r);
            return r;
        }
    }
}
