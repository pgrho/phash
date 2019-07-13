using System;
using System.Numerics;

namespace Shipwreck.Phash.Imaging
{
    internal static class VectorHelper
    {
        public static float MaxComponent(this Vector2 v)
            => Math.Max(v.X, v.Y);

        public static unsafe float MaxComponent(this Vector4 v)
            => Math.Max(((Vector2*)&v)[0].MaxComponent(), ((Vector2*)&v)[1].MaxComponent());

        public static unsafe float MaxComponent(this Vector<float> v)
        {
            var v4c = Vector<float>.Count / 4;
            if (v4c > 1
                && Vector<float>.Count == v4c * 4)
            {
                var v4p = (Vector4*)&v;
                var max = v4p[0].MaxComponent();
                for (var i = 1; i < v4c; i++)
                {
                    max = Math.Max(max, v4p[i].MaxComponent());
                }
                return max;
            }
            else
            {
                var max = v[0];
                for (var i = 0; i < Vector<float>.Count; i++)
                {
                    max = Math.Max(max, v[i]);
                }
                return max;
            }
        }
    }
}
