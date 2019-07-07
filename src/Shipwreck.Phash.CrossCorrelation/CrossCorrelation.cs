using System;

namespace Shipwreck.Phash
{
    public static partial class CrossCorrelation
    {
        public static double GetCrossCorrelation(byte[] coefficients1, byte[] coefficients2)
            => GetCrossCorrelationCore(coefficients1, coefficients2, Math.Min(coefficients1.Length, coefficients2.Length));

        public static int GetHammingDistance(long x, long y)
            => GetHammingDistance(x ^ y);

        public static int GetHammingDistance(ulong x, ulong y)
            => GetHammingDistance(x ^ y);

        public static int GetHammingDistance(long v)
            => GetHammingDistanceCore(unchecked((ulong)v));

        public static int GetHammingDistance(ulong v)
            => GetHammingDistanceCore(v);
    }
}