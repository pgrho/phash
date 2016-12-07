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
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash
{
    public static class Fft
    {
        public static unsafe void fft_calc(int N, double* x, Complexd* X, Complexd* P, int step, Complexd* twids)
        {
            int k;
            Complexd* S = P + N / 2;
            if (N == 1)
            {
                X[0].re = x[0];
                X[0].im = 0;
                return;
            }

            fft_calc(N / 2, x, S, X, 2 * step, twids);
            fft_calc(N / 2, x + step, P, X, 2 * step, twids);

            for (k = 0; k < N / 2; k++)
            {
                P[k] = Complexd.mult_complex(P[k], twids[k * step]);
                X[k] = Complexd.add_complex(S[k], P[k]);
                X[k + N / 2] = Complexd.sub_complex(S[k], P[k]);
            }
        }

        public static unsafe int fft(double* x, int N, Complexd* X)
        {
            var twiddle_factors = new Complexd[N / 2];
            var Xt = new Complexd[N];
            int k;
            for (k = 0; k < N / 2; k++)
            {
                twiddle_factors[k] = Complexd.polar_to_complex(1.0, 2.0 * Math.PI * k / N);
            }

            fixed (Complexd* tfp = twiddle_factors)
            fixed (Complexd* xtp = Xt)
            {
                fft_calc(N, x, X, xtp, 1, tfp);
            }

            return 0;
        }
    }
}