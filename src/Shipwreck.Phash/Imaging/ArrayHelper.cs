using System;
using System.Linq;
using System.Numerics;

namespace Shipwreck.Phash.Imaging
{
    internal static class ArrayHelper
    {
        public static unsafe float Max(this float[] array)
        {
            var vc = Vector<float>.Count;

            if (vc > 1
                && Vector.IsHardwareAccelerated
                && array.Length >= 2 * vc)
            {
                var mv = new Vector<float>(array, 0);
                var i = vc;
                for (; i < array.Length;)
                {
                    var ni = i + vc;
                    if (ni <= array.Length)
                    {
                        mv = Vector.Max(mv, new Vector<float>(array, i));
                        i = ni;
                    }
                    else
                    {
                        break;
                    }
                }

                var max = mv.MaxComponent();

                for (; i < array.Length; i++)
                {
                    max = Math.Max(max, array[i]);
                }

                return max;
            }
            else
            {
                return Enumerable.Max(array);
            }
        }

        public static float Sum(this float[] array)
        {
            var r = 0f;
            var i = 0;
            var vc = Vector<float>.Count;

            if (vc > 1
                && Vector.IsHardwareAccelerated
                && array.Length >= vc)
            {
                var sum = new Vector<float>(array, 0);
                i = vc;
                for (; i < array.Length;)
                {
                    var ni = i + vc;
                    if (ni <= array.Length)
                    {
                        sum = Vector.Add(sum, new Vector<float>(array, i));
                        i = ni;
                    }
                    else
                    {
                        break;
                    }
                }
                r = Vector.Dot(sum, new Vector<float>(1));
            }

            for (; i < array.Length; i++)
            {
                r += array[i];
            }

            return r;
        }
    }
}
