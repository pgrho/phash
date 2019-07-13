using System;
#if !NO_VECTOR
using System.Numerics;
#endif
#if NETCOREAPP3_0
using System.Runtime.Intrinsics.X86;
#endif

namespace Shipwreck.Phash
{
    partial class CrossCorrelation
    {
        #region Internal Overloads

        internal static double GetCrossCorrelationCore(byte[] x, byte[] y, int length)
        {
            var sumX = 0;
            var sumY = 0;
            for (var i = 0; i < length; i++)
            {
                sumX += x[i];
                sumY += y[i];
            }

            var meanX = sumX / (float)length;
            var meanY = sumY / (float)length;

            var fx = new float[length];
            var fy = new float[length];

            for (var i = 0; i < length; i++)
            {
                fx[i] = x[i] - meanX;
                fy[i] = y[i] - meanY;
            }

            return GetCrossCorrelationCore(fx, fy);
        }

#if !NO_UNSAFE
        internal unsafe static double GetCrossCorrelationCore(byte* x, byte* y, int length)
        {
            var sumX = 0;
            var sumY = 0;
            for (var i = 0; i < length; i++)
            {
                sumX += x[i];
                sumY += y[i];
            }

            var meanX = sumX / (float)length;
            var meanY = sumY / (float)length;

            var fx = new float[length];
            var fy = new float[length];

            for (var i = 0; i < length; i++)
            {
                fx[i] = x[i] - meanX;
                fy[i] = y[i] - meanY;
            }

            return GetCrossCorrelationCore(fx, fy);
        }
#endif

#if !NO_SPAN
        internal static double GetCrossCorrelationCore(Span<byte> x, Span<byte> y, int length)
        {
            var sumX = 0;
            var sumY = 0;
            for (var i = 0; i < length; i++)
            {
                sumX += x[i];
                sumY += y[i];
            }

            var meanX = sumX / (float)length;
            var meanY = sumY / (float)length;

            var fx = new float[length];
            var fy = new float[length];

            for (var i = 0; i < length; i++)
            {
                fx[i] = x[i] - meanX;
                fy[i] = y[i] - meanY;
            }

            return GetCrossCorrelationCore(fx, fy);
        }
#endif

        #endregion

        private static double GetCrossCorrelationCore(float[] x, float[] y)
        {
            var max = 0f;
            for (var d = 0; d < x.Length; d++)
            {
                float v;
                v = GetCrossCorrelationForOffset(x, y, d);
                max = Math.Max(max, v);
            }

            return Math.Sqrt(max);
        }

        private static float GetCrossCorrelationForOffset(float[] x, float[] y, int offset)
        {
            var num = 0f;
            var denx = 0f;
            var deny = 0f;

            for (var j = 0; j < 2; j++)
            {
                var th = j == 0 ? x.Length - offset : x.Length;
                var i = j == 0 ? 0 : x.Length - offset;
                var yo = offset - j * x.Length;

                for (; i < th;)
                {
#if !NO_VECTOR
                    if (Vector<float>.Count > 1 && Vector.IsHardwareAccelerated)
                    {
                        var ni = i + Vector<float>.Count;
                        if (ni <= th)
                        {
                            var vx = new Vector<float>(x, i);
                            var vy = new Vector<float>(y, i + yo);
                            num += Vector.Dot(vx, vy);
                            denx += Vector.Dot(vx, vx);
                            deny += Vector.Dot(vy, vy);
                            i = ni;
                            continue;
                        }
                    }
#endif
                    var dx = x[i];
                    var dy = y[i + yo];
                    num += dx * dy;
                    denx += dx * dx;
                    deny += dy * dy;
                    i++;
                }
            }

            return num < 0 || denx == 0 || deny == 0 ? 0 : (num * num / (denx * deny));
        }

        internal static int GetHammingDistanceCore(ulong v)
        {
#if NETCOREAPP3_0
            unchecked
            {
                if (Popcnt.X64.IsSupported)
                {
                    return (int)Popcnt.X64.PopCount(v);
                }
                if (Popcnt.IsSupported)
                {
                    return (int)(Popcnt.PopCount((uint)v) + Popcnt.PopCount((uint)(v >> 32)));
                }
            }
#endif
            unchecked
            {
                v = v - ((v >> 1) & 0x5555555555555555UL);
                v = (v & 0x3333333333333333UL) + ((v >> 2) & 0x3333333333333333UL);
                return (int)((((v + (v >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
            }
        }
    }
}
