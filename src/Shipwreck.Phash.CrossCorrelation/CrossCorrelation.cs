using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash
{
    public static class CrossCorrelation
    {
        public static double GetCrossCorrelation(byte[] coefficients1, byte[] coefficients2)
        {
            var length = coefficients1.Length;

            var r = new double[length];
            var sumx = 0.0;
            var sumy = 0.0;
            for (int i = 0; i < length; i++)
            {
                sumx += coefficients1[i];
                sumy += coefficients2[i];
            }
            var meanx = sumx / length;
            var meany = sumy / length;
            var max = 0.0;
            for (int d = 0; d < length; d++)
            {
                var num = 0.0;
                var denx = 0.0;
                var deny = 0.0;
                for (int i = 0; i < length; i++)
                {
                    num += (coefficients1[i] - meanx) * (coefficients2[(length + i - d) % length] - meany);
                    denx += Math.Pow((coefficients1[i] - meanx), 2);
                    deny += Math.Pow((coefficients2[(length + i - d) % length] - meany), 2);
                }
                r[d] = num / Math.Sqrt(denx * deny);
                if (r[d] > max)
                {
                    max = r[d];
                }
            }

            return max;
        }
    }
}
