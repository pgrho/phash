/*

pHash, the open source perceptual hash library
Copyright (C) 2009 Aetilius, Inc.
All rights reserved.

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

Evan Klinger - eklinger@phash.org
David Starkweather - dstarkweather@phash.org

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash
{
    public static class Fft
    {
        public static unsafe void fft_calc(int N, double* x, Complex* X, Complex* P, int step, Complex* twids)
        {
            Complex* S = P + N / 2;
            if (N == 1)
            {
                X[0] = new Complex(x[0], 0);
                return;
            }

            fft_calc(N / 2, x, S, X, 2 * step, twids);
            fft_calc(N / 2, x + step, P, X, 2 * step, twids);

            for (var k = 0; k < N / 2; k++)
            {
                P[k] *= twids[k * step];
                X[k] = S[k] + P[k];
                X[k + N / 2] = S[k] - P[k];
            }
        }

        public static unsafe int fft(double* x, int N, Complex* X)
        {
            var twiddle_factors = new Complex[N / 2];
            var Xt = new Complex[N];

            for (var k = 0; k < N / 2; k++)
            {
                twiddle_factors[k] = Complex.FromPolarCoordinates(1.0, 2.0 * Math.PI * k / N);
            }

            fixed (Complex* tfp = twiddle_factors)
            fixed (Complex* xtp = Xt)
            {
                fft_calc(N, x, X, xtp, 1, tfp);
            }

            return 0;
        }
    }
}