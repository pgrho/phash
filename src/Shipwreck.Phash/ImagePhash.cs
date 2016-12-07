using Shipwreck.Phash.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash
{
    public static class ImagePhash
    {
        private const double DEFAULT_SIGMA = 3.5;
        private const double DEFAULT_GAMMA = 1.0;
        private const int DEFAULT_NUMBER_OF_ANGLES = 180;

        public static Digest GetDigest(string img, double sigma = DEFAULT_SIGMA, double gamma = DEFAULT_GAMMA, int numberOfAngles = DEFAULT_NUMBER_OF_ANGLES)
        {
            using (var fs = new FileStream(img, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return GetDigest(fs, sigma: sigma, gamma: gamma, numberOfAngles: numberOfAngles);
            }
        }

        public static Digest GetDigest(Stream img, double sigma = DEFAULT_SIGMA, double gamma = DEFAULT_GAMMA, int numberOfAngles = DEFAULT_NUMBER_OF_ANGLES)
        {
            var bf = BitmapFrame.Create(img);

            return pHash._ph_image_digest(bf.ToByteImageOfYOrB(), sigma, gamma, numberOfAngles: numberOfAngles);
        }

        public static double GetCrossCorrelation(Digest x, Digest y)
        {
            int N = y.coeffs.Length; 

            var x_coeffs = x.coeffs;
            var y_coeffs = y.coeffs;

            var r = new double[N];
            var sumx = 0.0;
            var sumy = 0.0;
            for (int i = 0; i < N; i++)
            {
                sumx += x_coeffs[i];
                sumy += y_coeffs[i];
            }
            double meanx = sumx / N;
            double meany = sumy / N;
            double max = 0;
            for (int d = 0; d < N; d++)
            {
                double num = 0.0;
                double denx = 0.0;
                double deny = 0.0;
                for (int i = 0; i < N; i++)
                {
                    num += (x_coeffs[i] - meanx) * (y_coeffs[(N + i - d) % N] - meany);
                    denx += Math.Pow((x_coeffs[i] - meanx), 2);
                    deny += Math.Pow((y_coeffs[(N + i - d) % N] - meany), 2);
                }
                r[d] = num / Math.Sqrt(denx * deny);
                if (r[d] > max)
                    max = r[d];
            }

            return max;
        }
    }
}
