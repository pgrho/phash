using System;

namespace Shipwreck.Phash.V0_3_6.Imaging
{
    public static class ImageExtensions
    {
        public static void TransposeTo<T>(this IImage<T> source, IImage<T> dest)
            where T : struct, IEquatable<T>
        {
            var w = source.Width;
            var h = source.Height;

            if (dest.Height != w || dest.Width != h)
            {
                throw new ArgumentException();
            }

            var sa = (source as IArrayImage<T>)?.Array;
            var da = (dest as IArrayImage<T>)?.Array;

            if (sa != null && da != null)
            {
                var i = 0;
                for (var sy = 0; sy < h; sy++)
                {
                    for (var sx = 0; sx < w; sx++)
                    {
                        da[sy + h * sx] = sa[i++];
                    }
                }
            }
            else
            {
                for (var sy = 0; sy < h; sy++)
                {
                    for (var sx = 0; sx < w; sx++)
                    {
                        dest[sy, sx] = source[sx, sy];
                    }
                }
            }
        }
    }
}