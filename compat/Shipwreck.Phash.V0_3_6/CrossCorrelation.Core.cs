using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipwreck.Phash.V0_3_6
{
    partial class CrossCorrelation
    {
        internal static double GetCrossCorrelationCore(byte[] coefficients1, byte[] coefficients2, int length)
        {
            var sumx = 0.0;
            var sumy = 0.0;
            for (var i = 0; i < length; i++)
            {
                sumx += coefficients1[i];
                sumy += coefficients2[i];
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
                    var dx = coefficients1[i] - meanx;
                    var dy = coefficients2[(length + i - d) % length] - meany;
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

#if !NO_UNSAFE
        internal unsafe static double GetCrossCorrelationCore(byte* coefficients1, byte* coefficients2, int length)
        {
            var sumx = 0.0;
            var sumy = 0.0;
            for (var i = 0; i < length; i++)
            {
                sumx += coefficients1[i];
                sumy += coefficients2[i];
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
                    var dx = coefficients1[i] - meanx;
                    var dy = coefficients2[(length + i - d) % length] - meany;
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
#endif

#if !NO_SPAN
        internal static double GetCrossCorrelationCore(Span<byte> coefficients1, Span<byte> coefficients2, int length)
        {
            var sumx = 0.0;
            var sumy = 0.0;
            for (var i = 0; i < length; i++)
            {
                sumx += coefficients1[i];
                sumy += coefficients2[i];
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
                    var dx = coefficients1[i] - meanx;
                    var dy = coefficients2[(length + i - d) % length] - meany;
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
#endif
        internal static int GetHammingDistanceCore(ulong v)
        {
            unchecked
            {
                v = v - ((v >> 1) & 0x5555555555555555UL);
                v = (v & 0x3333333333333333UL) + ((v >> 2) & 0x3333333333333333UL);
                return (int)((((v + (v >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
            }
        }
    }
}
