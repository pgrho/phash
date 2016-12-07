using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash.Imaging
{
    internal interface IArrayImage<T> : IImage<T>
        where T : struct, IEquatable<T>
    {
        T[] Array { get; }
    }
}