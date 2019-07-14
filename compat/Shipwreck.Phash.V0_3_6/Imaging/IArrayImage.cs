using System;

namespace Shipwreck.Phash.V0_3_6.Imaging
{
    internal interface IArrayImage<T> : IImage<T>
        where T : struct, IEquatable<T>
    {
        T[] Array { get; }
    }
}