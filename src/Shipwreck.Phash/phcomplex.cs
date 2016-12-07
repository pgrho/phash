using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash
{
    // TODO Replace with System.Numerics.Complex
    public struct Complexd
    {
        public double re;
        public double im;

        public static Complexd polar_to_complex(double r, double theta)
        {
            Complexd result;
            result.re = r * Math.Cos(theta);
            result.im = r * Math.Sin(theta);
            return result;
        }
        public static Complexd add_complex(Complexd a, Complexd b)
        {
            Complexd result;
            result.re = a.re + b.re;
            result.im = a.im + b.im;
            return result;
        }
        public static Complexd sub_complex(Complexd a, Complexd b)
        {
            Complexd result;
            result.re = a.re - b.re;
            result.im = a.im - b.im;
            return result;
        }
        public static Complexd mult_complex(Complexd a, Complexd b)
        {
            Complexd result;
            result.re = (a.re * b.re) - (a.im * b.im);
            result.im = (a.re * b.im) + (a.im * b.re);
            return result;
        }
    }
}