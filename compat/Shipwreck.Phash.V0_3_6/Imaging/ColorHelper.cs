using System;
using System.Numerics;

namespace Shipwreck.Phash.V0_3_6.Imaging
{
    public static class ColorHelper
    {
        private static byte ToByte(this int i)
            => (byte)Math.Max(byte.MinValue, Math.Min(i, byte.MaxValue));

        private static byte ToByte(this float f)
            => (byte)Math.Max(byte.MinValue, Math.Min(Math.Round(f), byte.MaxValue));

        public static byte GetIntensity(this Vector3 rgb)
            => Vector3.Dot(rgb, new Vector3(1 / 3f)).ToByte();

        public static byte GetLuminance(this Vector3 rgb)
            => (((int)(Math.Round(Vector3.Dot(rgb, new Vector3(66, 129, 25))) + 128) >> 8) + 16).ToByte();
    }
}