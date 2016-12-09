using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash
{
    public static partial class CrossCorrelation
    {
        public static double GetCrossCorrelation(byte[] coefficients1, byte[] coefficients2)
            => GetCrossCorrelationCore(coefficients1, coefficients2, Math.Min(coefficients1.Length, coefficients2.Length));

        public unsafe static double GetCrossCorrelation(byte* coefficients1, byte* coefficients2, int length)
            => GetCrossCorrelationCore(coefficients1, coefficients2, length);
    }
}
